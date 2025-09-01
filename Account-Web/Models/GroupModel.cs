using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 群組，控制可以看到的專案清單
/// </summary>
[Table("Group")]
public class Group: BaseModel
{
    public required string GroupId { get; set; }
    public required string GroupName { get; set; }
    public string? Description { get; set; }
    // 逗號分隔的專案ID清單
    public string? ProjectIds { get; set; }
}
