using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x020005A3 RID: 1443
	public struct SpriteFrame
	{
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x00651186 File Offset: 0x0064F386
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x0065118E File Offset: 0x0064F38E
		public byte CurrentColumn
		{
			get
			{
				return this._currentColumn;
			}
			set
			{
				this._currentColumn = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x00651197 File Offset: 0x0064F397
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x0065119F File Offset: 0x0064F39F
		public byte CurrentRow
		{
			get
			{
				return this._currentRow;
			}
			set
			{
				this._currentRow = value;
			}
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x006511A8 File Offset: 0x0064F3A8
		public SpriteFrame(byte columns, byte rows)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = 0;
			this._currentRow = 0;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x006511D4 File Offset: 0x0064F3D4
		public SpriteFrame(byte columns, byte rows, byte currentColumn, byte currentRow)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = currentColumn;
			this._currentRow = currentRow;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x00651204 File Offset: 0x0064F404
		public SpriteFrame With(byte columnToUse, byte rowToUse)
		{
			SpriteFrame spriteFrame = this;
			spriteFrame.CurrentColumn = columnToUse;
			spriteFrame.CurrentRow = rowToUse;
			return spriteFrame;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x0065122C File Offset: 0x0064F42C
		public Rectangle GetSourceRectangle(Texture2D texture)
		{
			int num = texture.Width / (int)this.ColumnCount;
			int num2 = texture.Height / (int)this.RowCount;
			return new Rectangle((int)this.CurrentColumn * num, (int)this.CurrentRow * num2, num - ((this.ColumnCount == 1) ? 0 : this.PaddingX), num2 - ((this.RowCount == 1) ? 0 : this.PaddingY));
		}

		// Token: 0x04005D56 RID: 23894
		public int PaddingX;

		// Token: 0x04005D57 RID: 23895
		public int PaddingY;

		// Token: 0x04005D58 RID: 23896
		private byte _currentColumn;

		// Token: 0x04005D59 RID: 23897
		private byte _currentRow;

		// Token: 0x04005D5A RID: 23898
		public readonly byte ColumnCount;

		// Token: 0x04005D5B RID: 23899
		public readonly byte RowCount;
	}
}
