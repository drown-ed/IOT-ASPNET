using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace TodoApiServer.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [Column("Title", TypeName = "Varchar(100)")]
        public string? Title { get; set; }
        public DateTime? TodoDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
