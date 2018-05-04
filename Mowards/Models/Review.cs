using Mowards.ViewModels;
using System;
namespace Mowards.Models
{
    public class Review : MutableDataObject
    {
        public string Id
        {
            get;
            set;
        }
        public string MovieId { get; set; }
        public string Username { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
