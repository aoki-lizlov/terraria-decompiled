using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000259 RID: 601
	public readonly struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006E65A File Offset: 0x0006C85A
		public T PreviousValue
		{
			[CompilerGenerated]
			get
			{
				return this.<PreviousValue>k__BackingField;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0006E662 File Offset: 0x0006C862
		public T CurrentValue
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentValue>k__BackingField;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0006E66A File Offset: 0x0006C86A
		public bool ThreadContextChanged
		{
			[CompilerGenerated]
			get
			{
				return this.<ThreadContextChanged>k__BackingField;
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0006E672 File Offset: 0x0006C872
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this = default(AsyncLocalValueChangedArgs<T>);
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}

		// Token: 0x0400190A RID: 6410
		[CompilerGenerated]
		private readonly T <PreviousValue>k__BackingField;

		// Token: 0x0400190B RID: 6411
		[CompilerGenerated]
		private readonly T <CurrentValue>k__BackingField;

		// Token: 0x0400190C RID: 6412
		[CompilerGenerated]
		private readonly bool <ThreadContextChanged>k__BackingField;
	}
}
