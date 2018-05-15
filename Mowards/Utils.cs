using System;
namespace Mowards
{
    public class Utils
    {
        public const string BASE_URL = "https://mowardsdev.azurewebsites.net/";
        public const string AUTH_CONTROLLER = BASE_URL + "api/auth";
        public const string AWARDSCATEGORIES_CONTROLLER = BASE_URL + "tables/awardscategories";
        public const string AWARDSCATEGORIES_BY_YEAR_URL = AWARDSCATEGORIES_CONTROLLER + "?year=";
        public const string AWARDS_CONTROLLER = BASE_URL + "/tables/awards";
        public const string YEARS_URL = BASE_URL + "/api/years";
        public const string USER_URL = BASE_URL + "/api/user";
        public const string TRIVIA_URL = BASE_URL + "/api/trivia";
        public const string POLLS_URL = BASE_URL + "/api/polls";
        public const string POLLSRESULTS_URL = BASE_URL + "/api/pollresults";
        public const string POLLSANSWERS_URL = BASE_URL + "/api/pollanswers";

        public const string TOKEN_KEY = "AppToken";
        public const int TRIVIA_QUESTIONS_LIMIT = 5;
        //public const string REALM_DB_NAME = "mowardsData";
    }
}
