using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.Social.Base
{
	// Token: 0x02000156 RID: 342
	public abstract class AWorkshopEntry
	{
		// Token: 0x06001D42 RID: 7490 RVA: 0x00501734 File Offset: 0x004FF934
		public static string ReadHeader(string jsonText)
		{
			JToken jtoken;
			if (!JObject.Parse(jsonText).TryGetValue("ContentType", ref jtoken))
			{
				return null;
			}
			return jtoken.ToObject<string>();
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00501760 File Offset: 0x004FF960
		protected static string CreateHeaderJson(string contentTypeName, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			new JObject();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["WorkshopPublishedVersion"] = 1;
			dictionary["ContentType"] = contentTypeName;
			dictionary["SteamEntryId"] = workshopEntryId;
			if (tags != null && tags.Length != 0)
			{
				dictionary["Tags"] = JArray.FromObject(tags);
			}
			dictionary["Publicity"] = publicity;
			return JsonConvert.SerializeObject(dictionary, AWorkshopEntry.SerializerSettings);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x005017DC File Offset: 0x004FF9DC
		public static bool TryReadingManifest(string filePath, out FoundWorkshopEntryInfo info)
		{
			info = null;
			if (!File.Exists(filePath))
			{
				return false;
			}
			string text = File.ReadAllText(filePath);
			info = new FoundWorkshopEntryInfo();
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(text, AWorkshopEntry.SerializerSettings);
			if (dictionary == null)
			{
				return false;
			}
			if (!AWorkshopEntry.TryGet<ulong>(dictionary, "SteamEntryId", out info.workshopEntryId))
			{
				return false;
			}
			int num;
			if (!AWorkshopEntry.TryGet<int>(dictionary, "WorkshopPublishedVersion", out num))
			{
				num = 1;
			}
			info.publishedVersion = num;
			JArray jarray;
			if (AWorkshopEntry.TryGet<JArray>(dictionary, "Tags", out jarray))
			{
				info.tags = jarray.ToObject<string[]>();
			}
			int num2;
			if (AWorkshopEntry.TryGet<int>(dictionary, "Publicity", out num2))
			{
				info.publicity = (WorkshopItemPublicSettingId)num2;
			}
			AWorkshopEntry.TryGet<string>(dictionary, "PreviewImagePath", out info.previewImagePath);
			return true;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0050188C File Offset: 0x004FFA8C
		protected static bool TryGet<T>(Dictionary<string, object> dict, string name, out T outputValue)
		{
			outputValue = default(T);
			bool flag;
			try
			{
				object obj;
				if (dict.TryGetValue(name, out obj))
				{
					if (obj is T)
					{
						outputValue = (T)((object)obj);
						flag = true;
					}
					else if (obj is JObject)
					{
						outputValue = JsonConvert.DeserializeObject<T>(((JObject)obj).ToString());
						flag = true;
					}
					else
					{
						outputValue = (T)((object)Convert.ChangeType(obj, typeof(T)));
						flag = true;
					}
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0000357B File Offset: 0x0000177B
		protected AWorkshopEntry()
		{
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00501920 File Offset: 0x004FFB20
		// Note: this type is marked as 'beforefieldinit'.
		static AWorkshopEntry()
		{
		}

		// Token: 0x0400163B RID: 5691
		public const int CurrentWorkshopPublishVersion = 1;

		// Token: 0x0400163C RID: 5692
		public const string ContentTypeName_World = "World";

		// Token: 0x0400163D RID: 5693
		public const string ContentTypeName_ResourcePack = "ResourcePack";

		// Token: 0x0400163E RID: 5694
		protected const string HeaderFileName = "Workshop.json";

		// Token: 0x0400163F RID: 5695
		protected const string ContentTypeJsonCategoryField = "ContentType";

		// Token: 0x04001640 RID: 5696
		protected const string WorkshopPublishedVersionField = "WorkshopPublishedVersion";

		// Token: 0x04001641 RID: 5697
		protected const string WorkshopEntryField = "SteamEntryId";

		// Token: 0x04001642 RID: 5698
		protected const string TagsField = "Tags";

		// Token: 0x04001643 RID: 5699
		protected const string PreviewImageField = "PreviewImagePath";

		// Token: 0x04001644 RID: 5700
		protected const string PublictyField = "Publicity";

		// Token: 0x04001645 RID: 5701
		protected static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			TypeNameHandling = 0,
			MetadataPropertyHandling = 1,
			Formatting = 1
		};
	}
}
