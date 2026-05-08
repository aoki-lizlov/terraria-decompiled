using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000158 RID: 344
	public class TexturePackWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x0050195A File Offset: 0x004FFB5A
		public static string GetHeaderTextFor(ResourcePack resourcePack, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("ResourcePack", workshopEntryId, tags, publicity, previewImagePath);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00501952 File Offset: 0x004FFB52
		public TexturePackWorkshopEntry()
		{
		}
	}
}
