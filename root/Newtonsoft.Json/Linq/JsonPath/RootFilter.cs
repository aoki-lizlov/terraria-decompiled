using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CB RID: 203
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00029260 File Offset: 0x00027460
		private RootFilter()
		{
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00017F2F File Offset: 0x0001612F
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			return root;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002A86B File Offset: 0x00028A6B
		// Note: this type is marked as 'beforefieldinit'.
		static RootFilter()
		{
		}

		// Token: 0x04000391 RID: 913
		public static readonly RootFilter Instance = new RootFilter();
	}
}
