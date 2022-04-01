using Alex.Worlds;

namespace Alex.Entities.Hostile
{
	public class Giant : HostileMob
	{
		public Giant(World level) : base(level)
		{
			Height = 10.8;
			Width = 3.6;
		}
	}
}