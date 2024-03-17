# Charity Donations API 🇲🇼

A .NET [Minimal API](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0) implementation of the Malawi Charity Donations API.

## Description

The Charity Donation API project aims to provide a platform for facilitating online donations to charitable organizations in Malawi. It enables individuals to contribute to various causes and make a positive impact on society. The API will integrate with popular payment gateways to securely handle financial transactions.

## Motivation

The Charity Donation API project is driven by the goal to modernize charitable giving in Malawi, a country where many important causes often face limited funding avenues. By creating an accessible online platform, the project aims to connect donors with charitable organizations seamlessly. This initiative seeks to enhance transparency, efficiency, and global reach in philanthropic efforts, making positive social impact easier to achieve.

### Documentation

You may find out about the project in the documentation [here](https://github.com/tauraigombera/charity-donations/tree/main/Docs/REQUIREMENTS.md)

## Technologies

The application is built with the following technologies:

- [.NET 7.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-7)
- [Microsoft SQL Server](https://hub.docker.com/_/microsoft-mssql-server) for database
- [Auth0](https://auth0.com/) for authentication and authorization

## Local Development

### Prerequisites

- Download and install [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Download and install [Docker](https://docs.docker.com/engine/install/)
- Download and install [Postman](https://www.postman.com/downloads/)
- Create an [Auth0 Account](https://auth0.com/) and register your API by following this [APIs registration process](https://auth0.com/docs/get-started/auth0-overview/set-up-apis).

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

5. Start mssql server with docker

   Set an SA (System Administrator) password:

   ```bash
   $sa_password = "[SET YOUR SA PASSWORD HERE]"
   ```

   Pull the docker image:

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
   ```

   Run `docker ps` to see the docker container.

   Run `docker stop mssql` whenever you want to stop the docker container.

   Run the same command you used for pulling to restart the docker container.

6. Safe storage of app secrets using .NET secret manager

   We are using Secret Manager provided by .NET to safely store secrets such as connection strings and secret keys. Read more about [.NET secrets manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows).

   To safe secrets, first enter your SA password:

   ```bash
   $sa_password = "[YOUR SA PASSWORD HERE]"
   ```

   Safe a connection string:

   ```bash
   dotnet user-secrets set "ConnectionStrings: CharityOrganizationsContext" "Server=localhost; Database=CharityDonationsApi; User Id=sa; Password=$sa_password;TrustServerCertificate=True"
   ```

   Safe Auth0 Domain

   ```bash
   dotnet user-secrets set "Auth0:Domain" "[YOUR AUTH0 DOMAIN HERE]"
   ```

   Safe Auth0 Audience

   ```bash
   dotnet user-secrets set "Auth0:Audience" "[YOUR AUTH0 AUDIENCE HERE]"
   ```

   To see the list of your secrets, run

   ```bash
   dotnet user-secrets list
   ```

7. Run the application:

   ```bash
   dotnet run
   ```

   The application runs on [`http://localhost:5073/`](http://localhost:5073/).

   You can test the application with swagger or postman. We recommend testing with postman.

## How to Contribute

Contributions to the project are welcome!
Make sure you read and understand [project requirements](https://github.com/tauraigombera/charity-donations/tree/main/Docs/REQUIREMENTS.md) before starting making contributions.

Here are the steps to contribute:

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
