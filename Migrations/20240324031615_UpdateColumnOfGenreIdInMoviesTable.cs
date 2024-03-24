using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEgyMoviesAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnOfGenreIdInMoviesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GendreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "GendreId",
                table: "Movies",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GendreId",
                table: "Movies",
                newName: "IX_Movies_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Movies",
                newName: "GendreId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                newName: "IX_Movies_GendreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GendreId",
                table: "Movies",
                column: "GendreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
