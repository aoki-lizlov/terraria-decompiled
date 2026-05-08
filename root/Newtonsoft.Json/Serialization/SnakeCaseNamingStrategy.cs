using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000080 RID: 128
	public class SnakeCaseNamingStrategy : NamingStrategy
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x00017EF8 File Offset: 0x000160F8
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
		{
			base.ProcessDictionaryKeys = processDictionaryKeys;
			base.OverrideSpecifiedNames = overrideSpecifiedNames;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018445 File Offset: 0x00016645
		public SnakeCaseNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
			: this(processDictionaryKeys, overrideSpecifiedNames)
		{
			base.ProcessExtensionDataNames = processExtensionDataNames;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00017F1F File Offset: 0x0001611F
		public SnakeCaseNamingStrategy()
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00018456 File Offset: 0x00016656
		protected override string ResolvePropertyName(string name)
		{
			return StringUtils.ToSnakeCase(name);
		}
	}
}
