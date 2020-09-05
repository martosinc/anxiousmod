using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using System.IO;
using Terraria.ModLoader;
using AnxiousMod.NPCs.Bosses;

namespace AnxiousMod.NPCs.Bosses
{
    public class AngryCloud : ModNPC {
        int phase = 1;
        int phaseSteps = 1;
        int shootTimer = 0;
        bool hasShoot = false;
        Vector2 rotationCenter;
        Vector2 direction;
        Vector2 attackDirection;
        float angle = 0;
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("TestEnemy");

            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults() {
            npc.boss = true;
            npc.width = 18;
            npc.height = 40;

            npc.lifeMax = 5000;
            npc.damage = 18;
            npc.defense = 10;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 10f;
            npc.knockBackResist = 0;

            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI() {
            npc.TargetClosest(true);
            Vector2 targetPosition = Main.player[npc.target].Center;
            Vector2 direction;
            float frameAngle = 0.4f;
            float radius = 25f;
            if (phase == 1) {
                if (phaseSteps == 1) {
                    rotationCenter = new Vector2(npc.position.X+radius, npc.position.Y);
                    phaseSteps = 2;
                    angle = -3;
                }
                if (phaseSteps == 2) {
                    npc.velocity = new Vector2(0f,0f);
                    npc.position.X = (float)(rotationCenter.X + Math.Cos(angle)*radius); // очень интересная формула. Так совпало, что синус как бы "привязан" к иксу, а косинус к игрику
                    npc.position.Y = (float)(rotationCenter.Y + Math.Sin(angle)*radius);
                    if (angle >= 6f) {
                        angle = (float)Math.PI;
                        phaseSteps = 3;
                    }
                    angle += frameAngle;
                    using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter("/home/martos/.local/share/Terraria/ModLoader/Mod Sources/AnxiousMod/NPCs/Bosses/smth.txt"))
                    {
                        // file.WriteLine(Texture);
                    }
                }
                if (phaseSteps == 3) {
                    if (Vector2.Distance(npc.position, targetPosition) > 225f) {
                        direction = targetPosition - npc.position;
                        direction.Normalize();
                        direction *= 16;
                        npc.velocity = direction;
                    }
                    else {
                        npc.velocity = new Vector2(0,0);
                        phaseSteps = 4;
                    }
                }

                if (phaseSteps == 4) {
                    if (!hasShoot) {
                        if (shootTimer == 10) {
                            attackDirection = targetPosition - npc.Center;
                            attackDirection.Normalize();
                            attackDirection *= 10f;
                            var type = ProjectileID.EyeLaser;
                            Projectile.NewProjectile(npc.position, attackDirection, type, 50, 0f);
                            shootTimer = 0;
                            hasShoot = true;
                        }
                    }
                    if (hasShoot) {
                        if (shootTimer == 20) {
                            phaseSteps = 1;
                            hasShoot = false;
                            shootTimer = 0;
                        }
                    }
                    shootTimer++;
                }
            }
            if (npc.life < npc.lifeMax / 2 && phase != 2) {
                phase = 2;
                phaseSteps = 1;
            }
            if (phase == 2) {
                if (phaseSteps == 1) {
                    if (Vector2.Distance(new Vector2(targetPosition.X, targetPosition.Y - 200f), npc.position) > 30f) {
                        direction = new Vector2(targetPosition.X, targetPosition.Y - 200f) - npc.position;
                        direction.Normalize();
                        direction *= 16;
                        npc.velocity = direction;
                    }
                    else {
                        phaseSteps = 2;
                    }
                }
            }
        }

    }
    public class AngryCloudClone : ModNPC {
        int phase = 1;
        int phaseSteps = 1;
        Vector2 rotationCenter;
        Vector2 direction;
        Vector2 attackDirection;
        float angle = 0;
        public override string Texture => (GetType().Namespace + "." + "AngryCloud").Replace('.', '/');
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("TestEnemy");

            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults() {
            npc.boss = true;
            npc.width = 18;
            npc.height = 40;

            npc.lifeMax = 5000;
            npc.damage = 18;
            npc.defense = 10;

            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 10f;
            npc.knockBackResist = 0;

            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void AI() {
            npc.alpha = 200;
        }
    }
}