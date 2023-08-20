# Charity Donations API 🇲🇼

A .NET [Minimal API](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0) implementation of the Malawi Charity Donations API.

## Description

The Charity Donation API project aims to provide a platform for facilitating online donations to charitable organizations in Malawi. It enables individuals to contribute to various causes and make a positive impact on society. The API will integrate with popular payment gateways to securely handle financial transactions.

## Motivation

### Documentation

You may find out about the documentation [here](https://github.com/...Usecases.md)

## Technologies

The application is built with the following technologies:

- [.NET 7.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7)

## Local Development

### Prerequisites

- .NET 7.0 SDK

### Getting Started

1. Clone the repository to your local machine.

2. Navigate to the root directory of the project in the terminal:

   ```bash
   cd charity-donations
   cd CharityDonations.Api
   ```

3. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

4. Build the application:

   ```bash
   dotnet build
   ```

5. Run the application:

   ```bash
   dotnet run
   ```

   The application runs on [`http://localhost:5073/`](http://localhost:5073/).

### Starting sql server with docker

# install docker

If you don't have docker installed and running on your machine, go ahead and [install docker](https://docs.docker.com/desktop/).

# pull mssql server docker image

Pull Microsoft SQL Server image from docker hub and start it as a container on your machine:

```bash
$sa_password = "[YOUR SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

Run `docker ps` to see the container.

Run `docker stop mssql` whenever you want to stop the container.

Run the "docker run..." command above whenever you want to restart the container.

# safe storage of connection string to .NET secret manager

Instead of defining connection string in appsettings.json, we are using SecretManager provided by .NET to safely store our connection strings

```bash
$sa_password = "[YOUR SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings: CharityOrganizationsContext" "Server=localhost; Database=CharityDonationsApi; User Id=sa; Password=$sa_password;TrustServerCertificate=True"
```

Notice that the sa password is the one that you set before when configuring mssql server. If this command is successfuly excuted, you will see a message, "successfully saved ConnectionStrings... to the secret store".

To see the list of your secrets, run

```bash
dotnet user-secrets list
```

## How to Contribute

Contributions to the project are welcome! Here are the steps to contribute:

1. Fork the project repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes.
4. Submit a pull request.

Please make sure your code adheres to the existing style for consistency.

## License

This project is licensed under the MIT License - see the [LICENSE file](https://opensource.org/license/mit/) for details.

## Author

- [Taurai Gombera](https://github.com/tauraigombera)
  Feel free to reach out if you have any questions or suggestions!
