using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice
{
    public class Customer
    {
        [key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }

        public List<Requirement> Requirement { get; set; } = new List<Requirement>();
    }
}
