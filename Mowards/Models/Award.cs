using System;
namespace Mowards.Models
{
    public class Award
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }
        public int Awards_of_Year { get; set; }
        public string Ceremony_Number { get; set; }
        public string Category { get; set; }
        public string Nominee { get; set; }
        public string Additional_Info { get; set; }
        public int Won { get; set; }
    }
}
