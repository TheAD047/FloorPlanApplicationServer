using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace FloorPlanApplication.Models
{
    public class Company
    {
        [Key]
        public int ID { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public IEnumerable<User> Staff { get; set; } = new List<User>();
    }
}
