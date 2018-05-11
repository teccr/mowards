using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class Favorite: MutableDataObject

    {
        public string Id { get;set;}
        public string MovieId { get; set; }
        public string Username { get; set; }
       
    }
}
