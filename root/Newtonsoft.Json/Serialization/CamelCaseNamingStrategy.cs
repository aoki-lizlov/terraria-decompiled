using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000074 RID: 116
	public class CamelCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x00017EF8 File Offset: 0x000160F8
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00017F0E File Offset: 0x0001610E
		public CamelCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017F1F File Offset: 0x0001611F
		public CamelCaseNamingStrategy()
		{
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017F27 File Offset: 0x00016127
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToCamelCase(name);
		}
	}
}
