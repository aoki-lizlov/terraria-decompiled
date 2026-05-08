using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000178 RID: 376
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class Type : MemberInfo, IReflect, _Type
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x000470F4 File Offset: 0x000452F4
		public virtual bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(this))
				{
					throw new ArgumentException(SR.Format("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", type.ToString(), this.ToString()));
				}
				type = type.GetEnumUnderlyingType();
			}
			if (type == typeof(string))
			{
				object[] enumNames = this.GetEnumNames();
				return Array.IndexOf<object>(enumNames, value) >= 0;
			}
			if (!Type.IsIntegerType(type))
			{
				throw new InvalidOperationException("Unknown enum type.");
			}
			Type enumUnderlyingType = this.GetEnumUnderlyingType();
			if (enumUnderlyingType.GetTypeCodeImpl() != type.GetTypeCodeImpl())
			{
				throw new ArgumentException(SR.Format("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", type.ToString(), enumUnderlyingType.ToString()));
			}
			return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000471E0 File Offset: 0x000453E0
		public virtual string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException("The value passed in must be an enum base or an underlying type for an enum, such as an Int32.", "value");
			}
			int num = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
			if (num >= 0)
			{
				return this.GetEnumNames()[num];
			}
			return null;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00047258 File Offset: 0x00045458
		public virtual string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00047288 File Offset: 0x00045488
		private Array GetEnumRawConstantValues()
		{
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array2;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000472A0 File Offset: 0x000454A0
		private void GetEnumData(out string[] enumNames, out Array enumValues)
		{
			FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			object[] array = new object[fields.Length];
			string[] array2 = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array2[i] = fields[i].Name;
				array[i] = fields[i].GetRawConstantValue();
			}
			IComparer @default = Comparer<object>.Default;
			for (int j = 1; j < array.Length; j++)
			{
				int num = j;
				string text = array2[j];
				object obj = array[j];
				bool flag = false;
				while (@default.Compare(array[num - 1], obj) > 0)
				{
					array2[num] = array2[num - 1];
					array[num] = array[num - 1];
					num--;
					flag = true;
					if (num == 0)
					{
						break;
					}
				}
				if (flag)
				{
					array2[num] = text;
					array[num] = obj;
				}
			}
			enumNames = array2;
			enumValues = array;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004736C File Offset: 0x0004556C
		private static int BinarySearch(Array array, object value)
		{
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Enum.ToUInt64(array.GetValue(i));
			}
			ulong num = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array2, num);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000473B4 File Offset: 0x000455B4
		internal static bool IsIntegerType(Type t)
		{
			return t == typeof(int) || t == typeof(short) || t == typeof(ushort) || t == typeof(byte) || t == typeof(sbyte) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong) || t == typeof(char) || t == typeof(bool);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0004747C File Offset: 0x0004567C
		public virtual bool IsSerializable
		{
			get
			{
				if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type.IsRuntimeImplemented())
				{
					while (!(type == typeof(Delegate)) && !(type == typeof(Enum)))
					{
						type = type.BaseType;
						if (!(type != null))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x000474E0 File Offset: 0x000456E0
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.HasElementType)
				{
					return this.GetRootElementType().ContainsGenericParameters;
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (!this.IsGenericType)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00047538 File Offset: 0x00045738
		internal Type GetRootElementType()
		{
			Type type = this;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			return type;
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004755C File Offset: 0x0004575C
		public bool IsVisible
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.HasElementType)
				{
					return this.GetElementType().IsVisible;
				}
				Type type = this;
				while (type.IsNested)
				{
					if (!type.IsNestedPublic)
					{
						return false;
					}
					type = type.DeclaringType;
				}
				if (!type.IsPublic)
				{
					return false;
				}
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (!genericArguments[i].IsVisible)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000475E0 File Offset: 0x000457E0
		public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			Type[] interfaces = this.GetInterfaces();
			int num = 0;
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (!filter(interfaces[i], filterCriteria))
				{
					interfaces[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == interfaces.Length)
			{
				return interfaces;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] != null)
				{
					array[num++] = interfaces[j];
				}
			}
			return array;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00047664 File Offset: 0x00045864
		public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			MethodInfo[] array = null;
			ConstructorInfo[] array2 = null;
			FieldInfo[] array3 = null;
			PropertyInfo[] array4 = null;
			EventInfo[] array5 = null;
			Type[] array6 = null;
			int num = 0;
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				array = this.GetMethods(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!filter(array[i], filterCriteria))
						{
							array[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array.Length;
				}
			}
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				array2 = this.GetConstructors(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (!filter(array2[i], filterCriteria))
						{
							array2[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array2.Length;
				}
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				array3 = this.GetFields(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array3.Length; i++)
					{
						if (!filter(array3[i], filterCriteria))
						{
							array3[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array3.Length;
				}
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				array4 = this.GetProperties(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array4.Length; i++)
					{
						if (!filter(array4[i], filterCriteria))
						{
							array4[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array4.Length;
				}
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				array5 = this.GetEvents(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array5.Length; i++)
					{
						if (!filter(array5[i], filterCriteria))
						{
							array5[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array5.Length;
				}
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				array6 = this.GetNestedTypes(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array6.Length; i++)
					{
						if (!filter(array6[i], filterCriteria))
						{
							array6[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array6.Length;
				}
			}
			MemberInfo[] array7 = new MemberInfo[num];
			num = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array7[num++] = array[i];
					}
				}
			}
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != null)
					{
						array7[num++] = array2[i];
					}
				}
			}
			if (array3 != null)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != null)
					{
						array7[num++] = array3[i];
					}
				}
			}
			if (array4 != null)
			{
				for (int i = 0; i < array4.Length; i++)
				{
					if (array4[i] != null)
					{
						array7[num++] = array4[i];
					}
				}
			}
			if (array5 != null)
			{
				for (int i = 0; i < array5.Length; i++)
				{
					if (array5[i] != null)
					{
						array7[num++] = array5[i];
					}
				}
			}
			if (array6 != null)
			{
				for (int i = 0; i < array6.Length; i++)
				{
					if (array6[i] != null)
					{
						array7[num++] = array6[i];
					}
				}
			}
			return array7;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00047970 File Offset: 0x00045B70
		public virtual bool IsSubclassOf(Type c)
		{
			Type type = this;
			if (type == c)
			{
				return false;
			}
			while (type != null)
			{
				if (type == c)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000479A8 File Offset: 0x00045BA8
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this == c)
			{
				return true;
			}
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType.IsRuntimeImplemented())
			{
				return underlyingSystemType.IsAssignableFrom(c);
			}
			if (c.IsSubclassOf(this))
			{
				return true;
			}
			if (this.IsInterface)
			{
				return c.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(c))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00047A2C File Offset: 0x00045C2C
		internal bool ImplementInterface(Type ifaceType)
		{
			Type type = this;
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null)
				{
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (interfaces[i] == ifaceType || (interfaces[i] != null && interfaces[i].ImplementInterface(ifaceType)))
						{
							return true;
						}
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00047A8C File Offset: 0x00045C8C
		private static bool FilterAttributeImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null)
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MemberTypes memberType = m.MemberType;
			if (memberType != MemberTypes.Constructor)
			{
				if (memberType == MemberTypes.Field)
				{
					FieldAttributes fieldAttributes = FieldAttributes.PrivateScope;
					try
					{
						fieldAttributes = (FieldAttributes)((int)filterCriteria);
					}
					catch
					{
						throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
					}
					FieldAttributes attributes = ((FieldInfo)m).Attributes;
					return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope);
				}
				if (memberType != MemberTypes.Method)
				{
					return false;
				}
			}
			MethodAttributes methodAttributes = MethodAttributes.PrivateScope;
			try
			{
				methodAttributes = (MethodAttributes)((int)filterCriteria);
			}
			catch
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MethodAttributes methodAttributes2;
			if (m.MemberType == MemberTypes.Method)
			{
				methodAttributes2 = ((MethodInfo)m).Attributes;
			}
			else
			{
				methodAttributes2 = ((ConstructorInfo)m).Attributes;
			}
			return ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes & MethodAttributes.MemberAccessMask)) && ((methodAttributes & MethodAttributes.Static) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Final) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (methodAttributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00047C08 File Offset: 0x00045E08
		private static bool FilterNameImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return text2.StartsWith(text, StringComparison.Ordinal);
			}
			return text2.Equals(text);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00047C9C File Offset: 0x00045E9C
		private static bool FilterNameIgnoreCaseImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return string.Compare(text2, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected Type()
		{
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00047D44 File Offset: 0x00045F44
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00047D48 File Offset: 0x00045F48
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600110C RID: 4364
		public abstract string Namespace { get; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600110D RID: 4365
		public abstract string AssemblyQualifiedName { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600110E RID: 4366
		public abstract string FullName { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600110F RID: 4367
		public abstract Assembly Assembly { get; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001110 RID: 4368
		public new abstract Module Module { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00047D50 File Offset: 0x00045F50
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001115 RID: 4373
		public abstract Type UnderlyingSystemType { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsTypeDefinition
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00047D65 File Offset: 0x00045F65
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		// Token: 0x06001118 RID: 4376
		protected abstract bool IsArrayImpl();

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00047D6D File Offset: 0x00045F6D
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		// Token: 0x0600111A RID: 4378
		protected abstract bool IsByRefImpl();

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00047D75 File Offset: 0x00045F75
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		// Token: 0x0600111C RID: 4380
		protected abstract bool IsPointerImpl();

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsConstructedGenericType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00047D7D File Offset: 0x00045F7D
		public virtual bool IsGenericTypeParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod == null;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00047D95 File Offset: 0x00045F95
		public virtual bool IsGenericMethodParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod != null;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSZArray
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00047DAD File Offset: 0x00045FAD
		public virtual bool IsVariableBoundArray
		{
			get
			{
				return this.IsArray && !this.IsSZArray;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual bool IsByRefLike
		{
			get
			{
				throw new NotSupportedException("Derived classes must provide an implementation.");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00047DCE File Offset: 0x00045FCE
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		// Token: 0x06001127 RID: 4391
		protected abstract bool HasElementTypeImpl();

		// Token: 0x06001128 RID: 4392
		public abstract Type GetElementType();

		// Token: 0x06001129 RID: 4393 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00047DD6 File Offset: 0x00045FD6
		public virtual Type[] GenericTypeArguments
		{
			get
			{
				if (!this.IsGenericType || this.IsGenericTypeDefinition)
				{
					return Array.Empty<Type>();
				}
				return this.GetGenericArguments();
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00047DF4 File Offset: 0x00045FF4
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00047E07 File Offset: 0x00046007
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
			throw new InvalidOperationException();
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00047E21 File Offset: 0x00046021
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		// Token: 0x06001131 RID: 4401
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00047E29 File Offset: 0x00046029
		public bool IsAbstract
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x00047E3A File Offset: 0x0004603A
		public bool IsImport
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00047E4B File Offset: 0x0004604B
		public bool IsSealed
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00047E5C File Offset: 0x0004605C
		public bool IsSpecialName
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00047E6D File Offset: 0x0004606D
		public bool IsClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00047E85 File Offset: 0x00046085
		public bool IsNestedAssembly
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00047E92 File Offset: 0x00046092
		public bool IsNestedFamANDAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00047E9F File Offset: 0x0004609F
		public bool IsNestedFamily
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00047EAC File Offset: 0x000460AC
		public bool IsNestedFamORAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00047EB9 File Offset: 0x000460B9
		public bool IsNestedPrivate
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00047EC6 File Offset: 0x000460C6
		public bool IsNestedPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00047ED3 File Offset: 0x000460D3
		public bool IsNotPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00047EE0 File Offset: 0x000460E0
		public bool IsPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00047EED File Offset: 0x000460ED
		public bool IsAutoLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00047EFB File Offset: 0x000460FB
		public bool IsExplicitLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00047F0A File Offset: 0x0004610A
		public bool IsLayoutSequential
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00047F18 File Offset: 0x00046118
		public bool IsAnsiClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00047F29 File Offset: 0x00046129
		public bool IsAutoClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00047F3E File Offset: 0x0004613E
		public bool IsUnicodeClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00047F53 File Offset: 0x00046153
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		// Token: 0x06001146 RID: 4422
		protected abstract bool IsCOMObjectImpl();

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00047F5B File Offset: 0x0004615B
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00047F63 File Offset: 0x00046163
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool IsCollectible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00047F75 File Offset: 0x00046175
		public virtual bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00047F87 File Offset: 0x00046187
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00047F8F File Offset: 0x0004618F
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00047FA1 File Offset: 0x000461A1
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		// Token: 0x0600114E RID: 4430
		protected abstract bool IsPrimitiveImpl();

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00047FA9 File Offset: 0x000461A9
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00047FB1 File Offset: 0x000461B1
		protected virtual bool IsValueTypeImpl()
		{
			return this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsSignatureType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00047FC3 File Offset: 0x000461C3
		public ConstructorInfo TypeInitializer
		{
			get
			{
				return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00047FD5 File Offset: 0x000461D5
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00047FE2 File Offset: 0x000461E2
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00047FF0 File Offset: 0x000461F0
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x0600115A RID: 4442
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600115B RID: 4443 RVA: 0x0004803F File Offset: 0x0004623F
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600115C RID: 4444
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		// Token: 0x0600115D RID: 4445 RVA: 0x00048049 File Offset: 0x00046249
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600115E RID: 4446
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x0600115F RID: 4447 RVA: 0x00048054 File Offset: 0x00046254
		public virtual EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001160 RID: 4448
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		// Token: 0x06001161 RID: 4449 RVA: 0x0004805E File Offset: 0x0004625E
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001162 RID: 4450
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06001163 RID: 4451 RVA: 0x00048069 File Offset: 0x00046269
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001164 RID: 4452
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06001165 RID: 4453 RVA: 0x00048073 File Offset: 0x00046273
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004807E File Offset: 0x0004627E
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0004808D File Offset: 0x0004628D
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001169 RID: 4457
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x0600116A RID: 4458 RVA: 0x00048097 File Offset: 0x00046297
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000480A2 File Offset: 0x000462A2
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000480BE File Offset: 0x000462BE
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, types, null);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000480C9 File Offset: 0x000462C9
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000480D7 File Offset: 0x000462D7
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000480E8 File Offset: 0x000462E8
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001170 RID: 4464
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001171 RID: 4465 RVA: 0x00048147 File Offset: 0x00046347
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types)
		{
			return this.GetMethod(name, genericParameterCount, types, null);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00048153 File Offset: 0x00046353
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00048163 File Offset: 0x00046363
		public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00048178 File Offset: 0x00046378
		public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (genericParameterCount < 0)
			{
				throw new ArgumentException("Non-negative number required.", "genericParameterCount");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, genericParameterCount, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00047E00 File Offset: 0x00046000
		protected virtual MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000481ED File Offset: 0x000463ED
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001177 RID: 4471
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06001178 RID: 4472 RVA: 0x000481F7 File Offset: 0x000463F7
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001179 RID: 4473
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		// Token: 0x0600117A RID: 4474 RVA: 0x00048202 File Offset: 0x00046402
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600117B RID: 4475
		public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

		// Token: 0x0600117C RID: 4476 RVA: 0x0004820C File Offset: 0x0004640C
		public PropertyInfo GetProperty(string name)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00048217 File Offset: 0x00046417
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00048233 File Offset: 0x00046433
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00048264 File Offset: 0x00046464
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			return this.GetProperty(name, null, types);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0004826F File Offset: 0x0004646F
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, returnType, types, null);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004827B File Offset: 0x0004647B
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004828B File Offset: 0x0004648B
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		// Token: 0x06001183 RID: 4483
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001184 RID: 4484 RVA: 0x000482B9 File Offset: 0x000464B9
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		// Token: 0x06001185 RID: 4485
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06001186 RID: 4486 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual MemberInfo[] GetDefaultMembers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000482C3 File Offset: 0x000464C3
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(null, "Invalid handle.");
			}
			return o.GetType().TypeHandle;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000482E0 File Offset: 0x000464E0
		public static Type[] GetTypeArray(object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			Type[] array = new Type[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (args[i] == null)
				{
					throw new ArgumentNullException();
				}
				array[i] = args[i].GetType();
			}
			return array;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00048329 File Offset: 0x00046529
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			return type.GetTypeCodeImpl();
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0004833C File Offset: 0x0004653C
		protected virtual TypeCode GetTypeCodeImpl()
		{
			if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != null)
			{
				return Type.GetTypeCode(this.UnderlyingSystemType);
			}
			return TypeCode.Object;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600118C RID: 4492
		public abstract Guid GUID { get; }

		// Token: 0x0600118D RID: 4493 RVA: 0x00048367 File Offset: 0x00046567
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return Type.GetTypeFromCLSID(clsid, null, false);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00048371 File Offset: 0x00046571
		public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
		{
			return Type.GetTypeFromCLSID(clsid, null, throwOnError);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004837B File Offset: 0x0004657B
		public static Type GetTypeFromCLSID(Guid clsid, string server)
		{
			return Type.GetTypeFromCLSID(clsid, server, false);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00048385 File Offset: 0x00046585
		public static Type GetTypeFromProgID(string progID)
		{
			return Type.GetTypeFromProgID(progID, null, false);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0004838F File Offset: 0x0004658F
		public static Type GetTypeFromProgID(string progID, bool throwOnError)
		{
			return Type.GetTypeFromProgID(progID, null, throwOnError);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00048399 File Offset: 0x00046599
		public static Type GetTypeFromProgID(string progID, string server)
		{
			return Type.GetTypeFromProgID(progID, server, false);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001193 RID: 4499
		public abstract Type BaseType { get; }

		// Token: 0x06001194 RID: 4500 RVA: 0x000483A4 File Offset: 0x000465A4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, null, null);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000483C4 File Offset: 0x000465C4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, culture, null);
		}

		// Token: 0x06001196 RID: 4502
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x06001197 RID: 4503 RVA: 0x000483E2 File Offset: 0x000465E2
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		// Token: 0x06001198 RID: 4504
		public abstract Type GetInterface(string name, bool ignoreCase);

		// Token: 0x06001199 RID: 4505
		public abstract Type[] GetInterfaces();

		// Token: 0x0600119A RID: 4506 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000483EC File Offset: 0x000465EC
		public virtual bool IsInstanceOfType(object o)
		{
			return o != null && this.IsAssignableFrom(o.GetType());
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000483FF File Offset: 0x000465FF
		public virtual bool IsEquivalentTo(Type other)
		{
			return this == other;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00048408 File Offset: 0x00046608
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fields == null || fields.Length != 1)
			{
				throw new ArgumentException("The Enum type should contain one and only one instance field.", "enumType");
			}
			return fields[0].FieldType;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00048457 File Offset: 0x00046657
		public virtual Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual Type MakeArrayType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual Type MakeArrayType(int rank)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual Type MakeByRefType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00047E00 File Offset: 0x00046000
		public virtual Type MakePointerType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00048476 File Offset: 0x00046676
		public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
		{
			return new SignatureConstructedGenericType(genericTypeDefinition, typeArguments);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0004847F File Offset: 0x0004667F
		public static Type MakeGenericMethodParameter(int position)
		{
			if (position < 0)
			{
				throw new ArgumentException("Non-negative number required.", "position");
			}
			return new SignatureGenericMethodParameterType(position);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004849B File Offset: 0x0004669B
		public override string ToString()
		{
			return "Type: " + this.Name;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000484AD File Offset: 0x000466AD
		public override bool Equals(object o)
		{
			return o != null && this.Equals(o as Type);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000484C0 File Offset: 0x000466C0
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000484E5 File Offset: 0x000466E5
		public virtual bool Equals(Type o)
		{
			return !(o == null) && this.UnderlyingSystemType == o.UnderlyingSystemType;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00048500 File Offset: 0x00046700
		public static Binder DefaultBinder
		{
			get
			{
				if (Type.s_defaultBinder == null)
				{
					DefaultBinder defaultBinder = new DefaultBinder();
					Interlocked.CompareExchange<Binder>(ref Type.s_defaultBinder, defaultBinder, null);
				}
				return Type.s_defaultBinder;
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000174FB File Offset: 0x000156FB
		void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000174FB File Offset: 0x000156FB
		void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000174FB File Offset: 0x000156FB
		void _Type.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000174FB File Offset: 0x000156FB
		void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00048530 File Offset: 0x00046730
		internal virtual Type InternalResolve()
		{
			return this.UnderlyingSystemType;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Type RuntimeResolve()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal virtual bool IsUserType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00048538 File Offset: 0x00046738
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00048538 File Offset: 0x00046738
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00048538 File Offset: 0x00046738
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00048544 File Offset: 0x00046744
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		// Token: 0x060011B6 RID: 4534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_handle(IntPtr handle);

		// Token: 0x060011B7 RID: 4535 RVA: 0x00048567 File Offset: 0x00046767
		internal virtual RuntimeTypeHandle GetTypeHandleInternal()
		{
			return this.TypeHandle;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual bool IsWindowsRuntimeObjectImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual bool IsExportedToWindowsRuntimeImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004856F File Offset: 0x0004676F
		internal bool IsWindowsRuntimeObject
		{
			get
			{
				return this.IsWindowsRuntimeObjectImpl();
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00048577 File Offset: 0x00046777
		internal bool IsExportedToWindowsRuntime
		{
			get
			{
				return this.IsExportedToWindowsRuntimeImpl();
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0000408A File Offset: 0x0000228A
		internal virtual bool HasProxyAttributeImpl()
		{
			return false;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0000408A File Offset: 0x0000228A
		internal virtual bool IsSzArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0004857F File Offset: 0x0004677F
		internal string FormatTypeName()
		{
			return this.FormatTypeName(false);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual string FormatTypeName(bool serialization)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00048588 File Offset: 0x00046788
		public bool IsInterface
		{
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsInterface(runtimeType);
				}
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000485BC File Offset: 0x000467BC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, ignoreCase, false, ref stackCrawlMark);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000485D8 File Offset: 0x000467D8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, false, false, ref stackCrawlMark);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000485F4 File Offset: 0x000467F4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, false, false, false, ref stackCrawlMark);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00048610 File Offset: 0x00046810
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackCrawlMark);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0004862C File Offset: 0x0004682C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackCrawlMark);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00048648 File Offset: 0x00046848
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackCrawlMark);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		public static bool operator ==(Type left, Type right)
		{
			return left == right;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
		public static bool operator !=(Type left, Type right)
		{
			return left != right;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00048664 File Offset: 0x00046864
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwIfNotFound, ignoreCase, true, ref stackCrawlMark);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0004867E File Offset: 0x0004687E
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00048688 File Offset: 0x00046888
		public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00048694 File Offset: 0x00046894
		internal string FullNameOrDefault
		{
			get
			{
				if (this.InternalNameIfAvailable == null)
				{
					return "UnknownType";
				}
				string text;
				try
				{
					text = this.FullName;
				}
				catch (MissingMetadataException)
				{
					text = "UnknownType";
				}
				return text;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000486D4 File Offset: 0x000468D4
		internal bool IsRuntimeImplemented()
		{
			return this.UnderlyingSystemType is RuntimeType;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000486E4 File Offset: 0x000468E4
		internal virtual string InternalGetNameIfAvailable(ref Type rootCauseForFailure)
		{
			return this.Name;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000486EC File Offset: 0x000468EC
		internal string InternalNameIfAvailable
		{
			get
			{
				Type type = null;
				return this.InternalGetNameIfAvailable(ref type);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00048703 File Offset: 0x00046903
		internal string NameOrDefault
		{
			get
			{
				return this.InternalNameIfAvailable ?? "UnknownType";
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00048714 File Offset: 0x00046914
		// Note: this type is marked as 'beforefieldinit'.
		static Type()
		{
		}

		// Token: 0x04001225 RID: 4645
		private static volatile Binder s_defaultBinder;

		// Token: 0x04001226 RID: 4646
		public static readonly char Delimiter = '.';

		// Token: 0x04001227 RID: 4647
		public static readonly Type[] EmptyTypes = Array.Empty<Type>();

		// Token: 0x04001228 RID: 4648
		public static readonly object Missing = global::System.Reflection.Missing.Value;

		// Token: 0x04001229 RID: 4649
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttributeImpl);

		// Token: 0x0400122A RID: 4650
		public static readonly MemberFilter FilterName = new MemberFilter(Type.FilterNameImpl);

		// Token: 0x0400122B RID: 4651
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(Type.FilterNameIgnoreCaseImpl);

		// Token: 0x0400122C RID: 4652
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x0400122D RID: 4653
		internal RuntimeTypeHandle _impl;

		// Token: 0x0400122E RID: 4654
		internal const string DefaultTypeNameWhenMissingMetadata = "UnknownType";
	}
}
