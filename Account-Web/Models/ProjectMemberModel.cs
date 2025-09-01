using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 專案成員，綁定使用者與專案之間的關係
/// </summary>
[Table("ProjectMember")]
public class ProjectMemberModel: BaseModel
{
    public int UserId { get; set; }
    public int ProjectId { get; set; }
}
