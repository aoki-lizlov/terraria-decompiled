using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200004A RID: 74
	internal class CodePointIndexer
	{
		// Token: 0x06000143 RID: 323 RVA: 0x000054A0 File Offset: 0x000036A0
		public static Array CompressArray(Array source, Type type, CodePointIndexer indexer)
		{
			int num = 0;
			for (int i = 0; i < indexer.ranges.Length; i++)
			{
				num += indexer.ranges[i].Count;
			}
			Array array = Array.CreateInstance(type, num);
			for (int j = 0; j < indexer.ranges.Length; j++)
			{
				Array.Copy(source, indexer.ranges[j].Start, array, indexer.ranges[j].IndexStart, indexer.ranges[j].Count);
			}
			return array;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000552C File Offset: 0x0000372C
		public CodePointIndexer(int[] starts, int[] ends, int defaultIndex, int defaultCP)
		{
			this.defaultIndex = defaultIndex;
			this.defaultCP = defaultCP;
			this.ranges = new CodePointIndexer.TableRange[starts.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new CodePointIndexer.TableRange(starts[i], ends[i], (i == 0) ? 0 : (this.ranges[i - 1].IndexStart + this.ranges[i - 1].Count));
			}
			for (int j = 0; j < this.ranges.Length; j++)
			{
				this.TotalCount += this.ranges[j].Count;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000055E4 File Offset: 0x000037E4
		public int ToIndex(int cp)
		{
			for (int i = 0; i < this.ranges.Length; i++)
			{
				if (cp < this.ranges[i].Start)
				{
					return this.defaultIndex;
				}
				if (cp < this.ranges[i].End)
				{
					return cp - this.ranges[i].Start + this.ranges[i].IndexStart;
				}
			}
			return this.defaultIndex;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005660 File Offset: 0x00003860
		public int ToCodePoint(int i)
		{
			for (int j = 0; j < this.ranges.Length; j++)
			{
				if (i < this.ranges[j].IndexStart)
				{
					return this.defaultCP;
				}
				if (i < this.ranges[j].IndexEnd)
				{
					return i - this.ranges[j].IndexStart + this.ranges[j].Start;
				}
			}
			return this.defaultCP;
		}

		// Token: 0x04000D27 RID: 3367
		private readonly CodePointIndexer.TableRange[] ranges;

		// Token: 0x04000D28 RID: 3368
		public readonly int TotalCount;

		// Token: 0x04000D29 RID: 3369
		private int defaultIndex;

		// Token: 0x04000D2A RID: 3370
		private int defaultCP;

		// Token: 0x0200004B RID: 75
		[Serializable]
		internal struct TableRange
		{
			// Token: 0x06000147 RID: 327 RVA: 0x000056DB File Offset: 0x000038DB
			public TableRange(int start, int end, int indexStart)
			{
				this.Start = start;
				this.End = end;
				this.Count = this.End - this.Start;
				this.IndexStart = indexStart;
				this.IndexEnd = this.IndexStart + this.Count;
			}

			// Token: 0x04000D2B RID: 3371
			public readonly int Start;

			// Token: 0x04000D2C RID: 3372
			public readonly int End;

			// Token: 0x04000D2D RID: 3373
			public readonly int Count;

			// Token: 0x04000D2E RID: 3374
			public readonly int IndexStart;

			// Token: 0x04000D2F RID: 3375
			public readonly int IndexEnd;
		}
	}
}
