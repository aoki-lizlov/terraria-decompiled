using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074C RID: 1868
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F59ED4E4-E68F-3218-BD77-061AA82824BF")]
	[TypeLibImportClass(typeof(PropertyInfo))]
	[ComVisible(true)]
	public interface _PropertyInfo
	{
		// Token: 0x06004374 RID: 17268
		bool Equals(object other);

		// Token: 0x06004375 RID: 17269
		MethodInfo[] GetAccessors();

		// Token: 0x06004376 RID: 17270
		MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x06004377 RID: 17271
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004378 RID: 17272
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004379 RID: 17273
		MethodInfo GetGetMethod();

		// Token: 0x0600437A RID: 17274
		MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x0600437B RID: 17275
		int GetHashCode();

		// Token: 0x0600437C RID: 17276
		ParameterInfo[] GetIndexParameters();

		// Token: 0x0600437D RID: 17277
		MethodInfo GetSetMethod();

		// Token: 0x0600437E RID: 17278
		MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x0600437F RID: 17279
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004380 RID: 17280
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004381 RID: 17281
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06004382 RID: 17282
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x06004383 RID: 17283
		Type GetType();

		// Token: 0x06004384 RID: 17284
		object GetValue(object obj, object[] index);

		// Token: 0x06004385 RID: 17285
		object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004386 RID: 17286
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x06004387 RID: 17287
		void SetValue(object obj, object value, object[] index);

		// Token: 0x06004388 RID: 17288
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004389 RID: 17289
		string ToString();

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600438A RID: 17290
		PropertyAttributes Attributes { get; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600438B RID: 17291
		bool CanRead { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600438C RID: 17292
		bool CanWrite { get; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600438D RID: 17293
		Type DeclaringType { get; }

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600438E RID: 17294
		bool IsSpecialName { get; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600438F RID: 17295
		MemberTypes MemberType { get; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06004390 RID: 17296
		string Name { get; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06004391 RID: 17297
		Type PropertyType { get; }

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06004392 RID: 17298
		Type ReflectedType { get; }
	}
}
