using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class UsersVotings
    {
        [Key]
        public Guid Id;
        public User User { get; set; }
        public Voting Voting { get; set; }
    }
}
