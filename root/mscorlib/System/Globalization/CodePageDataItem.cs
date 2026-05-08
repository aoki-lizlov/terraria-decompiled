using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009FA RID: 2554
	[Serializable]
	internal class CodePageDataItem
	{
		// Token: 0x06005EFC RID: 24316 RVA: 0x0014457E File Offset: 0x0014277E
		[SecurityCritical]
		internal CodePageDataItem(int dataIndex)
		{
			this.m_dataIndex = dataIndex;
			this.m_uiFamilyCodePage = (int)EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
			this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x001445B9 File Offset: 0x001427B9
		[SecurityCritical]
		internal static string CreateString(string pStrings, uint index)
		{
			if (pStrings[0] == '|')
			{
				return pStrings.Split(CodePageDataItem.sep, StringSplitOptions.RemoveEmptyEntries)[(int)index];
			}
			return pStrings;
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06005EFE RID: 24318 RVA: 0x001445D6 File Offset: 0x001427D6
		public string WebName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_webName == null)
				{
					this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
				}
				return this.m_webName;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00144607 File Offset: 0x00142807
		public virtual int UIFamilyCodePage
		{
			get
			{
				return this.m_uiFamilyCodePage;
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x0014460F File Offset: 0x0014280F
		public string HeaderName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_headerName == null)
				{
					this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
				}
				return this.m_headerName;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06005F01 RID: 24321 RVA: 0x00144640 File Offset: 0x00142840
		public string BodyName
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_bodyName == null)
				{
					this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
				}
				return this.m_bodyName;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005F02 RID: 24322 RVA: 0x00144671 File Offset: 0x00142871
		public uint Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x00144679 File Offset: 0x00142879
		// Note: this type is marked as 'beforefieldinit'.
		static CodePageDataItem()
		{
		}

		// Token: 0x04003925 RID: 14629
		internal int m_dataIndex;

		// Token: 0x04003926 RID: 14630
		internal int m_uiFamilyCodePage;

		// Token: 0x04003927 RID: 14631
		internal string m_webName;

		// Token: 0x04003928 RID: 14632
		internal string m_headerName;

		// Token: 0x04003929 RID: 14633
		internal string m_bodyName;

		// Token: 0x0400392A RID: 14634
		internal uint m_flags;

		// Token: 0x0400392B RID: 14635
		private static readonly char[] sep = new char[] { '|' };
	}
}
