using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007BC RID: 1980
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06004587 RID: 17799 RVA: 0x000E5187 File Offset: 0x000E3387
		public DateTimeConstantAttribute(long ticks)
		{
			this._date = new DateTime(ticks);
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x000E519B File Offset: 0x000E339B
		public override object Value
		{
			get
			{
				return this._date;
			}
		}

		// Token: 0x04002CB7 RID: 11447
		private DateTime _date;
	}
}
