using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

[Table("AccountActionRecord")]
public class AccountActionRecordModel : BaseModel
{
    /// <summary>
    /// 操作者
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// 操作帳號
    /// </summary>
    public int AccountId { get; set; }
    /// <summary>
    /// 進行的動作類型
    /// </summary>
    public string ActionType { get; set; } = string.Empty; // 'create', 'update', 'delete', 'view', 'login', 'logout'
    /// <summary>
    /// 進行時間
    /// </summary>
    public DateTime ActionTime { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string PreviousData { get; set; } = string.Empty; // JSON format
    public string NewData { get; set; } = string.Empty; // JSON format
    public string IPAddress { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
}

[Flags]
public enum AccountActionType
{
    Create,
    Update,
    Delete,
    View,
    Login,
    Logout
}