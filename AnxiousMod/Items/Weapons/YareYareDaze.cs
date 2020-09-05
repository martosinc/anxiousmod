using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using System;
using Terraria.ModLoader;

namespace AnxiousMod.Items.Weapons
{
    public class YareYareDaze : ModItem {
        public static int ProjIter = 0;

        public static float finishRot;
        private float FindAngle(Vector2 point, Vector2 point2) { // я это сам написал, а не спздл
            return (float)Math.Atan2(point2.Y - point.Y,point2.X - point.X);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yare Yare Daze");
            Tooltip.SetDefault("Ora Ora Ora Ora");
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.knockBack = 6f;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 25;
            item.useTime = 5;
            item.width = 30;
            item.height = 30;
            item.melee = false;
            item.autoReuse = true;
            item.value = 5000;
            item.rare = 2;
            item.shootSpeed = 10f;
            item.shoot = ModContent.ProjectileType<YareYareDazeProjectile>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (ProjIter == 0) {
                var angle = FindAngle(position, Main.MouseWorld);
                var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 8;
                finishRot = angle;
                var projDirection = new Vector2((float)Math.Cos(angle-90), (float)Math.Sin(angle-90)) * 8;
                var projPos = position + projDirection*8;
                Projectile.NewProjectile(projPos.X+5, projPos.Y, direction.X, direction.Y, type, 50, 0f, player.whoAmI);
                // Projectile.NewProjectile(projPos, direction, type, 50, 0f, player.whoAmI);
            }
            if (ProjIter == 1) {
                var angle = FindAngle(position, Main.MouseWorld);
                var direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 8;
                finishRot = angle;
                var projDirection = new Vector2((float)Math.Cos(angle+90), (float)Math.Sin(angle+90)) * 8;
                var projPos = position + projDirection*8;
                Projectile.NewProjectile(projPos.X-10, projPos.Y, direction.X, direction.Y, type, 50, 0f, player.whoAmI);
                // Projectile.NewProjectile(projPos, direction, type, 50, 0f, player.whoAmI);
            }
            ProjIter++;
            if (ProjIter == 2) {
                ProjIter = 0;
            }
            return false;
        }
    }
    public class YareYareDazeProjectile : ModProjectile {
        float frameAngle = 0.15f;
        bool start = true;
        int dissTimer = 0;
        int side;
        Random rand = new Random();
        bool isReversed;

        public override void SetDefaults() {
            projectile.tileCollide = false;
            projectile.width = 26;
            projectile.height = 14;
            projectile.melee = true;
            projectile.friendly = true;
        }
        public override void AI() {
            if (start) {
                for (int i = 0; i < 1; i++) {
                    int randAngle = rand.Next(0, 360);
                    Vector2 direction = new Vector2((float)Math.Cos(randAngle), (float)Math.Sin(randAngle));
                    direction.Normalize();
                    direction *= 2;
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("YareYareDazeDust"), direction.X, direction.Y);
                }
                projectile.rotation = YareYareDaze.finishRot;
                start = false;
                side = YareYareDaze.ProjIter; 
            }
            var angleDir = new Vector2((float)Math.Cos(projectile.rotation), (float)Math.Sin(projectile.rotation));
            projectile.velocity = angleDir*8;
            if (side == 1) {
                projectile.rotation += 0.03f;
            }
            if (side == 0) {
                projectile.rotation -= 0.03f;
            }
            // frameAngle += frameAngle/50;
            dissTimer += 1;
            if (dissTimer == 25) {
                projectile.active = false;
            }
            if (dissTimer >= 15) {
                projectile.alpha += 35;
            }
            Lighting.AddLight(projectile.Center, 1f, 1f, 1f);
        }
    }
	public class YareYareDazeDust : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1.5f;
		}

		public override bool Update(Dust dust) {
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;
			float light = 0.35f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.3f) {
				dust.active = false;
            }
            return false;
        }
    }
}