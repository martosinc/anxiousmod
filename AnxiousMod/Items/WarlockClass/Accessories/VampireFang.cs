using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AnxiousMod.Items.WarlockClass.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class VampireFang : ModItem {
        public override void SetStaticDefaults() {
			Tooltip.SetDefault("+15% warlock damage.");
		}
		public override void SetDefaults() {
			item.width = 24;
			item.height = 28;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual) {
			WarlockPlayer modPlayer = WarlockPlayer.ModPlayer(player);
			modPlayer.warlockDamageAdd += 0.15f;
		}
    }
}