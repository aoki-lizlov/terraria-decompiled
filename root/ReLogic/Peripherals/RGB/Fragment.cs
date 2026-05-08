using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002E RID: 46
	public class Fragment
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000057FD File Offset: 0x000039FD
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005805 File Offset: 0x00003A05
		public Vector2 CanvasTopLeft
		{
			[CompilerGenerated]
			get
			{
				return this.<CanvasTopLeft>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanvasTopLeft>k__BackingField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000580E File Offset: 0x00003A0E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005816 File Offset: 0x00003A16
		public Vector2 CanvasBottomRight
		{
			[CompilerGenerated]
			get
			{
				return this.<CanvasBottomRight>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanvasBottomRight>k__BackingField = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000581F File Offset: 0x00003A1F
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005827 File Offset: 0x00003A27
		public Vector2 CanvasSize
		{
			[CompilerGenerated]
			get
			{
				return this.<CanvasSize>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanvasSize>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005830 File Offset: 0x00003A30
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005838 File Offset: 0x00003A38
		public Vector2 CanvasCenter
		{
			[CompilerGenerated]
			get
			{
				return this.<CanvasCenter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanvasCenter>k__BackingField = value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005841 File Offset: 0x00003A41
		private Fragment(Point[] gridPositions)
			: this(gridPositions, Fragment.CreateCanvasPositions(gridPositions))
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005850 File Offset: 0x00003A50
		private Fragment(Point[] gridPositions, Vector2[] canvasPositions)
		{
			this.Count = gridPositions.Length;
			this._gridPositions = gridPositions;
			this._canvasPositions = canvasPositions;
			this.Colors = new Vector4[this.Count];
			this.SetupCanvasBounds();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005888 File Offset: 0x00003A88
		public static Fragment FromGrid(Rectangle grid)
		{
			Point[] array = new Point[grid.Width * grid.Height];
			int num = 0;
			for (int i = 0; i < grid.Height; i++)
			{
				for (int j = 0; j < grid.Width; j++)
				{
					array[num++] = new Point(grid.X + j, grid.Y + i);
				}
			}
			return new Fragment(array);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000058F2 File Offset: 0x00003AF2
		public static Fragment FromCustom(Point[] gridPositions)
		{
			return new Fragment(gridPositions);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000058FA File Offset: 0x00003AFA
		public static Fragment FromCustom(Point[] gridPositions, Vector2[] canvasPositions)
		{
			return new Fragment(gridPositions, canvasPositions);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005903 File Offset: 0x00003B03
		public Fragment CreateCopy()
		{
			return new Fragment(this._gridPositions, this._canvasPositions);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005916 File Offset: 0x00003B16
		public Vector2 GetCanvasPositionOfIndex(int index)
		{
			return this._canvasPositions[index];
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005924 File Offset: 0x00003B24
		public Point GetGridPositionOfIndex(int index)
		{
			return this._gridPositions[index];
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005934 File Offset: 0x00003B34
		public void Clear()
		{
			for (int i = 0; i < this.Colors.Length; i++)
			{
				this.Colors[i] = Vector4.Zero;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005965 File Offset: 0x00003B65
		public void SetColor(int index, Vector4 color)
		{
			this.Colors[index] = color;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005974 File Offset: 0x00003B74
		public void SetColor(int index, float r, float g, float b, float a = 1f)
		{
			this.Colors[index] = new Vector4(r, g, b, a);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005990 File Offset: 0x00003B90
		public Vector4 GetAverage()
		{
			Vector4 vector = Vector4.Zero;
			for (int i = 0; i < this.Colors.Length; i++)
			{
				vector += this.Colors[i];
			}
			return vector / (float)this.Colors.Length;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000059DC File Offset: 0x00003BDC
		private void SetupCanvasBounds()
		{
			if (this._canvasPositions.Length == 0)
			{
				return;
			}
			float num = this._canvasPositions[0].X;
			float num2 = this._canvasPositions[0].X;
			float num3 = this._canvasPositions[0].Y;
			float num4 = this._canvasPositions[0].Y;
			for (int i = 1; i < this._canvasPositions.Length; i++)
			{
				num = Math.Min(num, this._canvasPositions[i].X);
				num3 = Math.Min(num3, this._canvasPositions[i].Y);
				num2 = Math.Max(num2, this._canvasPositions[i].X);
				num4 = Math.Max(num4, this._canvasPositions[i].Y);
			}
			this.CanvasTopLeft = new Vector2(num, num3);
			this.CanvasBottomRight = new Vector2(num2, num4);
			this.CanvasSize = new Vector2(num2 - num, num4 - num3);
			this.CanvasCenter = this.CanvasTopLeft + this.CanvasSize * 0.5f;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B04 File Offset: 0x00003D04
		private static Vector2[] CreateCanvasPositions(Point[] gridPositions)
		{
			Vector2[] array = new Vector2[gridPositions.Length];
			int num = gridPositions[0].Y;
			int num2 = gridPositions[0].Y;
			int num3 = gridPositions[0].X;
			for (int i = 1; i < gridPositions.Length; i++)
			{
				num3 = Math.Min(num3, gridPositions[i].X);
				num = Math.Min(num, gridPositions[i].Y);
				num2 = Math.Max(num2, gridPositions[i].Y);
			}
			float num4 = 1f;
			if (num2 != num)
			{
				num4 = 1f / (float)(num2 - num);
			}
			Vector2 vector;
			vector..ctor((float)num3 * 0.16666667f, (float)num * 0.16666667f);
			for (int j = 0; j < gridPositions.Length; j++)
			{
				array[j] = new Vector2((float)(gridPositions[j].X - num3), (float)(gridPositions[j].Y - num)) * num4 + vector;
			}
			return array;
		}

		// Token: 0x0400007A RID: 122
		public readonly Vector4[] Colors;

		// Token: 0x0400007B RID: 123
		private readonly Vector2[] _canvasPositions;

		// Token: 0x0400007C RID: 124
		private readonly Point[] _gridPositions;

		// Token: 0x0400007D RID: 125
		[CompilerGenerated]
		private Vector2 <CanvasTopLeft>k__BackingField;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private Vector2 <CanvasBottomRight>k__BackingField;

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private Vector2 <CanvasSize>k__BackingField;

		// Token: 0x04000080 RID: 128
		[CompilerGenerated]
		private Vector2 <CanvasCenter>k__BackingField;

		// Token: 0x04000081 RID: 129
		public readonly int Count;
	}
}
