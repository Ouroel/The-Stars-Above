﻿
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class NaganadelProjectileFinal1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Naganadel");
			Main.projFrames[Projectile.type] = 10;
		}

		public override void SetDefaults() {
			Projectile.width = 68;
			Projectile.height = 68;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			Lighting.AddLight(Projectile.Center, new Vector3(0.2f, 0.4f, 0.99f));
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 15f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				/*if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor -= 2.4f;
				}*/
				
					
				float speed = 30f;
				Vector2 velocity = (projOwner.GetModPlayer<StarsAbovePlayer>().naganadelWeaponPosition - Projectile.Center).SafeNormalize(-Vector2.UnitY) * speed;
				Projectile.velocity = velocity;
			}
			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.frame = 10;
				}
			}
			// Change the spear position based off of the velocity and the movementFactor
			
			// When we reach the end of the animation, we can kill the spear projectile
			if (projOwner.itemAnimation == 0) {
				Projectile.Kill();
			}
			
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

			// These dusts are added later, for the 'ExampleMod' effect
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 204,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, new Color(0, 42, 255), Scale: 1.2f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(33, Main.LocalPlayer);

				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 204,
					0, 0, 254, new Color(0, 42, 255), Scale: 0.3f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(33, Main.LocalPlayer);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}

		}
	}
}
