using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.IO;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000BF RID: 191
	public class WorldGenConfiguration : GameConfiguration
	{
		// Token: 0x060017B6 RID: 6070 RVA: 0x004DFAF4 File Offset: 0x004DDCF4
		public WorldGenConfiguration(JObject configurationRoot)
			: base(configurationRoot)
		{
			this._biomeRoot = ((JObject)configurationRoot["Biomes"]) ?? new JObject();
			this._passRoot = ((JObject)configurationRoot["Passes"]) ?? new JObject();
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x004DFB46 File Offset: 0x004DDD46
		public T CreateBiome<T>() where T : MicroBiome, new()
		{
			return this.CreateBiome<T>(typeof(T).Name);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x004DFB60 File Offset: 0x004DDD60
		public T CreateBiome<T>(string name) where T : MicroBiome, new()
		{
			JToken jtoken;
			if (this._biomeRoot.TryGetValue(name, ref jtoken))
			{
				return jtoken.ToObject<T>();
			}
			return new T();
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x004DFB8C File Offset: 0x004DDD8C
		public GameConfiguration GetPassConfiguration(string name)
		{
			JToken jtoken;
			if (this._passRoot.TryGetValue(name, ref jtoken))
			{
				return new GameConfiguration((JObject)jtoken);
			}
			return new GameConfiguration(new JObject());
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x004DFBC0 File Offset: 0x004DDDC0
		public static WorldGenConfiguration FromEmbeddedPath(string path)
		{
			WorldGenConfiguration worldGenConfiguration;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					worldGenConfiguration = new WorldGenConfiguration(JsonConvert.DeserializeObject<JObject>(streamReader.ReadToEnd()));
				}
			}
			return worldGenConfiguration;
		}

		// Token: 0x04001297 RID: 4759
		private readonly JObject _biomeRoot;

		// Token: 0x04001298 RID: 4760
		private readonly JObject _passRoot;
	}
}
