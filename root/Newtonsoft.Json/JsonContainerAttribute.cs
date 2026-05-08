using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(1028, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002395 File Offset: 0x00000595
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000239D File Offset: 0x0000059D
		public string Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023A6 File Offset: 0x000005A6
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023AE File Offset: 0x000005AE
		public string Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Title>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023B7 File Offset: 0x000005B7
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000023BF File Offset: 0x000005BF
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023C8 File Offset: 0x000005C8
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000023D0 File Offset: 0x000005D0
		public Type ItemConverterType
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemConverterType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemConverterType>k__BackingField = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023D9 File Offset: 0x000005D9
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000023E1 File Offset: 0x000005E1
		public object[] ItemConverterParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemConverterParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemConverterParameters>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023EA File Offset: 0x000005EA
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000023F2 File Offset: 0x000005F2
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002402 File Offset: 0x00000602
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000240A File Offset: 0x0000060A
		public object[] NamingStrategyParameters
		{
			get
			{
				return this._namingStrategyParameters;
			}
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002422 File Offset: 0x00000622
		internal NamingStrategy NamingStrategyInstance
		{
			[CompilerGenerated]
			get
			{
				return this.<NamingStrategyInstance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NamingStrategyInstance>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002452 File Offset: 0x00000652
		public bool IsReference
		{
			get
			{
				return this._isReference ?? false;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002460 File Offset: 0x00000660
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002486 File Offset: 0x00000686
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference ?? false;
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002494 File Offset: 0x00000694
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000024BA File Offset: 0x000006BA
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? itemReferenceLoopHandling = this._itemReferenceLoopHandling;
				if (itemReferenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000024EE File Offset: 0x000006EE
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				TypeNameHandling? itemTypeNameHandling = this._itemTypeNameHandling;
				if (itemTypeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002098 File Offset: 0x00000298
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000024FC File Offset: 0x000006FC
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private string <Id>k__BackingField;

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private string <Title>k__BackingField;

		// Token: 0x0400003E RID: 62
		[CompilerGenerated]
		private string <Description>k__BackingField;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private Type <ItemConverterType>k__BackingField;

		// Token: 0x04000040 RID: 64
		[CompilerGenerated]
		private object[] <ItemConverterParameters>k__BackingField;

		// Token: 0x04000041 RID: 65
		[CompilerGenerated]
		private NamingStrategy <NamingStrategyInstance>k__BackingField;

		// Token: 0x04000042 RID: 66
		internal bool? _isReference;

		// Token: 0x04000043 RID: 67
		internal bool? _itemIsReference;

		// Token: 0x04000044 RID: 68
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000045 RID: 69
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x04000046 RID: 70
		private Type _namingStrategyType;

		// Token: 0x04000047 RID: 71
		private object[] _namingStrategyParameters;
	}
}
