using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength (2, ErrorMessage="First Name must be at least 2 characters!")]

        public string FirstName {get;set;}

        [Required]
        [MinLength (2, ErrorMessage="Last Name must be at least 2 characters!")]

        public string LastName {get;set;}

        [Required]
        public string Email {get;set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength (8, ErrorMessage="Password must be at least 8 characters!")]
        public string Password {get;set;}

        [NotMapped]
        [Compare("Password", ErrorMessage = "The passwords must be the same!")]
        [DataType(DataType.Password)]
        public string PwConfirmation {get;set;}


        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}