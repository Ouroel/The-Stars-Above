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
    public class SolemnAegis : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solemn Aegis");
            Description.SetDefault("The next instance of damage is ignored");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
           // player.immune = true;
            //player.immuneTime = 20;
            
        }

    }
}
