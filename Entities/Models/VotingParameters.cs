using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class VotingParameters : QueryStringParameters
    {
        public VotingParameters()
        {
            OrderBy = "DateCreated";
        }
        public string Name { get; set; }
    }
}
