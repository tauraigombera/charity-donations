﻿using System.ComponentModel.DataAnnotations;

namespace CharityDonations.Api.BankAccountDtos;

public record CreateBankAccountDto
(
    [Required] [StringLength(20)] string AccountNumber,
    [Required] [StringLength(50)] string AccountHolderName,
    [Required] [StringLength(50)] string BankName,
    [Required] [StringLength(50)] string BranchName
);
