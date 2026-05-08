using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B2 RID: 178
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x00022F50 File Offset: 0x00021150
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00022F59 File Offset: 0x00021159
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00008020 File Offset: 0x00006220
		public JTokenEqualityComparer()
		{
		}
	}
}
