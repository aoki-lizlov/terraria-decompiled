using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D2 RID: 2002
	public static class RuntimeFeature
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x000E5290 File Offset: 0x000E3490
		public static bool IsSupported(string feature)
		{
			if (feature == "PortablePdb" || feature == "DefaultImplementationsOfInterfaces")
			{
				return true;
			}
			if (!(feature == "IsDynamicCodeSupported"))
			{
				return feature == "IsDynamicCodeCompiled" && RuntimeFeature.IsDynamicCodeCompiled;
			}
			return RuntimeFeature.IsDynamicCodeSupported;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static bool IsDynamicCodeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060045AD RID: 17837 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static bool IsDynamicCodeCompiled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002CBF RID: 11455
		public const string PortablePdb = "PortablePdb";

		// Token: 0x04002CC0 RID: 11456
		public const string DefaultImplementationsOfInterfaces = "DefaultImplementationsOfInterfaces";
	}
}
