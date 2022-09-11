using Microsoft.Xna.Framework;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Pets
{
	public class MumeiPetBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Guardian of Civilization");
			Description.SetDefault($"She forgor");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<StarsAbovePlayer>().MumeiPet = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.MumeiPet>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) {
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.MumeiPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
			
			
		}
	}
}