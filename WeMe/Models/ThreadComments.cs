using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class ThreadComments
    {
        public int Id { get; set; }
        public int? IdThread { get; set; }
        public int? IdUser { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Threads IdThreadNavigation { get; set; }
        public virtual Users IdUserNavigation { get; set; }
    }
}
