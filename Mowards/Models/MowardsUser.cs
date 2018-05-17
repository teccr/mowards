using Mowards.ViewModels;
using System;
using Xamarin.Forms;

namespace Mowards.Models
{
    public class MowardsUser : MutableDataObject
    {
        public MowardsUser() {
            UserProfilePictureUrl = "User_104px.png";
        }
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
        public string UserProfilePictureUrl { get; set; }
        
    }
}
