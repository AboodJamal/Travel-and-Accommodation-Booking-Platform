using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TABP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PostOffice = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomAmenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PricePerNight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FloorsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotels_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountPercentage = table.Column<float>(type: "real", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomAmenityRoomType",
                columns: table => new
                {
                    AmenitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenityRoomType", x => new { x.AmenitiesId, x.RoomTypesId });
                    table.ForeignKey(
                        name: "FK_RoomAmenityRoomType_RoomAmenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "RoomAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomAmenityRoomType_RoomTypes_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdultsCapacity = table.Column<int>(type: "int", nullable: false),
                    ChildrenCapacity = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryCode", "CountryName", "Name", "PostOffice" },
                values: new object[,]
                {
                    { new Guid("6d7b2be5-455f-4bbf-91ae-87b95b5fbb89"), "FR", "France", "Paris", "PAR" },
                    { new Guid("a23d7e4f-0c9d-4d91-bb2e-df8f2fa12a6e"), "DE", "Germany", "Berlin", "BER" },
                    { new Guid("ec9d0150-b648-4d0d-9149-8be65f7d0b10"), "AU", "Australia", "Sydney", "SYD" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("3a89e98d-cb27-4632-a7f9-36b163479e70"), "alicesmith@example.com", "Alice", "Smith", "9876543210" },
                    { new Guid("9bfc8bc4-8278-453b-9ff4-d075f960ea44"), "johndoe@example.com", "John", "Doe", "1234567890" },
                    { new Guid("b5e9f7b9-1a25-4c5c-b378-078c76df1f7a"), "bobjohnson@example.com", "Bob", "Johnson", "1122334455" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Category", "HotelId", "PricePerNight" },
                values: new object[,]
                {
                    { new Guid("48a98ac1-9079-413a-8cc2-299a6c8a4515"), "Double", new Guid("a42d4d56-865b-4526-9a45-c8d5d8da3e6f"), 180f },
                    { new Guid("cc8eb9be-0398-4a0d-bb6f-7ea5d58a9cf0"), "Single", new Guid("572f85b1-6223-442a-bf0a-7dbb9307f1d7"), 300f },
                    { new Guid("f67a1832-c747-4bfe-946f-9b941d1059b3"), "Single", new Guid("b2e04d28-78c5-404d-9264-215f88e6b3a1"), 120f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "Role", "Salt" },
                values: new object[,]
                {
                    { new Guid("473c85c7-8e77-4bc3-b5fa-1d27c5f9d2f1"), "johndoe@example.com", "John", "Doe", "hashedPassword1", "1234567890", "Admin", "salt1" },
                    { new Guid("9dcbf1b8-1a88-47b1-bc74-76c2fd10f23a"), "alicesmith@example.com", "Alice", "Smith", "hashedPassword2", "9876543210", "Guest", "salt2" },
                    { new Guid("a9d0a22f-5411-4c76-b1cc-056b9b400a61"), "bobjohnson@example.com", "Bob", "Johnson", "hashedPassword3", "1122334455", "Guest", "salt3" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "CityId", "Description", "FloorsNumber", "Name", "OwnerId", "PhoneNumber", "Rating", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("39a6cb7d-31c4-4752-bb6e-8c76a04e3e9f"), new Guid("ec9d0150-b648-4d0d-9149-8be65f7d0b10"), "A hotel offering stunning views of the ocean waves.", 8, "Seaside View Hotel", new Guid("b5e9f7b9-1a25-4c5c-b378-078c76df1f7a"), "7145678902", 4.3f, "45 Oceanfront Way" },
                    { new Guid("a3d5c7b1-36fa-4b52-bc42-d8d7589478fd"), new Guid("a23d7e4f-0c9d-4d91-bb2e-df8f2fa12a6e"), "An upscale hotel with panoramic city views.", 15, "Grand Plaza Hotel", new Guid("9bfc8bc4-8278-453b-9ff4-d075f960ea44"), "9876543210", 4.7f, "1500 Skyline Blvd" },
                    { new Guid("bb507c23-35fd-4b6c-83c0-b3fc54d40d4b"), new Guid("6d7b2be5-455f-4bbf-91ae-87b95b5fbb89"), "A peaceful retreat in the heart of the mountains.", 6, "Mountain Retreat", new Guid("3a89e98d-cb27-4632-a7f9-36b163479e70"), "6145678901", 4.1f, "102 Pine Ridge Lane" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AdultsCapacity", "ChildrenCapacity", "Rating", "RoomTypeId", "View" },
                values: new object[,]
                {
                    { new Guid("8ac05d5d-f8d9-49de-bf2d-2746763b1459"), 4, 2, 4.6f, new Guid("cc8eb9be-0398-4a0d-bb6f-7ea5d58a9cf0"), "Lake View" },
                    { new Guid("aa08b4e7-cbbc-4661-9bc3-2b2333bfe4de"), 3, 2, 4.3f, new Guid("48a98ac1-9079-413a-8cc2-299a6c8a4515"), "Sea View" },
                    { new Guid("b362b1ae-4f39-453f-b0f3-5a8f9d1b2815"), 2, 1, 4.7f, new Guid("f67a1832-c747-4bfe-946f-9b941d1059b3"), "Garden View" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CheckInDate", "CheckOutDate", "Price", "RoomId", "UserId" },
                values: new object[,]
                {
                    { new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"), new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 175.0, new Guid("aa08b4e7-cbbc-4661-9bc3-2b2333bfe4de"), new Guid("9dcbf1b8-1a88-47b1-bc74-76c2fd10f23a") },
                    { new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 250.0, new Guid("8ac05d5d-f8d9-49de-bf2d-2746763b1459"), new Guid("a9d0a22f-5411-4c76-b1cc-056b9b400a61") },
                    { new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 120.0, new Guid("b362b1ae-4f39-453f-b0f3-5a8f9d1b2815"), new Guid("473c85c7-8e77-4bc3-b5fa-1d27c5f9d2f1") }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "BookingId", "Method", "Status" },
                values: new object[,]
                {
                    { new Guid("66e6cfe3-91d3-4266-bf42-e4bcb024c8a7"), 2200.0, new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"), "Cash", "Pending" },
                    { new Guid("d9c0b7c4-4c8f-4a93-8785-d72011fdc17b"), 800.0, new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"), "Cash", "Cancelled" },
                    { new Guid("ea5d0358-0ed8-4c16-8693-77d1c5f6f1e1"), 1750.0, new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"), "CreditCard", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookingId", "Comment", "Rating", "ReviewDate" },
                values: new object[,]
                {
                    { new Guid("d6c8fe16-8f4b-47a7-99a0-c55693f253b9"), new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"), "Had a fantastic experience, would definitely stay again!", 4.9f, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e85f951d-b0f1-4655-90f1-8bcdb563b3c2"), new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"), "Good location, but the room could be cleaner.", 3.8f, new DateTime(2023, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f1f4d8f0-e8bc-4ed3-bb99-435b8ff7d8ab"), new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"), "Great value for the price, will recommend to friends.", 4.3f, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_RoomTypeId",
                table: "Discounts",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId",
                table: "Hotels",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OwnerId",
                table: "Hotels",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Method",
                table: "Payments",
                column: "Method");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Status",
                table: "Payments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenityRoomType_RoomTypesId",
                table: "RoomAmenityRoomType",
                column: "RoomTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_Category",
                table: "RoomTypes",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoomAmenityRoomType");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "RoomAmenities");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
