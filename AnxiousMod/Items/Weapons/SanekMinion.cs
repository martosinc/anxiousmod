using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AnxiousMod.Items.Weapons
{
    public class SanekMinionBuff : ModBuff
    {

		public override void SetDefaults() {
			DisplayName.SetDefault("Sanek Minion");
			Description.SetDefault("The sanek will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex) {
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SanekMinion>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}

    }
    public class SanekMinionStaff : ModItem
    {
        public override void SetDefaults() {
			item.damage = 30;
			item.knockBack = 3f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 30, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item44;

            item.noMelee = true;
            item.summon = true;
            item.buffType = ModContent.BuffType<SanekMinionBuff>();
            item.shoot = ModContent.ProjectileType<SanekMinion>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            player.AddBuff(item.buffType, 2);
            position = Main.MouseWorld;
            return true;
        }

    }
    public class SanekMinion : ModProjectile
    {
        bool startRotate = false;
        float rotationAngle;
        float frameRotation;
        bool canDash;
        NPC npc;

        public override void SetStaticDefaults() {
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

        }

        public override void SetDefaults() {
            projectile.height = 102;
            projectile.width = 102;

            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
        }

        public override bool? CanCutTiles() {
            return false;
        }

        public override bool MinionContactDamage() {
            return true;
        }

        public override void AI() {
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active) {
                player.ClearBuff(ModContent.BuffType<SanekMinionBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<SanekMinionBuff>())) {
                projectile.timeLeft = 2;
            }     
            int frameSpeed = 5;
            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed) {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type]) {
                    projectile.frame = 0;
                }
            }

            Lighting.AddLight(projectile.Center, 0.9f, 0.1f, 0.3f);

            Vector2 idlePosition = player.Center;

			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX;             

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f) {
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}            

            float distanceFromTarget = 700f;
            Vector2 targetCenter = projectile.position;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC) {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, projectile.Center);
                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f) {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }            

            if (!foundTarget) {
                for (int i = 0; i < Main.maxNPCs; i++) {
                    npc = Main.npc[i];
                    if (npc.CanBeChasedBy()) {
                        float between = Vector2.Distance(npc.Center, projectile.Center);
                        bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
                        if (((closest && inRange) || !foundTarget) && (lineOfSight)) {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

			float speed = 8f;
			float inertia = 20f;

            if (foundTarget) {
                if (!startRotate) {
                    rotationAngle = 0f;
                    frameRotation = 0.01f;
                    startRotate = true;
                    canDash = false;
                }

                if (!canDash) {
                    projectile.rotation += frameRotation;
                    rotationAngle += frameRotation;
                    frameRotation += frameRotation/30;
                    projectile.velocity *= 0.9f;
                    if (rotationAngle > 6 * 10) { // 6 because projectile.rotation = 1 equals 60 degrees(so value 6 equals 360)
                        canDash = true;
                        frameRotation /= 4;
                    }
                }
                else {
                    if (distanceFromTarget > 40f) {
                        Vector2 direction = targetCenter - projectile.Center;
                        direction.Normalize();
                        direction *= speed;
                        projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
                        projectile.rotation += frameRotation;
                        // projectile.rotation = projectile.velocity.ToRotation() - 90;
                    }
                    else {
                        float dashSpeed = speed * 2;
                        float dashInertia = inertia * 2;                        
                        Vector2 direction = targetCenter - projectile.Center;
                        direction.Normalize();
                        direction *= dashSpeed;
                        projectile.velocity = (projectile.velocity * (dashInertia - 1) + direction) / dashInertia;
                        projectile.rotation += frameRotation;
                        if (npc.active) {
                            canDash = false;
                            startRotate = false;
                        }
                    }
                }

            }
			else {
                canDash = false;
                startRotate = false;
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f) {
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else {
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f) {

					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                    projectile.rotation -= projectile.rotation / 25;
				}
				else if (projectile.velocity == Vector2.Zero) {
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}
        }
    }       
}