using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FullSerializer.Internal
{
	// Token: 0x02000030 RID: 48
	public class fsMetaProperty
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00007D2C File Offset: 0x00005F2C
		internal fsMetaProperty(fsConfig config, FieldInfo field)
		{
			this._memberInfo = field;
			this.StorageType = field.FieldType;
			this.MemberName = field.Name;
			this.IsPublic = field.IsPublic;
			this.CanRead = true;
			this.CanWrite = true;
			this.CommonInitialize(config);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007D80 File Offset: 0x00005F80
		internal fsMetaProperty(fsConfig config, PropertyInfo property)
		{
			this._memberInfo = property;
			this.StorageType = property.PropertyType;
			this.MemberName = property.Name;
			this.IsPublic = property.GetGetMethod() != null && property.GetGetMethod().IsPublic && property.GetSetMethod() != null && property.GetSetMethod().IsPublic;
			this.CanRead = property.CanRead;
			this.CanWrite = property.CanWrite;
			this.CommonInitialize(config);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007E14 File Offset: 0x00006014
		private void CommonInitialize(fsConfig config)
		{
			fsPropertyAttribute attribute = fsPortableReflection.GetAttribute<fsPropertyAttribute>(this._memberInfo);
			if (attribute != null)
			{
				this.JsonName = attribute.Name;
				this.OverrideConverterType = attribute.Converter;
			}
			if (string.IsNullOrEmpty(this.JsonName))
			{
				this.JsonName = config.GetJsonNameFromMemberName.Invoke(this.MemberName, this._memberInfo);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007E72 File Offset: 0x00006072
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00007E7A File Offset: 0x0000607A
		public Type StorageType
		{
			[CompilerGenerated]
			get
			{
				return this.<StorageType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StorageType>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007E83 File Offset: 0x00006083
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00007E8B File Offset: 0x0000608B
		public Type OverrideConverterType
		{
			[CompilerGenerated]
			get
			{
				return this.<OverrideConverterType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OverrideConverterType>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007E94 File Offset: 0x00006094
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00007E9C File Offset: 0x0000609C
		public bool CanRead
		{
			[CompilerGenerated]
			get
			{
				return this.<CanRead>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanRead>k__BackingField = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007EA5 File Offset: 0x000060A5
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007EAD File Offset: 0x000060AD
		public bool CanWrite
		{
			[CompilerGenerated]
			get
			{
				return this.<CanWrite>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CanWrite>k__BackingField = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007EB6 File Offset: 0x000060B6
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007EBE File Offset: 0x000060BE
		public string JsonName
		{
			[CompilerGenerated]
			get
			{
				return this.<JsonName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<JsonName>k__BackingField = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007EC7 File Offset: 0x000060C7
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007ECF File Offset: 0x000060CF
		public string MemberName
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MemberName>k__BackingField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007ED8 File Offset: 0x000060D8
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007EE0 File Offset: 0x000060E0
		public bool IsPublic
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPublic>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsPublic>k__BackingField = value;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007EEC File Offset: 0x000060EC
		public void Write(object context, object value)
		{
			FieldInfo fieldInfo = this._memberInfo as FieldInfo;
			PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(context, value);
				return;
			}
			if (propertyInfo != null)
			{
				MethodInfo setMethod = propertyInfo.GetSetMethod(true);
				if (setMethod != null)
				{
					setMethod.Invoke(context, new object[] { value });
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007F4F File Offset: 0x0000614F
		public object Read(object context)
		{
			if (this._memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this._memberInfo).GetValue(context, new object[0]);
			}
			return ((FieldInfo)this._memberInfo).GetValue(context);
		}

		// Token: 0x04000057 RID: 87
		private MemberInfo _memberInfo;

		// Token: 0x04000058 RID: 88
		[CompilerGenerated]
		private Type <StorageType>k__BackingField;

		// Token: 0x04000059 RID: 89
		[CompilerGenerated]
		private Type <OverrideConverterType>k__BackingField;

		// Token: 0x0400005A RID: 90
		[CompilerGenerated]
		private bool <CanRead>k__BackingField;

		// Token: 0x0400005B RID: 91
		[CompilerGenerated]
		private bool <CanWrite>k__BackingField;

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private string <JsonName>k__BackingField;

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		private string <MemberName>k__BackingField;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private bool <IsPublic>k__BackingField;
	}
}
