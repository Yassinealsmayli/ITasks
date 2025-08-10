using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITasks.Models
{
    public class Job
    {
        [Key]
        [ForeignKey("UID")]
        public int UID { get; set; }

        public string Title {  get; set; }
    }
}
