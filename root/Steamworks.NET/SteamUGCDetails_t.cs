using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000178 RID: 376
	[StructLayout(0, Pack = 4)]
	public struct SteamUGCDetails_t
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0000C53B File Offset: 0x0000A73B
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0000C548 File Offset: 0x0000A748
		public string m_rgchTitle
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTitle_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTitle_, 129);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0000C55B File Offset: 0x0000A75B
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0000C568 File Offset: 0x0000A768
		public string m_rgchDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchDescription_, 8000);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0000C57B File Offset: 0x0000A77B
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0000C588 File Offset: 0x0000A788
		public string m_rgchTags
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchTags_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchTags_, 1025);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0000C59B File Offset: 0x0000A79B
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public string m_pchFileName
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_pchFileName_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_pchFileName_, 260);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0000C5BB File Offset: 0x0000A7BB
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		public string m_rgchURL
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchURL_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchURL_, 256);
			}
		}

		// Token: 0x04000A01 RID: 2561
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000A02 RID: 2562
		public EResult m_eResult;

		// Token: 0x04000A03 RID: 2563
		public EWorkshopFileType m_eFileType;

		// Token: 0x04000A04 RID: 2564
		public AppId_t m_nCreatorAppID;

		// Token: 0x04000A05 RID: 2565
		public AppId_t m_nConsumerAppID;

		// Token: 0x04000A06 RID: 2566
		[MarshalAs(30, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x04000A07 RID: 2567
		[MarshalAs(30, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x04000A08 RID: 2568
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000A09 RID: 2569
		public uint m_rtimeCreated;

		// Token: 0x04000A0A RID: 2570
		public uint m_rtimeUpdated;

		// Token: 0x04000A0B RID: 2571
		public uint m_rtimeAddedToUserList;

		// Token: 0x04000A0C RID: 2572
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000A0D RID: 2573
		[MarshalAs(3)]
		public bool m_bBanned;

		// Token: 0x04000A0E RID: 2574
		[MarshalAs(3)]
		public bool m_bAcceptedForUse;

		// Token: 0x04000A0F RID: 2575
		[MarshalAs(3)]
		public bool m_bTagsTruncated;

		// Token: 0x04000A10 RID: 2576
		[MarshalAs(30, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x04000A11 RID: 2577
		public UGCHandle_t m_hFile;

		// Token: 0x04000A12 RID: 2578
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000A13 RID: 2579
		[MarshalAs(30, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x04000A14 RID: 2580
		public int m_nFileSize;

		// Token: 0x04000A15 RID: 2581
		public int m_nPreviewFileSize;

		// Token: 0x04000A16 RID: 2582
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x04000A17 RID: 2583
		public uint m_unVotesUp;

		// Token: 0x04000A18 RID: 2584
		public uint m_unVotesDown;

		// Token: 0x04000A19 RID: 2585
		public float m_flScore;

		// Token: 0x04000A1A RID: 2586
		public uint m_unNumChildren;

		// Token: 0x04000A1B RID: 2587
		public ulong m_ulTotalFilesSize;
	}
}
