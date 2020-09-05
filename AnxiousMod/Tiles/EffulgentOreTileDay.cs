using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnxiousMod.Tiles
{
    public class EffulgentOreTileDay : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true; // Is the tile solid
            Main.tileMergeDirt[Type] = true; // Will tile merge with dirt?
            Main.tileLighted[Type] = true; // ???
            Main.tileBlockLight[Type] = true; // Emits Light
            Main.tileSpelunker[Type] = true;
            Main.tileFrameImportant[Type] = true;

            drop = 0; // What item drops after destorying the tile
            // drop = 0;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Effulgent Ore");
            AddMapEntry(new Color(251, 182, 229), name); // Colour of Tile on Map
            minPick = 100; // What power pick minimum is needed to mine this block.
        }
        // private readonly int animationFrameHeight = 2;
        // private readonly int animationFrameWidth = 16;

		// public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) {
		// 	// Tweak the frame drawn by x position so tiles next to each other are off-sync and look much more interesting.
		// 	int uniqueAnimationFrameWidth = Main.tileFrame[Type] + i;
		// 	if (i % 2 == 0) {
		// 		uniqueAnimationFrameWidth += 3;
		// 	}
		// 	if (i % 3 == 0) {
		// 		uniqueAnimationFrameWidth += 3;
		// 	}
		// 	if (i % 4 == 0) {
		// 		uniqueAnimationFrameWidth += 3;
		// 	}

		// 	int uniqueAnimationFrameHeight = Main.tileFrame[Type] + j;
		// 	if (i % 2 == 0) {
		// 		uniqueAnimationFrameHeight += 3;
		// 	}
		// 	if (i % 3 == 0) {
		// 		uniqueAnimationFrameHeight += 3;
		// 	}
		// 	if (i % 4 == 0) {
		// 		uniqueAnimationFrameHeight += 3;
		// 	}
		// 	// uniqueAnimationFrame = uniqueAnimationFrame % 13;

        //     frameYOffset = uniqueAnimationFrameHeight * animationFrameHeight;
        //     frameXOffset = uniqueAnimationFrameWidth * animationFrameWidth;
		// }
        
        // public override void AnimateTile (ref int frame, ref int frameCounter) {
        //     if (Main.dayTime) {
        //         WorldGen.PlaceTile(this.type.position.X, this.position.Y, 0);
        //     }
        //     using (StreamWriter sw = File.CreateText("/home/martos/.local/share/Terraria/ModLoader/Mod Sources/AnxiousMod/Tiles/smth.txt"))
        //     {
        //         sw.WriteLine("haha lol " + frame);
        //     }
        // }

        // public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        // {
        //     r = 0.75f;
        //     g = 0.25f;
        //     b = 0.5f;
        // }
    }
}