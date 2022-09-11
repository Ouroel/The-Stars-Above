﻿using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace StarsAbove.Buffs
{
    public class DownForTheCount : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Down For The Count");
            Description.SetDefault("The foe before you spreads fear into the deepest recesses of your heart, immobilizing you");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.frozen = true;
            player.immune = true;
            player.immuneTime = 120;
            
        }

    }
}
