using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000B00 RID: 2816
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal readonly struct CopyPosition
	{
		// Token: 0x060067B0 RID: 26544 RVA: 0x0015F81C File Offset: 0x0015DA1C
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x0015F82C File Offset: 0x0015DA2C
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060067B2 RID: 26546 RVA: 0x0015F842 File Offset: 0x0015DA42
		internal int Row
		{
			[CompilerGenerated]
			get
			{
				return this.<Row>k__BackingField;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060067B3 RID: 26547 RVA: 0x0015F84A File Offset: 0x0015DA4A
		internal int Column
		{
			[CompilerGenerated]
			get
			{
				return this.<Column>k__BackingField;
			}
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x0015F852 File Offset: 0x0015DA52
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060067B5 RID: 26549 RVA: 0x0015F872 File Offset: 0x0015DA72
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}

		// Token: 0x04003C26 RID: 15398
		[CompilerGenerated]
		private readonly int <Row>k__BackingField;

		// Token: 0x04003C27 RID: 15399
		[CompilerGenerated]
		private readonly int <Column>k__BackingField;
	}
}
