using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200055C RID: 1372
	public class SettingsForCharacterPreview
	{
		// Token: 0x060037BD RID: 14269 RVA: 0x0062FDD0 File Offset: 0x0062DFD0
		public void ApplyTo(Projectile proj, bool walking)
		{
			proj.position += this.Offset;
			proj.spriteDirection = this.SpriteDirection;
			proj.direction = this.SpriteDirection;
			if (walking)
			{
				this.Selected.ApplyTo(proj);
			}
			else
			{
				this.NotSelected.ApplyTo(proj);
			}
			if (this.CustomAnimation != null)
			{
				this.CustomAnimation(proj, walking);
			}
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x0062FE3E File Offset: 0x0062E03E
		public SettingsForCharacterPreview WhenSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.Selected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x0062FE51 File Offset: 0x0062E051
		public SettingsForCharacterPreview WhenNotSelected(int? startFrame = null, int? frameCount = null, int? delayPerFrame = null, bool? bounceLoop = null)
		{
			SettingsForCharacterPreview.Modify(ref this.NotSelected, startFrame, frameCount, delayPerFrame, bounceLoop);
			return this;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x0062FE64 File Offset: 0x0062E064
		private static void Modify(ref SettingsForCharacterPreview.SelectionBasedSettings target, int? startFrame, int? frameCount, int? delayPerFrame, bool? bounceLoop)
		{
			if (frameCount != null && frameCount.Value < 1)
			{
				frameCount = new int?(1);
			}
			target.StartFrame = ((startFrame != null) ? startFrame.Value : target.StartFrame);
			target.FrameCount = ((frameCount != null) ? frameCount.Value : target.FrameCount);
			target.DelayPerFrame = ((delayPerFrame != null) ? delayPerFrame.Value : target.DelayPerFrame);
			target.BounceLoop = ((bounceLoop != null) ? bounceLoop.Value : target.BounceLoop);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x0062FF04 File Offset: 0x0062E104
		public SettingsForCharacterPreview WithOffset(Vector2 offset)
		{
			this.Offset = offset;
			return this;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x0062FF0E File Offset: 0x0062E10E
		public SettingsForCharacterPreview WithOffset(float x, float y)
		{
			this.Offset = new Vector2(x, y);
			return this;
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x0062FF1E File Offset: 0x0062E11E
		public SettingsForCharacterPreview WithSpriteDirection(int spriteDirection)
		{
			this.SpriteDirection = spriteDirection;
			return this;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x0062FF28 File Offset: 0x0062E128
		public SettingsForCharacterPreview WithCode(SettingsForCharacterPreview.CustomAnimationCode customAnimation)
		{
			this.CustomAnimation = customAnimation;
			return this;
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x0062FF32 File Offset: 0x0062E132
		public SettingsForCharacterPreview()
		{
		}

		// Token: 0x04005BD6 RID: 23510
		public Vector2 Offset;

		// Token: 0x04005BD7 RID: 23511
		public SettingsForCharacterPreview.SelectionBasedSettings Selected;

		// Token: 0x04005BD8 RID: 23512
		public SettingsForCharacterPreview.SelectionBasedSettings NotSelected;

		// Token: 0x04005BD9 RID: 23513
		public int SpriteDirection = 1;

		// Token: 0x04005BDA RID: 23514
		public SettingsForCharacterPreview.CustomAnimationCode CustomAnimation;

		// Token: 0x020009BC RID: 2492
		// (Invoke) Token: 0x06004A37 RID: 18999
		public delegate void CustomAnimationCode(Projectile proj, bool walking);

		// Token: 0x020009BD RID: 2493
		public struct SelectionBasedSettings
		{
			// Token: 0x06004A3A RID: 19002 RVA: 0x006D3F18 File Offset: 0x006D2118
			public void ApplyTo(Projectile proj)
			{
				if (this.FrameCount == 0)
				{
					return;
				}
				if (proj.frame < this.StartFrame)
				{
					proj.frame = this.StartFrame;
				}
				int num = proj.frame - this.StartFrame;
				int num2 = this.FrameCount * this.DelayPerFrame;
				int num3 = num2;
				if (this.BounceLoop)
				{
					num3 = num2 * 2 - this.DelayPerFrame * 2;
				}
				int num4 = proj.frameCounter + 1;
				proj.frameCounter = num4;
				if (num4 >= num3)
				{
					proj.frameCounter = 0;
				}
				num = proj.frameCounter / this.DelayPerFrame;
				if (this.BounceLoop && num >= this.FrameCount)
				{
					num = this.FrameCount * 2 - num - 2;
				}
				proj.frame = this.StartFrame + num;
			}

			// Token: 0x040076D0 RID: 30416
			public int StartFrame;

			// Token: 0x040076D1 RID: 30417
			public int FrameCount;

			// Token: 0x040076D2 RID: 30418
			public int DelayPerFrame;

			// Token: 0x040076D3 RID: 30419
			public bool BounceLoop;
		}
	}
}
