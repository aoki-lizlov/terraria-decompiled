using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200074F RID: 1871
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
	[TypeLibImportClass(typeof(Type))]
	[ComVisible(true)]
	public interface _Type
	{
		// Token: 0x0600439B RID: 17307
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600439C RID: 17308
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600439D RID: 17309
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600439E RID: 17310
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		// Token: 0x0600439F RID: 17311
		string ToString();

		// Token: 0x060043A0 RID: 17312
		bool Equals(object other);

		// Token: 0x060043A1 RID: 17313
		int GetHashCode();

		// Token: 0x060043A2 RID: 17314
		Type GetType();

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060043A3 RID: 17315
		MemberTypes MemberType { get; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060043A4 RID: 17316
		string Name { get; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060043A5 RID: 17317
		Type DeclaringType { get; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060043A6 RID: 17318
		Type ReflectedType { get; }

		// Token: 0x060043A7 RID: 17319
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x060043A8 RID: 17320
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x060043A9 RID: 17321
		bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060043AA RID: 17322
		Guid GUID { get; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060043AB RID: 17323
		Module Module { get; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060043AC RID: 17324
		Assembly Assembly { get; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060043AD RID: 17325
		RuntimeTypeHandle TypeHandle { get; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060043AE RID: 17326
		string FullName { get; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060043AF RID: 17327
		string Namespace { get; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060043B0 RID: 17328
		string AssemblyQualifiedName { get; }

		// Token: 0x060043B1 RID: 17329
		int GetArrayRank();

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060043B2 RID: 17330
		Type BaseType { get; }

		// Token: 0x060043B3 RID: 17331
		ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x060043B4 RID: 17332
		Type GetInterface(string name, bool ignoreCase);

		// Token: 0x060043B5 RID: 17333
		Type[] GetInterfaces();

		// Token: 0x060043B6 RID: 17334
		Type[] FindInterfaces(TypeFilter filter, object filterCriteria);

		// Token: 0x060043B7 RID: 17335
		EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x060043B8 RID: 17336
		EventInfo[] GetEvents();

		// Token: 0x060043B9 RID: 17337
		EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x060043BA RID: 17338
		Type[] GetNestedTypes(BindingFlags bindingAttr);

		// Token: 0x060043BB RID: 17339
		Type GetNestedType(string name, BindingFlags bindingAttr);

		// Token: 0x060043BC RID: 17340
		MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr);

		// Token: 0x060043BD RID: 17341
		MemberInfo[] GetDefaultMembers();

		// Token: 0x060043BE RID: 17342
		MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria);

		// Token: 0x060043BF RID: 17343
		Type GetElementType();

		// Token: 0x060043C0 RID: 17344
		bool IsSubclassOf(Type c);

		// Token: 0x060043C1 RID: 17345
		bool IsInstanceOfType(object o);

		// Token: 0x060043C2 RID: 17346
		bool IsAssignableFrom(Type c);

		// Token: 0x060043C3 RID: 17347
		InterfaceMapping GetInterfaceMap(Type interfaceType);

		// Token: 0x060043C4 RID: 17348
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043C5 RID: 17349
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060043C6 RID: 17350
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060043C7 RID: 17351
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060043C8 RID: 17352
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060043C9 RID: 17353
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060043CA RID: 17354
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043CB RID: 17355
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060043CC RID: 17356
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060043CD RID: 17357
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060043CE RID: 17358
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060043CF RID: 17359
		Type UnderlyingSystemType { get; }

		// Token: 0x060043D0 RID: 17360
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture);

		// Token: 0x060043D1 RID: 17361
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args);

		// Token: 0x060043D2 RID: 17362
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043D3 RID: 17363
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043D4 RID: 17364
		ConstructorInfo GetConstructor(Type[] types);

		// Token: 0x060043D5 RID: 17365
		ConstructorInfo[] GetConstructors();

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060043D6 RID: 17366
		ConstructorInfo TypeInitializer { get; }

		// Token: 0x060043D7 RID: 17367
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043D8 RID: 17368
		MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043D9 RID: 17369
		MethodInfo GetMethod(string name, Type[] types);

		// Token: 0x060043DA RID: 17370
		MethodInfo GetMethod(string name);

		// Token: 0x060043DB RID: 17371
		MethodInfo[] GetMethods();

		// Token: 0x060043DC RID: 17372
		FieldInfo GetField(string name);

		// Token: 0x060043DD RID: 17373
		FieldInfo[] GetFields();

		// Token: 0x060043DE RID: 17374
		Type GetInterface(string name);

		// Token: 0x060043DF RID: 17375
		EventInfo GetEvent(string name);

		// Token: 0x060043E0 RID: 17376
		PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043E1 RID: 17377
		PropertyInfo GetProperty(string name, Type returnType, Type[] types);

		// Token: 0x060043E2 RID: 17378
		PropertyInfo GetProperty(string name, Type[] types);

		// Token: 0x060043E3 RID: 17379
		PropertyInfo GetProperty(string name, Type returnType);

		// Token: 0x060043E4 RID: 17380
		PropertyInfo GetProperty(string name);

		// Token: 0x060043E5 RID: 17381
		PropertyInfo[] GetProperties();

		// Token: 0x060043E6 RID: 17382
		Type[] GetNestedTypes();

		// Token: 0x060043E7 RID: 17383
		Type GetNestedType(string name);

		// Token: 0x060043E8 RID: 17384
		MemberInfo[] GetMember(string name);

		// Token: 0x060043E9 RID: 17385
		MemberInfo[] GetMembers();

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060043EA RID: 17386
		TypeAttributes Attributes { get; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060043EB RID: 17387
		bool IsNotPublic { get; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060043EC RID: 17388
		bool IsPublic { get; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060043ED RID: 17389
		bool IsNestedPublic { get; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060043EE RID: 17390
		bool IsNestedPrivate { get; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060043EF RID: 17391
		bool IsNestedFamily { get; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060043F0 RID: 17392
		bool IsNestedAssembly { get; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060043F1 RID: 17393
		bool IsNestedFamANDAssem { get; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060043F2 RID: 17394
		bool IsNestedFamORAssem { get; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060043F3 RID: 17395
		bool IsAutoLayout { get; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060043F4 RID: 17396
		bool IsLayoutSequential { get; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060043F5 RID: 17397
		bool IsExplicitLayout { get; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060043F6 RID: 17398
		bool IsClass { get; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060043F7 RID: 17399
		bool IsInterface { get; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060043F8 RID: 17400
		bool IsValueType { get; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060043F9 RID: 17401
		bool IsAbstract { get; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060043FA RID: 17402
		bool IsSealed { get; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060043FB RID: 17403
		bool IsEnum { get; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060043FC RID: 17404
		bool IsSpecialName { get; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060043FD RID: 17405
		bool IsImport { get; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060043FE RID: 17406
		bool IsSerializable { get; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060043FF RID: 17407
		bool IsAnsiClass { get; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004400 RID: 17408
		bool IsUnicodeClass { get; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004401 RID: 17409
		bool IsAutoClass { get; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004402 RID: 17410
		bool IsArray { get; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004403 RID: 17411
		bool IsByRef { get; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004404 RID: 17412
		bool IsPointer { get; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004405 RID: 17413
		bool IsPrimitive { get; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004406 RID: 17414
		bool IsCOMObject { get; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06004407 RID: 17415
		bool HasElementType { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004408 RID: 17416
		bool IsContextful { get; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004409 RID: 17417
		bool IsMarshalByRef { get; }

		// Token: 0x0600440A RID: 17418
		bool Equals(Type o);
	}
}
