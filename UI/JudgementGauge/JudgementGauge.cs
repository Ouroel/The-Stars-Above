﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.JudgementGauge
{
    internal class JudgementGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private UIImage bulletIndicator;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(120, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/JudgementGauge/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);


			/*
			bulletIndicator = new UIImage(Request<Texture2D>("StarsAbove/UI/Hawkmoon/"));
			bulletIndicator.Left.Set(62 , 0f);
			bulletIndicator.Top.Set(0, 0f);
			bulletIndicator.Width.Set(34, 0f);
			bulletIndicator.Height.Set(34, 0f);*/
			
			text = new UIText("", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(205, 205, 180); // 
			gradientB = new Color(245, 205, 77); // 
			finalColor = new Color(255, 197, 0);


			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility > 0))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.judgementGauge / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			Rectangle indicator = new Rectangle((1025), (580), (34), (34));
			indicator.X += 0;
			indicator.Width -= 0;
			indicator.Y += 0;
			indicator.Height -= 0;

			

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
				if (i >= 113)
				{
					spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left, hitbox.Y, 113, hitbox.Height), finalColor);

				}
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 8)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge8"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 7)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge7"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 6)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge6"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 5)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge5"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 4)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge4"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 3)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge3"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge2"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility == 1)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/JudgementGauge/JudgementGauge1"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
			}
			Recalculate();
		}
			
		
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().judgementGaugeVisibility>0))
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Setting the text per tick to update and show our resource values.
			text.SetText($"");

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
