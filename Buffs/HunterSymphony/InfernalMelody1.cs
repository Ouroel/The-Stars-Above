﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.HunterSymphony
{
    public class InfernalMelody1 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Melody (1/3)");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
