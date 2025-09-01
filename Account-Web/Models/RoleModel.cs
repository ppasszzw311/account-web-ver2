using System.ComponentModel.DataAnnotations.Schema;

namespace Account_Web.Models;

/// <summary>
/// 權限角色
/// </summary>
[Table("Roles")]
public class Role : BaseModel
{
    public required string RoleId { get; set; }
    public required string RoleName { get; set; }
    public string? Description { get; set; }
}


// 初始階段先寫死四個角色，可以應用在不同人對專案的權限
// 每個人可能同時擁有不同權限角色
// 1. Admin: 系統管理員，擁有最高權限，可以管理所有專案和使用者
// 2. FactoryAdmin 廠區管理員: 負責管理特定廠區的專案和使用者
// 3. ProjectManager: 專案經理，負責專案的整體規劃和管理，可以分配任務和資源
// 4. Developer: 開發人員，負責專案的具體開發工作，可以查看和更新自己的任務
public static class RoleSeedData
{
    public static List<Role> Roles = new List<Role>
    {
        new Role
        {
            Id = 1,
            RoleId = "Admin",
            RoleName = "系統管理員",
            Description = "擁有最高權限，可以管理所有專案和使用者",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new Role
        {
            Id = 2,
            RoleId = "FactoryAdmin",
            RoleName = "廠區管理員",
            Description = "負責管理特定廠區的專案和使用者",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new Role
        {
            Id = 3,
            RoleId = "ProjectManager",
            RoleName = "專案經理",
            Description = "負責專案的整體規劃和管理，可以分配任務和資源",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new Role
        {
            Id = 4,
            RoleId = "Developer",
            RoleName = "開發人員",
            Description = "負責專案的具體開發工作，可以查看和更新自己的任務",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };
}