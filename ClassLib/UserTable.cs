using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLib
{
    public class UserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;


        [StringLength(50)]
        public string Email { get; set; } = null!;


        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        public DateTime RegistrationDate { get; set; }


        [StringLength(50)]

        public string RollType { get; set; } = null!;
    }

}
