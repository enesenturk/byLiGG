using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_request_telemetry : IMutationEntity
{
	public Guid? t_user_id { get; set; }

	public string handler_name { get; set; }

	public int duration_ms { get; set; }

	public bool is_success { get; set; }
}
