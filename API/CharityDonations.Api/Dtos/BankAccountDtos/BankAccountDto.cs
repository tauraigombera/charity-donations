namespace CharityDonations.Api.BankAccountDtos;

public record BankAccountDto
(
    int Id,
    string BankAccount,
    string AccountHolderName,
    string BankName,
    string BranchName
);
