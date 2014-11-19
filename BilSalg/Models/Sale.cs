using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BilSalg.Models
{
    public class Sale
    {
        public int Id { get; set; }

        //I named this Variant instead of Model, as it make the form submission break
        [Required]
        [RegularExpression(@"^.*(CRX|CR-X)+.*$", ErrorMessage = "Model name must contain CRX or CR-X")]
        [Display(Name = "Model")]
        public string Variant { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string Engine { get; set; }

        [Required]
        public string Mileage { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //A simple getter to retrieve the full name, without having to concatenate elsewhere
        public string Name 
        { 
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Price { get; set; }

        //When the virtual keyword is used in this context, it creates a foreign key to the users in the AspNetUsers table
        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}