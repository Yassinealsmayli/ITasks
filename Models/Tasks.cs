
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITasks.Models
{
    public class ITask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; } = "";

        public DateTime DeadLine { get; set; }
    }
}
