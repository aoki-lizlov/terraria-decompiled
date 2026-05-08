using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameInput
{
	// Token: 0x0200008B RID: 139
	public class SmartSelectGamepadPointer
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x004D34F3 File Offset: 0x004D16F3
		public bool ShouldBeUsed()
		{
			return PlayerInput.UsingGamepad && Main.LocalPlayer.controlTorch && Main.SmartCursorIsUsed;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x004D3510 File Offset: 0x004D1710
		public void SmartSelectLookup_GetTargetTile(Player player, out int tX, out int tY)
		{
			tX = (int)(((float)Main.mouseX + Main.screenPosition.X) / 16f);
			tY = (int)(((float)Main.mouseY + Main.screenPosition.Y) / 16f);
			if (player.gravDir == -1f)
			{
				tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16f);
			}
			if (!this.ShouldBeUsed())
			{
				return;
			}
			Point point = this.GetPointerPosition().ToPoint();
			tX = (int)(((float)point.X + Main.screenPosition.X) / 16f);
			tY = (int)(((float)point.Y + Main.screenPosition.Y) / 16f);
			if (player.gravDir == -1f)
			{
				tY = (int)((Main.screenPosition.Y + (float)Main.screenHeight - (float)point.Y) / 16f);
			}
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x004D35F9 File Offset: 0x004D17F9
		public void UpdateSize(Vector2 size)
		{
			this._size = size;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x004D3602 File Offset: 0x004D1802
		public void UpdateCenter(Vector2 center)
		{
			this._center = center;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x004D360C File Offset: 0x004D180C
		public Vector2 GetPointerPosition()
		{
			Vector2 vector = (new Vector2((float)Main.mouseX, (float)Main.mouseY) - this._center) / this._size;
			float num = Math.Abs(vector.X);
			if (num < Math.Abs(vector.Y))
			{
				num = Math.Abs(vector.Y);
			}
			if (num > 1f)
			{
				vector /= num;
			}
			vector *= Main.GameViewMatrix.RenderZoom.X;
			return vector * this._distUniform + this._center;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x004D36A4 File Offset: 0x004D18A4
		public SmartSelectGamepadPointer()
		{
		}

		// Token: 0x04001116 RID: 4374
		private Vector2 _size;

		// Token: 0x04001117 RID: 4375
		private Vector2 _center;

		// Token: 0x04001118 RID: 4376
		private Vector2 _distUniform = new Vector2(80f, 64f);
	}
}
