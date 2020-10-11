using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class Threads
    {
        public Threads()
        {
            ThreadComments = new HashSet<ThreadComments>();
            ThreadLikes = new HashSet<ThreadLikes>();
        }

        public int Id { get; set; }
        public int? IdCategory { get; set; }
        public string Content { get; set; }
        public string Tags { get; set; }
        public bool? Pin { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Categories IdCategoryNavigation { get; set; }
        public virtual ICollection<ThreadComments> ThreadComments { get; set; }
        public virtual ICollection<ThreadLikes> ThreadLikes { get; set; }
    }
}
