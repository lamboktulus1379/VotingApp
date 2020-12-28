using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasData(

                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Lambok Tulus",
                    LastName = "Simamora",
                    Age = 25,
                    Gender = "L",                    
                    Email = "lamboktulus1379@gmail.com",
                    Password = "Gra0307"
                }
            );
        }
    }
}
