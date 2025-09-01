using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 綁定人與權限關係
/// </summary>
[Table("UserRoleMapping")]
public class UserRoleMappingModel: BaseModel
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string ScopeType { get; set; } = string.Empty; // 'system', 'factory', 'project'

    public int? ScopeId { get; set; } // factoryId or projectId
}


public enum ScopeType
{
    System,
    Factory,
    Project
}