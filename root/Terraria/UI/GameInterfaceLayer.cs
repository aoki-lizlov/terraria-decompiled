using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

namespace Terraria.UI
{
	// Token: 0x020000EA RID: 234
	public class GameInterfaceLayer
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x004E62DB File Offset: 0x004E44DB
		public GameInterfaceLayer(string name, InterfaceScaleType scaleType)
		{
			this.Name = name;
			this.ScaleType = scaleType;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x004E62F4 File Offset: 0x004E44F4
		public bool Draw()
		{
			Matrix matrix;
			if (this.ScaleType == InterfaceScaleType.Game)
			{
				PlayerInput.SetZoom_World();
				matrix = Main.GameViewMatrix.ZoomMatrix;
			}
			else if (this.ScaleType == InterfaceScaleType.UI)
			{
				PlayerInput.SetZoom_UI();
				matrix = Main.UIScaleMatrix;
			}
			else
			{
				PlayerInput.SetZoom_Unscaled();
				matrix = Matrix.Identity;
			}
			bool flag = false;
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, matrix);
			try
			{
				flag = this.DrawSelf();
			}
			catch (Exception ex)
			{
				TimeLogger.DrawException(ex);
			}
			Main.spriteBatch.End();
			return flag;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000379E9 File Offset: 0x00035BE9
		protected virtual bool DrawSelf()
		{
			return true;
		}

		// Token: 0x0400131B RID: 4891
		public readonly string Name;

		// Token: 0x0400131C RID: 4892
		public InterfaceScaleType ScaleType;
	}
}
