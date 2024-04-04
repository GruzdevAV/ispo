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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronym = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronym = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    { "068aefff-3046-4157-80a4-06d71d20ad34", null, "Admin", "ADMIN" },
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", null, "Instructor", "INSTRUCTOR" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", null, "Student", "STUDENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b68d11c-b737-4bb4-b2c3-1e054734de16", 0, "9bc561c6-8297-48c9-a038-d0bef001b167", "Instructor3@example.com", false, false, null, "INSTRUCTOR3@EXAMPLE.COM", "INSTRUCTOR3@EXAMPLE.COM", "AQAAAAIAAYagAAAAELcsm6pyP5QRPU5XVRCp2YqmdSjPQk9uYV2KLGp+ejwr6FabXaBdCyF2GIrzzCJaTQ==", null, false, "cd7a2979-a4a8-4cd4-bc1c-59132db6483f", false, "Instructor3@example.com" },
                    { "1b7dafc3-feb9-4ad7-b12e-014b6ad84f89", 0, "f1b905be-f259-41c2-ad0e-14a71f4db70a", "Student2@example.com", false, false, null, "STUDENT2@EXAMPLE.COM", "STUDENT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAECCmq7m61TwYLCH3zU6gLMBqLY8hE1+NONiPJiuWK6YJ3lEz7RjOMefFBAeK6k5gPg==", null, false, "8fbbca68-a001-419b-be0e-3db8c77ec12f", false, "Student2@example.com" },
                    { "3b91d4b7-37db-4b1e-8954-ade244999d51", 0, "2f76f0ee-043e-44f8-9c64-a04dc5e43f00", "Student4@example.com", false, false, null, "STUDENT4@EXAMPLE.COM", "STUDENT4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEP++oVeTl2o7/VC6bZcYDBygQ+RtI2hKp9lwq3BG15gTBdd5rs+1nUPBkRaEWxm9hg==", null, false, "e2495f7e-a8a3-4cd5-afbd-08b756465be2", false, "Student4@example.com" },
                    { "5b6fb9d6-696c-45f0-a39e-38dbb8e4712d", 0, "016155b0-2386-407b-b81b-7192bb963722", "Student3@example.com", false, false, null, "STUDENT3@EXAMPLE.COM", "STUDENT3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEO/WaNV7d+j7ZKo9YX8En8IXSY5pHsIgBrnDsxUUoDfZ7TBEEv+ssXl5ppsdkV+ezw==", null, false, "a38d44dd-0f17-4d95-8ee3-be4294b87f5f", false, "Student3@example.com" },
                    { "5bcf6870-4a90-45ba-9ab4-546d7b9753f2", 0, "6c4a4639-9ea3-46b1-8519-4f047af45e84", "Instructor4@example.com", false, false, null, "INSTRUCTOR4@EXAMPLE.COM", "INSTRUCTOR4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBfQviM7yiO8p8JRDhOXksVemUQBAOgHRO9VVnBr6mY6veKSEX01E+hjMsuBQPVcvQ==", null, false, "dc942fca-e480-4a32-b663-bb3842361d34", false, "Instructor4@example.com" },
                    { "5e4b2e26-0835-4a11-ae3a-c00f2115ec34", 0, "bfc1f80c-3e76-4182-bf36-fc5fe96b1a3c", "Instructor1@example.com", false, false, null, "INSTRUCTOR1@EXAMPLE.COM", "INSTRUCTOR1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEASWRJhdmZhltp+TQVgdIxohX0fEGgK7bbO/2a/WlIkC4tBPtcrqtTdP3iatnaQPcQ==", null, false, "7a75fc63-8d25-4a31-a7c5-a8c46badf030", false, "Instructor1@example.com" },
                    { "6cc40534-fe85-46d4-9926-0ba27e9e976d", 0, "3f570a3b-d3a0-4a28-8e05-a88e16ac1d75", "Instructor0@example.com", false, false, null, "INSTRUCTOR0@EXAMPLE.COM", "INSTRUCTOR0@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJNNXRHcbZbAw11YwyNmteVGotDH7lbAa+t/pYIoz0h3iikG3+W7n/2tbzcnLGK8NA==", null, false, "7ef97b4b-5cbd-4d3c-ad72-ee89cae5c17d", false, "Instructor0@example.com" },
                    { "7504afc7-322e-491c-b8db-691a8b7bb066", 0, "15949e6f-8a5a-4226-a554-9337445e5b01", "Student1@example.com", false, false, null, "STUDENT1@EXAMPLE.COM", "STUDENT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDJNZVaYHxThhDok0cCm0tLMFeCM6Jb6QEnnGcVOBTYbZmfRULWBdvlQ9I3MR9UOmw==", null, false, "9004d0b4-1c14-4eb4-be3a-a42b08de8870", false, "Student1@example.com" },
                    { "b211d8c1-7076-4dfc-8952-8652d66db07a", 0, "705200e7-99c2-4ea0-8672-0f3433582ca6", "admin@example.com", false, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEqP6yd9tSf+Gq0Bj+MK4FI5OoI4cbZd9Yfxj58q/+R8hsU2n2xxsWJuKJihTWx1fw==", null, false, "b237c846-5afa-4fc0-b43e-738c0ee6c722", false, "admin@example.com" },
                    { "c0267e6f-dea9-4fe3-b02d-31a53ffb66be", 0, "57412aac-452b-46e0-b209-577d2496fde5", "Instructor2@example.com", false, false, null, "INSTRUCTOR2@EXAMPLE.COM", "INSTRUCTOR2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEK8hgSAmsr/aGFtZXmecW9AixDxny/irhVG79P8468EqysBoGUB8+hLJwm3H/ne/kA==", null, false, "4ac7eb05-5ba2-4556-87e6-8fba42f52bcb", false, "Instructor2@example.com" },
                    { "e7cf6263-fe14-452a-9e8d-84ab4154cad9", 0, "fd404765-47d8-4952-bdc6-6d0e7a3d63c5", "Student0@example.com", false, false, null, "STUDENT0@EXAMPLE.COM", "STUDENT0@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMfyKuvtxsVBNL+cPqDA0QPhsPkG334byuFc1rILvTRJ3d7lGAKckBgz+Gbbp+QKsg==", null, false, "21b347cd-cd80-48c8-85a5-c54a1a2bcbda", false, "Student0@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", "0b68d11c-b737-4bb4-b2c3-1e054734de16" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", "1b7dafc3-feb9-4ad7-b12e-014b6ad84f89" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", "3b91d4b7-37db-4b1e-8954-ade244999d51" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", "5b6fb9d6-696c-45f0-a39e-38dbb8e4712d" },
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", "5bcf6870-4a90-45ba-9ab4-546d7b9753f2" },
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", "5e4b2e26-0835-4a11-ae3a-c00f2115ec34" },
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", "6cc40534-fe85-46d4-9926-0ba27e9e976d" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", "7504afc7-322e-491c-b8db-691a8b7bb066" },
                    { "068aefff-3046-4157-80a4-06d71d20ad34", "b211d8c1-7076-4dfc-8952-8652d66db07a" },
                    { "73e22705-bb8d-44f4-827d-c3f2bd189a10", "c0267e6f-dea9-4fe3-b02d-31a53ffb66be" },
                    { "918f5dbd-de7f-4531-aebe-9e51ce4d69fc", "e7cf6263-fe14-452a-9e8d-84ab4154cad9" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "FirstName", "LastName", "Patronym", "UserId" },
                values: new object[,]
                {
                    { 1, "Elaine", "Jamie", "Charlotte", "6cc40534-fe85-46d4-9926-0ba27e9e976d" },
                    { 2, "Mary", "Mary", "Jackson", "5e4b2e26-0835-4a11-ae3a-c00f2115ec34" },
                    { 3, "Sara", "Miles", "Williams", "c0267e6f-dea9-4fe3-b02d-31a53ffb66be" },
                    { 4, "Crawford", "Brown", "Crawford", "0b68d11c-b737-4bb4-b2c3-1e054734de16" },
                    { 5, "Elaine", "Douglas", "Mary", "5bcf6870-4a90-45ba-9ab4-546d7b9753f2" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "InstructorId", "LastName", "Patronym", "UserId" },
                values: new object[,]
                {
                    { 1, "Gill", null, "Mattie", "Jeremy", "e7cf6263-fe14-452a-9e8d-84ab4154cad9" },
                    { 2, "Jackson", null, "Murphy", "Douglas", "7504afc7-322e-491c-b8db-691a8b7bb066" },
                    { 3, "Nelson", null, "Amy", "Saunders", "1b7dafc3-feb9-4ad7-b12e-014b6ad84f89" },
                    { 4, "Stacey", null, "Timothy", "Stacey", "5b6fb9d6-696c-45f0-a39e-38dbb8e4712d" },
                    { 5, "Washington", null, "Charlotte", "Elaine", "3b91d4b7-37db-4b1e-8954-ade244999d51" }
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
