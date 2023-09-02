using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnonymousForum.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    ThreadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadTitle = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ThreadDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkTopicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.ThreadId);
                    table.ForeignKey(
                        name: "FK_Threads_Topics_FkTopicId",
                        column: x => x.FkTopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyTitle = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ReplyDescription = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FkThreadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_Replies_Threads_FkThreadId",
                        column: x => x.FkThreadId,
                        principalTable: "Threads",
                        principalColumn: "ThreadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_FkThreadId",
                table: "Replies",
                column: "FkThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_FkTopicId",
                table: "Threads",
                column: "FkTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ThreadTitle",
                table: "Threads",
                column: "ThreadTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_TopicName",
                table: "Topics",
                column: "TopicName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
