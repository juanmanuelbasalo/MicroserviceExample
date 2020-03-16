using Identity.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Activity name can not be empty.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Activity name can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid E-mail.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A password is required")]
        [StringLength(maximumLength: 15, MinimumLength = 6, ErrorMessage = "Between 6 and 15 characters.")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public void SetPassword(string password)
        {
            var hashedPassword = SecurePasswordHasher.Hash(password);
            Password = hashedPassword;
        }

        public bool ValidatePassword(string password)
        {
            var isValid = SecurePasswordHasher.IsValid(password, Password);

            return isValid;
        }
    }
}
