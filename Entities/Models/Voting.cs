using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
	[Table("Voting")]
	public class Voting : IEntity
	{
		[Key]
		[Column("Id")]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		[StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
		public string Name { get; set; }

		[StringLength(1024, ErrorMessage = "Description can't be longer than 1024 characters")]
        public string Description { get; set; }

		[Required(ErrorMessage = "Date Created  is required")]
        public DateTime DateCreated { get; set; }
		[Required(ErrorMessage = "Voters Count is required")]
        public int VotersCount { get; set; }
		[Required(ErrorMessage = "Due Date is requried")]
        public DateTime DueDate { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
