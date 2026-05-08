using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B44 RID: 2884
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x0600696D RID: 26989 RVA: 0x00165A4C File Offset: 0x00163C4C
		// (set) Token: 0x0600696E RID: 26990 RVA: 0x00165A54 File Offset: 0x00163C54
		public ReadOnlyMemory<T> Memory
		{
			[CompilerGenerated]
			get
			{
				return this.<Memory>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Memory>k__BackingField = value;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x0600696F RID: 26991 RVA: 0x00165A5D File Offset: 0x00163C5D
		// (set) Token: 0x06006970 RID: 26992 RVA: 0x00165A65 File Offset: 0x00163C65
		public ReadOnlySequenceSegment<T> Next
		{
			[CompilerGenerated]
			get
			{
				return this.<Next>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Next>k__BackingField = value;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06006971 RID: 26993 RVA: 0x00165A6E File Offset: 0x00163C6E
		// (set) Token: 0x06006972 RID: 26994 RVA: 0x00165A76 File Offset: 0x00163C76
		public long RunningIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<RunningIndex>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<RunningIndex>k__BackingField = value;
			}
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x000025BE File Offset: 0x000007BE
		protected ReadOnlySequenceSegment()
		{
		}

		// Token: 0x04003CAB RID: 15531
		[CompilerGenerated]
		private ReadOnlyMemory<T> <Memory>k__BackingField;

		// Token: 0x04003CAC RID: 15532
		[CompilerGenerated]
		private ReadOnlySequenceSegment<T> <Next>k__BackingField;

		// Token: 0x04003CAD RID: 15533
		[CompilerGenerated]
		private long <RunningIndex>k__BackingField;
	}
}
