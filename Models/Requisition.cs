using LibraryAPI.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApp.Models
{
    public class Requisition
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Data de Requisição")]
        public DateTime StartDate { get; set; }
        [Required]
        [DisplayName("Data Limite de Devolução")]
        public DateTime EndDate { get; set; }

        // Devolvido / Em Posse 
        public string Status { get; set; }


        [ForeignKey("ReaderId")]
        public long ReaderId { get; set; }
        [ForeignKey("BookId")]
        public long BookId { get; set; }
        public virtual Reader Reader { get; set; }
        public virtual Book Book { get; set; }
    }
}
