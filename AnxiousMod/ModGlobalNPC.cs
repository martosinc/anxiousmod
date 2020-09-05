using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AnxiousMod
{
	public class ModGlobalNPC : GlobalNPC
	{
		public override void NPCLoot(NPC npc)
		{
			if (Main.rand.Next(20) == 0)
			{
				int[] bats = {49,51,60,93,137,150,151,158};
				if (bats.Contains(npc.type))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VampireFang"));
				}
			}
        }
    }
}