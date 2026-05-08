using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073C RID: 1852
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9DE59C64-D889-35A1-B897-587D74469E5B")]
	[TypeLibImportClass(typeof(EventInfo))]
	[ComVisible(true)]
	public interface _EventInfo
	{
		// Token: 0x060042A8 RID: 17064
		void AddEventHandler(object target, Delegate handler);

		// Token: 0x060042A9 RID: 17065
		bool Equals(object other);

		// Token: 0x060042AA RID: 17066
		MethodInfo GetAddMethod();

		// Token: 0x060042AB RID: 17067
		MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x060042AC RID: 17068
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x060042AD RID: 17069
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x060042AE RID: 17070
		int GetHashCode();

		// Token: 0x060042AF RID: 17071
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042B0 RID: 17072
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042B1 RID: 17073
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042B2 RID: 17074
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x060042B3 RID: 17075
		MethodInfo GetRaiseMethod();

		// Token: 0x060042B4 RID: 17076
		MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x060042B5 RID: 17077
		MethodInfo GetRemoveMethod();

		// Token: 0x060042B6 RID: 17078
		MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x060042B7 RID: 17079
		Type GetType();

		// Token: 0x060042B8 RID: 17080
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x060042B9 RID: 17081
		void RemoveEventHandler(object target, Delegate handler);

		// Token: 0x060042BA RID: 17082
		string ToString();

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060042BB RID: 17083
		EventAttributes Attributes { get; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060042BC RID: 17084
		Type DeclaringType { get; }

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060042BD RID: 17085
		Type EventHandlerType { get; }

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060042BE RID: 17086
		bool IsMulticast { get; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060042BF RID: 17087
		bool IsSpecialName { get; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060042C0 RID: 17088
		MemberTypes MemberType { get; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060042C1 RID: 17089
		string Name { get; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060042C2 RID: 17090
		Type ReflectedType { get; }
	}
}
