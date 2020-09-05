using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AnxiousMod.Items.WarlockClass
{
	// This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
	public class WarlockPlayer : ModPlayer
	{
		public static WarlockPlayer ModPlayer(Player player) {
			return player.GetModPlayer<WarlockPlayer>();
		}

		// Vanilla only really has damage multipliers in code
		// And crit and knockback is usually just added to
		// As a modder, you could make separate variables for multipliers and simple addition bonuses
		public float warlockDamageAdd;
		public float warlockDamageMult = 1f;
		public float warlockKnockback;
		public int warlockCrit;


		public override void ResetEffects() {
			ResetVariables();
		}

		public override void UpdateDead() {
			ResetVariables();
		}

		private void ResetVariables() {
            warlockDamageAdd = 0f;
			warlockDamageMult = 1f;
			warlockKnockback = 0f;
			warlockCrit = 0;
		}
	}
}