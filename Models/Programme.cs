using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MockProject.Models;

public class Programme
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public bool IsActive { get; set; }

    [Required]
    [ForeignKey("Contact")]
    public int ContactId { get; set; }

    // Không thêm [Required] cho trường Contact
    public Contact? Contact { get; set; }
}