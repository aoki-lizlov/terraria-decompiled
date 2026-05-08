using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using FullSerializer.Internal;

namespace FullSerializer
{
	// Token: 0x0200001B RID: 27
	public class fsMetaType
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000539C File Offset: 0x0000359C
		public static fsMetaType Get(fsConfig config, Type type)
		{
			Dictionary<Type, fsMetaType> dictionary;
			if (!fsMetaType._configMetaTypes.TryGetValue(config, ref dictionary))
			{
				dictionary = (fsMetaType._configMetaTypes[config] = new Dictionary<Type, fsMetaType>());
			}
			fsMetaType fsMetaType;
			if (!dictionary.TryGetValue(type, ref fsMetaType))
			{
				fsMetaType = new fsMetaType(config, type);
				dictionary[type] = fsMetaType;
			}
			return fsMetaType;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000053E8 File Offset: 0x000035E8
		public static void ClearCache()
		{
			fsMetaType._configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000053F4 File Offset: 0x000035F4
		private fsMetaType(fsConfig config, Type reflectedType)
		{
			this.ReflectedType = reflectedType;
			List<fsMetaProperty> list = new List<fsMetaProperty>();
			fsMetaType.CollectProperties(config, list, reflectedType);
			this.Properties = list.ToArray();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005428 File Offset: 0x00003628
		private static void CollectProperties(fsConfig config, List<fsMetaProperty> properties, Type reflectedType)
		{
			bool flag = config.DefaultMemberSerialization == fsMemberSerialization.OptIn;
			bool flag2 = config.DefaultMemberSerialization == fsMemberSerialization.OptOut;
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(reflectedType);
			if (attribute != null)
			{
				flag = attribute.MemberSerialization == fsMemberSerialization.OptIn;
				flag2 = attribute.MemberSerialization == fsMemberSerialization.OptOut;
			}
			MemberInfo[] declaredMembers = reflectedType.GetDeclaredMembers();
			MemberInfo[] array = declaredMembers;
			for (int i = 0; i < array.Length; i++)
			{
				MemberInfo member = array[i];
				if (!Enumerable.Any<Type>(config.IgnoreSerializeAttributes, (Type t) => fsPortableReflection.HasAttribute(member, t)))
				{
					PropertyInfo propertyInfo = member as PropertyInfo;
					FieldInfo fieldInfo = member as FieldInfo;
					if ((!(propertyInfo == null) || !(fieldInfo == null)) && (!flag || Enumerable.Any<Type>(config.SerializeAttributes, (Type t) => fsPortableReflection.HasAttribute(member, t))) && (!flag2 || !Enumerable.Any<Type>(config.IgnoreSerializeAttributes, (Type t) => fsPortableReflection.HasAttribute(member, t))))
					{
						if (propertyInfo != null)
						{
							if (fsMetaType.CanSerializeProperty(config, propertyInfo, declaredMembers, flag2))
							{
								properties.Add(new fsMetaProperty(config, propertyInfo));
							}
						}
						else if (fieldInfo != null && fsMetaType.CanSerializeField(config, fieldInfo, flag2))
						{
							properties.Add(new fsMetaProperty(config, fieldInfo));
						}
					}
				}
			}
			if (reflectedType.Resolve().BaseType != null)
			{
				fsMetaType.CollectProperties(config, properties, reflectedType.Resolve().BaseType);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005598 File Offset: 0x00003798
		private static bool IsAutoProperty(PropertyInfo property, MemberInfo[] members)
		{
			if (!property.CanWrite || !property.CanRead)
			{
				return false;
			}
			string text = "<" + property.Name + ">k__BackingField";
			for (int i = 0; i < members.Length; i++)
			{
				if (members[i].Name == text)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000055F0 File Offset: 0x000037F0
		private static bool CanSerializeProperty(fsConfig config, PropertyInfo property, MemberInfo[] members, bool annotationFreeValue)
		{
			if (typeof(Delegate).IsAssignableFrom(property.PropertyType))
			{
				return false;
			}
			MethodInfo getMethod = property.GetGetMethod(false);
			MethodInfo setMethod = property.GetSetMethod(false);
			return (!(getMethod != null) || !getMethod.IsStatic) && (!(setMethod != null) || !setMethod.IsStatic) && property.GetIndexParameters().Length == 0 && (Enumerable.Any<Type>(config.SerializeAttributes, (Type t) => fsPortableReflection.HasAttribute(property, t)) || (property.CanRead && property.CanWrite && (((config.SerializeNonAutoProperties || fsMetaType.IsAutoProperty(property, members)) && getMethod != null && (config.SerializeNonPublicSetProperties || setMethod != null)) || annotationFreeValue)));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000056E0 File Offset: 0x000038E0
		private static bool CanSerializeField(fsConfig config, FieldInfo field, bool annotationFreeValue)
		{
			return !typeof(Delegate).IsAssignableFrom(field.FieldType) && !field.IsDefined(typeof(CompilerGeneratedAttribute), false) && !field.IsStatic && (Enumerable.Any<Type>(config.SerializeAttributes, (Type t) => fsPortableReflection.HasAttribute(field, t)) || annotationFreeValue || field.IsPublic);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005770 File Offset: 0x00003970
		public bool EmitAotData()
		{
			if (this._hasEmittedAotData)
			{
				return false;
			}
			this._hasEmittedAotData = true;
			for (int i = 0; i < this.Properties.Length; i++)
			{
				if (!this.Properties[i].IsPublic)
				{
					return false;
				}
			}
			if (!this.HasDefaultConstructor)
			{
				return false;
			}
			fsAotCompilationManager.AddAotCompilation(this.ReflectedType, this.Properties, this._isDefaultConstructorPublic);
			return true;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000057D4 File Offset: 0x000039D4
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000057DC File Offset: 0x000039DC
		public fsMetaProperty[] Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Properties>k__BackingField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000057E8 File Offset: 0x000039E8
		public bool HasDefaultConstructor
		{
			get
			{
				if (this._hasDefaultConstructorCache == null)
				{
					if (this.ReflectedType.Resolve().IsArray)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else if (this.ReflectedType.Resolve().IsValueType)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else
					{
						ConstructorInfo declaredConstructor = this.ReflectedType.GetDeclaredConstructor(fsPortableReflection.EmptyTypes);
						this._hasDefaultConstructorCache = new bool?(declaredConstructor != null);
						if (declaredConstructor != null)
						{
							this._isDefaultConstructorPublic = declaredConstructor.IsPublic;
						}
					}
				}
				return this._hasDefaultConstructorCache.Value;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005898 File Offset: 0x00003A98
		public object CreateInstance()
		{
			if (this.ReflectedType.Resolve().IsInterface || this.ReflectedType.Resolve().IsAbstract)
			{
				throw new Exception("Cannot create an instance of an interface or abstract type for " + this.ReflectedType);
			}
			if (typeof(string) == this.ReflectedType)
			{
				return string.Empty;
			}
			if (!this.HasDefaultConstructor)
			{
				return FormatterServices.GetSafeUninitializedObject(this.ReflectedType);
			}
			if (this.ReflectedType.Resolve().IsArray)
			{
				return Array.CreateInstance(this.ReflectedType.GetElementType(), 0);
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(this.ReflectedType, true);
			}
			catch (MissingMethodException ex)
			{
				throw new InvalidOperationException("Unable to create instance of " + this.ReflectedType + "; there is no default constructor", ex);
			}
			catch (TargetInvocationException ex2)
			{
				throw new InvalidOperationException("Constructor of " + this.ReflectedType + " threw an exception when creating an instance", ex2);
			}
			catch (MemberAccessException ex3)
			{
				throw new InvalidOperationException("Unable to access constructor of " + this.ReflectedType, ex3);
			}
			return obj;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000053E8 File Offset: 0x000035E8
		// Note: this type is marked as 'beforefieldinit'.
		static fsMetaType()
		{
		}

		// Token: 0x04000040 RID: 64
		private static Dictionary<fsConfig, Dictionary<Type, fsMetaType>> _configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();

		// Token: 0x04000041 RID: 65
		public Type ReflectedType;

		// Token: 0x04000042 RID: 66
		private bool _hasEmittedAotData;

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		private fsMetaProperty[] <Properties>k__BackingField;

		// Token: 0x04000044 RID: 68
		private bool? _hasDefaultConstructorCache;

		// Token: 0x04000045 RID: 69
		private bool _isDefaultConstructorPublic;

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000298 RID: 664 RVA: 0x00002493 File Offset: 0x00000693
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00009C5F File Offset: 0x00007E5F
			internal bool <CollectProperties>b__0(Type t)
			{
				return fsPortableReflection.HasAttribute(this.member, t);
			}

			// Token: 0x0600029A RID: 666 RVA: 0x00009C5F File Offset: 0x00007E5F
			internal bool <CollectProperties>b__1(Type t)
			{
				return fsPortableReflection.HasAttribute(this.member, t);
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00009C5F File Offset: 0x00007E5F
			internal bool <CollectProperties>b__2(Type t)
			{
				return fsPortableReflection.HasAttribute(this.member, t);
			}

			// Token: 0x0400024F RID: 591
			public MemberInfo member;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x0600029C RID: 668 RVA: 0x00002493 File Offset: 0x00000693
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00009C6D File Offset: 0x00007E6D
			internal bool <CanSerializeProperty>b__0(Type t)
			{
				return fsPortableReflection.HasAttribute(this.property, t);
			}

			// Token: 0x04000250 RID: 592
			public PropertyInfo property;
		}

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x0600029E RID: 670 RVA: 0x00002493 File Offset: 0x00000693
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00009C7B File Offset: 0x00007E7B
			internal bool <CanSerializeField>b__0(Type t)
			{
				return fsPortableReflection.HasAttribute(this.field, t);
			}

			// Token: 0x04000251 RID: 593
			public FieldInfo field;
		}
	}
}
