using AnxiousMod.Items.Armor.Leggings;
using AnxiousMod.Items.Armor.Helmets;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace AnxiousMod.Items.Armor.Breastplates
{
	[AutoloadEquip(EquipType.Body)]
	public class SomethingBreastplate : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("i limke mdis breastplamte");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 1;
		}

		// public override bool IsArmorSet(Item head, Item body, Item legs) {
		// 	return body.type == ItemType<SomethingLeggings>() && legs.type == ItemType<SomethingHelmet>();
		// }
	}
}