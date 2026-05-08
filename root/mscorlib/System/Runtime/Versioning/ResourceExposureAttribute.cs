using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x02000611 RID: 1553
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		// Token: 0x06003BC7 RID: 15303 RVA: 0x000D0BE7 File Offset: 0x000CEDE7
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000D0BF6 File Offset: 0x000CEDF6
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x0400267C RID: 9852
		private ResourceScope _resourceExposureLevel;
	}
}
