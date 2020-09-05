using AnxiousMod.Items.Armor.Leggings;
using AnxiousMod.Items.Armor.Breastplates;
using System.Windows.Forms;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using System.IO;

namespace AnxiousMod.Items.Armor.Helmets
{
	[AutoloadEquip(EquipType.Head)]
	public class SomethingHelmet : ModItem
	{
        float HealDamage;
		bool ArmorSet;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Strommmmmmg gelmet.");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 1;
		}

		// public override bool IsArmorSet(Item head, Item body, Item legs) {
		// 	ArmorSet = body.type == ItemType<SomethingBreastplate>() && legs.type == ItemType<SomethingLeggings>();
		// 	return body.type == ItemType<SomethingBreastplate>() && legs.type == ItemType<SomethingLeggings>();
		// }

		public override void UpdateArmorSet(Player player) {
            // HealDamage = player.statLife / player.statLifeMax;
			HealDamage = (float)(1 - (double)player.statLife / player.statLifeMax);
			// HealDamage = 1 * (1 + (1 - 160 / 180));
            // using (StreamWriter sw = File.CreateText("/home/martos/.local/share/Terraria/ModLoader/Mod Sources/AnxiousMod/Items/Armor/Helmets/smth.txt"))
            // {
            //     sw.WriteLine(1 * (1 + (1 - 90.0 / 180)));
			// 	sw.WriteLine(player.statLifeMax);
			// 	sw.WriteLine(HealDamage);
			// 	sw.WriteLine(player.allDamage);
            // }
			// if (HealDamage >= 1.5f) {
			player.allDamage += HealDamage / 2;
			// }
			// player.allDamage *= 1.5f;
		}

		// public override void AddRecipes() {
		// 	ModRecipe recipe = new ModRecipe(mod);
		// 	recipe.AddIngredient(ItemType<EquipMaterial>(), 30);
		// 	recipe.AddTile(TileType<ExampleWorkbench>());
		// 	recipe.SetResult(this);
		// 	recipe.AddRecipe();
		// }
	}
}