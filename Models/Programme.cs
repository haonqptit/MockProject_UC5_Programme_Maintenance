using System.ComponentModel.DataAnnotations;

namespace MockProject.Models;

public class Programme
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }

    // Relationship with Contact
    public int ContactId { get; set; }
    public Contact Contact { get; set; }
}