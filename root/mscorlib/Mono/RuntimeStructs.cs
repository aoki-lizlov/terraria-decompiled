using System;

namespace Mono
{
	// Token: 0x02000030 RID: 48
	internal static class RuntimeStructs
	{
		// Token: 0x02000031 RID: 49
		internal struct RemoteClass
		{
			// Token: 0x04000CE4 RID: 3300
			internal IntPtr default_vtable;

			// Token: 0x04000CE5 RID: 3301
			internal IntPtr xdomain_vtable;

			// Token: 0x04000CE6 RID: 3302
			internal unsafe RuntimeStructs.MonoClass* proxy_class;

			// Token: 0x04000CE7 RID: 3303
			internal IntPtr proxy_class_name;

			// Token: 0x04000CE8 RID: 3304
			internal uint interface_count;
		}

		// Token: 0x02000032 RID: 50
		internal struct MonoClass
		{
		}

		// Token: 0x02000033 RID: 51
		internal struct GenericParamInfo
		{
			// Token: 0x04000CE9 RID: 3305
			internal unsafe RuntimeStructs.MonoClass* pklass;

			// Token: 0x04000CEA RID: 3306
			internal IntPtr name;

			// Token: 0x04000CEB RID: 3307
			internal ushort flags;

			// Token: 0x04000CEC RID: 3308
			internal uint token;

			// Token: 0x04000CED RID: 3309
			internal unsafe RuntimeStructs.MonoClass** constraints;
		}

		// Token: 0x02000034 RID: 52
		internal struct GPtrArray
		{
			// Token: 0x04000CEE RID: 3310
			internal unsafe IntPtr* data;

			// Token: 0x04000CEF RID: 3311
			internal int len;
		}
	}
}
