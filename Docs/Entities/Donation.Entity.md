# Donation Entity

## Description

The Donation entity stores the donations information for each registered organization and users. It includes fields for the amount, date, Transaction Status, organization name and donor name. The Donation entity is related to the Organization entity in a one-to-many relationship, allowing each organization to receive many donations. It also relates to users in one-to-many relationship, allowing users to make many donations.

## Methods

```csharp
public interface IDonationRepository
{
    // Get all organizations
    void GetAll();

    // Get an organization by Id
    void GetById(int Id);

    // Get donation(s) by organization Id
    void GetByOrganization(int OrganizationId);

    // Get donation(s) by user Id
    void GetByUser(int UserId);

    // Create a donation
    void Create(Donation donation);
}
```

## Example Payload

```json
{
  "Amount": "decimal",
  "Date": "DateTime",
  "TransactionStatus": "enum",
  "OrganizationName": "string",
  "DonorName": "string"
}
```
