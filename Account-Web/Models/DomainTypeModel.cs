using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

[Table("DomainType")]
public class DomainType: BaseModel
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty; // Windows, Linux, Oracle DB, etc.
    // Navigation properties
    public DomainCategory Category { get; set; } = null!;
}