using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000022 RID: 34
	[AttributeUsage(2432, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000722A File Offset: 0x0000542A
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00007232 File Offset: 0x00005432
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000723B File Offset: 0x0000543B
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00007243 File Offset: 0x00005443
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000724C File Offset: 0x0000544C
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00007254 File Offset: 0x00005454
		public Type NamingStrategyType
		{
			[CompilerGenerated]
			get
			{
				return this.<NamingStrategyType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NamingStrategyType>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000725D File Offset: 0x0000545D
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00007265 File Offset: 0x00005465
		public object[] NamingStrategyParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<NamingStrategyParameters>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NamingStrategyParameters>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007270 File Offset: 0x00005470
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00007296 File Offset: 0x00005496
		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				if (nullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000072A4 File Offset: 0x000054A4
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000072CA File Offset: 0x000054CA
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				if (defaultValueHandling == null)
				{
					return DefaultValueHandling.Include;
				}
				return defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000072D8 File Offset: 0x000054D8
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000072FE File Offset: 0x000054FE
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				if (referenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000730C File Offset: 0x0000550C
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00007332 File Offset: 0x00005532
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				if (objectCreationHandling == null)
				{
					return ObjectCreationHandling.Auto;
				}
				return objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00007340 File Offset: 0x00005540
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00007366 File Offset: 0x00005566
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				if (typeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00007374 File Offset: 0x00005574
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000739A File Offset: 0x0000559A
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000073A8 File Offset: 0x000055A8
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000073CE File Offset: 0x000055CE
		public int Order
		{
			get
			{
				int? order = this._order;
				if (order == null)
				{
					return 0;
				}
				return order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000073DC File Offset: 0x000055DC
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00007402 File Offset: 0x00005602
		public Required Required
		{
			get
			{
				Required? required = this._required;
				if (required == null)
				{
					return Required.Default;
				}
				return required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007410 File Offset: 0x00005610
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00007418 File Offset: 0x00005618
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PropertyName>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007424 File Offset: 0x00005624
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000744A File Offset: 0x0000564A
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007458 File Offset: 0x00005658
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000747E File Offset: 0x0000567E
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000748C File Offset: 0x0000568C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000074B2 File Offset: 0x000056B2
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

		// Token: 0x06000128 RID: 296 RVA: 0x00002098 File Offset: 0x00000298
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000074C0 File Offset: 0x000056C0
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x040000AB RID: 171
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000AC RID: 172
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000AD RID: 173
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000AE RID: 174
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000AF RID: 175
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000B0 RID: 176
		internal bool? _isReference;

		// Token: 0x040000B1 RID: 177
		internal int? _order;

		// Token: 0x040000B2 RID: 178
		internal Required? _required;

		// Token: 0x040000B3 RID: 179
		internal bool? _itemIsReference;

		// Token: 0x040000B4 RID: 180
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x040000B5 RID: 181
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x040000B6 RID: 182
		[CompilerGenerated]
		private Type <ItemConverterType>k__BackingField;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private object[] <ItemConverterParameters>k__BackingField;

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private Type <NamingStrategyType>k__BackingField;

		// Token: 0x040000B9 RID: 185
		[CompilerGenerated]
		private object[] <NamingStrategyParameters>k__BackingField;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private string <PropertyName>k__BackingField;
	}
}
