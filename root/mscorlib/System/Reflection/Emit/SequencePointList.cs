using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000900 RID: 2304
	internal class SequencePointList
	{
		// Token: 0x06005041 RID: 20545 RVA: 0x000FCF2C File Offset: 0x000FB12C
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x000FCF3B File Offset: 0x000FB13B
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x000FCF44 File Offset: 0x000FB144
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x000FCF84 File Offset: 0x000FB184
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x000FCFC4 File Offset: 0x000FB1C4
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x000FD004 File Offset: 0x000FB204
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x000FD044 File Offset: 0x000FB244
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x000FD083 File Offset: 0x000FB283
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06005049 RID: 20553 RVA: 0x000FD096 File Offset: 0x000FB296
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x000FD0B0 File Offset: 0x000FB2B0
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x000FD0C3 File Offset: 0x000FB2C3
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x000FD0E0 File Offset: 0x000FB2E0
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] array = new SequencePoint[this.count + 10];
				Array.Copy(this.points, array, this.points.Length);
				this.points = array;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x04003128 RID: 12584
		private ISymbolDocumentWriter doc;

		// Token: 0x04003129 RID: 12585
		private SequencePoint[] points;

		// Token: 0x0400312A RID: 12586
		private int count;

		// Token: 0x0400312B RID: 12587
		private const int arrayGrow = 10;
	}
}
