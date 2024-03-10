using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharityDonations.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationDateAndForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Donations",
                newName: "DonationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DonationDate",
                table: "Donations",
                newName: "Date");
        }
    }
}
