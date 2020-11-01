using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public string Content { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Users IdUserNavigation { get; set; }
    }
}
