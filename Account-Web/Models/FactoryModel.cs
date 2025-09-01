using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// ¼t°Ï
/// </summary>
[Table("Factories")]
public class Factory : BaseModel
{
    public required string FactoryId { get; set; }
    public required string FactoryName { get; set; }
}