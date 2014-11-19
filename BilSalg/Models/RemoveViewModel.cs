using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilSalg.Models
{
    public class RemoveViewModel
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Tick for yes")]
        public bool yesNo { get; set; }
    }
}