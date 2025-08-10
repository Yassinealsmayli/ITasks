using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITasks.Models
{
    public class Admins
    {
        [Key]
        [ForeignKey("UID")]
        public int UID { get; set; }
    }
}
