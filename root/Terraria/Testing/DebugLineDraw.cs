using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Testing
{
	// Token: 0x02000118 RID: 280
	public class DebugLineDraw
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x004F9BAD File Offset: 0x004F7DAD
		private DebugLineDraw(bool ui)
		{
			this._ui = ui;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x004F9BC7 File Offset: 0x004F7DC7
		public void AddLine(Vector2 start, Vector2 end, Color colorStart, Color colorEnd = default(Color), int LifeTime = 1, float width = 1f)
		{
			this.lines.Add(new DebugLineDraw.LineDrawer(start, end, colorStart, colorEnd, LifeTime, width));
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x004F9BE2 File Offset: 0x004F7DE2
		public void AddLine(Point start, Point end, Color colorStart, Color colorEnd = default(Color), int LifeTime = 1, float width = 1f)
		{
			this.lines.Add(new DebugLineDraw.LineDrawer(start.ToVector2(), end.ToVector2(), colorStart, colorEnd, LifeTime, width));
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x004F9C08 File Offset: 0x004F7E08
		public void AddRectangle(Vector2 start, Vector2 end, Color colorStart, Color colorEnd = default(Color), int LifeTime = 1, float width = 1f)
		{
			this.lines.Add(new DebugLineDraw.LineDrawer(start, new Vector2(start.X, end.Y), colorStart, colorEnd, LifeTime, width));
			this.lines.Add(new DebugLineDraw.LineDrawer(start, new Vector2(end.X, start.Y), colorStart, colorEnd, LifeTime, width));
			this.lines.Add(new DebugLineDraw.LineDrawer(end, new Vector2(start.X, end.Y), colorStart, colorEnd, LifeTime, width));
			this.lines.Add(new DebugLineDraw.LineDrawer(end, new Vector2(end.X, start.Y), colorStart, colorEnd, LifeTime, width));
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x004F9CB9 File Offset: 0x004F7EB9
		public static void PreUpdate()
		{
			DebugLineDraw.SetPhase(DebugLineDraw.UpdatePhase.Update);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x004F9CC1 File Offset: 0x004F7EC1
		public static void PreWorldUpdate()
		{
			DebugLineDraw.SetPhase(DebugLineDraw.UpdatePhase.UpdateInWorld);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x004F9CC9 File Offset: 0x004F7EC9
		public static void PreDraw()
		{
			DebugLineDraw.SetPhase(DebugLineDraw.UpdatePhase.Draw);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x004F9CD1 File Offset: 0x004F7ED1
		private static void SetPhase(DebugLineDraw.UpdatePhase phase)
		{
			DebugLineDraw.CurrentPhase = phase;
			DebugLineDraw.UI.TickLines();
			DebugLineDraw.World.TickLines();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x004F9CF0 File Offset: 0x004F7EF0
		public void TickLines()
		{
			int num = 0;
			for (int i = 0; i < this.lines.Count; i++)
			{
				DebugLineDraw.LineDrawer lineDrawer = this.lines[i];
				if (lineDrawer.Phase == DebugLineDraw.CurrentPhase)
				{
					lineDrawer.TimeLeft--;
				}
				if (lineDrawer.TimeLeft >= 0)
				{
					this.lines[num++] = lineDrawer;
				}
			}
			this.lines.RemoveRange(num, this.lines.Count - num);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x004F9D70 File Offset: 0x004F7F70
		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.lines.Count == 0)
			{
				return;
			}
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, null, null, null, this._ui ? (Matrix.CreateTranslation(Main.screenPosition.X, Main.screenPosition.Y, 0f) * Main.UIScaleMatrix) : Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < this.lines.Count; i++)
			{
				this.lines[i].Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x004F9E09 File Offset: 0x004F8009
		// Note: this type is marked as 'beforefieldinit'.
		static DebugLineDraw()
		{
		}

		// Token: 0x04001558 RID: 5464
		public static readonly DebugLineDraw UI = new DebugLineDraw(true);

		// Token: 0x04001559 RID: 5465
		public static readonly DebugLineDraw World = new DebugLineDraw(false);

		// Token: 0x0400155A RID: 5466
		private static DebugLineDraw.UpdatePhase CurrentPhase;

		// Token: 0x0400155B RID: 5467
		private readonly List<DebugLineDraw.LineDrawer> lines = new List<DebugLineDraw.LineDrawer>();

		// Token: 0x0400155C RID: 5468
		private readonly bool _ui;

		// Token: 0x02000729 RID: 1833
		private enum UpdatePhase
		{
			// Token: 0x0400697B RID: 27003
			Update,
			// Token: 0x0400697C RID: 27004
			UpdateInWorld,
			// Token: 0x0400697D RID: 27005
			Draw
		}

		// Token: 0x0200072A RID: 1834
		private class LineDrawer
		{
			// Token: 0x06004080 RID: 16512 RVA: 0x0069E6E8 File Offset: 0x0069C8E8
			public LineDrawer(Vector2 start, Vector2 end, Color colorStart, Color colorEnd = default(Color), int LifeTime = 1, float width = 1f)
			{
				this.vS = start;
				this.vE = end;
				this.cS = colorStart;
				this.cE = ((colorEnd == default(Color)) ? colorStart : colorEnd);
				this.TimeLeft = LifeTime;
				this.Width = width;
			}

			// Token: 0x06004081 RID: 16513 RVA: 0x0069E748 File Offset: 0x0069C948
			public void Draw(SpriteBatch spriteBatch)
			{
				Utils.DrawLine(spriteBatch, this.vS, this.vE, this.cS, this.cE, this.Width);
			}

			// Token: 0x0400697E RID: 27006
			public Vector2 vS;

			// Token: 0x0400697F RID: 27007
			public Vector2 vE;

			// Token: 0x04006980 RID: 27008
			public Color cS;

			// Token: 0x04006981 RID: 27009
			public Color cE;

			// Token: 0x04006982 RID: 27010
			public int TimeLeft;

			// Token: 0x04006983 RID: 27011
			public float Width;

			// Token: 0x04006984 RID: 27012
			public DebugLineDraw.UpdatePhase Phase = DebugLineDraw.CurrentPhase;
		}
	}
}
