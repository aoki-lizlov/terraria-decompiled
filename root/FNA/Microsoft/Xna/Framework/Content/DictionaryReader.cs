using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000114 RID: 276
	internal class DictionaryReader<TKey, TValue> : ContentTypeReader<Dictionary<TKey, TValue>>
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public override bool CanDeserializeIntoExistingObject
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00038EED File Offset: 0x000370ED
		public DictionaryReader()
		{
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00038EF8 File Offset: 0x000370F8
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			this.keyType = typeof(TKey);
			this.valueType = typeof(TValue);
			this.keyReader = manager.GetTypeReader(this.keyType);
			this.valueReader = manager.GetTypeReader(this.valueType);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00038F4C File Offset: 0x0003714C
		protected internal override Dictionary<TKey, TValue> Read(ContentReader input, Dictionary<TKey, TValue> existingInstance)
		{
			int num = input.ReadInt32();
			Dictionary<TKey, TValue> dictionary = existingInstance;
			if (dictionary == null)
			{
				dictionary = new Dictionary<TKey, TValue>(num);
			}
			else
			{
				dictionary.Clear();
			}
			for (int i = 0; i < num; i++)
			{
				TKey tkey;
				if (this.keyType.IsValueType)
				{
					tkey = input.ReadObject<TKey>(this.keyReader);
				}
				else
				{
					int num2 = input.Read7BitEncodedInt();
					tkey = ((num2 > 0) ? input.ReadObject<TKey>(input.TypeReaders[num2 - 1]) : default(TKey));
				}
				TValue tvalue;
				if (this.valueType.IsValueType)
				{
					tvalue = input.ReadObject<TValue>(this.valueReader);
				}
				else
				{
					int num3 = input.Read7BitEncodedInt();
					tvalue = ((num3 > 0) ? input.ReadObject<TValue>(input.TypeReaders[num3 - 1]) : default(TValue));
				}
				dictionary.Add(tkey, tvalue);
			}
			return dictionary;
		}

		// Token: 0x04000AB4 RID: 2740
		private ContentTypeReader keyReader;

		// Token: 0x04000AB5 RID: 2741
		private ContentTypeReader valueReader;

		// Token: 0x04000AB6 RID: 2742
		private Type keyType;

		// Token: 0x04000AB7 RID: 2743
		private Type valueType;
	}
}
