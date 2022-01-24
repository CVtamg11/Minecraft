using Alex.Worlds;
using MiNET.Entities;

namespace Alex.Entities.Passive
{
	public class Squid : WaterMob
	{
		public Squid(World level) : base(level)
		{
			Height = 0.8;
			Width = 0.8;
		}
	}
}