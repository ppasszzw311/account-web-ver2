using System.ComponentModel.DataAnnotations;

namespace Account_Web.Models.Dtos;

public class FactoryRequestDto
{
    [Required]
    public required string FactoryId { get; set; }
    [Required]
    public required string FactoryName { get; set; }
}

public class EditFactoryDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public required string FactoryId { get; set; }
    [Required]
    public required string FactoryName { get; set; }
}
public class FactoryResponseDto
{
    public int Id { get; set; }
    public required string FactoryId { get; set; }
    public required string FactoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
