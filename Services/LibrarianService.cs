using System.Threading.Tasks;
using System;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Services.Communication;
using System.Collections.Generic;
using LibraryWebApp.Resources;
using AutoMapper;
using System.Linq;

namespace LibraryWebApp.Services
{
    public class LibrarianService : ILibrarianService
    {

        public ILibrarianRepository _librarianRepository;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public LibrarianService(ILibrarianRepository librarianRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _librarianRepository = librarianRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LibrarianResponse> GetAllLibrariansAsync()
        {
            var librarians = await _librarianRepository.ListAsync();

            if (!librarians.Any())
                return new LibrarianResponse("The website has no Librarians :'(  \r\n  Don't worry soon they will start looking for knowledge ;D");

            var librariansResource = _mapper.Map<IEnumerable<Librarian>, IEnumerable<LibrarianResource>>(librarians);

            return new LibrarianResponse(librariansResource);
        }

        public async Task<LibrarianResponse> GetLibrarianByIdAsync(long id)
        {
            var librarian = await _librarianRepository.GetLibrarianByIdAsync(id);

            if (librarian == null)
                return new LibrarianResponse("Librarian not found.");

            var librariansResource = _mapper.Map<Librarian, LibrarianResource>(librarian);

            return new LibrarianResponse(librariansResource);
        }

        public async Task<LibrarianResponse> SaveLibrarianAsync(CreateUserResource librarianResource)
        {
            try
            {
                var librarian = _mapper.Map<CreateUserResource, Librarian>(librarianResource);
                _librarianRepository.AddLibrarian(librarian);
                await _unitOfWork.CompleteAsync();

                var newLibrarianResource = _mapper.Map<Librarian, LibrarianResource>(librarian);

                return new LibrarianResponse(newLibrarianResource);
            }
            catch (Exception ex)
            {
                return new LibrarianResponse($"An error occurred when saving the librarian: {ex.Message}");
            }
        }

        public async Task<LibrarianResponse> DeleteLibrarianAsync(long id)
        {
            var existingLibrarian = await _librarianRepository.GetLibrarianByIdAsync(id);

            if (existingLibrarian == null)
                return new LibrarianResponse("Librarian not found.");

            try
            {
                _librarianRepository.DeleteLibrarian(existingLibrarian);
                await _unitOfWork.CompleteAsync();

                var deletedLibrarian = _mapper.Map<Librarian, LibrarianResource>(existingLibrarian);

                return new LibrarianResponse(deletedLibrarian);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new LibrarianResponse($"An error occurred when deleting the librarian: {ex.Message}");
            }
        }
    }
}