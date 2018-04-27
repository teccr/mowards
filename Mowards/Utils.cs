﻿using System;
namespace Mowards
{
    public class Utils
    {
        public const string BASE_URL = "https://mowardsdev.azurewebsites.net/";
        public const string AUTH_CONTROLLER = BASE_URL + "api/auth";
        public const string AWARDSCATEGORIES_CONTROLLER = BASE_URL + "tables/awardscategories";
        public const string AWARDSCATEGORIES_BY_YEAR_URL = AWARDSCATEGORIES_CONTROLLER + "?year=";
        public const string YEARS_URL = BASE_URL + "/api/years";

        public const string TOKEN_KEY = "AppToken";
    }
}