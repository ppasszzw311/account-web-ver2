using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 登入紀錄
/// </summary>
[Table("LoginRecord")]
public class LoginRecordModel: BaseModel
{
    public int UserId { get; set; }
    public DateTime LoginTime { get; set; }
    public string IPAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}
