using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class MovieCredits: MutableDataObject
    {
        public int id { get; set; }
        public List<MovieCast> Cast { get; set; }
        public List<MovieCrew> Crew { get; set; }
    }
}
