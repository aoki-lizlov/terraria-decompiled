using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007E RID: 126
	public class ReflectionAttributeProvider : IAttributeProvider
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x000183DF File Offset: 0x000165DF
		public ReflectionAttributeProvider(object attributeProvider)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000183F9 File Offset: 0x000165F9
		public IList<Attribute> GetAttributes(bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, null, inherit);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018408 File Offset: 0x00016608
		public IList<Attribute> GetAttributes(Type attributeType, bool inherit)
		{
			return ReflectionUtils.GetAttributes(this._attributeProvider, attributeType, inherit);
		}

		// Token: 0x04000278 RID: 632
		private readonly object _attributeProvider;
	}
}
