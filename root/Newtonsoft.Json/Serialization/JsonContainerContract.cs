using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007B RID: 123
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001813C File Offset: 0x0001633C
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x00018144 File Offset: 0x00016344
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001817E File Offset: 0x0001637E
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00018186 File Offset: 0x00016386
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001818E File Offset: 0x0001638E
		public JsonConverter ItemConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemConverter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemConverter>k__BackingField = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00018197 File Offset: 0x00016397
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001819F File Offset: 0x0001639F
		public bool? ItemIsReference
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemIsReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemIsReference>k__BackingField = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000181A8 File Offset: 0x000163A8
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x000181B0 File Offset: 0x000163B0
		public ReferenceLoopHandling? ItemReferenceLoopHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemReferenceLoopHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemReferenceLoopHandling>k__BackingField = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x000181B9 File Offset: 0x000163B9
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x000181C1 File Offset: 0x000163C1
		public TypeNameHandling? ItemTypeNameHandling
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemTypeNameHandling>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemTypeNameHandling>k__BackingField = value;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000181CC File Offset: 0x000163CC
		internal JsonContainerContract(Type underlyingType)
			: base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x0400026D RID: 621
		private JsonContract _itemContract;

		// Token: 0x0400026E RID: 622
		private JsonContract _finalItemContract;

		// Token: 0x0400026F RID: 623
		[CompilerGenerated]
		private JsonConverter <ItemConverter>k__BackingField;

		// Token: 0x04000270 RID: 624
		[CompilerGenerated]
		private bool? <ItemIsReference>k__BackingField;

		// Token: 0x04000271 RID: 625
		[CompilerGenerated]
		private ReferenceLoopHandling? <ItemReferenceLoopHandling>k__BackingField;

		// Token: 0x04000272 RID: 626
		[CompilerGenerated]
		private TypeNameHandling? <ItemTypeNameHandling>k__BackingField;
	}
}
