using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

[Table("DomainCategory")]
public class DomainCategory: BaseModel
{
    public string Name { get; set; } = string.Empty; // OS, Application
}