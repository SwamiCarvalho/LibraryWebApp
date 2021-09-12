using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using LibraryWebApp.Resources;
using LibraryWebApp.Services;
//using Foolproof;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LibraryWebApp.Domain.Models
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IReaderService _readerService;
        private readonly ILibrarianService _librarianService;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IReaderService readerService,
            IMapper mapper,
            ILibrarianService librarianService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _readerService = readerService;
            _librarianService = librarianService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string[] Roles { get; set; }


        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            //[DataType(DataType.Custom)]
            [Display(Name = "Citzen Card Number")]
            [Required(ErrorMessage = "This field needs to be inserted.")]
            public string CCNumber { get; set; }
            public string Role { get; set; }
            //[RequiredIf("Role", "Librarian", ErrorMessage = "A Access Code is required in order to register as Librarian.")]
            [Display(Name = "Librarian Access Code")]
            //[Compare(Utility.SecretCodes.LibCode, ErrorMessage = "You need to enter a access code in order to register as Librarian.")]
            public string LibrarianCode { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Roles = new String[] { Utility.Roles.ReaderEndUser, Utility.Roles.LibrarianEndUser };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };

                if (Input.Role == "Librarian" & Input.LibrarianCode == Utility.SecretCodes.LibCode || Input.Role == "Reader")
                {

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        ///////////////////////////////// My Code /////////////////////////////////

                        // Associate User to a Role
                        var userRole = await _userManager.AddToRoleAsync(user, Input.Role);

                        if (userRole.Succeeded)
                        {
                            _logger.LogInformation("Role associated with User.");
                        }
                        else
                        {
                            _logger.LogInformation("It was not possible to associate Role to User.");
                        }

                        // Associating IdentityUser to a User
                        CreateUserResource userType = new CreateUserResource();
                        userType.Email = Input.Email;
                        userType.UserId = user.Id;
                        userType.CCNumber = long.Parse(Input.CCNumber);

                        if (Input.Role == "Reader")
                        {
                            var resultUser = await _readerService.SaveReaderAsync(userType);
                            if (!resultUser.Success)
                            {
                                ViewData["Feedback"] = resultUser.Message;
                                return LocalRedirect(returnUrl);
                            }
                        }
                        else
                        {
                            var resultUser = await _librarianService.SaveLibrarianAsync(userType);
                            if (!resultUser.Success)
                            {
                                ViewData["Feedback"] = resultUser.Message;
                                return LocalRedirect(returnUrl);
                            }
                        }

                        ///////////////////////////////////////////////////////////////////////////

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToPage("RegisterConfirmation");
        }
    }
}
