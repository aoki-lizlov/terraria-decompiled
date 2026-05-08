using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000372 RID: 882
	public class WorkshopPublishingIndicator
	{
		// Token: 0x06002947 RID: 10567 RVA: 0x0057A340 File Offset: 0x00578540
		public void Hide()
		{
			this._displayUpPercent = 0f;
			this._frameCounter = 0;
			this._timesSoundWasPlayed = 0;
			this._shouldPlayEndingSound = false;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0057A364 File Offset: 0x00578564
		public void Draw(SpriteBatch spriteBatch)
		{
			WorkshopSocialModule workshop = SocialAPI.Workshop;
			if (workshop == null)
			{
				return;
			}
			AWorkshopProgressReporter progressReporter = workshop.ProgressReporter;
			bool hasOngoingTasks = progressReporter.HasOngoingTasks;
			bool flag = this._displayUpPercent == 1f;
			this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float)hasOngoingTasks.ToDirectionInt() / 60f, 0f, 1f);
			bool flag2 = this._displayUpPercent == 1f;
			if (flag && !flag2)
			{
				this._shouldPlayEndingSound = true;
			}
			if (this._displayUpPercent == 0f)
			{
				return;
			}
			if (this._indicatorTexture == null)
			{
				this._indicatorTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/InProgress", 1);
			}
			Texture2D value = this._indicatorTexture.Value;
			int num = 6;
			this._frameCounter++;
			int num2 = 5;
			int num3 = this._frameCounter / num2 % num;
			Vector2 vector = Main.ScreenSize.ToVector2() + new Vector2(-40f, 40f);
			Vector2 vector2 = vector + new Vector2(0f, -80f);
			Vector2 vector3 = Vector2.Lerp(vector, vector2, this._displayUpPercent);
			Rectangle rectangle = value.Frame(1, 6, 0, num3, 0, 0);
			Vector2 vector4 = rectangle.Size() / 2f;
			spriteBatch.Draw(value, vector3, new Rectangle?(rectangle), Color.White, 0f, vector4, 1f, SpriteEffects.None, 0f);
			float num4;
			if (progressReporter.TryGetProgress(out num4) && !float.IsNaN(num4))
			{
				string text = num4.ToString("P");
				DynamicSpriteFont value2 = FontAssets.ItemStack.Value;
				int num5 = 1;
				Vector2 vector5 = value2.MeasureString(text) * (float)num5 * new Vector2(0.5f, 1f);
				Utils.DrawBorderStringFourWay(spriteBatch, value2, text, vector3.X, vector3.Y - 10f, Color.White, Color.Black, vector5, (float)num5);
			}
			if (num3 == 3 && this._frameCounter % num2 == 0)
			{
				if (this._shouldPlayEndingSound)
				{
					this._shouldPlayEndingSound = false;
					this._timesSoundWasPlayed = 0;
					SoundEngine.PlaySound(64, -1, -1, 1, 1f, 0f);
				}
				if (hasOngoingTasks)
				{
					float num6 = Utils.Remap((float)this._timesSoundWasPlayed, 0f, 10f, 1f, 0f, true);
					SoundEngine.PlaySound(21, -1, -1, 1, num6, 0f);
					this._timesSoundWasPlayed++;
				}
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0000357B File Offset: 0x0000177B
		public WorkshopPublishingIndicator()
		{
		}

		// Token: 0x040051E1 RID: 20961
		private float _displayUpPercent;

		// Token: 0x040051E2 RID: 20962
		private int _frameCounter;

		// Token: 0x040051E3 RID: 20963
		private bool _shouldPlayEndingSound;

		// Token: 0x040051E4 RID: 20964
		private Asset<Texture2D> _indicatorTexture;

		// Token: 0x040051E5 RID: 20965
		private int _timesSoundWasPlayed;
	}
}
