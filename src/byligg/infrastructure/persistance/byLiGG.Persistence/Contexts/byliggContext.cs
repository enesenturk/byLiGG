using byLiGG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace byLiGG.Persistence.Contexts
{
	public partial class byliggContext : DbContext
	{
		public static string connectionString { get; set; }

		public byliggContext()
		{
		}

		public byliggContext(DbContextOptions<byliggContext> options)
			: base(options)
		{
		}

		public virtual DbSet<t_request_telemetry> t_request_telemetries { get; set; }

		public virtual DbSet<t_badge> t_badges { get; set; }

		public virtual DbSet<t_competition> t_competitions { get; set; }

		public virtual DbSet<t_country> t_countries { get; set; }

		public virtual DbSet<t_derby> t_derbies { get; set; }

		public virtual DbSet<t_leaderboard_snapshot> t_leaderboard_snapshots { get; set; }

		public virtual DbSet<t_login_log> t_login_logs { get; set; }

		public virtual DbSet<t_match> t_matches { get; set; }

		public virtual DbSet<t_prediction> t_predictions { get; set; }

		public virtual DbSet<t_private_league> t_private_leagues { get; set; }

		public virtual DbSet<t_private_league_member> t_private_league_members { get; set; }

		public virtual DbSet<t_scoring_rule> t_scoring_rules { get; set; }

		public virtual DbSet<t_system_property> t_system_properties { get; set; }

		public virtual DbSet<t_system_property_type> t_system_property_types { get; set; }

		public virtual DbSet<t_team> t_teams { get; set; }

		public virtual DbSet<t_user> t_users { get; set; }

		public virtual DbSet<t_user_badge> t_user_badges { get; set; }

		public virtual DbSet<t_user_stat> t_user_stats { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.HasPostgresExtension("citext")
				.HasPostgresExtension("uuid-ossp");

			modelBuilder.Entity<t_request_telemetry>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_request_telemetry_pkey");

				entity.ToTable("t_request_telemetry");

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

				entity.Property(e => e.handler_name)
					.IsRequired()
					.HasMaxLength(200);

				entity.HasOne<t_user>()
					.WithMany()
					.HasForeignKey(e => e.t_user_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_request_telemetry_t_user_id_fkey");
			});

			modelBuilder.Entity<t_badge>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_badge_pkey");

				entity.ToTable("t_badge");

				entity.HasIndex(e => e.language_key, "t_badge_language_key_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.description_key).HasMaxLength(100);
				entity.Property(e => e.language_key)
					.IsRequired()
					.HasMaxLength(100);

				entity.HasOne(d => d.t_system_property_badge_trigger).WithMany(p => p.t_badges)
					.HasForeignKey(d => d.t_system_property_badge_trigger_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_badge_t_system_property_badge_trigger_id_fkey");
			});

			modelBuilder.Entity<t_competition>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_competition_pkey");

				entity.ToTable("t_competition");

				entity.HasIndex(e => e.feed_reference_id, "t_competition_feed_reference_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.code)
					.IsRequired()
					.HasMaxLength(20);
				entity.Property(e => e.current_season).HasMaxLength(20);
				entity.Property(e => e.name)
					.IsRequired()
					.HasMaxLength(100);

				entity.HasOne(d => d.t_country).WithMany(p => p.t_competitions)
					.HasForeignKey(d => d.t_country_id)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("t_competition_t_country_id_fkey");
			});

			modelBuilder.Entity<t_country>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_country_pkey");

				entity.ToTable("t_country");

				entity.HasIndex(e => e.code, "t_country_code_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.code)
					.IsRequired()
					.HasMaxLength(3);
			});

			modelBuilder.Entity<t_derby>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_derby_pkey");

				entity.ToTable("t_derby");

				entity.HasIndex(e => new { e.t_team_a_id, e.t_team_b_id }, "t_derby_t_team_a_id_t_team_b_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.default_multiplier).HasPrecision(3, 1);

				entity.HasOne(d => d.t_team_a).WithMany(p => p.t_derbyt_team_as)
					.HasForeignKey(d => d.t_team_a_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_derby_t_team_a_id_fkey");

				entity.HasOne(d => d.t_team_b).WithMany(p => p.t_derbyt_team_bs)
					.HasForeignKey(d => d.t_team_b_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_derby_t_team_b_id_fkey");
			});

			modelBuilder.Entity<t_leaderboard_snapshot>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_leaderboard_snapshot_pkey");

				entity.ToTable("t_leaderboard_snapshot");

				entity.HasIndex(e => new { e.t_user_id, e.t_competition_id, e.matchday }, "t_leaderboard_snapshot_t_user_id_t_competition_id_matchday_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

				entity.HasOne(d => d.t_competition).WithMany(p => p.t_leaderboard_snapshots)
					.HasForeignKey(d => d.t_competition_id)
					.HasConstraintName("t_leaderboard_snapshot_t_competition_id_fkey");

				entity.HasOne(d => d.t_user).WithMany(p => p.t_leaderboard_snapshots)
					.HasForeignKey(d => d.t_user_id)
					.HasConstraintName("t_leaderboard_snapshot_t_user_id_fkey");
			});

			modelBuilder.Entity<t_login_log>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_login_log_pkey");

				entity.ToTable("t_login_log");

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.ip_address)
					.HasMaxLength(45);

				entity.HasOne(d => d.t_user).WithMany(p => p.t_login_logs)
					.HasForeignKey(d => d.t_user_id)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("t_login_log_t_user_id_fkey");
			});

			modelBuilder.Entity<t_match>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_match_pkey");

				entity.ToTable("t_match");

				entity.HasIndex(e => e.feed_reference_id, "t_match_feed_reference_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.multiplier).HasPrecision(3, 1);

				entity.HasOne(d => d.t_away_team).WithMany(p => p.t_matcht_away_teams)
					.HasForeignKey(d => d.t_away_team_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_match_t_away_team_id_fkey");

				entity.HasOne(d => d.t_competition).WithMany(p => p.t_matches)
					.HasForeignKey(d => d.t_competition_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_match_t_competition_id_fkey");

				entity.HasOne(d => d.t_home_team).WithMany(p => p.t_matcht_home_teams)
					.HasForeignKey(d => d.t_home_team_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_match_t_home_team_id_fkey");

				entity.HasOne(d => d.t_system_property_match_status).WithMany(p => p.t_matches)
					.HasForeignKey(d => d.t_system_property_match_status_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_match_t_system_property_match_status_id_fkey");
			});

			modelBuilder.Entity<t_prediction>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_prediction_pkey");

				entity.ToTable("t_prediction");

				entity.HasIndex(e => new { e.t_user_id, e.t_match_id }, "t_prediction_t_user_id_t_match_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.multiplier).HasPrecision(3, 1);

				entity.HasOne(d => d.t_match).WithMany(p => p.t_predictions)
					.HasForeignKey(d => d.t_match_id)
					.HasConstraintName("t_prediction_t_match_id_fkey");

				entity.HasOne(d => d.t_system_property_prediction_result).WithMany(p => p.t_predictions)
					.HasForeignKey(d => d.t_system_property_prediction_result_id)
					.HasConstraintName("t_prediction_t_system_property_prediction_result_id_fkey");

				entity.HasOne(d => d.t_user).WithMany(p => p.t_predictions)
					.HasForeignKey(d => d.t_user_id)
					.HasConstraintName("t_prediction_t_user_id_fkey");
			});

			modelBuilder.Entity<t_private_league>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_private_league_pkey");

				entity.ToTable("t_private_league");

				entity.HasIndex(e => e.invite_code, "t_private_league_invite_code_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.invite_code)
					.IsRequired()
					.HasMaxLength(12);
				entity.Property(e => e.name)
					.IsRequired()
					.HasMaxLength(80);

				entity.HasOne(d => d.t_competition).WithMany(p => p.t_private_leagues)
					.HasForeignKey(d => d.t_competition_id)
					.HasConstraintName("t_private_league_t_competition_id_fkey");

				entity.HasOne(d => d.t_user).WithMany(p => p.t_private_leagues)
					.HasForeignKey(d => d.t_user_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_private_league_t_user_id_fkey");
			});

			modelBuilder.Entity<t_private_league_member>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_private_league_member_pkey");

				entity.ToTable("t_private_league_member");

				entity.HasIndex(e => new { e.t_private_league_id, e.t_user_id }, "t_private_league_member_t_private_league_id_t_user_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

				entity.HasOne(d => d.t_private_league).WithMany(p => p.t_private_league_members)
					.HasForeignKey(d => d.t_private_league_id)
					.HasConstraintName("t_private_league_member_t_private_league_id_fkey");

				entity.HasOne(d => d.t_system_property_private_league_role).WithMany(p => p.t_private_league_members)
					.HasForeignKey(d => d.t_system_property_private_league_role_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_private_league_member_t_system_property_private_league_r_fkey");

				entity.HasOne(d => d.t_user).WithMany(p => p.t_private_league_members)
					.HasForeignKey(d => d.t_user_id)
					.HasConstraintName("t_private_league_member_t_user_id_fkey");
			});

			modelBuilder.Entity<t_scoring_rule>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_scoring_rule_pkey");

				entity.ToTable("t_scoring_rule");

				entity.HasIndex(e => e.t_system_property_prediction_result_id, "t_scoring_rule_t_system_property_prediction_result_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.description_language_key).HasMaxLength(100);

				entity.HasOne(d => d.t_system_property_prediction_result).WithOne(p => p.t_scoring_rule)
					.HasForeignKey<t_scoring_rule>(d => d.t_system_property_prediction_result_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_scoring_rule_t_system_property_prediction_result_id_fkey");
			});

			modelBuilder.Entity<t_system_property>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_system_property_pkey");

				entity.ToTable("t_system_property");

				entity.HasIndex(e => new { e.t_system_property_type_id, e.language_key }, "t_system_property_t_system_property_type_id_language_key_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.language_key)
					.IsRequired()
					.HasMaxLength(100);

				entity.HasOne(d => d.t_system_property_type).WithMany(p => p.t_system_properties)
					.HasForeignKey(d => d.t_system_property_type_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_system_property_t_system_property_type_id_fkey");
			});

			modelBuilder.Entity<t_system_property_type>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_system_property_type_pkey");

				entity.ToTable("t_system_property_type");

				entity.HasIndex(e => e.language_key, "t_system_property_type_language_key_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.language_key)
					.IsRequired()
					.HasMaxLength(100);
			});

			modelBuilder.Entity<t_team>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_team_pkey");

				entity.ToTable("t_team");

				entity.HasIndex(e => e.feed_reference_id, "t_team_feed_reference_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.name)
					.IsRequired()
					.HasMaxLength(100);
				entity.Property(e => e.primary_color).HasMaxLength(7);
				entity.Property(e => e.secondary_color).HasMaxLength(7);
				entity.Property(e => e.short_name).HasMaxLength(40);
				entity.Property(e => e.tla).HasMaxLength(5);

				entity.HasOne(d => d.t_country).WithMany(p => p.t_teams)
					.HasForeignKey(d => d.t_country_id)
					.OnDelete(DeleteBehavior.SetNull)
					.HasConstraintName("t_team_t_country_id_fkey");
			});

			modelBuilder.Entity<t_user>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_user_pkey");

				entity.ToTable("t_user");

				entity.HasIndex(e => e.username, "idx_t_user_username").HasFilter("(is_deleted = false)");

				entity.HasIndex(e => e.email, "t_user_email_key").IsUnique();

				entity.HasIndex(e => e.username, "t_user_username_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");
				entity.Property(e => e.display_name).HasMaxLength(60);
				entity.Property(e => e.email)
					.IsRequired()
					.HasColumnType("citext");
				entity.Property(e => e.language_preference)
					.IsRequired()
					.HasMaxLength(10);
				entity.Property(e => e.username)
					.IsRequired()
					.HasColumnType("citext");

				entity.HasOne(d => d.t_team).WithMany(p => p.t_users)
					.HasForeignKey(d => d.t_team_id)
					.HasConstraintName("fk_t_user_t_team");
			});

			modelBuilder.Entity<t_user_badge>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_user_badge_pkey");

				entity.ToTable("t_user_badge");

				entity.HasIndex(e => new { e.t_user_id, e.t_badge_id }, "t_user_badge_t_user_id_t_badge_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

				entity.HasOne(d => d.t_badge).WithMany(p => p.t_user_badges)
					.HasForeignKey(d => d.t_badge_id)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("t_user_badge_t_badge_id_fkey");

				entity.HasOne(d => d.t_user).WithMany(p => p.t_user_badges)
					.HasForeignKey(d => d.t_user_id)
					.HasConstraintName("t_user_badge_t_user_id_fkey");
			});

			modelBuilder.Entity<t_user_stat>(entity =>
			{
				entity.HasKey(e => e.id).HasName("t_user_stat_pkey");

				entity.ToTable("t_user_stat");

				entity.HasIndex(e => e.t_user_id, "t_user_stat_t_user_id_key").IsUnique();

				entity.Property(e => e.id).HasDefaultValueSql("uuid_generate_v4()");

				entity.HasOne(d => d.t_user).WithOne(p => p.t_user_stat)
					.HasForeignKey<t_user_stat>(d => d.t_user_id)
					.HasConstraintName("t_user_stat_t_user_id_fkey");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}

}
