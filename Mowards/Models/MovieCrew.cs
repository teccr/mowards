using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class MovieCrew : MutableDataObject
    {
        
        public string credit_id { get; set; }

        public string department { get; set; }
        public string  gender { get; set; }
        public int id { get; set; }
        public string job { get; set; }
        public string name { get; set; }

        public string profile_path { get; set; }
   
    }
}
