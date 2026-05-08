using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000138 RID: 312
	internal class UInt64Reader : ContentTypeReader<ulong>
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0003A768 File Offset: 0x00038968
		internal UInt64Reader()
		{
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0003A770 File Offset: 0x00038970
		protected internal override ulong Read(ContentReader input, ulong existingInstance)
		{
			return input.ReadUInt64();
		}
	}
}
