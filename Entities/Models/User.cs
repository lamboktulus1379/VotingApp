using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Email not correctly formatted")]
        [StringLength(1024, ErrorMessage = "Email can't be longer than 1024 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password Should be at least 8 characters")]
        [StringLength(1024, ErrorMessage = "Password can't be no longer than 1024 characters")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*", ErrorMessage = "Password must be a combination of upper case, lower case, number, and at least 8 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public uint Age { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public ICollection<Voting> Votings { get; set; }
        public ICollection<UsersVotings> UsersVotings { get; set; }
    }
}
