﻿using Alex.Blocks.Minecraft;
using Alex.ResourcePackLib.Json.Bedrock.Entity;
using Alex.Utils;
using Alex.Worlds;
using Microsoft.Xna.Framework;

namespace Alex.Entities.BlockEntities
{
	public class EnchantTableBlockEntity : BlockEntity
	{
		public EnchantTableBlockEntity(World level) : base(level)
		{
			Type = "minecraft:enchanttable";

			Width = 1f;
			Height = 1f;

			Offset = new Vector3(0.5f, 0f, 0.5f);

			HideNameTag = true;
			IsAlwaysShowName = false;
			AnimationController.Enabled = true;
		}
	}
}