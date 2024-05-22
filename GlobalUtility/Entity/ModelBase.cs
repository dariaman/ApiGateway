using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalUtility.Entity
{
    public class ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsDelete { get; set; } = false;
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; } = "NONE";

        public string? LastUpdateBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
