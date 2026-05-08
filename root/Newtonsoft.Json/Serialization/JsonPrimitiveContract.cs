using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000087 RID: 135
	public class JsonPrimitiveContract : JsonContract
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00019309 File Offset: 0x00017509
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00019311 File Offset: 0x00017511
		internal PrimitiveTypeCode TypeCode
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeCode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TypeCode>k__BackingField = value;
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001931C File Offset: 0x0001751C
		public JsonPrimitiveContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Primitive;
			this.TypeCode = ConvertUtils.GetTypeCode(underlyingType);
			this.IsReadOnlyOrFixedSize = true;
			ReadType readType;
			if (JsonPrimitiveContract.ReadTypeMap.TryGetValue(this.NonNullableUnderlyingType, ref readType))
			{
				this.InternalReadType = readType;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00019368 File Offset: 0x00017568
		// Note: this type is marked as 'beforefieldinit'.
		static JsonPrimitiveContract()
		{
			Dictionary<Type, ReadType> dictionary = new Dictionary<Type, ReadType>();
			Type typeFromHandle = typeof(byte[]);
			dictionary[typeFromHandle] = ReadType.ReadAsBytes;
			Type typeFromHandle2 = typeof(byte);
			dictionary[typeFromHandle2] = ReadType.ReadAsInt32;
			Type typeFromHandle3 = typeof(short);
			dictionary[typeFromHandle3] = ReadType.ReadAsInt32;
			Type typeFromHandle4 = typeof(int);
			dictionary[typeFromHandle4] = ReadType.ReadAsInt32;
			Type typeFromHandle5 = typeof(decimal);
			dictionary[typeFromHandle5] = ReadType.ReadAsDecimal;
			Type typeFromHandle6 = typeof(bool);
			dictionary[typeFromHandle6] = ReadType.ReadAsBoolean;
			Type typeFromHandle7 = typeof(string);
			dictionary[typeFromHandle7] = ReadType.ReadAsString;
			Type typeFromHandle8 = typeof(DateTime);
			dictionary[typeFromHandle8] = ReadType.ReadAsDateTime;
			Type typeFromHandle9 = typeof(DateTimeOffset);
			dictionary[typeFromHandle9] = ReadType.ReadAsDateTimeOffset;
			Type typeFromHandle10 = typeof(float);
			dictionary[typeFromHandle10] = ReadType.ReadAsDouble;
			Type typeFromHandle11 = typeof(double);
			dictionary[typeFromHandle11] = ReadType.ReadAsDouble;
			JsonPrimitiveContract.ReadTypeMap = dictionary;
		}

		// Token: 0x04000288 RID: 648
		[CompilerGenerated]
		private PrimitiveTypeCode <TypeCode>k__BackingField;

		// Token: 0x04000289 RID: 649
		private static readonly Dictionary<Type, ReadType> ReadTypeMap;
	}
}
