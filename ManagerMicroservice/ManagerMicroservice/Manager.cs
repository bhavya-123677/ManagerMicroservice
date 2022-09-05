using System.ComponentModel.DataAnnotations;

namespace ManagerMicroservice
{
    public class Manager
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public long ContactNumber { get; set; }
        public string Locality { get; set; }
        public string Emailid { get; set; }




    }
}
