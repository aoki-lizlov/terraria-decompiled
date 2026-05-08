using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000129 RID: 297
	internal class ReflectiveReader<T> : ContentTypeReader
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x00039AA8 File Offset: 0x00037CA8
		public override bool CanDeserializeIntoExistingObject
		{
			get
			{
				return base.TargetType.IsClass;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00039AB5 File Offset: 0x00037CB5
		internal ReflectiveReader()
			: base(typeof(T))
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00039AC8 File Offset: 0x00037CC8
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			base.Initialize(manager);
			Type baseType = base.TargetType.BaseType;
			if (baseType != null && baseType != typeof(object))
			{
				this.baseTypeReader = manager.GetTypeReader(baseType);
			}
			this.constructor = base.TargetType.GetDefaultConstructor();
			PropertyInfo[] properties = base.TargetType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] fields = base.TargetType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.readers = new List<ReflectiveReader<T>.ReadElement>(fields.Length + properties.Length);
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo getMethod = propertyInfo.GetGetMethod(true);
				if (!(getMethod == null) && !(getMethod != getMethod.GetBaseDefinition()))
				{
					ReflectiveReader<T>.ReadElement elementReader = ReflectiveReader<T>.GetElementReader(manager, propertyInfo);
					if (elementReader != null)
					{
						this.readers.Add(elementReader);
					}
				}
			}
			foreach (FieldInfo fieldInfo in fields)
			{
				ReflectiveReader<T>.ReadElement elementReader2 = ReflectiveReader<T>.GetElementReader(manager, fieldInfo);
				if (elementReader2 != null)
				{
					this.readers.Add(elementReader2);
				}
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00039BE0 File Offset: 0x00037DE0
		protected internal override object Read(ContentReader input, object existingInstance)
		{
			T t;
			if (existingInstance != null)
			{
				t = (T)((object)existingInstance);
			}
			else if (this.constructor == null)
			{
				t = (T)((object)Activator.CreateInstance(typeof(T)));
			}
			else
			{
				t = (T)((object)this.constructor.Invoke(null));
			}
			if (this.baseTypeReader != null)
			{
				this.baseTypeReader.Read(input, t);
			}
			object obj = t;
			foreach (ReflectiveReader<T>.ReadElement readElement in this.readers)
			{
				readElement(input, obj);
			}
			t = (T)((object)obj);
			return t;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00039CA4 File Offset: 0x00037EA4
		private static ReflectiveReader<T>.ReadElement GetElementReader(ContentTypeReaderManager manager, MemberInfo member)
		{
			PropertyInfo property = member as PropertyInfo;
			FieldInfo fieldInfo = member as FieldInfo;
			if (property != null)
			{
				if (!property.CanRead)
				{
					return null;
				}
				if (property.GetIndexParameters().Length != 0)
				{
					return null;
				}
			}
			if (Attribute.GetCustomAttribute(member, typeof(ContentSerializerIgnoreAttribute)) != null)
			{
				return null;
			}
			ContentSerializerAttribute contentSerializerAttribute = Attribute.GetCustomAttribute(member, typeof(ContentSerializerAttribute)) as ContentSerializerAttribute;
			if (contentSerializerAttribute == null)
			{
				if (property != null)
				{
					MethodInfo getMethod = property.GetGetMethod(true);
					if (getMethod != null && !getMethod.IsPublic)
					{
						return null;
					}
					MethodInfo setMethod = property.GetSetMethod(true);
					if (setMethod != null && !setMethod.IsPublic)
					{
						return null;
					}
					if (!property.CanWrite)
					{
						ContentTypeReader typeReader = manager.GetTypeReader(property.PropertyType);
						if (typeReader == null || !typeReader.CanDeserializeIntoExistingObject)
						{
							return null;
						}
					}
				}
				else
				{
					if (!fieldInfo.IsPublic)
					{
						return null;
					}
					if (fieldInfo.IsInitOnly)
					{
						return null;
					}
				}
			}
			Action<object, object> setter;
			Type type;
			if (property != null)
			{
				type = property.PropertyType;
				if (property.CanWrite)
				{
					setter = delegate(object o, object v)
					{
						property.SetValue(o, v, null);
					};
				}
				else
				{
					setter = delegate(object o, object v)
					{
					};
				}
			}
			else
			{
				type = fieldInfo.FieldType;
				setter = new Action<object, object>(fieldInfo.SetValue);
			}
			if (contentSerializerAttribute != null && contentSerializerAttribute.SharedResource)
			{
				return delegate(ContentReader input, object parent)
				{
					Action<object> action = delegate(object value)
					{
						setter(parent, value);
					};
					input.ReadSharedResource<object>(action);
				};
			}
			ContentTypeReader reader = manager.GetTypeReader(type);
			if (reader == null)
			{
				throw new ContentLoadException(string.Format("Content reader could not be found for {0} type.", type.FullName));
			}
			Func<object, object> construct = (object parent) => null;
			if (property != null && !property.CanWrite)
			{
				construct = (object parent) => property.GetValue(parent, null);
			}
			return delegate(ContentReader input, object parent)
			{
				object obj = construct(parent);
				object obj2 = input.ReadObject<object>(reader, obj);
				setter(parent, obj2);
			};
		}

		// Token: 0x04000ABB RID: 2747
		private List<ReflectiveReader<T>.ReadElement> readers;

		// Token: 0x04000ABC RID: 2748
		private ConstructorInfo constructor;

		// Token: 0x04000ABD RID: 2749
		private ContentTypeReader baseTypeReader;

		// Token: 0x020003DD RID: 989
		// (Invoke) Token: 0x06001AFC RID: 6908
		private delegate void ReadElement(ContentReader input, object parent);

		// Token: 0x020003DE RID: 990
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001AFF RID: 6911 RVA: 0x0003FA42 File Offset: 0x0003DC42
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001B00 RID: 6912 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c()
			{
			}

			// Token: 0x06001B01 RID: 6913 RVA: 0x00009E6B File Offset: 0x0000806B
			internal void <GetElementReader>b__9_1(object o, object v)
			{
			}

			// Token: 0x06001B02 RID: 6914 RVA: 0x0003FA4E File Offset: 0x0003DC4E
			internal object <GetElementReader>b__9_3(object parent)
			{
				return null;
			}

			// Token: 0x04001DE9 RID: 7657
			public static readonly ReflectiveReader<T>.<>c <>9 = new ReflectiveReader<T>.<>c();

			// Token: 0x04001DEA RID: 7658
			public static Action<object, object> <>9__9_1;

			// Token: 0x04001DEB RID: 7659
			public static Func<object, object> <>9__9_3;
		}

		// Token: 0x020003DF RID: 991
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06001B03 RID: 6915 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06001B04 RID: 6916 RVA: 0x0003FA51 File Offset: 0x0003DC51
			internal void <GetElementReader>b__0(object o, object v)
			{
				this.property.SetValue(o, v, null);
			}

			// Token: 0x06001B05 RID: 6917 RVA: 0x0003FA64 File Offset: 0x0003DC64
			internal void <GetElementReader>b__2(ContentReader input, object parent)
			{
				Action<object> action = new Action<object>(new ReflectiveReader<T>.<>c__DisplayClass9_1
				{
					CS$<>8__locals1 = this,
					parent = parent
				}.<GetElementReader>b__6);
				input.ReadSharedResource<object>(action);
			}

			// Token: 0x06001B06 RID: 6918 RVA: 0x0003FA97 File Offset: 0x0003DC97
			internal object <GetElementReader>b__4(object parent)
			{
				return this.property.GetValue(parent, null);
			}

			// Token: 0x06001B07 RID: 6919 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
			internal void <GetElementReader>b__5(ContentReader input, object parent)
			{
				object obj = this.construct(parent);
				object obj2 = input.ReadObject<object>(this.reader, obj);
				this.setter(parent, obj2);
			}

			// Token: 0x04001DEC RID: 7660
			public PropertyInfo property;

			// Token: 0x04001DED RID: 7661
			public Action<object, object> setter;

			// Token: 0x04001DEE RID: 7662
			public Func<object, object> construct;

			// Token: 0x04001DEF RID: 7663
			public ContentTypeReader reader;
		}

		// Token: 0x020003E0 RID: 992
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_1
		{
			// Token: 0x06001B08 RID: 6920 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c__DisplayClass9_1()
			{
			}

			// Token: 0x06001B09 RID: 6921 RVA: 0x0003FADD File Offset: 0x0003DCDD
			internal void <GetElementReader>b__6(object value)
			{
				this.CS$<>8__locals1.setter(this.parent, value);
			}

			// Token: 0x04001DF0 RID: 7664
			public object parent;

			// Token: 0x04001DF1 RID: 7665
			public ReflectiveReader<T>.<>c__DisplayClass9_0 CS$<>8__locals1;
		}
	}
}
