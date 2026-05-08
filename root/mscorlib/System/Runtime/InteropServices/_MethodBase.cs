using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000743 RID: 1859
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("6240837A-707F-3181-8E98-A36AE086766B")]
	[TypeLibImportClass(typeof(MethodBase))]
	[ComVisible(true)]
	public interface _MethodBase
	{
		// Token: 0x0600430F RID: 17167
		bool Equals(object other);

		// Token: 0x06004310 RID: 17168
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004311 RID: 17169
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004312 RID: 17170
		int GetHashCode();

		// Token: 0x06004313 RID: 17171
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x06004314 RID: 17172
		ParameterInfo[] GetParameters();

		// Token: 0x06004315 RID: 17173
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004316 RID: 17174
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004317 RID: 17175
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004318 RID: 17176
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06004319 RID: 17177
		Type GetType();

		// Token: 0x0600431A RID: 17178
		object Invoke(object obj, object[] parameters);

		// Token: 0x0600431B RID: 17179
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x0600431C RID: 17180
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x0600431D RID: 17181
		string ToString();

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600431E RID: 17182
		MethodAttributes Attributes { get; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600431F RID: 17183
		CallingConventions CallingConvention { get; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06004320 RID: 17184
		Type DeclaringType { get; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06004321 RID: 17185
		bool IsAbstract { get; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06004322 RID: 17186
		bool IsAssembly { get; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06004323 RID: 17187
		bool IsConstructor { get; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06004324 RID: 17188
		bool IsFamily { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06004325 RID: 17189
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06004326 RID: 17190
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06004327 RID: 17191
		bool IsFinal { get; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06004328 RID: 17192
		bool IsHideBySig { get; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06004329 RID: 17193
		bool IsPrivate { get; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600432A RID: 17194
		bool IsPublic { get; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600432B RID: 17195
		bool IsSpecialName { get; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600432C RID: 17196
		bool IsStatic { get; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600432D RID: 17197
		bool IsVirtual { get; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600432E RID: 17198
		MemberTypes MemberType { get; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600432F RID: 17199
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06004330 RID: 17200
		string Name { get; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06004331 RID: 17201
		Type ReflectedType { get; }
	}
}
