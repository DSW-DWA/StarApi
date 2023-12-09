using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION audit_trigger_function()
            RETURNS TRIGGER AS $$
            BEGIN
                IF (TG_OP = 'DELETE') THEN
                    INSERT INTO audit (table_name, row_id, old_value, operation_type, timestamp)
                    VALUES (TG_TABLE_NAME, OLD.id, row_to_json(OLD), TG_OP, now());
                    RETURN OLD;
                ELSIF (TG_OP = 'UPDATE') THEN
                    INSERT INTO audit (table_name, row_id, old_value, new_value, operation_type, timestamp)
                    VALUES (TG_TABLE_NAME, NEW.id, row_to_json(OLD), row_to_json(NEW), TG_OP, now());
                    RETURN NEW;
                ELSIF (TG_OP = 'INSERT') THEN
                    INSERT INTO audit (table_name, row_id, new_value, operation_type, timestamp)
                    VALUES (TG_TABLE_NAME, NEW.id, row_to_json(NEW), TG_OP, now());
                    RETURN NEW;
                END IF;
                RETURN NULL;
            END;
            $$ LANGUAGE plpgsql;
            ");

            string[] tables = new string[]
            {
            "universe",
            "star_constellation",
            "star",
            "planet",
            "galaxy",
            "constellation",
            };

            foreach (var table in tables)
            {
                migrationBuilder.Sql($@"
                CREATE TRIGGER {table}_audit_trigger
                AFTER INSERT OR UPDATE OR DELETE ON {table}
                FOR EACH ROW
                EXECUTE FUNCTION audit_trigger_function();
            ");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string[] tables = new string[]
            {
            "universe",
            "star_constellation",
            "star",
            "planet",
            "galaxy",
            "constellation",
            };

            foreach (var table in tables)
            {
                migrationBuilder.Sql($@"
                DROP TRIGGER IF EXISTS {table}_audit_trigger ON {table};
            ");
            }

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS audit_trigger_function;");
        }
    }
}
