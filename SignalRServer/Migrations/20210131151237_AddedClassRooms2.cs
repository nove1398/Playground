using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRServer.Migrations
{
    public partial class AddedClassRooms2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.ClassRoomId);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ClassRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_ClassRooms_ClassRoomId",
                        column: x => x.ClassRoomId,
                        principalTable: "ClassRooms",
                        principalColumn: "ClassRoomId");
                });

            migrationBuilder.CreateTable(
                name: "GradeStudent",
                columns: table => new
                {
                    GradesID = table.Column<int>(type: "int", nullable: false),
                    StudentsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeStudent", x => new { x.GradesID, x.StudentsID });
                    table.ForeignKey(
                        name: "FK_GradeStudent_Grade_GradesID",
                        column: x => x.GradesID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeStudent_Student_StudentsID",
                        column: x => x.StudentsID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeStudent_StudentsID",
                table: "GradeStudent",
                column: "StudentsID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassRoomId",
                table: "Student",
                column: "ClassRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeStudent");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "ClassRooms");
        }
    }
}