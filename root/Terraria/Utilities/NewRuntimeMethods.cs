using System;
using System.Reflection;

namespace Terraria.Utilities
{
	// Token: 0x020000C7 RID: 199
	public static class NewRuntimeMethods
	{
		// Token: 0x060017EC RID: 6124 RVA: 0x004E0BF0 File Offset: 0x004DEDF0
		public static void GC_Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			if (NewRuntimeMethods.IsNet45OrNewer)
			{
				MethodInfo methodInfo;
				if ((methodInfo = NewRuntimeMethods._collect) == null)
				{
					methodInfo = typeof(GC).GetMethod("Collect", BindingFlags.Static | BindingFlags.Public, null, new Type[]
					{
						typeof(int),
						typeof(GCCollectionMode),
						typeof(bool)
					}, null);
				}
				NewRuntimeMethods._collect = methodInfo;
				NewRuntimeMethods._collect.Invoke(null, new object[] { generation, mode, blocking });
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x004E0C87 File Offset: 0x004DEE87
		public static long GC_GetTotalAllocatedBytes()
		{
			return 0L;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x004BB77D File Offset: 0x004B997D
		public static TimeSpan GC_GetTotalPauseDuration()
		{
			return TimeSpan.Zero;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x004E0C8B File Offset: 0x004DEE8B
		// Note: this type is marked as 'beforefieldinit'.
		static NewRuntimeMethods()
		{
		}

		// Token: 0x040012B7 RID: 4791
		private static bool IsNet45OrNewer = Type.GetType("System.Reflection.ReflectionContext", false) != null;

		// Token: 0x040012B8 RID: 4792
		private static MethodInfo _collect;
	}
}
