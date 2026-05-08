using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000831 RID: 2097
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SatelliteContractVersionAttribute : Attribute
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x000E81D8 File Offset: 0x000E63D8
		public SatelliteContractVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000E81F5 File Offset: 0x000E63F5
		public string Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		// Token: 0x04002D47 RID: 11591
		[CompilerGenerated]
		private readonly string <Version>k__BackingField;
	}
}
