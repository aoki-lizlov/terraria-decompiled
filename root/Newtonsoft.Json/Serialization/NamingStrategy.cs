using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007D RID: 125
	public abstract class NamingStrategy
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00018370 File Offset: 0x00016570
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00018378 File Offset: 0x00016578
		public bool ProcessDictionaryKeys
		{
			[CompilerGenerated]
			get
			{
				return this.<ProcessDictionaryKeys>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ProcessDictionaryKeys>k__BackingField = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x00018381 File Offset: 0x00016581
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x00018389 File Offset: 0x00016589
		public bool ProcessExtensionDataNames
		{
			[CompilerGenerated]
			get
			{
				return this.<ProcessExtensionDataNames>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ProcessExtensionDataNames>k__BackingField = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00018392 File Offset: 0x00016592
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001839A File Offset: 0x0001659A
		public bool OverrideSpecifiedNames
		{
			[CompilerGenerated]
			get
			{
				return this.<OverrideSpecifiedNames>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OverrideSpecifiedNames>k__BackingField = value;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000183A3 File Offset: 0x000165A3
		public virtual string GetPropertyName(string name, bool hasSpecifiedName)
		{
			if (hasSpecifiedName && !this.OverrideSpecifiedNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000183B9 File Offset: 0x000165B9
		public virtual string GetExtensionDataName(string name)
		{
			if (!this.ProcessExtensionDataNames)
			{
				return name;
			}
			return this.ResolvePropertyName(name);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000183CC File Offset: 0x000165CC
		public virtual string GetDictionaryKey(string key)
		{
			if (!this.ProcessDictionaryKeys)
			{
				return key;
			}
			return this.ResolvePropertyName(key);
		}

		// Token: 0x060005C2 RID: 1474
		protected abstract string ResolvePropertyName(string name);

		// Token: 0x060005C3 RID: 1475 RVA: 0x00008020 File Offset: 0x00006220
		protected NamingStrategy()
		{
		}

		// Token: 0x04000275 RID: 629
		[CompilerGenerated]
		private bool <ProcessDictionaryKeys>k__BackingField;

		// Token: 0x04000276 RID: 630
		[CompilerGenerated]
		private bool <ProcessExtensionDataNames>k__BackingField;

		// Token: 0x04000277 RID: 631
		[CompilerGenerated]
		private bool <OverrideSpecifiedNames>k__BackingField;
	}
}
