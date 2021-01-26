using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BidOneCodingTest.Models
{
    public class UserConfiguration
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
