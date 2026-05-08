using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000742 RID: 1858
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
	[TypeLibImportClass(typeof(MemberInfo))]
	[ComVisible(true)]
	public interface _MemberInfo
	{
		// Token: 0x06004300 RID: 17152
		bool Equals(object other);

		// Token: 0x06004301 RID: 17153
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004302 RID: 17154
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004303 RID: 17155
		int GetHashCode();

		// Token: 0x06004304 RID: 17156
		Type GetType();

		// Token: 0x06004305 RID: 17157
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004306 RID: 17158
		string ToString();

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06004307 RID: 17159
		Type DeclaringType { get; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06004308 RID: 17160
		MemberTypes MemberType { get; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06004309 RID: 17161
		string Name { get; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600430A RID: 17162
		Type ReflectedType { get; }

		// Token: 0x0600430B RID: 17163
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600430C RID: 17164
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600430D RID: 17165
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600430E RID: 17166
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
