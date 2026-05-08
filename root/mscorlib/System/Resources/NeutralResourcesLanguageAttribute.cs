using System;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x0200082C RID: 2092
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		// Token: 0x060046D3 RID: 18131 RVA: 0x000E7B24 File Offset: 0x000E5D24
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this.CultureName = cultureName;
			this.Location = UltimateResourceFallbackLocation.MainAssembly;
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x000E7B48 File Offset: 0x000E5D48
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(SR.Format("The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".", location));
			}
			this.CultureName = cultureName;
			this.Location = location;
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x000E7BA4 File Offset: 0x000E5DA4
		public string CultureName
		{
			[CompilerGenerated]
			get
			{
				return this.<CultureName>k__BackingField;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x000E7BAC File Offset: 0x000E5DAC
		public UltimateResourceFallbackLocation Location
		{
			[CompilerGenerated]
			get
			{
				return this.<Location>k__BackingField;
			}
		}

		// Token: 0x04002D22 RID: 11554
		[CompilerGenerated]
		private readonly string <CultureName>k__BackingField;

		// Token: 0x04002D23 RID: 11555
		[CompilerGenerated]
		private readonly UltimateResourceFallbackLocation <Location>k__BackingField;
	}
}
