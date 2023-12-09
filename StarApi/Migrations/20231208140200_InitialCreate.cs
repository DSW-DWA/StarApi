using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StarApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alembic_version",
                columns: table => new
                {
                    version_num = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("alembic_version_pkc", x => x.version_num);
                });

            migrationBuilder.CreateTable(
                name: "audit",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    table_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    row_id = table.Column<Guid>(type: "uuid", nullable: true),
                    old_value = table.Column<string>(type: "text", nullable: true),
                    new_value = table.Column<string>(type: "text", nullable: true),
                    operation_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("audit_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "universe",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    size = table.Column<double>(type: "double precision", nullable: false),
                    composition = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("universe_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "galaxy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    universe_id = table.Column<Guid>(type: "uuid", nullable: true),
                    size = table.Column<double>(type: "double precision", nullable: false),
                    shape = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    composition = table.Column<string>(type: "text", nullable: true),
                    distance_from_earth = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("galaxy_pkey", x => x.id);
                    table.ForeignKey(
                        name: "galaxy_universe_id_fkey",
                        column: x => x.universe_id,
                        principalTable: "universe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "constellation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    galaxy_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    shape = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    abbreviation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    history = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("constellation_pkey", x => x.id);
                    table.ForeignKey(
                        name: "constellation_galaxy_id_fkey",
                        column: x => x.galaxy_id,
                        principalTable: "galaxy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "star",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    galaxy_id = table.Column<Guid>(type: "uuid", nullable: true),
                    spectral_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    luminosity = table.Column<double>(type: "double precision", nullable: false),
                    distance_from_earth = table.Column<double>(type: "double precision", nullable: false),
                    temperature = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("star_pkey", x => x.id);
                    table.ForeignKey(
                        name: "star_galaxy_id_fkey",
                        column: x => x.galaxy_id,
                        principalTable: "galaxy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mass = table.Column<double>(type: "double precision", nullable: false),
                    diameter = table.Column<double>(type: "double precision", nullable: false),
                    distance_from_star = table.Column<double>(type: "double precision", nullable: false),
                    surface_temperature = table.Column<double>(type: "double precision", nullable: true),
                    star_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("planet_pkey", x => x.id);
                    table.ForeignKey(
                        name: "planet_star_id_fkey",
                        column: x => x.star_id,
                        principalTable: "star",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "star_constellation",
                columns: table => new
                {
                    star_id = table.Column<Guid>(type: "uuid", nullable: false),
                    constellation_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("star_constellation_pkey", x => new { x.star_id, x.constellation_id });
                    table.ForeignKey(
                        name: "star_constellation_constellation_id_fkey",
                        column: x => x.constellation_id,
                        principalTable: "constellation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "star_constellation_star_id_fkey",
                        column: x => x.star_id,
                        principalTable: "star",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_constellation_galaxy_id",
                table: "constellation",
                column: "galaxy_id");

            migrationBuilder.CreateIndex(
                name: "IX_galaxy_universe_id",
                table: "galaxy",
                column: "universe_id");

            migrationBuilder.CreateIndex(
                name: "IX_planet_star_id",
                table: "planet",
                column: "star_id");

            migrationBuilder.CreateIndex(
                name: "IX_star_galaxy_id",
                table: "star",
                column: "galaxy_id");

            migrationBuilder.CreateIndex(
                name: "IX_star_constellation_constellation_id",
                table: "star_constellation",
                column: "constellation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alembic_version");

            migrationBuilder.DropTable(
                name: "audit");

            migrationBuilder.DropTable(
                name: "planet");

            migrationBuilder.DropTable(
                name: "star_constellation");

            migrationBuilder.DropTable(
                name: "constellation");

            migrationBuilder.DropTable(
                name: "star");

            migrationBuilder.DropTable(
                name: "galaxy");

            migrationBuilder.DropTable(
                name: "universe");
        }
    }
}
