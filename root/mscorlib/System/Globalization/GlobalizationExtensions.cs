using System;

namespace System.Globalization
{
	// Token: 0x020009BE RID: 2494
	public static class GlobalizationExtensions
	{
		// Token: 0x06005B56 RID: 23382 RVA: 0x00136EC8 File Offset: 0x001350C8
		public static StringComparer GetStringComparer(this CompareInfo compareInfo, CompareOptions options)
		{
			if (compareInfo == null)
			{
				throw new ArgumentNullException("compareInfo");
			}
			if (options == CompareOptions.Ordinal)
			{
				return StringComparer.Ordinal;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return StringComparer.OrdinalIgnoreCase;
			}
			return new CultureAwareComparer(compareInfo, options);
		}
	}
}
