using System;
using rail;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000127 RID: 295
	public class FriendsSocialModule : FriendsSocialModule
	{
		// Token: 0x06001BA9 RID: 7081 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Initialize()
		{
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x004FBB58 File Offset: 0x004F9D58
		public override string GetUsername()
		{
			string text;
			rail_api.RailFactory().RailPlayer().GetPlayerName(ref text);
			WeGameHelper.WriteDebugString("GetUsername by wegame" + text, new object[0]);
			return text;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x004FBB8E File Offset: 0x004F9D8E
		public override void OpenJoinInterface()
		{
			WeGameHelper.WriteDebugString("OpenJoinInterface by wegame", new object[0]);
			rail_api.RailFactory().RailFloatingWindow().AsyncShowRailFloatingWindow(10, "");
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x004FBBB7 File Offset: 0x004F9DB7
		public FriendsSocialModule()
		{
		}
	}
}
