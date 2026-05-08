using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	[StructLayout(0, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x0000E905 File Offset: 0x0000CB05
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0000E924 File Offset: 0x0000CB24
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + "\0");
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0000E941 File Offset: 0x0000CB41
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0000E960 File Offset: 0x0000CB60
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + "\0");
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0000E97D File Offset: 0x0000CB7D
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0000E99C File Offset: 0x0000CB9C
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + "\0");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0000E9EE File Offset: 0x0000CBEE
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + "\0");
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0000EA0B File Offset: 0x0000CC0B
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + "\0");
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		public gameserveritem_t()
		{
		}

		// Token: 0x04000A98 RID: 2712
		public servernetadr_t m_NetAdr;

		// Token: 0x04000A99 RID: 2713
		public int m_nPing;

		// Token: 0x04000A9A RID: 2714
		[MarshalAs(3)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000A9B RID: 2715
		[MarshalAs(3)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000A9C RID: 2716
		[MarshalAs(30, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000A9D RID: 2717
		[MarshalAs(30, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000A9E RID: 2718
		[MarshalAs(30, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000A9F RID: 2719
		public uint m_nAppID;

		// Token: 0x04000AA0 RID: 2720
		public int m_nPlayers;

		// Token: 0x04000AA1 RID: 2721
		public int m_nMaxPlayers;

		// Token: 0x04000AA2 RID: 2722
		public int m_nBotPlayers;

		// Token: 0x04000AA3 RID: 2723
		[MarshalAs(3)]
		public bool m_bPassword;

		// Token: 0x04000AA4 RID: 2724
		[MarshalAs(3)]
		public bool m_bSecure;

		// Token: 0x04000AA5 RID: 2725
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000AA6 RID: 2726
		public int m_nServerVersion;

		// Token: 0x04000AA7 RID: 2727
		[MarshalAs(30, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000AA8 RID: 2728
		[MarshalAs(30, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000AA9 RID: 2729
		public CSteamID m_steamID;
	}
}
