﻿using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class SpatialBurn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disintegration");
            Description.SetDefault("Touched by the vacuum of space");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLife--;
            player.electrified = true;
            player.onFire = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
           
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
