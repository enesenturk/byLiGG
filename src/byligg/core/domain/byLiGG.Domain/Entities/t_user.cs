using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_user : IMutationEntity
{
	public string username { get; set; }

	public string email { get; set; }

	public string password_hash { get; set; }

	public string display_name { get; set; }

	public string avatar_url { get; set; }

	public Guid? t_team_id { get; set; }

	public string language_preference { get; set; }

	public bool is_active { get; set; }

	public bool is_verified { get; set; }

	public DateTime? last_login_at { get; set; }

	public virtual ICollection<t_leaderboard_snapshot> t_leaderboard_snapshots { get; set; } = new List<t_leaderboard_snapshot>();

	public virtual ICollection<t_login_log> t_login_logs { get; set; } = new List<t_login_log>();

	public virtual ICollection<t_prediction> t_predictions { get; set; } = new List<t_prediction>();

	public virtual ICollection<t_private_league_member> t_private_league_members { get; set; } = new List<t_private_league_member>();

	public virtual ICollection<t_private_league> t_private_leagues { get; set; } = new List<t_private_league>();

	public virtual t_team t_team { get; set; }

	public virtual ICollection<t_user_badge> t_user_badges { get; set; } = new List<t_user_badge>();

	public virtual t_user_stat t_user_stat { get; set; }
}
