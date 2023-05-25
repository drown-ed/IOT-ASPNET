using System.ComponentModel.DataAnnotations;

namespace _02_boardapp.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string RoleName { get; set; }

    }
}
