﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_email_unique",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_email_unique",
                table: "users");
        }
    }
}
