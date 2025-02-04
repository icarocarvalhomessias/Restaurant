using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioAndOrderRelation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UsuarioId",
                table: "Orders",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Usuarios_UsuarioId",
                table: "Orders",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Usuarios_UsuarioId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UsuarioId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Orders");
        }
    }
}
