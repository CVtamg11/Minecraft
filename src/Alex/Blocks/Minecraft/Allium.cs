using Alex.Blocks.Materials;

namespace Alex.Blocks.Minecraft
{
	public class Allium : FlowerBase
	{
		public Allium()
		{
			Solid = false;
			Transparent = true;

			BlockMaterial = Material.Plants;
		}
	}
}