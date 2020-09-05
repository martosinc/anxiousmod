using Terraria.ModLoader;
using Terraria.ID;

namespace AnxiousMod.Items.Materials
{
    public class SolarSpark : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Effulgent Ore");
            Tooltip.SetDefault("-From sun.");
        }

        public override void SetDefaults()
        {
            item.width = 15; // Hitbox Width
            item.height = 15; // Hitbox Height
            item.value = 50; // 10 | 00 | 00 | 00 : Platinum | Gold | Silver | Bronze
            item.rare = 2; // Item Tier
            item.maxStack = 999; // The maximum number you can have of this item.
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.GetInstance<Items.Placeable.EffulgentOre>(), 1);
            recipe.AddIngredient(75, 1);            
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}