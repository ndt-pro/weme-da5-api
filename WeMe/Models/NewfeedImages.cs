using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class NewfeedImages
    {
        public int Id { get; set; }
        public int? IdNewfeed { get; set; }
        public string Image { get; set; }

        public virtual Newfeeds IdNewfeedNavigation { get; set; }
    }
}
