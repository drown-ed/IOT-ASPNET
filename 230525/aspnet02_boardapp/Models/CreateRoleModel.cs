using System.ComponentModel.DataAnnotations;

namespace aspnet02_boardapp.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
