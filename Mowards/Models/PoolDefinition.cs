using System;
using System.Collections.Generic;

namespace Mowards.Models
{
    public class PoolDefinition
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }
        public string Category { get; set; }
        public bool Closed { get; set; }
        public bool Visible { get; set; }

        public List<PoolOption> Options { get; set; }
    }
}
