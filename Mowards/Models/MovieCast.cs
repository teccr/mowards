using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class MovieCast : MutableDataObject
    {
        
        public int Cast_id { get; set; }

        public string Character { get; set; }

        public string Credit_id { get; set; }

        public int Gender  { get; set; }

        public int Id { get; set; }

        public string Name  { get; set; }

        public int Order { get; set; }
        private string _Profile_path { get; set; }
        public string Profile_path { get { return _Profile_path; } set { _Profile_path= "https://image.tmdb.org/t/p/original" + value ; } }



    }
}
