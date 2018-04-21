using System;
namespace Mowards.Models
{
    public class PoolAnswer
    {
        public string Id
        {
            get;
            set;
        }

        public PoolDefinition Definition { get; set; }

        public PoolOption SelectedOption { get; set; }

        public string Username { get; set; }
    }
}
