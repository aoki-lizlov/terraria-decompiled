using System;
using Newtonsoft.Json.Linq;

namespace Terraria.IO
{
	// Token: 0x02000075 RID: 117
	public class GameConfiguration
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x004BDB3F File Offset: 0x004BBD3F
		public GameConfiguration(JObject configurationRoot)
		{
			this._root = configurationRoot;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x004BDB4E File Offset: 0x004BBD4E
		public T Get<T>(string entry)
		{
			return this._root[entry].ToObject<T>();
		}

		// Token: 0x040010D3 RID: 4307
		private readonly JObject _root;
	}
}
