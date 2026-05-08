using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200036E RID: 878
	public class IssueReportsIndicator
	{
		// Token: 0x06002939 RID: 10553 RVA: 0x005793CA File Offset: 0x005775CA
		public void AttemptLettingPlayerKnow()
		{
			this.Setup();
			this._shouldBeShowing = true;
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, -1, -1, 0f, 1f);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x005793F0 File Offset: 0x005775F0
		public void Hide()
		{
			this._shouldBeShowing = false;
			this._displayUpPercent = 0f;
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x00579404 File Offset: 0x00577604
		private void OpenUI()
		{
			this.Setup();
			Main.OpenReportsMenu();
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00579411 File Offset: 0x00577611
		private void Setup()
		{
			this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton", 1);
			this._buttonOutlineTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton_Outline", 1);
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x00579440 File Offset: 0x00577640
		public void Draw(SpriteBatch spriteBatch)
		{
			bool shouldBeShowing = this._shouldBeShowing;
			this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float)shouldBeShowing.ToDirectionInt(), 0f, 1f);
			if (this._displayUpPercent == 0f)
			{
				return;
			}
			Texture2D value = this._buttonTexture.Value;
			Vector2 vector = Main.ScreenSize.ToVector2() + new Vector2(40f, -80f);
			Vector2 vector2 = vector + new Vector2(-80f, 0f);
			Vector2 vector3 = Vector2.Lerp(vector, vector2, this._displayUpPercent);
			Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
			Vector2 vector4 = rectangle.Size() / 2f;
			bool flag = false;
			if (Utils.CenteredRectangle(vector3, rectangle.Size()).Contains(Main.MouseScreen.ToPoint()))
			{
				flag = true;
				string textValue = Language.GetTextValue("UI.IssueReporterHasThingsToShow");
				Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
				if (Main.mouseLeft)
				{
					this.OpenUI();
					this.Hide();
					return;
				}
			}
			float num = 1f;
			spriteBatch.Draw(value, vector3, new Rectangle?(rectangle), Color.White, 0f, vector4, num, SpriteEffects.None, 0f);
			if (flag)
			{
				Texture2D value2 = this._buttonOutlineTexture.Value;
				Rectangle rectangle2 = value2.Frame(1, 1, 0, 0, 0, 0);
				spriteBatch.Draw(value2, vector3, new Rectangle?(rectangle2), Color.White, 0f, rectangle2.Size() / 2f, num, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0000357B File Offset: 0x0000177B
		public IssueReportsIndicator()
		{
		}

		// Token: 0x040051DB RID: 20955
		private float _displayUpPercent;

		// Token: 0x040051DC RID: 20956
		private bool _shouldBeShowing;

		// Token: 0x040051DD RID: 20957
		private Asset<Texture2D> _buttonTexture;

		// Token: 0x040051DE RID: 20958
		private Asset<Texture2D> _buttonOutlineTexture;
	}
}
