using System;

namespace ReLogic.Utilities
{
	// Token: 0x0200000A RID: 10
	public static class XnaExtensions
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003891 File Offset: 0x00001A91
		public static T Get<T>(this IServiceProvider services) where T : class
		{
			return services.GetService(typeof(T)) as T;
		}
	}
}
