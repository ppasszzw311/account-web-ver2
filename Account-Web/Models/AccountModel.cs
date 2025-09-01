using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

[Table("Account")]
public class AccountModel: BaseModel
{
    public required string AccountId { get; set; }
    public required string AccountPassword { get; set; }

    public int OsType { get; set; }

    public int ProjectId { get; set; }
    public string? ServerIp { get; set; }
    public int? Port { get; set; }
    public string? Description { get; set; }
}
