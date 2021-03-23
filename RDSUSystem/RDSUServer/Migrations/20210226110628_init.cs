using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RDSUServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RDSUWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDSUWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RDSUWorkers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    TrainerFullName = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlannerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Adress = table.Column<string>(type: "TEXT", nullable: true),
                    PlannerFullName = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_RDSUWorkers_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "RDSUWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id1 = table.Column<int>(type: "INTEGER", nullable: true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true),
                    AthletNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isMale = table.Column<bool>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    St = table.Column<byte>(type: "INTEGER", nullable: false),
                    La = table.Column<byte>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Number);
                    table.ForeignKey(
                        name: "FK_Athletes_Athletes_AthletNumber",
                        column: x => x.AthletNumber,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Users_Id1",
                        column: x => x.Id1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ch_LogIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isMale = table.Column<bool>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ch_LogIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ch_LogIns_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Link = table.Column<string>(type: "TEXT", nullable: true),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: true),
                    OldCategory = table.Column<byte>(type: "INTEGER", nullable: false),
                    DanceCategory = table.Column<byte>(type: "INTEGER", nullable: false),
                    isSt = table.Column<bool>(type: "INTEGER", nullable: false),
                    Protocol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ch_FullNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AthletNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    NewFullName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ch_FullNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ch_FullNames_Athletes_AthletNumber",
                        column: x => x.AthletNumber,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ch_Partnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FromNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    ToNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ch_Partnerships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ch_Partnerships_Athletes_FromNumber",
                        column: x => x.FromNumber,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ch_Partnerships_Athletes_ToNumber",
                        column: x => x.ToNumber,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ch_Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DanserNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true),
                    DanserWord = table.Column<bool>(type: "INTEGER", nullable: false),
                    FromWord = table.Column<bool>(type: "INTEGER", nullable: false),
                    ToWord = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ch_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ch_Transfers_Athletes_DanserNumber",
                        column: x => x.DanserNumber,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ch_Transfers_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scorecards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Athletes1Number = table.Column<int>(type: "INTEGER", nullable: true),
                    Athletes2Number = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scorecards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scorecards_Athletes_Athletes1Number",
                        column: x => x.Athletes1Number,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scorecards_Athletes_Athletes2Number",
                        column: x => x.Athletes2Number,
                        principalTable: "Athletes",
                        principalColumn: "Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scorecards_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scorecards_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Disqualificatons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScorecardId = table.Column<int>(type: "INTEGER", nullable: true),
                    Period = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disqualificatons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disqualificatons_Scorecards_ScorecardId",
                        column: x => x.ScorecardId,
                        principalTable: "Scorecards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScorecardId = table.Column<int>(type: "INTEGER", nullable: true),
                    Placement = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodResults_Scorecards_ScorecardId",
                        column: x => x.ScorecardId,
                        principalTable: "Scorecards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_AthletNumber",
                table: "Athletes",
                column: "AthletNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_Id1",
                table: "Athletes",
                column: "Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_TrainerId",
                table: "Athletes",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TournamentId",
                table: "Categories",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_FullNames_AthletNumber",
                table: "Ch_FullNames",
                column: "AthletNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_LogIns_TrainerId",
                table: "Ch_LogIns",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_Partnerships_FromNumber",
                table: "Ch_Partnerships",
                column: "FromNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_Partnerships_ToNumber",
                table: "Ch_Partnerships",
                column: "ToNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_Transfers_DanserNumber",
                table: "Ch_Transfers",
                column: "DanserNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Ch_Transfers_TrainerId",
                table: "Ch_Transfers",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Disqualificatons_ScorecardId",
                table: "Disqualificatons",
                column: "ScorecardId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodResults_ScorecardId",
                table: "GoodResults",
                column: "ScorecardId");

            migrationBuilder.CreateIndex(
                name: "IX_News_TrainerId",
                table: "News",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_RDSUWorkers_UserId",
                table: "RDSUWorkers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scorecards_Athletes1Number",
                table: "Scorecards",
                column: "Athletes1Number");

            migrationBuilder.CreateIndex(
                name: "IX_Scorecards_Athletes2Number",
                table: "Scorecards",
                column: "Athletes2Number");

            migrationBuilder.CreateIndex(
                name: "IX_Scorecards_CategoryId",
                table: "Scorecards",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Scorecards_TrainerId",
                table: "Scorecards",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_PlannerId",
                table: "Tournaments",
                column: "PlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ch_FullNames");

            migrationBuilder.DropTable(
                name: "Ch_LogIns");

            migrationBuilder.DropTable(
                name: "Ch_Partnerships");

            migrationBuilder.DropTable(
                name: "Ch_Transfers");

            migrationBuilder.DropTable(
                name: "Disqualificatons");

            migrationBuilder.DropTable(
                name: "GoodResults");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Scorecards");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "RDSUWorkers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
