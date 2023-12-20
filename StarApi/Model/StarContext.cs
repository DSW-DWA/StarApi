using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarApi.Model;

public partial class StarContext : DbContext
{
    public StarContext()
    {
    }

    public StarContext(DbContextOptions<StarContext> options)
        : base(options)
    {
        if (!IsDatabaseExists())
        {
            this.Database.EnsureCreated();
            this.Database.ExecuteSqlRaw("INSERT INTO universe (id, name, size, composition)\r\nVALUES\r\n('a287b06f-9397-47d3-9ce8-92f2676d522c'::uuid, 'Andromeda', 1.5, 'Stellar, dust, and gas'),\r\n('9cbbf051-2c1a-428f-8c23-98e714f2cf1c'::uuid, 'Whirlpool', 0.5, 'Stellar, dust, and gas'),\r\n('eae94eb1-6f60-4f84-a9c9-ccf59b3d05c3'::uuid, 'Pinwheel', 0.75, 'Stellar, dust, and gas');\r\n\r\nINSERT INTO galaxy (id, name, universe_id, size, shape, composition, distance_from_earth)\r\nVALUES\r\n('8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'Triangulum', 'a287b06f-9397-47d3-9ce8-92f2676d522c'::uuid, 0.3, 'Spiral', 'Stellar, dust, and gas', 3.0),\r\n('2d3a05a3-9296-4397-8d89-ae7388cb92ed'::uuid, 'Bode', '9cbbf051-2c1a-428f-8c23-98e714f2cf1c'::uuid, 0.2, 'Spiral', 'Stellar, dust, and gas', 4.0),\r\n('48bea8d2-98c1-4f68-a94b-bf8d2fbff111'::uuid, 'Sunflower', 'eae94eb1-6f60-4f84-a9c9-ccf59b3d05c3'::uuid, 0.6, 'Spiral', 'Stellar, dust, and gas', 2.5);\r\n\r\nINSERT INTO constellation (id, galaxy_id, name, shape, abbreviation, history)\r\nVALUES\r\n('c32b81a9-83a2-40cf-97fc-bcbecc223ee7'::uuid, '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'Triangulum', 'Triangular', 'Tri', 'Named after the Latin word for triangle'),\r\n('d4a81c3b-d481-4e0d-a8d5-7c987b2e9f02'::uuid, '2d3a05a3-9296-4397-8d89-ae7388cb92ed'::uuid, 'Bode', 'W-shaped', 'Boo', 'Named after the German astronomer Johann Elert Bode'),\r\n('84b94c48-f9af-43db-b2a2-b7d50be72de5'::uuid, '48bea8d2-98c1-4f68-a94b-bf8d2fbff111'::uuid, 'Hercules', 'Irregular', 'Her', 'Named after the mythological Greek hero Hercules');\r\n\r\nINSERT INTO star (id, name, galaxy_id, spectral_type, luminosity, distance_from_earth, temperature)\r\nVALUES \r\n('cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid, 'Sol', '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'G2V', 1.0, 8.31, 5778),\r\n('f6a51829-9e2f-4c5f-a1c7-7115d8bda7fc'::uuid, 'Proxima Centauri', '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'M5Ve', 0.0015, 4.24, 3042),\r\n('c54ba042-0c11-4b0e-9d9b-c5f5c5b5d86c'::uuid, 'Alpha Centauri A', '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'G2V', 1.1, 4.37, 5790),\r\n('76a2a29a-af8c-4617-bb57-0d79b1319d04'::uuid, 'Alpha Centauri B', '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'K1V', 0.907, 4.37, 5260),\r\n('af14baab-2e2b-4a12-a9f9-007aa0a0e64e'::uuid, 'Barnards Star', '8d4a54a4-4c11-4db5-a8a5-86e55a124f61'::uuid, 'M4Ve', 0.0004, 5.96, 3134);\r\n\r\nINSERT INTO star_constellation (star_id, constellation_id)\r\nVALUES\r\n('cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid, 'c32b81a9-83a2-40cf-97fc-bcbecc223ee7'::uuid),\r\n('f6a51829-9e2f-4c5f-a1c7-7115d8bda7fc'::uuid, 'c32b81a9-83a2-40cf-97fc-bcbecc223ee7'::uuid),\r\n('c54ba042-0c11-4b0e-9d9b-c5f5c5b5d86c'::uuid, 'c32b81a9-83a2-40cf-97fc-bcbecc223ee7'::uuid),\r\n('76a2a29a-af8c-4617-bb57-0d79b1319d04'::uuid, 'c32b81a9-83a2-40cf-97fc-bcbecc223ee7'::uuid),\r\n('af14baab-2e2b-4a12-a9f9-007aa0a0e64e'::uuid, 'd4a81c3b-d481-4e0d-a8d5-7c987b2e9f02'::uuid),\r\n('cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid, '84b94c48-f9af-43db-b2a2-b7d50be72de5'::uuid),\r\n('c54ba042-0c11-4b0e-9d9b-c5f5c5b5d86c'::uuid, '84b94c48-f9af-43db-b2a2-b7d50be72de5'::uuid);\r\n\r\nINSERT INTO planet (id, name, mass, diameter, distance_from_star, surface_temperature, star_id)\r\nVALUES\r\n('1d4cc4af-4c08-4283-aa18-74b48a2b19c3'::uuid, 'Mercury', 0.330, 4879, 0.39, 340, 'cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid),\r\n('68ef44cf-dc0e-44a4-9d84-d023768787a4'::uuid, 'Venus', 4.87, 12104, 0.72, 737, 'cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid),\r\n('d9e9e1d1-003c-49ec-a619-4f29a372aeb6'::uuid, 'Earth', 5.97, 12756, 1.00, 288, 'cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid),\r\n('7a8d5c5a-05c3-40b2-8d01-b6c853ed07aa'::uuid, 'Mars', 0.642, 6792, 1.52, -63, 'cc98dce8-afec-4d5c-b027-3a3a3a3a3a3a'::uuid),\r\n('05886f56-c95e-4063-a7e3-ae3dc1a7f3bb'::uuid, 'Proxima Centauri b', 0.003, 11385, 0.0485, -39, 'f6a51829-9e2f-4c5f-a1c7-7115d8bda7fc'::uuid),\r\n('2c5c1e29-4cf0-4f01-88a4-7284e9b3fbcd'::uuid, 'Alpha Centauri Bb', 1.133, 15000, 0.04, 1500, '76a2a29a-af8c-4617-bb57-0d79b1319d04'::uuid),\r\n('d82f43c3-3d90-4017-a1e3-0570a9d2f848'::uuid, 'Tartarus', 0.007, 2453, 0.23, 1273, 'c54ba042-0c11-4b0e-9d9b-c5f5c5b5d86c'::uuid);\r\n");
            this.Database.ExecuteSql($@"
            CREATE OR REPLACE FUNCTION audit_trigger_function()
                RETURNS TRIGGER AS $$
                DECLARE
                    has_id_column BOOLEAN;
                BEGIN
                    -- Check if 'id' column exists in the table
                    SELECT EXISTS (
                        SELECT 1
                        FROM information_schema.columns 
                        WHERE table_name = TG_TABLE_NAME 
                        AND column_name = 'id'
                    ) INTO has_id_column;

                    IF (TG_OP = 'DELETE') THEN
                        IF has_id_column THEN
                            INSERT INTO audit (table_name, row_id, old_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, OLD.id, row_to_json(OLD), TG_OP, now());
                        ELSE
                            INSERT INTO audit (table_name, old_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, row_to_json(OLD), TG_OP, now());
                        END IF;
                        RETURN OLD;
                    ELSIF (TG_OP = 'UPDATE') THEN
                        IF has_id_column THEN
                            INSERT INTO audit (table_name, row_id, old_value, new_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, NEW.id, row_to_json(OLD), row_to_json(NEW), TG_OP, now());
                        ELSE
                            INSERT INTO audit (table_name, old_value, new_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, row_to_json(OLD), row_to_json(NEW), TG_OP, now());
                        END IF;
                        RETURN NEW;
                    ELSIF (TG_OP = 'INSERT') THEN
                        IF has_id_column THEN
                            INSERT INTO audit (table_name, row_id, new_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, NEW.id, row_to_json(NEW), TG_OP, now());
                        ELSE
                            INSERT INTO audit (table_name, new_value, operation_type, timestamp)
                            VALUES (TG_TABLE_NAME, row_to_json(NEW), TG_OP, now());
                        END IF;
                        RETURN NEW;
                    END IF;
                    RETURN NULL; -- result is ignored since this is an AFTER trigger
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
                this.Database.ExecuteSqlRaw($@"CREATE TRIGGER {table}_audit_trigger AFTER INSERT OR UPDATE OR DELETE ON {table} FOR EACH ROW EXECUTE FUNCTION audit_trigger_function();");
            }
        }
    }

    public virtual DbSet<AlembicVersion> AlembicVersions { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<Constellation> Constellations { get; set; }

    public virtual DbSet<Galaxy> Galaxies { get; set; }

    public virtual DbSet<Planet> Planets { get; set; }

    public virtual DbSet<Star> Stars { get; set; }

    public virtual DbSet<Universe> Universes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=star;Username=test_user;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlembicVersion>(entity =>
        {
            entity.HasKey(e => e.VersionNum).HasName("alembic_version_pkc");

            entity.ToTable("alembic_version");

            entity.Property(e => e.VersionNum)
                .HasMaxLength(32)
                .HasColumnName("version_num");
        });

        modelBuilder.Entity<Audit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_pkey");

            entity.ToTable("audit");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NewValue).HasColumnName("new_value");
            entity.Property(e => e.OldValue).HasColumnName("old_value");
            entity.Property(e => e.OperationType)
                .HasMaxLength(10)
                .HasColumnName("operation_type");
            entity.Property(e => e.RowId).HasColumnName("row_id");
            entity.Property(e => e.TableName)
                .HasMaxLength(256)
                .HasColumnName("table_name");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<Constellation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("constellation_pkey");

            entity.ToTable("constellation");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Abbreviation)
                .HasMaxLength(50)
                .HasColumnName("abbreviation");
            entity.Property(e => e.GalaxyId).HasColumnName("galaxy_id");
            entity.Property(e => e.History).HasColumnName("history");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Shape)
                .HasMaxLength(50)
                .HasColumnName("shape");

            entity.HasOne(d => d.Galaxy).WithMany(p => p.Constellations)
                .HasForeignKey(d => d.GalaxyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("constellation_galaxy_id_fkey");
        });

        modelBuilder.Entity<Galaxy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("galaxy_pkey");

            entity.ToTable("galaxy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Composition).HasColumnName("composition");
            entity.Property(e => e.DistanceFromEarth).HasColumnName("distance_from_earth");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Shape)
                .HasMaxLength(50)
                .HasColumnName("shape");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.UniverseId).HasColumnName("universe_id");

            entity.HasOne(d => d.Universe).WithMany(p => p.Galaxies)
                .HasForeignKey(d => d.UniverseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("galaxy_universe_id_fkey");
        });

        modelBuilder.Entity<Planet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planet_pkey");

            entity.ToTable("planet");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Diameter).HasColumnName("diameter");
            entity.Property(e => e.DistanceFromStar).HasColumnName("distance_from_star");
            entity.Property(e => e.Mass).HasColumnName("mass");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.StarId).HasColumnName("star_id");
            entity.Property(e => e.SurfaceTemperature).HasColumnName("surface_temperature");

            entity.HasOne(d => d.Star).WithMany(p => p.Planets)
                .HasForeignKey(d => d.StarId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("planet_star_id_fkey");
        });

        modelBuilder.Entity<Star>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("star_pkey");

            entity.ToTable("star");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DistanceFromEarth).HasColumnName("distance_from_earth");
            entity.Property(e => e.GalaxyId).HasColumnName("galaxy_id");
            entity.Property(e => e.Luminosity).HasColumnName("luminosity");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SpectralType)
                .HasMaxLength(50)
                .HasColumnName("spectral_type");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Galaxy).WithMany(p => p.Stars)
                .HasForeignKey(d => d.GalaxyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("star_galaxy_id_fkey");

            entity.HasMany(d => d.Constellations).WithMany(p => p.Stars)
                .UsingEntity<Dictionary<string, object>>(
                    "StarConstellation",
                    r => r.HasOne<Constellation>().WithMany()
                        .HasForeignKey("ConstellationId")
                        .HasConstraintName("star_constellation_constellation_id_fkey"),
                    l => l.HasOne<Star>().WithMany()
                        .HasForeignKey("StarId")
                        .HasConstraintName("star_constellation_star_id_fkey"),
                    j =>
                    {
                        j.HasKey("StarId", "ConstellationId").HasName("star_constellation_pkey");
                        j.ToTable("star_constellation");
                        j.IndexerProperty<Guid>("StarId").HasColumnName("star_id");
                        j.IndexerProperty<Guid>("ConstellationId").HasColumnName("constellation_id");
                    });
        });

        modelBuilder.Entity<Universe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("universe_pkey");

            entity.ToTable("universe");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Composition).HasColumnName("composition");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Size).HasColumnName("size");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public bool IsDatabaseExists()
    {
        return Database.CanConnect();
    }
}
