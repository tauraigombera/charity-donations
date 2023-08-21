# Organization Entity

## Description

The Organization entity represents charitable organizations that are registered on the platform. Each organization has a unique identifier, a name, a mission statement describing their goals, and a description providing additional details about the organization.

## Methods

```csharp
public interface IOrganizationRepository
{
    // Get all organizations
    void GetAll();

    // Get an organization by Id
    void GetById(int Id);

    // Create an organization
    void Create(Organization organization);

    // Update an organization
    void Update(Organization updatedOrganization);

    // Delete an organization
    void Delete(int Id);
}
```

## Example Payload

```json
{
  "id": "int",
  "Name": "string",
  "Mission": "string",
  "Description": "string",
  "ImageUrl": "string",
  "contact": {
    "id": "int",
    "email": "string",
    "phone": "string",
    "address1": "string",
    "address2": "string",
    "address3": "string"
  },
  "bankAccount": {
    "id": "int",
    "bankAccount": "string",
    "accountHolderName": "string",
    "bankName": "string",
    "branchName": "string"
  }
}
```
