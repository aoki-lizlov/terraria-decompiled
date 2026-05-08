using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E4 RID: 996
	public class UIWorldCreationPreview : UIElement
	{
		// Token: 0x06002E4C RID: 11852 RVA: 0x005A9DE4 File Offset: 0x005A7FE4
		public UIWorldCreationPreview()
		{
			this._BorderTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewBorder", 1);
			this._BackgroundNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal1", 1);
			this._BackgroundExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert1", 1);
			this._BackgroundMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster1", 1);
			this._BunnyNormalTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyNormal2", 1);
			this._BunnyExpertTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyExpert2", 1);
			this._BunnyCreativeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyCreative2", 1);
			this._BunnyMasterTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewDifficultyMaster2", 1);
			this._EvilRandomTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilRandom", 1);
			this._EvilCorruptionTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCorruption", 1);
			this._EvilCrimsonTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewEvilCrimson", 1);
			this._SizeSmallTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeSmall", 1);
			this._SizeMediumTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeMedium", 1);
			this._SizeLargeTexture = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/PreviewSizeLarge", 1);
			this.Width.Set((float)this._BackgroundExpertTexture.Width(), 0f);
			this.Height.Set((float)this._BackgroundExpertTexture.Height(), 0f);
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x005A9F63 File Offset: 0x005A8163
		public void UpdateOption(byte difficulty, byte evil, byte size)
		{
			this._difficulty = difficulty;
			this._evil = evil;
			this._size = size;
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x005A9F7C File Offset: 0x005A817C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector = new Vector2(dimensions.X + 4f, dimensions.Y + 4f);
			Color color = Color.White;
			switch (this._difficulty)
			{
			case 0:
			case 3:
				spriteBatch.Draw(this._BackgroundNormalTexture.Value, vector, Color.White);
				color = Color.White;
				break;
			case 1:
				spriteBatch.Draw(this._BackgroundExpertTexture.Value, vector, Color.White);
				color = Color.DarkGray;
				break;
			case 2:
				spriteBatch.Draw(this._BackgroundMasterTexture.Value, vector, Color.White);
				color = Color.DarkGray;
				break;
			}
			switch (this._size)
			{
			case 0:
				spriteBatch.Draw(this._SizeSmallTexture.Value, vector, color);
				break;
			case 1:
				spriteBatch.Draw(this._SizeMediumTexture.Value, vector, color);
				break;
			case 2:
				spriteBatch.Draw(this._SizeLargeTexture.Value, vector, color);
				break;
			}
			switch (this._evil)
			{
			case 0:
				spriteBatch.Draw(this._EvilRandomTexture.Value, vector, color);
				break;
			case 1:
				spriteBatch.Draw(this._EvilCorruptionTexture.Value, vector, color);
				break;
			case 2:
				spriteBatch.Draw(this._EvilCrimsonTexture.Value, vector, color);
				break;
			}
			switch (this._difficulty)
			{
			case 0:
				spriteBatch.Draw(this._BunnyNormalTexture.Value, vector, color);
				break;
			case 1:
				spriteBatch.Draw(this._BunnyExpertTexture.Value, vector, color);
				break;
			case 2:
				spriteBatch.Draw(this._BunnyMasterTexture.Value, vector, color * 1.2f);
				break;
			case 3:
				spriteBatch.Draw(this._BunnyCreativeTexture.Value, vector, color);
				break;
			}
			spriteBatch.Draw(this._BorderTexture.Value, new Vector2(dimensions.X, dimensions.Y), Color.White);
		}

		// Token: 0x0400554F RID: 21839
		private readonly Asset<Texture2D> _BorderTexture;

		// Token: 0x04005550 RID: 21840
		private readonly Asset<Texture2D> _BackgroundExpertTexture;

		// Token: 0x04005551 RID: 21841
		private readonly Asset<Texture2D> _BackgroundNormalTexture;

		// Token: 0x04005552 RID: 21842
		private readonly Asset<Texture2D> _BackgroundMasterTexture;

		// Token: 0x04005553 RID: 21843
		private readonly Asset<Texture2D> _BunnyExpertTexture;

		// Token: 0x04005554 RID: 21844
		private readonly Asset<Texture2D> _BunnyNormalTexture;

		// Token: 0x04005555 RID: 21845
		private readonly Asset<Texture2D> _BunnyCreativeTexture;

		// Token: 0x04005556 RID: 21846
		private readonly Asset<Texture2D> _BunnyMasterTexture;

		// Token: 0x04005557 RID: 21847
		private readonly Asset<Texture2D> _EvilRandomTexture;

		// Token: 0x04005558 RID: 21848
		private readonly Asset<Texture2D> _EvilCorruptionTexture;

		// Token: 0x04005559 RID: 21849
		private readonly Asset<Texture2D> _EvilCrimsonTexture;

		// Token: 0x0400555A RID: 21850
		private readonly Asset<Texture2D> _SizeSmallTexture;

		// Token: 0x0400555B RID: 21851
		private readonly Asset<Texture2D> _SizeMediumTexture;

		// Token: 0x0400555C RID: 21852
		private readonly Asset<Texture2D> _SizeLargeTexture;

		// Token: 0x0400555D RID: 21853
		private byte _difficulty;

		// Token: 0x0400555E RID: 21854
		private byte _evil;

		// Token: 0x0400555F RID: 21855
		private byte _size;
	}
}
