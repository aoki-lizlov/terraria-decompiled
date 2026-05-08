using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x02000610 RID: 1552
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		// Token: 0x06003BC3 RID: 15299 RVA: 0x000D0BA6 File Offset: 0x000CEDA6
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000D0BC1 File Offset: 0x000CEDC1
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000D0BD7 File Offset: 0x000CEDD7
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000D0BDF File Offset: 0x000CEDDF
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x0400267A RID: 9850
		private ResourceScope _consumptionScope;

		// Token: 0x0400267B RID: 9851
		private ResourceScope _resourceScope;
	}
}
