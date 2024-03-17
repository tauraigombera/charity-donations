using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.Models;

public class BankAccount
{
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public required string AccountNumber { get; set; }
    [Required]
    [StringLength(50)]
    public required string AccountHolderName { get; set; }
    [Required]
    [StringLength(50)]
    public required string BankName { get; set; }
    [Required]
    [StringLength(50)]
    public required string BranchName { get; set; }
    public int OrganizationId { get; set; }
}
