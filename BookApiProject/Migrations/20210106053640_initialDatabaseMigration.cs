using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookApiProject.Migrations
{
    public partial class initialDatabaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "emsBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Isbn = table.Column<string>(maxLength: 10, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    DatePublished = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emsCountries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emsReviewers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsReviewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "emsBookCategories",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsBookCategories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_emsBookCategories_emsBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "emsBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emsBookCategories_emsCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "emsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "emsAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emsAuthors_emsCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "emsCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emsReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Headline = table.Column<string>(maxLength: 200, nullable: false),
                    ReviewText = table.Column<string>(maxLength: 2000, nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    ReviewerId = table.Column<int>(nullable: true),
                    BookId = table.Column<int>(nullable: true)
                },
                constraints: table => 
                {
                    table.PrimaryKey("PK_emsReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_emsReviews_emsBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "emsBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_emsReviews_emsReviewers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "emsReviewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "emsBookAuthors",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emsBookAuthors", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_emsBookAuthors_emsAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "emsAuthors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_emsBookAuthors_emsBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "emsBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emsAuthors_CountryId",
                table: "emsAuthors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_emsBookAuthors_AuthorId",
                table: "emsBookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_emsBookCategories_CategoryId",
                table: "emsBookCategories",
                column: "CategoryId");   

            migrationBuilder.CreateIndex(
                name: "IX_emsReviews_BookId",
                table: "emsReviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_emsReviews_ReviewerId",
                table: "emsReviews",
                column: "ReviewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emsBookAuthors");

            migrationBuilder.DropTable(
                name: "emsBookCategories");

            migrationBuilder.DropTable(
                name: "emsReviews");

            migrationBuilder.DropTable(
                name: "emsAuthors");

            migrationBuilder.DropTable(
                name: "emsCategories");

            migrationBuilder.DropTable(
                name: "emsBooks");

            migrationBuilder.DropTable(
                name: "emsReviewers");

            migrationBuilder.DropTable(
                name: "emsCountries");
        }
    }
}
