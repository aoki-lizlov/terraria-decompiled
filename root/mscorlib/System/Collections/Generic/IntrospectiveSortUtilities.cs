using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B13 RID: 2835
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06006848 RID: 26696 RVA: 0x00161920 File Offset: 0x0015FB20
		internal static int FloorLog2PlusOne(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x0016193F File Offset: 0x0015FB3F
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			if (Environment.GetEnvironmentVariable("MONO_FORCE_COMPAT") == null)
			{
				throw new ArgumentException(SR.Format("Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.", comparer));
			}
		}

		// Token: 0x04003C5E RID: 15454
		internal const int IntrosortSizeThreshold = 16;
	}
}
