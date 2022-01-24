using Alex.Worlds;
using MiNET.Entities;

namespace Alex.Entities.Hostile
{
	public class Husk : HostileMob
	{
		public Husk(World level) : base(level)
		{
			Height = 1.95;
			Width = 0.6;
		}
	}
}