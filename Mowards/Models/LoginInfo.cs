using System;
using System.Threading.Tasks;
using Mowards.MowardsService;
using Realms;

namespace Mowards.Models
{
    public class LoginInfo : RealmObject
    {
        public string Username
        {
            get;
            set;
        }

        [Ignored]
        public string Password
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public static async Task<UserImageInfo> SaveProfileImage(string imageAzureUrl, string userName)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Post<UserImageInfo>(Utils.UPDATE_PICTURE_URL, 
                new UserImageInfo { Username = userName, AzureUrl = imageAzureUrl });
        }
    }
}
