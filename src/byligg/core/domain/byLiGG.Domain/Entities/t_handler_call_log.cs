using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_handler_call_log : IMutationEntity
{
	public string handler_name { get; set; }

	public int duration_ms { get; set; }

	public bool is_success { get; set; }

	public string error_type { get; set; }
}
