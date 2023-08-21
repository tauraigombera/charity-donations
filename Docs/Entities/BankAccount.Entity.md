# BankAccount Entity

## Description

The BankAccount entity stores the bank account information for each registered organization. It includes fields for the organization's bank account number, account holder name, bank name, and branch name. The BankAccount entity is related to the Organization entity in a one-to-one relationship, allowing each organization to have its own bank account information.

## Methods

```csharp
public interface IContactRepository
{
    // Add bank information for an organization
    void AddBankInfo(int organizationId, string email, string phone, string address);

    // Update bank information for an organization
    void UpdateBankInfo(int organizationId, string newEmail, string newPhone, string newAddress);

    // Get bank information for an organization
    Contact GetBankInfo(int organizationId);

    // Remove bank information for an organization
    void RemoveBankInfo(int organizationId);
}
```

## Example Payload

```json
{
  "AccountNumber": "string",
  "AccountHolderName": "string",
  "BankName": "string",
  "BranchName": "string"
}
```
