using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// ±M®×
/// </summary>
[Table("Projects")]
public class Project : BaseModel
{
    public required string ProjectId { get; set; }
    public required string ProjectName { get; set; }
    public required int FactoryId { get; set; }
}