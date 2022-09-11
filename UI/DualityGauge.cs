﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
	internal class DualityGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(84, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(40, 0f);

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(188, 255, 249); // 
			gradientB = new Color(188, 255, 249); //
			finalColor = new Color(0, 224, 255);

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is StygianNymph))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.duality / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 10;
			hitbox.Height -= 24;
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/DualityGaugeEmpty"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1) {
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height ), Color.Lerp(gradientA, gradientB, percent));
				
			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/DualityGauge"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			if(modPlayer.duality >= 50)
            {
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/DualityBlue"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			else
            {
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/DualityRed"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is StygianNymph))
				return;


			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
