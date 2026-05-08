using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000108 RID: 264
	internal class ArrayReader<T> : ContentTypeReader<T[]>
	{
		// Token: 0x06001718 RID: 5912 RVA: 0x00038C02 File Offset: 0x00036E02
		public ArrayReader()
		{
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00038C0C File Offset: 0x00036E0C
		protected internal override void Initialize(ContentTypeReaderManager manager)
		{
			Type typeFromHandle = typeof(T);
			this.elementReader = manager.GetTypeReader(typeFromHandle);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00038C34 File Offset: 0x00036E34
		protected internal override T[] Read(ContentReader input, T[] existingInstance)
		{
			uint num = input.ReadUInt32();
			T[] array = existingInstance;
			if (array == null)
			{
				array = new T[num];
			}
			if (typeof(T).IsValueType)
			{
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					array[(int)num2] = input.ReadObject<T>(this.elementReader);
				}
			}
			else
			{
				for (uint num3 = 0U; num3 < num; num3 += 1U)
				{
					int num4 = input.Read7BitEncodedInt();
					if (num4 > 0)
					{
						array[(int)num3] = input.ReadObject<T>(input.TypeReaders[num4 - 1]);
					}
					else
					{
						array[(int)num3] = default(T);
					}
				}
			}
			return array;
		}

		// Token: 0x04000AB3 RID: 2739
		private ContentTypeReader elementReader;
	}
}
