using System.ComponentModel.DataAnnotations.Schema;

namespace ITasks.Models
{
    public class UserTask
    {
        [ForeignKey("UID")]
        public int UID { get; set; }
        [ForeignKey("TaskID")]
        public int TaskID { get; set; }

        public bool isChecked { get; set; } = false;
    }
}
