﻿using System;
using System.Collections.Generic;
using System.Text;
using Alex.API.Utils;
using Alex.Utils;
using Alex.Worlds;
using Microsoft.Xna.Framework;

namespace Alex.Entities
{
	public class PlayerMob : Mob
	{
		public UUID Uuid { get; private set; }
		public Skin Skin { get; set; }

		public short Boots { get; set; }
		public short Leggings { get; set; }
		public short Chest { get; set; }
		public short Helmet { get; set; }

		//public Item ItemInHand { get; set; }

		public PlayerMob(string name, World level) : base(63, level)
		{
			Uuid = new UUID(Guid.NewGuid().ToByteArray());

			Width = 0.6;
			Length = 0.6;
			Height = 1.80;

			IsSpawned = false;

			NameTag = name;
			Skin = new Skin { Slim = false, SkinData = Encoding.Default.GetBytes(new string('Z', 8192)) };

		//	ItemInHand = new ItemAir();

			HideNameTag = false;
			IsAlwaysShowName = true;

			IsInWater = true;
			NoAi = true;
			//HealthManager.IsOnFire = false;
			Velocity = Vector3.Zero;
			PositionOffset = 1.62f;
		}
	}
}