using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetNinja.Dojo.Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Annotations",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annotations", x => new { x.Name, x.Value })
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Kind = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => new { x.Kind, x.Name })
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => new { x.Name, x.Value })
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "EntityAnnotations",
                columns: table => new
                {
                    AnnotationsName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    AnnotationsValue = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    EntitiesKind = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    EntitiesName = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityAnnotations", x => new { x.AnnotationsName, x.AnnotationsValue, x.EntitiesKind, x.EntitiesName });
                    table.ForeignKey(
                        name: "FK_EntityAnnotations_Annotations_AnnotationsName_AnnotationsValue",
                        columns: x => new { x.AnnotationsName, x.AnnotationsValue },
                        principalTable: "Annotations",
                        principalColumns: new[] { "Name", "Value" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityAnnotations_Entities_EntitiesKind_EntitiesName",
                        columns: x => new { x.EntitiesKind, x.EntitiesName },
                        principalTable: "Entities",
                        principalColumns: new[] { "Kind", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    EntityName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EntityKind = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Scheme = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => new { x.EntityKind, x.EntityName })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Locations_Entities_EntityKind_EntityName",
                        columns: x => new { x.EntityKind, x.EntityName },
                        principalTable: "Entities",
                        principalColumns: new[] { "Kind", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityLabels",
                columns: table => new
                {
                    EntitiesKind = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    EntitiesName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LabelsName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    LabelsValue = table.Column<string>(type: "nvarchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLabels", x => new { x.EntitiesKind, x.EntitiesName, x.LabelsName, x.LabelsValue });
                    table.ForeignKey(
                        name: "FK_EntityLabels_Entities_EntitiesKind_EntitiesName",
                        columns: x => new { x.EntitiesKind, x.EntitiesName },
                        principalTable: "Entities",
                        principalColumns: new[] { "Kind", "Name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityLabels_Labels_LabelsName_LabelsValue",
                        columns: x => new { x.LabelsName, x.LabelsValue },
                        principalTable: "Labels",
                        principalColumns: new[] { "Name", "Value" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityTags",
                columns: table => new
                {
                    TagsName = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    EntitiesKind = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    EntitiesName = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTags", x => new { x.TagsName, x.EntitiesKind, x.EntitiesName });
                    table.ForeignKey(
                        name: "FK_EntityTags_Entities_EntitiesKind_EntitiesName",
                        columns: x => new { x.EntitiesKind, x.EntitiesName },
                        principalTable: "Entities",
                        principalColumns: new[] { "Kind", "Name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityTags_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityAnnotations_EntitiesKind_EntitiesName",
                table: "EntityAnnotations",
                columns: new[] { "EntitiesKind", "EntitiesName" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityLabels_LabelsName_LabelsValue",
                table: "EntityLabels",
                columns: new[] { "LabelsName", "LabelsValue" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityTags_EntitiesKind_EntitiesName",
                table: "EntityTags",
                columns: new[] { "EntitiesKind", "EntitiesName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domains");

            migrationBuilder.DropTable(
                name: "EntityAnnotations");

            migrationBuilder.DropTable(
                name: "EntityLabels");

            migrationBuilder.DropTable(
                name: "EntityTags");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Annotations");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
