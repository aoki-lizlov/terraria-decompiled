using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000B4 RID: 180
	[CallbackIdentity(1318)]
	[StructLayout(0, Pack = 4)]
	public struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0000C3DB File Offset: 0x0000A5DB
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0000C3FB File Offset: 0x0000A5FB
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0000C408 File Offset: 0x0000A608
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0000C41B File Offset: 0x0000A61B
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0000C428 File Offset: 0x0000A628
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0000C43B File Offset: 0x0000A63B
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0000C448 File Offset: 0x0000A648
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0000C45B File Offset: 0x0000A65B
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000C468 File Offset: 0x0000A668
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

		// Token: 0x040001F8 RID: 504
		public const int k_iCallback = 1318;

		// Token: 0x040001F9 RID: 505
		public EResult m_eResult;

		// Token: 0x040001FA RID: 506
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x040001FB RID: 507
		public AppId_t m_nCreatorAppID;

		// Token: 0x040001FC RID: 508
		public AppId_t m_nConsumerAppID;

		// Token: 0x040001FD RID: 509
		[MarshalAs(30, SizeConst = 129)]
		private byte[] m_rgchTitle_;

		// Token: 0x040001FE RID: 510
		[MarshalAs(30, SizeConst = 8000)]
		private byte[] m_rgchDescription_;

		// Token: 0x040001FF RID: 511
		public UGCHandle_t m_hFile;

		// Token: 0x04000200 RID: 512
		public UGCHandle_t m_hPreviewFile;

		// Token: 0x04000201 RID: 513
		public ulong m_ulSteamIDOwner;

		// Token: 0x04000202 RID: 514
		public uint m_rtimeCreated;

		// Token: 0x04000203 RID: 515
		public uint m_rtimeUpdated;

		// Token: 0x04000204 RID: 516
		public ERemoteStoragePublishedFileVisibility m_eVisibility;

		// Token: 0x04000205 RID: 517
		[MarshalAs(3)]
		public bool m_bBanned;

		// Token: 0x04000206 RID: 518
		[MarshalAs(30, SizeConst = 1025)]
		private byte[] m_rgchTags_;

		// Token: 0x04000207 RID: 519
		[MarshalAs(3)]
		public bool m_bTagsTruncated;

		// Token: 0x04000208 RID: 520
		[MarshalAs(30, SizeConst = 260)]
		private byte[] m_pchFileName_;

		// Token: 0x04000209 RID: 521
		public int m_nFileSize;

		// Token: 0x0400020A RID: 522
		public int m_nPreviewFileSize;

		// Token: 0x0400020B RID: 523
		[MarshalAs(30, SizeConst = 256)]
		private byte[] m_rgchURL_;

		// Token: 0x0400020C RID: 524
		public EWorkshopFileType m_eFileType;

		// Token: 0x0400020D RID: 525
		[MarshalAs(3)]
		public bool m_bAcceptedForUse;
	}
}
