using LibraryAPI.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApp.Models
{
    public class Requisition
    {
        public long RequisitonId { get; set; }
        [DisplayName("Requisition Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("Delivery Deadline")]
        public DateTime EndDate { get; set; }

        // Devolvido / Em Posse 
        public string Status { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
    }
}
