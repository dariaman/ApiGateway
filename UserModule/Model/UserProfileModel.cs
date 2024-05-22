using GlobalUtility.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserModule.Model
{
    [Table("UserProfile")]
    public class UserProfileModel : ModelBase
    {
        public string Fullname { get; set; }
        public string Email { get; set; }

    }
}
