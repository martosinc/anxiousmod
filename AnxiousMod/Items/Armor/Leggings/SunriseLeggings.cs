using AnxiousMod.Items.Armor.Breastplates;
using AnxiousMod.Items.Armor.Helmets;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace AnxiousMod.Items.Armor.Leggings
{
	[AutoloadEquip(EquipType.Legs)]
	public class SunriseLeggings : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Vemy svommmg leggims.");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 1;
		}

		// public override bool IsArmorSet(Item head, Item body, Item legs) {
		// 	return body.type == ItemType<SomethingBreastplate>() && legs.type == ItemType<SomethingHelmet>();
		// }
	}
}