using System;
namespace Mowards.Models
{
    public class MowardsUser
    {
        public string Id
        {
            get;
            set;
        }
        public string Email { get; set; }
        public string Fullname { get; set; }

        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }

        public string Picture { get; set; }
    }
}
