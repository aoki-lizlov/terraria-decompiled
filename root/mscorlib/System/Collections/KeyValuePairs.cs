using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x02000A7E RID: 2686
	[DebuggerDisplay("{_value}", Name = "[{_key}]")]
	internal class KeyValuePairs
	{
		// Token: 0x0600621D RID: 25117 RVA: 0x0014EB01 File Offset: 0x0014CD01
		public KeyValuePairs(object key, object value)
		{
			this._value = value;
			this._key = key;
		}

		// Token: 0x04003AB5 RID: 15029
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _key;

		// Token: 0x04003AB6 RID: 15030
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly object _value;
	}
}
