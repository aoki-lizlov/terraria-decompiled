using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000738 RID: 1848
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E9A19478-9646-3679-9B10-8411AE1FD57D")]
	[TypeLibImportClass(typeof(ConstructorInfo))]
	[ComVisible(true)]
	public interface _ConstructorInfo
	{
		// Token: 0x06004277 RID: 17015
		bool Equals(object other);

		// Token: 0x06004278 RID: 17016
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004279 RID: 17017
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x0600427A RID: 17018
		int GetHashCode();

		// Token: 0x0600427B RID: 17019
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x0600427C RID: 17020
		ParameterInfo[] GetParameters();

		// Token: 0x0600427D RID: 17021
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600427E RID: 17022
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600427F RID: 17023
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004280 RID: 17024
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06004281 RID: 17025
		Type GetType();

		// Token: 0x06004282 RID: 17026
		object Invoke_5(object[] parameters);

		// Token: 0x06004283 RID: 17027
		object Invoke_3(object obj, object[] parameters);

		// Token: 0x06004284 RID: 17028
		object Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x06004285 RID: 17029
		object Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x06004286 RID: 17030
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004287 RID: 17031
		string ToString();

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06004288 RID: 17032
		MethodAttributes Attributes { get; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06004289 RID: 17033
		CallingConventions CallingConvention { get; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600428A RID: 17034
		Type DeclaringType { get; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600428B RID: 17035
		bool IsAbstract { get; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600428C RID: 17036
		bool IsAssembly { get; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600428D RID: 17037
		bool IsConstructor { get; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600428E RID: 17038
		bool IsFamily { get; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600428F RID: 17039
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06004290 RID: 17040
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06004291 RID: 17041
		bool IsFinal { get; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06004292 RID: 17042
		bool IsHideBySig { get; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06004293 RID: 17043
		bool IsPrivate { get; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06004294 RID: 17044
		bool IsPublic { get; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06004295 RID: 17045
		bool IsSpecialName { get; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004296 RID: 17046
		bool IsStatic { get; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004297 RID: 17047
		bool IsVirtual { get; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06004298 RID: 17048
		MemberTypes MemberType { get; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06004299 RID: 17049
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600429A RID: 17050
		string Name { get; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600429B RID: 17051
		Type ReflectedType { get; }
	}
}
