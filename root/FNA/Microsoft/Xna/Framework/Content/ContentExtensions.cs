using System;
using System.Reflection;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000103 RID: 259
	internal static class ContentExtensions
	{
		// Token: 0x060016E1 RID: 5857 RVA: 0x00037917 File Offset: 0x00035B17
		public static ConstructorInfo GetDefaultConstructor(this Type type)
		{
			return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], null);
		}
	}
}
