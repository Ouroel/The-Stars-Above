using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.NeoDealmaker
{
	public class BigShot : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Neo Dealmaker");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 1;

		}

		public override void SetDefaults() {
			Projectile.width = 14;               //The width of projectile hitbox
			Projectile.height = 24;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 8;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Ranged;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
        public override void AI()
        {

			


			base.AI();

        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(!target.active)
            {
				int k = Item.NewItem(target.GetSource_DropAsItem(),(int)target.position.X, (int)target.position.Y, target.width, target.height, ItemID.SilverCoin, 5, false);
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, null, k, 1f);
				}
			}

			base.OnHitNPC(target, damage, knockback, crit);
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (target.HasBuff(BuffID.Midas))
            {
				if(crit)
                {
					damage = (int)(damage * 1.25);
				}
				else
                {
					damage = (int)(damage * 1.15);
				}
				
            }


            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
		

		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			
			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}
