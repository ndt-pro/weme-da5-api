using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Threads = new HashSet<Threads>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Threads> Threads { get; set; }
    }
}
