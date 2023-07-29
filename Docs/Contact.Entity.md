# Contact Entity

## Description

Contact information of an organisation such as Email, PhoneNumber, and Address.

## Methods

```csharp
public interface IContactRepository
{
    // Add contact information for an organization
    void AddContactInfo(int organizationId, string email, string phone, string address);

    // Update contact information for an organization
    void UpdateContactInfo(int organizationId, string newEmail, string newPhone, string newAddress);

    // Get contact information for an organization
    Contact GetContactInfo(int organizationId);

    // Remove contact information for an organization
    void RemoveContactInfo(int organizationId);
}
```

## Example Payload

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "Email": "string",
  "PhoneNumber": "string",
  "Address1": "string",
  "Address2": "string",
  "Address3": "string",
  "OrganizationId": "00000000-0000-0000-0000-000000000000"
}
```
