using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnxiousMod.Tiles
{
    public class EffulgentOreTile : ModTile
    {
        Texture2D DayTexture;
        int start = 0;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true; // Is the tile solid
            Main.tileMergeDirt[Type] = true; // Will tile merge with dirt?
            Main.tileLighted[Type] = true; // ???
            Main.tileBlockLight[Type] = true; // Emits Light
            Main.tileSpelunker[Type] = true;

            drop = mod.ItemType("EffulgentOre"); // What item drops after destorying the tile
            // drop = 0;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Effulgent Ore");
            AddMapEntry(new Color(251, 182, 229), name); // Colour of Tile on Map
            minPick = 100; // What power pick minimum is needed to mine this block.
        }
        
        public override void AnimateTile (ref int frame, ref int frameCounter) {
            if (!Main.dayTime) {
                Main.tileTexture[Type] = Main.tileTexture[mod.TileType("EffulgentOreTileExtinct")];
                drop = 0;
                Main.tileSpelunker[Type] = false;
            }
            else {
                Main.tileTexture[Type] = Main.tileTexture[mod.TileType("EffulgentOreTileDay")];
                drop = mod.ItemType("EffulgentOre");
                Main.tileSpelunker[Type] = true;
            }
        }

        // public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        // {
        //     r = 0.75f;
        //     g = 0.25f;
        //     b = 0.5f;
        // }
    }
}