using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

// ¨Ï¥ÎªÌ
[Table("Users")]
public class User : BaseModel
{
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public int? FactoryId { get; set; }
    public required string Email { get; set; }
}