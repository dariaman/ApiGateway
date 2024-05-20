using GlobalUtility.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserModule.Model
{
    [Table("UserLogin")]
    public class UserLoginModel : ModelBase
    {
        [Required]
        public Int64 UserId { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public string Salt { get; set; }
    }
}
