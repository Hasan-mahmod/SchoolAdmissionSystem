using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystemAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationSystems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    BirthCertificateID = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: false),
                    FatherOccupation = table.Column<string>(nullable: true),
                    FatherPhone = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: false),
                    MotherOccupation = table.Column<string>(nullable: true),
                    MotherPhone = table.Column<string>(nullable: true),
                    GardianName = table.Column<string>(nullable: false),
                    GardianOccupation = table.Column<string>(nullable: true),
                    GardianPhone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PresentAddress = table.Column<string>(nullable: true),
                    ParmanentAddress = table.Column<string>(nullable: true),
                    ContuctNumber = table.Column<string>(maxLength: 11, nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Signature = table.Column<string>(nullable: true),
                    StudentRegDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "SchoolInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EIIN = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false),
                    SchoolName = table.Column<string>(nullable: true),
                    TokenCode = table.Column<string>(nullable: true),
                    EduSystem = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    SchoolRegDate = table.Column<DateTime>(nullable: false),
                    PrincipalSeal = table.Column<string>(nullable: true),
                    PrincipalSigneture = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    ShiftName = table.Column<int>(nullable: false),
                    ContactNumber1 = table.Column<string>(nullable: true),
                    ContactNumber2 = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolInfos_EducationSystems_EduSystem",
                        column: x => x.EduSystem,
                        principalTable: "EducationSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreviousSchoolInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousExam = table.Column<string>(nullable: false),
                    Board = table.Column<string>(nullable: false),
                    PreviousSchool = table.Column<string>(nullable: true),
                    Roll = table.Column<int>(nullable: false),
                    RegistrationNumber = table.Column<int>(nullable: false),
                    PassingYear = table.Column<int>(nullable: false),
                    ResultGPA = table.Column<decimal>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousSchoolInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreviousSchoolInfos_StudentInfos_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentSubjectGPAs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    GPA = table.Column<decimal>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubjectGPAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSubjectGPAs_StudentInfos_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(nullable: false),
                    ShiftName = table.Column<int>(nullable: false),
                    Class = table.Column<string>(nullable: true),
                    NumberOfSeat = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmissionClasses_SchoolInfos_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplyForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<int>(nullable: false),
                    AdmissionClas = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    SchoolTokenCode = table.Column<string>(nullable: false),
                    TokenNum = table.Column<int>(nullable: false),
                    Shift = table.Column<int>(nullable: false),
                    Group = table.Column<int>(nullable: false),
                    ApplyDate = table.Column<DateTime>(nullable: false),
                    IsEdit = table.Column<bool>(nullable: false),
                    PaymentStatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyForms_AdmissionClasses_AdmissionClas",
                        column: x => x.AdmissionClas,
                        principalTable: "AdmissionClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyForms_SchoolInfos_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplyForms_StudentInfos_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(nullable: true),
                    NoticeId = table.Column<string>(nullable: true),
                    SchoolId = table.Column<int>(nullable: false),
                    AdmissionClas = table.Column<int>(nullable: false),
                    Shift = table.Column<int>(nullable: false),
                    AvailableSeat = table.Column<int>(nullable: false),
                    StartApplyDate = table.Column<DateTime>(nullable: false),
                    LastApplyDate = table.Column<DateTime>(nullable: false),
                    NoticeDate = table.Column<DateTime>(nullable: false),
                    AdmissionDate = table.Column<DateTime>(nullable: false),
                    ExamDateOrLotteryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notices_AdmissionClasses_AdmissionClas",
                        column: x => x.AdmissionClas,
                        principalTable: "AdmissionClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notices_SchoolInfos_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AdmitCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationID = table.Column<int>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmitCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdmitCards_ApplyForms_ApplicationID",
                        column: x => x.ApplicationID,
                        principalTable: "ApplyForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(nullable: true),
                    ApplyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    SchoolAmount = table.Column<decimal>(nullable: false),
                    SchTrxID = table.Column<string>(nullable: true),
                    ChargeAmount = table.Column<decimal>(nullable: false),
                    ChaTrxID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_ApplyForms_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "ApplyForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoticeId = table.Column<int>(nullable: false),
                    ExamDate = table.Column<DateTime>(nullable: false),
                    TotalMarks = table.Column<decimal>(nullable: false),
                    PassMark = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamInfos_Notices_NoticeId",
                        column: x => x.NoticeId,
                        principalTable: "Notices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenCode = table.Column<string>(nullable: true),
                    TokenNumber = table.Column<int>(nullable: false),
                    TotalScore = table.Column<decimal>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false),
                    Merit = table.Column<string>(nullable: false),
                    Class = table.Column<string>(nullable: false),
                    Shift = table.Column<int>(nullable: false),
                    Year = table.Column<string>(nullable: true),
                    ExamInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_ExamInfos_ExamInfoId",
                        column: x => x.ExamInfoId,
                        principalTable: "ExamInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("95b25b97-897f-4e14-88a4-549c568dae2e"), "46fc08a5-e7aa-4e5b-88b6-7cb8ef62a156", "Super Admin", "SUPER ADMIN" },
                    { new Guid("5e618f4e-a97c-4633-9946-a70fba92b8f4"), "a267c528-687c-45bb-a34a-5ff2a55fd532", "Admin", "ADMIN" },
                    { new Guid("78a22a26-fd07-406d-a8c7-21ed5a35b0b5"), "54a7e1e3-2175-48ed-b5b0-b5b9e8728fa7", "Schools", "SCHOOLS" },
                    { new Guid("7d196e09-73da-43e7-aa37-b59600962fd5"), "b6b4ade3-b003-41ee-8762-d34fc6fe81c1", "Students", "STUDENTS" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("7f6898f0-d933-49a2-952e-46c2ae735270"), 0, "7c01d51c-f220-4272-a0a9-70d6fde7662a", "superadmin@admin.com", false, false, null, null, null, "ALf/Tnf6bFzZ65LehABGVqt61fKwdGAuR6Kp0jy/j2m/xqwKbrArD6zwAkhjvlRaaQ==", null, false, null, false, "Super Admin" },
                    { new Guid("680e7576-62ad-4e5e-a88f-a7754b9a7038"), 0, "a067bc7d-2294-48a8-b96a-885939e020e9", "admin@admin.com", false, false, null, null, null, "AMl5OsCdhjMWFicfjErtr2AEfeE9Bt8uzN5tdiIAyuu2gplhb82oiqmjcyKcmdZ0jQ==", null, false, null, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "EducationSystems",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Boys" },
                    { 2, "Girls" },
                    { 3, "Co-Education" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { new Guid("7f6898f0-d933-49a2-952e-46c2ae735270"), new Guid("95b25b97-897f-4e14-88a4-549c568dae2e"), "UserRole" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId", "Discriminator" },
                values: new object[] { new Guid("680e7576-62ad-4e5e-a88f-a7754b9a7038"), new Guid("5e618f4e-a97c-4633-9946-a70fba92b8f4"), "UserRole" });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionClasses_SchoolId",
                table: "AdmissionClasses",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmitCards_ApplicationID",
                table: "AdmitCards",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyForms_AdmissionClas",
                table: "ApplyForms",
                column: "AdmissionClas");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyForms_SchoolId",
                table: "ApplyForms",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyForms_StudentId",
                table: "ApplyForms",
                column: "StudentId");

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
                name: "IX_ExamInfos_NoticeId",
                table: "ExamInfos",
                column: "NoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notices_AdmissionClas",
                table: "Notices",
                column: "AdmissionClas");

            migrationBuilder.CreateIndex(
                name: "IX_Notices_SchoolId",
                table: "Notices",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplyId",
                table: "Payments",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PreviousSchoolInfos_StudentId",
                table: "PreviousSchoolInfos",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ExamInfoId",
                table: "Results",
                column: "ExamInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolInfos_EduSystem",
                table: "SchoolInfos",
                column: "EduSystem");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubjectGPAs_StudentId",
                table: "StudentSubjectGPAs",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmitCards");

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
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PreviousSchoolInfos");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "StudentSubjectGPAs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApplyForms");

            migrationBuilder.DropTable(
                name: "ExamInfos");

            migrationBuilder.DropTable(
                name: "StudentInfos");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "AdmissionClasses");

            migrationBuilder.DropTable(
                name: "SchoolInfos");

            migrationBuilder.DropTable(
                name: "EducationSystems");
        }
    }
}
