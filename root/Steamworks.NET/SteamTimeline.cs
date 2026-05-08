using System;

namespace Steamworks
{
	// Token: 0x02000022 RID: 34
	public static class SteamTimeline
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000A24C File Offset: 0x0000844C
		public static void SetTimelineStateDescription(string pchDescription, float flTimeDelta)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchDescription))
			{
				NativeMethods.ISteamTimeline_SetTimelineStateDescription(CSteamAPIContext.GetSteamTimeline(), utf8StringHandle, flTimeDelta);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000A290 File Offset: 0x00008490
		public static void ClearTimelineStateDescription(float flTimeDelta)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamTimeline_ClearTimelineStateDescription(CSteamAPIContext.GetSteamTimeline(), flTimeDelta);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public static void AddTimelineEvent(string pchIcon, string pchTitle, string pchDescription, uint unPriority, float flStartOffsetSeconds, float flDurationSeconds, ETimelineEventClipPriority ePossibleClip)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pchIcon))
			{
				using (InteropHelp.UTF8StringHandle utf8StringHandle2 = new InteropHelp.UTF8StringHandle(pchTitle))
				{
					using (InteropHelp.UTF8StringHandle utf8StringHandle3 = new InteropHelp.UTF8StringHandle(pchDescription))
					{
						NativeMethods.ISteamTimeline_AddTimelineEvent(CSteamAPIContext.GetSteamTimeline(), utf8StringHandle, utf8StringHandle2, utf8StringHandle3, unPriority, flStartOffsetSeconds, flDurationSeconds, ePossibleClip);
					}
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000A328 File Offset: 0x00008528
		public static void SetTimelineGameMode(ETimelineGameMode eMode)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamTimeline_SetTimelineGameMode(CSteamAPIContext.GetSteamTimeline(), eMode);
		}
	}
}
