using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200063D RID: 1597
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		// Token: 0x06003CFA RID: 15610 RVA: 0x000D4480 File Offset: 0x000D2680
		public OptionalFieldAttribute()
		{
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x000D448F File Offset: 0x000D268F
		// (set) Token: 0x06003CFC RID: 15612 RVA: 0x000D4497 File Offset: 0x000D2697
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Version value must be positive."));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x04002705 RID: 9989
		private int versionAdded = 1;
	}
}
