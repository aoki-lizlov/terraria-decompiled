using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200058A RID: 1418
	public class TileDrawSorter
	{
		// Token: 0x06003817 RID: 14359 RVA: 0x006308DA File Offset: 0x0062EADA
		public TileDrawSorter()
		{
			this._currentCacheIndex = 0;
			this._holderLength = 9000;
			this.tilesToDraw = new TileDrawSorter.TileTexPoint[this._holderLength];
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x00630910 File Offset: 0x0062EB10
		public void reset()
		{
			this._currentCacheIndex = 0;
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x0063091C File Offset: 0x0062EB1C
		public void Cache(int x, int y, int type)
		{
			int currentCacheIndex = this._currentCacheIndex;
			this._currentCacheIndex = currentCacheIndex + 1;
			int num = currentCacheIndex;
			this.tilesToDraw[num].X = x;
			this.tilesToDraw[num].Y = y;
			this.tilesToDraw[num].TileType = type;
			if (this._currentCacheIndex == this._holderLength)
			{
				this.IncreaseArraySize();
			}
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x00630985 File Offset: 0x0062EB85
		private void IncreaseArraySize()
		{
			this._holderLength *= 2;
			Array.Resize<TileDrawSorter.TileTexPoint>(ref this.tilesToDraw, this._holderLength);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x006309A6 File Offset: 0x0062EBA6
		public void Sort()
		{
			Array.Sort<TileDrawSorter.TileTexPoint>(this.tilesToDraw, 0, this._currentCacheIndex, this._tileComparer);
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x006309C0 File Offset: 0x0062EBC0
		public int GetAmountToDraw()
		{
			return this._currentCacheIndex;
		}

		// Token: 0x04005C48 RID: 23624
		public TileDrawSorter.TileTexPoint[] tilesToDraw;

		// Token: 0x04005C49 RID: 23625
		private int _holderLength;

		// Token: 0x04005C4A RID: 23626
		private int _currentCacheIndex;

		// Token: 0x04005C4B RID: 23627
		private TileDrawSorter.CustomComparer _tileComparer = new TileDrawSorter.CustomComparer();

		// Token: 0x020009BF RID: 2495
		public struct TileTexPoint
		{
			// Token: 0x06004A3B RID: 19003 RVA: 0x006D3FD0 File Offset: 0x006D21D0
			public override string ToString()
			{
				return string.Format("X:{0}, Y:{1}, Type:{2}", this.X, this.Y, this.TileType);
			}

			// Token: 0x040076D6 RID: 30422
			public int X;

			// Token: 0x040076D7 RID: 30423
			public int Y;

			// Token: 0x040076D8 RID: 30424
			public int TileType;
		}

		// Token: 0x020009C0 RID: 2496
		public class CustomComparer : Comparer<TileDrawSorter.TileTexPoint>
		{
			// Token: 0x06004A3C RID: 19004 RVA: 0x006D3FFD File Offset: 0x006D21FD
			public override int Compare(TileDrawSorter.TileTexPoint x, TileDrawSorter.TileTexPoint y)
			{
				return x.TileType.CompareTo(y.TileType);
			}

			// Token: 0x06004A3D RID: 19005 RVA: 0x006D4011 File Offset: 0x006D2211
			public CustomComparer()
			{
			}
		}
	}
}
