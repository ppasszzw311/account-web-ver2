using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 綁定使用者與群組的多對多關係
/// </summary>
[Table("UserGroupMapping")]
public class UserGroupMappingModel: BaseModel
{
    public int UserId { get; set; }

    public int GroupId { get; set; }
}
