using System;
using System.Collections.Generic;

namespace WeMe.Models
{
    public partial class Users
    {
        public Users()
        {
            MessagesFromUser = new HashSet<Messages>();
            MessagesToUser = new HashSet<Messages>();
            NewfeedComments = new HashSet<NewfeedComments>();
            NewfeedLikes = new HashSet<NewfeedLikes>();
            Newfeeds = new HashSet<Newfeeds>();
            ThreadComments = new HashSet<ThreadComments>();
            ThreadLikes = new HashSet<ThreadLikes>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public byte Role { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Messages> MessagesFromUser { get; set; }
        public virtual ICollection<Messages> MessagesToUser { get; set; }
        public virtual ICollection<NewfeedComments> NewfeedComments { get; set; }
        public virtual ICollection<NewfeedLikes> NewfeedLikes { get; set; }
        public virtual ICollection<Newfeeds> Newfeeds { get; set; }
        public virtual ICollection<ThreadComments> ThreadComments { get; set; }
        public virtual ICollection<ThreadLikes> ThreadLikes { get; set; }
    }
}
