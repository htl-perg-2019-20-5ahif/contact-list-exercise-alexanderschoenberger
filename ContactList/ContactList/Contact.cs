
using System.ComponentModel.DataAnnotations;

namespace AdressBook
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
