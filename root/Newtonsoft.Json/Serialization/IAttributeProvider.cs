using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000078 RID: 120
	public interface IAttributeProvider
	{
		// Token: 0x060005A1 RID: 1441
		IList<Attribute> GetAttributes(bool inherit);

		// Token: 0x060005A2 RID: 1442
		IList<Attribute> GetAttributes(Type attributeType, bool inherit);
	}
}
