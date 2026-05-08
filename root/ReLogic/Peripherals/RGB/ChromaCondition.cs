using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000029 RID: 41
	public abstract class ChromaCondition
	{
		// Token: 0x06000127 RID: 295
		public abstract bool IsActive();

		// Token: 0x06000128 RID: 296 RVA: 0x0000448A File Offset: 0x0000268A
		protected ChromaCondition()
		{
		}

		// Token: 0x020000B8 RID: 184
		public class Custom : ChromaCondition
		{
			// Token: 0x06000425 RID: 1061 RVA: 0x0000E107 File Offset: 0x0000C307
			public Custom(Func<bool> condition)
			{
				this._condition = condition;
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x0000E116 File Offset: 0x0000C316
			public override bool IsActive()
			{
				return this._condition.Invoke();
			}

			// Token: 0x04000566 RID: 1382
			private Func<bool> _condition;
		}
	}
}
