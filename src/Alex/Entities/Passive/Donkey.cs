using Alex.Worlds;
using MiNET.Entities;

namespace Alex.Entities.Passive
{
	public class Donkey : ChestedHorse
	{
		public Donkey(World level) : base((EntityType)24, level)
		{
			Height = 1.6;
			Width = 1.396484;
		}
	}
}
