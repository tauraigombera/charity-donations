# Contact Entity

## Description

The Contact entity stores the contact information for each registered organization. It includes fields for the organization's email address, phone number, and physical address. The Contact entity is related to the Organization entity in a one-to-one relationship, allowing each organization to have its own contact information.

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
  "Email": "string",
  "PhoneNumber": "string",
  "Address1": "string",
  "Address2": "string",
  "Address3": "string"
}
```
