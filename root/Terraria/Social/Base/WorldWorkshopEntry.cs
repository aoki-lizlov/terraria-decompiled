using System;
using Terraria.IO;

namespace Terraria.Social.Base
{
	// Token: 0x02000157 RID: 343
	public class WorldWorkshopEntry : AWorkshopEntry
	{
		// Token: 0x06001D48 RID: 7496 RVA: 0x00501941 File Offset: 0x004FFB41
		public static string GetHeaderTextFor(WorldFileData world, ulong workshopEntryId, string[] tags, WorkshopItemPublicSettingId publicity, string previewImagePath)
		{
			return AWorkshopEntry.CreateHeaderJson("World", workshopEntryId, tags, publicity, previewImagePath);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00501952 File Offset: 0x004FFB52
		public WorldWorkshopEntry()
		{
		}
	}
}
