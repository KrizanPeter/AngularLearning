﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePlans",
                columns: table => new
                {
                    GamePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlans", x => x.GamePlanId);
                });

            migrationBuilder.CreateTable(
                name: "HeroTypes",
                columns: table => new
                {
                    HeroTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroTypes", x => x.HeroTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    ItemTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.ItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MonsterTypes",
                columns: table => new
                {
                    MonsterTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonsterTypes", x => x.MonsterTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamePlanId = table.Column<int>(type: "int", nullable: false),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Sessions_GamePlans_GamePlanId",
                        column: x => x.GamePlanId,
                        principalTable: "GamePlans",
                        principalColumn: "GamePlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heroes",
                columns: table => new
                {
                    HeroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeroTypeId = table.Column<int>(type: "int", nullable: false),
                    HeroName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lives = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heroes", x => x.HeroId);
                    table.ForeignKey(
                        name: "FK_Heroes_HeroTypes_HeroTypeId",
                        column: x => x.HeroTypeId,
                        principalTable: "HeroTypes",
                        principalColumn: "HeroTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    MonsterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MonsterTypeId = table.Column<int>(type: "int", nullable: false),
                    MonsterName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.MonsterId);
                    table.ForeignKey(
                        name: "FK_Monsters_MonsterTypes_MonsterTypeId",
                        column: x => x.MonsterTypeId,
                        principalTable: "MonsterTypes",
                        principalColumn: "MonsterTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "HeroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameBlocks",
                columns: table => new
                {
                    GameBlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GamePlanId = table.Column<int>(type: "int", nullable: false),
                    HeroId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonsterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBlocks", x => x.GameBlockId);
                    table.ForeignKey(
                        name: "FK_GameBlocks_GamePlans_GamePlanId",
                        column: x => x.GamePlanId,
                        principalTable: "GamePlans",
                        principalColumn: "GamePlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameBlocks_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "HeroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameBlocks_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameBlocks_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "MonsterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameBlocks_GamePlanId",
                table: "GameBlocks",
                column: "GamePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBlocks_HeroId",
                table: "GameBlocks",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBlocks_ItemId",
                table: "GameBlocks",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GameBlocks_MonsterId",
                table: "GameBlocks",
                column: "MonsterId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_HeroTypeId",
                table: "Heroes",
                column: "HeroTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_HeroId",
                table: "Items",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_MonsterTypeId",
                table: "Monsters",
                column: "MonsterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GamePlanId",
                table: "Sessions",
                column: "GamePlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameBlocks");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Monsters");

            migrationBuilder.DropTable(
                name: "GamePlans");

            migrationBuilder.DropTable(
                name: "Heroes");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "MonsterTypes");

            migrationBuilder.DropTable(
                name: "HeroTypes");
        }
    }
}
