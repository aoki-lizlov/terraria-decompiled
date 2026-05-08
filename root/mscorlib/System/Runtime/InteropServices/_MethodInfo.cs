using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000745 RID: 1861
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("FFCC1B5D-ECB8-38DD-9B01-3DC8ABC2AA5F")]
	[TypeLibImportClass(typeof(MethodInfo))]
	[ComVisible(true)]
	public interface _MethodInfo
	{
		// Token: 0x06004336 RID: 17206
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004337 RID: 17207
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004338 RID: 17208
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004339 RID: 17209
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x0600433A RID: 17210
		string ToString();

		// Token: 0x0600433B RID: 17211
		bool Equals(object other);

		// Token: 0x0600433C RID: 17212
		int GetHashCode();

		// Token: 0x0600433D RID: 17213
		Type GetType();

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600433E RID: 17214
		MemberTypes MemberType { get; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600433F RID: 17215
		string Name { get; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004340 RID: 17216
		Type DeclaringType { get; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004341 RID: 17217
		Type ReflectedType { get; }

		// Token: 0x06004342 RID: 17218
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004343 RID: 17219
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004344 RID: 17220
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004345 RID: 17221
		ParameterInfo[] GetParameters();

		// Token: 0x06004346 RID: 17222
		MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06004347 RID: 17223
		RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06004348 RID: 17224
		MethodAttributes Attributes { get; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06004349 RID: 17225
		CallingConventions CallingConvention { get; }

		// Token: 0x0600434A RID: 17226
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x0600434B RID: 17227
		bool IsPublic { get; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x0600434C RID: 17228
		bool IsPrivate { get; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x0600434D RID: 17229
		bool IsFamily { get; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600434E RID: 17230
		bool IsAssembly { get; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600434F RID: 17231
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06004350 RID: 17232
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06004351 RID: 17233
		bool IsStatic { get; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06004352 RID: 17234
		bool IsFinal { get; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06004353 RID: 17235
		bool IsVirtual { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06004354 RID: 17236
		bool IsHideBySig { get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06004355 RID: 17237
		bool IsAbstract { get; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06004356 RID: 17238
		bool IsSpecialName { get; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06004357 RID: 17239
		bool IsConstructor { get; }

		// Token: 0x06004358 RID: 17240
		object Invoke(object obj, object[] parameters);

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06004359 RID: 17241
		Type ReturnType { get; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600435A RID: 17242
		ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x0600435B RID: 17243
		MethodInfo GetBaseDefinition();
	}
}
