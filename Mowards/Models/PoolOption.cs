using System;
namespace Mowards.Models
{
    public class PoolOption
    {
        public string Id
        {
            get;
            set;
        }
        public string Option { get; set; }
        public PoolDefinition Definition { get; set; }
    }
}
