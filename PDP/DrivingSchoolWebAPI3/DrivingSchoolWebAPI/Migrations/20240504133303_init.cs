using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrivingSchoolWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronym = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OuterSchedulesOfInstructors",
                columns: table => new
                {
                    OuterScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    GoogleSheetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleSheetPageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesOfClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatesOfClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreeClassExampleRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotFreeClassExampleRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearRange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterSchedulesOfInstructors", x => x.OuterScheduleId);
                    table.ForeignKey(
                        name: "FK_OuterSchedulesOfInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId");
                });

            migrationBuilder.CreateTable(
                name: "InnerSchedulesOfInstructors",
                columns: table => new
                {
                    InnerScheduleOfInstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    DayOfWork = table.Column<DateOnly>(type: "date", nullable: false),
                    OuterScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerSchedulesOfInstructors", x => x.InnerScheduleOfInstructorId);
                    table.ForeignKey(
                        name: "FK_InnerSchedulesOfInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InnerSchedulesOfInstructors_OuterSchedulesOfInstructors_OuterScheduleId",
                        column: x => x.OuterScheduleId,
                        principalTable: "OuterSchedulesOfInstructors",
                        principalColumn: "OuterScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InnerScheduleOfInstructorId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_InnerSchedulesOfInstructors_InnerScheduleOfInstructorId",
                        column: x => x.InnerScheduleOfInstructorId,
                        principalTable: "InnerSchedulesOfInstructors",
                        principalColumn: "InnerScheduleOfInstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "GradesByInstructorToStudent",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<byte>(type: "tinyint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesByInstructorToStudent", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_GradesByInstructorToStudent_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradesByStudentToInstructor",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<byte>(type: "tinyint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesByStudentToInstructor", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_GradesByStudentToInstructor_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abe9d1cc-fcd7-44c6-a097-6f6b7f959390", null, "Admin", "ADMIN" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", null, "Student", "STUDENT" },
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", null, "Instructor", "INSTRUCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronym", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureBytes", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3e6063e3-901c-4e34-a854-ffcd5a1eec14", 0, "e8d8f32b-51de-45a9-a280-ef29cef63a70", "Instructor0@example.com", false, "Nelson", "Rodriguez", false, null, "INSTRUCTOR0@EXAMPLE.COM", "INSTRUCTOR0@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGQp8qW9HIzlo4y8vQjohzdGpMytdEYAGl3qyGTddxwBsM87nipwU/DmQvM/lncC7w==", "Louis", null, false, null, "44f5a444-b3f9-4b9e-a364-8efefddd7f27", false, "Instructor0@example.com" },
                    { "4a1ea45b-1dc8-4fc5-9464-d5059a2255b0", 0, "4c74533c-cd1b-4cfe-92a3-320ff6d83024", "Student3@example.com", false, "Miles", "Rodney", false, null, "STUDENT3@EXAMPLE.COM", "STUDENT3@EXAMPLE.COM", "AQAAAAIAAYagAAAAENhzf7NmuloEZGVvqz8bIHJsnXvAJiH9PjfVqn0+CI3/VhR6HsAjySaJCDeNgvl2zQ==", "Amy", null, false, null, "16adc952-888d-460f-abb4-16ef08e3fb02", false, "Student3@example.com" },
                    { "5eb6a37c-e12d-4bb1-b404-c0e7c1b0cc23", 0, "c874f45c-eb6b-4518-8f08-6275d238d95f", "Instructor4@example.com", false, "Chambers", "Louis", false, null, "INSTRUCTOR4@EXAMPLE.COM", "INSTRUCTOR4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAofazjP3Hchk2ZrtUxhyNTWhx0XpQpjhbOrNcZZFqrqgqhhdtXzUYpFh4yyETniGA==", "Murphy", null, false, null, "d1d9c7b1-5431-471f-93f9-ebb121b69366", false, "Instructor4@example.com" },
                    { "6b011e27-afd5-4c3a-8842-10f18e89033d", 0, "70b10371-6a4c-4dc4-9deb-35611c76d0a9", "Instructor1@example.com", false, "Morales", "Myers", false, null, "INSTRUCTOR1@EXAMPLE.COM", "INSTRUCTOR1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKIWekqZFGqUoRmLQjEVcBUP7J/W1UV4LZpB0GCaBlX7ZFVlUoyCIkNdb7gwOQ61bg==", "Brooks", null, false, null, "fee2f89c-25e0-40d8-bca2-797f418a3f5f", false, "Instructor1@example.com" },
                    { "6ccdaac6-6f55-4459-a9eb-1919dd2387e7", 0, "e2bf3415-afe1-466b-871e-b79b788ee4ba", "Student2@example.com", false, "Washington", "Crawford", false, null, "STUDENT2@EXAMPLE.COM", "STUDENT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAENFOB2SY5qso3XTSped5lQaNCHG1jCrT7S4QQdACu29Aqm5aM7mpxHZZZsrZkjxIHg==", "Angela", null, false, null, "27103a48-e890-4f14-b055-8bd245c1592d", false, "Student2@example.com" },
                    { "6fa6554c-47a9-4cc4-8a65-cb3d00ab898a", 0, "afa2f221-33c9-4177-add3-717c35a33829", "Student1@example.com", false, "Ronnie", "Stacey", false, null, "STUDENT1@EXAMPLE.COM", "STUDENT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDCC8nk6IoIZGmaidGNnQtkFca0MNCKV/UC/sORF/6oFNJ7oJTToKo8NZiiZe2SG4A==", "Flores", null, false, null, "b605b244-63e4-4955-979d-433074544f3b", false, "Student1@example.com" },
                    { "ba1961f7-d5ad-4446-8752-43265868350e", 0, "d3a9393f-1a10-4eb8-b9e7-49942633418c", "Instructor2@example.com", false, "Chambers", "Mattie", false, null, "INSTRUCTOR2@EXAMPLE.COM", "INSTRUCTOR2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBMzrpoVWlp1jSlAgBmcmsMyaJ3f0GzmqWxVfCwkZNp+O+/NH/ioZWW1pxI/tXMnwg==", "Eleanor", null, false, null, "681d4e21-8057-4d62-a6ff-f7cf6a1faca5", false, "Instructor2@example.com" },
                    { "c2d36f8c-ebaf-4d10-a446-f381dd0f0ae5", 0, "644c8f4f-f644-440f-bf67-8718062fa300", "Student0@example.com", false, "Ronnie", "Chambers", false, null, "STUDENT0@EXAMPLE.COM", "STUDENT0@EXAMPLE.COM", "AQAAAAIAAYagAAAAEELpXJwCRXiq2l05iMB45QvKrlCSYiWlvlEGDpb56+AhPqWAQCk4IXWgF5IYjnvDBA==", "Saunders", null, false, null, "94e737e5-85a0-4d06-bc7d-98349b816f1e", false, "Student0@example.com" },
                    { "ec2d8787-e86e-426a-880d-c2b75ce91e11", 0, "ff75c0ac-f6b9-4869-9b1e-89973ea6bd59", "admin@example.com", false, "Mattie", "Williams", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGSWEP8+QP6CGd2xG4/VMFGYNd0U/OPNpLZmvhAk5hQ4fbsbPAH8Li1+rwTyZdJYvA==", "Charlotte", null, false, null, "263b4429-aa78-4149-8f51-7116c5a9a62b", false, "admin@example.com" },
                    { "ef444d72-a812-45cc-aa27-6e6cba8b4dec", 0, "fa6be2e0-10fd-43da-a924-2d0a920dae38", "Instructor3@example.com", false, "Elaine", "Dorothy", false, null, "INSTRUCTOR3@EXAMPLE.COM", "INSTRUCTOR3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPteOga7e9s7EeQ1FvgdMGwchLJDMusrICUyXUpiLtbg5cPgHoqVQITGUPtgd17XvQ==", "Curtis", null, false, null, "c05ef301-5d7d-4649-87bc-6c9fddee1a70", false, "Instructor3@example.com" },
                    { "fc6f240a-9935-40a7-9a94-1b25d6be551c", 0, "76e6b8ed-643b-4a52-a7cb-df6b9be7a8a4", "Student4@example.com", false, "Jeremy", "Stacey", false, null, "STUDENT4@EXAMPLE.COM", "STUDENT4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEnTYA/RLUhFlBj7LRHhuVmMnZkDOSHhPUNzXREyAH4QLpsS1uGtRuNFDazBn5S+ew==", "Williams", null, false, null, "f9ae3a8a-740a-4a6d-b3c5-3f0bbb2f69f5", false, "Student4@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", "3e6063e3-901c-4e34-a854-ffcd5a1eec14" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", "4a1ea45b-1dc8-4fc5-9464-d5059a2255b0" },
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", "5eb6a37c-e12d-4bb1-b404-c0e7c1b0cc23" },
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", "6b011e27-afd5-4c3a-8842-10f18e89033d" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", "6ccdaac6-6f55-4459-a9eb-1919dd2387e7" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", "6fa6554c-47a9-4cc4-8a65-cb3d00ab898a" },
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", "ba1961f7-d5ad-4446-8752-43265868350e" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", "c2d36f8c-ebaf-4d10-a446-f381dd0f0ae5" },
                    { "abe9d1cc-fcd7-44c6-a097-6f6b7f959390", "ec2d8787-e86e-426a-880d-c2b75ce91e11" },
                    { "d4284e2e-82bd-4aae-9fa8-71de003b5cba", "ef444d72-a812-45cc-aa27-6e6cba8b4dec" },
                    { "b416bc56-f1c4-473f-b4e4-f93abb8ad90a", "fc6f240a-9935-40a7-9a94-1b25d6be551c" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "UserId" },
                values: new object[,]
                {
                    { 1, "3e6063e3-901c-4e34-a854-ffcd5a1eec14" },
                    { 2, "6b011e27-afd5-4c3a-8842-10f18e89033d" },
                    { 3, "ba1961f7-d5ad-4446-8752-43265868350e" },
                    { 4, "ef444d72-a812-45cc-aa27-6e6cba8b4dec" },
                    { 5, "5eb6a37c-e12d-4bb1-b404-c0e7c1b0cc23" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "InstructorId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "c2d36f8c-ebaf-4d10-a446-f381dd0f0ae5" },
                    { 2, null, "6fa6554c-47a9-4cc4-8a65-cb3d00ab898a" },
                    { 3, null, "6ccdaac6-6f55-4459-a9eb-1919dd2387e7" },
                    { 4, null, "4a1ea45b-1dc8-4fc5-9464-d5059a2255b0" },
                    { 5, null, "fc6f240a-9935-40a7-9a94-1b25d6be551c" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InnerScheduleOfInstructorId",
                table: "Classes",
                column: "InnerScheduleOfInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentId",
                table: "Classes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerSchedulesOfInstructors_InstructorId",
                table: "InnerSchedulesOfInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerSchedulesOfInstructors_OuterScheduleId",
                table: "InnerSchedulesOfInstructors",
                column: "OuterScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OuterSchedulesOfInstructors_InstructorId",
                table: "OuterSchedulesOfInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstructorId",
                table: "Students",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GradesByInstructorToStudent");

            migrationBuilder.DropTable(
                name: "GradesByStudentToInstructor");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "InnerSchedulesOfInstructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "OuterSchedulesOfInstructors");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
