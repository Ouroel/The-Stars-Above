using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;

namespace StarsAbove.Items.Placeable.CyberWorld
{
	public class VendingMachine : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Foreign Vending Machine");
			Tooltip.SetDefault("");
			
        }

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = Mod.Find<ModTile>("VendingMachine").Type;
		}

		public override void AddRecipes()
		{
			
		}
	}
}