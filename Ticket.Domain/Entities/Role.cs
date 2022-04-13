using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Azmoon.Domain.Entities
{
    public class Role: IdentityRole
    {
        public Role()
        : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
           
        }
        public Role(string name , string description)
        {
            this.Name = name;
            this.Description = description;
        }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public Role Parent { get; set; }

    }

}




