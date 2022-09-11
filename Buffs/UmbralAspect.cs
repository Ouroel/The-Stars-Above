﻿using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class UmbralAspect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Umbral Attunement");
            Description.SetDefault("The Umbral Aspect grants extra attack and speed and allows for astral destruction at the cost of constant Mana drain");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 2;
            player.moveSpeed += 2f;
        }
    }
}
