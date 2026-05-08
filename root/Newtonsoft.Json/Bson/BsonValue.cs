using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F9 RID: 249
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x000301EC File Offset: 0x0002E3EC
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00030202 File Offset: 0x0002E402
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003020A File Offset: 0x0002E40A
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040003E4 RID: 996
		private readonly object _value;

		// Token: 0x040003E5 RID: 997
		private readonly BsonType _type;
	}
}
