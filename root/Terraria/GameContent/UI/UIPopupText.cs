using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200036D RID: 877
	public class UIPopupText
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x00043F2B File Offset: 0x0004212B
		public float TargetScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x00579046 File Offset: 0x00577246
		public void PrepareDisplayText()
		{
			this.displayText = this.name;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00579054 File Offset: 0x00577254
		public void Update(int whoAmI, UIPopupTextManager manager)
		{
			if (this.active)
			{
				this.framesSinceSpawn++;
				float targetScale = this.TargetScale;
				this.alpha += (float)this.alphaDir * 0.01f;
				if ((double)this.alpha <= 0.7)
				{
					this.alpha = 0.7f;
					this.alphaDir = 1;
				}
				if (this.alpha >= 1f)
				{
					this.alpha = 1f;
					this.alphaDir = -1;
				}
				bool flag = false;
				Vector2 textHitbox = this.GetTextHitbox();
				Rectangle rectangle = new Rectangle((int)(this.position.X - textHitbox.X / 2f), (int)(this.position.Y - textHitbox.Y / 2f), (int)textHitbox.X, (int)textHitbox.Y);
				for (int i = 0; i < 20; i++)
				{
					UIPopupText uipopupText = manager.popupText[i];
					if (uipopupText.active && i != whoAmI)
					{
						Vector2 textHitbox2 = uipopupText.GetTextHitbox();
						Rectangle rectangle2 = new Rectangle((int)(uipopupText.position.X - textHitbox2.X / 2f), (int)(uipopupText.position.Y - textHitbox2.Y / 2f), (int)textHitbox2.X, (int)textHitbox2.Y);
						if (rectangle.Intersects(rectangle2) && (this.position.Y < uipopupText.position.Y || (this.position.Y == uipopupText.position.Y && whoAmI < i)))
						{
							flag = true;
							int num = manager.numActive;
							if (num > 3)
							{
								num = 3;
							}
							uipopupText.lifeTime = UIPopupText.activeTime + 15 * num;
							this.lifeTime = UIPopupText.activeTime + 15 * num;
						}
					}
				}
				if (!flag)
				{
					if (this.context != UIPopupTextContext.SpecialSeed || (this.scale != targetScale && this.lifeTime > 0))
					{
						this.velocity.Y = this.velocity.Y * 0.86f;
						if (this.scale == targetScale)
						{
							this.velocity.Y = this.velocity.Y * 0.4f;
						}
					}
				}
				else if (this.velocity.Y > -6f)
				{
					this.velocity.Y = this.velocity.Y - 0.2f;
				}
				else
				{
					this.velocity.Y = this.velocity.Y * 0.86f;
				}
				this.velocity.X = this.velocity.X * 0.93f;
				this.position += this.velocity;
				this.lifeTime--;
				if (this.lifeTime <= 0)
				{
					this.scale -= 0.03f * targetScale;
					if ((double)this.scale < 0.1 * (double)targetScale)
					{
						this.active = false;
					}
					this.lifeTime = 0;
					return;
				}
				if (this.scale < targetScale)
				{
					this.scale += 0.1f * targetScale;
				}
				if (this.scale > targetScale)
				{
					this.scale = targetScale;
				}
			}
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x00579364 File Offset: 0x00577564
		public Vector2 GetTextHitbox()
		{
			string text = this.displayText;
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
			vector *= this.scale;
			vector.Y *= 0.8f;
			return vector;
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x005793A7 File Offset: 0x005775A7
		public UIPopupText()
		{
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x005793C1 File Offset: 0x005775C1
		// Note: this type is marked as 'beforefieldinit'.
		static UIPopupText()
		{
		}

		// Token: 0x040051CD RID: 20941
		public Vector2 position;

		// Token: 0x040051CE RID: 20942
		public Vector2 velocity;

		// Token: 0x040051CF RID: 20943
		public float alpha;

		// Token: 0x040051D0 RID: 20944
		public int alphaDir = 1;

		// Token: 0x040051D1 RID: 20945
		public string name;

		// Token: 0x040051D2 RID: 20946
		public string displayText;

		// Token: 0x040051D3 RID: 20947
		public float scale = 1f;

		// Token: 0x040051D4 RID: 20948
		public float rotation;

		// Token: 0x040051D5 RID: 20949
		public Color color;

		// Token: 0x040051D6 RID: 20950
		public bool active;

		// Token: 0x040051D7 RID: 20951
		public int lifeTime;

		// Token: 0x040051D8 RID: 20952
		public int framesSinceSpawn;

		// Token: 0x040051D9 RID: 20953
		public static int activeTime = 60;

		// Token: 0x040051DA RID: 20954
		public UIPopupTextContext context;
	}
}
