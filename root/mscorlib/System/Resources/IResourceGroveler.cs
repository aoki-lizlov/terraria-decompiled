using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x02000836 RID: 2102
	internal interface IResourceGroveler
	{
		// Token: 0x06004704 RID: 18180
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06004705 RID: 18181
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
