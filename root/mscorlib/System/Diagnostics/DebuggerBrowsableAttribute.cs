using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A13 RID: 2579
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		// Token: 0x06005FAC RID: 24492 RVA: 0x0014C024 File Offset: 0x0014A224
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06005FAD RID: 24493 RVA: 0x0014C046 File Offset: 0x0014A246
		public DebuggerBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x040039AC RID: 14764
		private DebuggerBrowsableState state;
	}
}
