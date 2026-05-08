using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000120 RID: 288
	internal class ListReader<T> : ContentTypeReader<List<T>>
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public override bool CanDeserializeIntoExistingObject
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00039565 File Offset: 0x00037765
		public ListReader()
		{
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00039570 File Offset: 0x00037770
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			Type typeFromHandle = typeof(T);
			this.elementReader = manager.GetTypeReader(typeFromHandle);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00039598 File Offset: 0x00037798
		protected internal override List<T> Read(ContentReader input, List<T> existingInstance)
		{
			int num = input.ReadInt32();
			List<T> list = existingInstance;
			if (list == null)
			{
				list = new List<T>(num);
			}
			for (int i = 0; i < num; i++)
			{
				if (typeof(T).IsValueType)
				{
					list.Add(input.ReadObject<T>(this.elementReader));
				}
				else
				{
					int num2 = input.Read7BitEncodedInt();
					list.Add((num2 > 0) ? input.ReadObject<T>(input.TypeReaders[num2 - 1]) : default(T));
				}
			}
			return list;
		}

		// Token: 0x04000AB9 RID: 2745
		private ContentTypeReader elementReader;
	}
}
