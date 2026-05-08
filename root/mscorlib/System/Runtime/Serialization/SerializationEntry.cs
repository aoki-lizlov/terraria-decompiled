using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200061D RID: 1565
	public readonly struct SerializationEntry
	{
		// Token: 0x06003BEC RID: 15340 RVA: 0x000D0EF0 File Offset: 0x000CF0F0
		internal SerializationEntry(string entryName, object entryValue, Type entryType)
		{
			this._name = entryName;
			this._value = entryValue;
			this._type = entryType;
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000D0F07 File Offset: 0x000CF107
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000D0F0F File Offset: 0x000CF10F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000D0F17 File Offset: 0x000CF117
		public Type ObjectType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400269A RID: 9882
		private readonly string _name;

		// Token: 0x0400269B RID: 9883
		private readonly object _value;

		// Token: 0x0400269C RID: 9884
		private readonly Type _type;
	}
}
