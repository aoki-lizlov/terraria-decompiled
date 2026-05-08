using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073F RID: 1855
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
	[TypeLibImportClass(typeof(FieldInfo))]
	[ComVisible(true)]
	public interface _FieldInfo
	{
		// Token: 0x060042D5 RID: 17109
		bool Equals(object other);

		// Token: 0x060042D6 RID: 17110
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x060042D7 RID: 17111
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x060042D8 RID: 17112
		int GetHashCode();

		// Token: 0x060042D9 RID: 17113
		Type GetType();

		// Token: 0x060042DA RID: 17114
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060042DB RID: 17115
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060042DC RID: 17116
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060042DD RID: 17117
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x060042DE RID: 17118
		object GetValue(object obj);

		// Token: 0x060042DF RID: 17119
		object GetValueDirect(TypedReference obj);

		// Token: 0x060042E0 RID: 17120
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x060042E1 RID: 17121
		void SetValue(object obj, object value);

		// Token: 0x060042E2 RID: 17122
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x060042E3 RID: 17123
		void SetValueDirect(TypedReference obj, object value);

		// Token: 0x060042E4 RID: 17124
		string ToString();

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060042E5 RID: 17125
		FieldAttributes Attributes { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060042E6 RID: 17126
		Type DeclaringType { get; }

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060042E7 RID: 17127
		RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060042E8 RID: 17128
		Type FieldType { get; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060042E9 RID: 17129
		bool IsAssembly { get; }

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060042EA RID: 17130
		bool IsFamily { get; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060042EB RID: 17131
		bool IsFamilyAndAssembly { get; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060042EC RID: 17132
		bool IsFamilyOrAssembly { get; }

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060042ED RID: 17133
		bool IsInitOnly { get; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060042EE RID: 17134
		bool IsLiteral { get; }

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060042EF RID: 17135
		bool IsNotSerialized { get; }

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060042F0 RID: 17136
		bool IsPinvokeImpl { get; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060042F1 RID: 17137
		bool IsPrivate { get; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060042F2 RID: 17138
		bool IsPublic { get; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060042F3 RID: 17139
		bool IsSpecialName { get; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060042F4 RID: 17140
		bool IsStatic { get; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060042F5 RID: 17141
		MemberTypes MemberType { get; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060042F6 RID: 17142
		string Name { get; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060042F7 RID: 17143
		Type ReflectedType { get; }
	}
}
