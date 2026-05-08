using System;
using System.IO;

namespace ReLogic.Content.Readers
{
	// Token: 0x020000A1 RID: 161
	public interface IAssetReader
	{
		// Token: 0x060003B2 RID: 946
		T FromStream<T>(Stream stream) where T : class;
	}
}
