using System;

namespace System.Security.Util
{
	// Token: 0x020003C7 RID: 967
	internal sealed class TokenizerStream
	{
		// Token: 0x06002950 RID: 10576 RVA: 0x00097CAA File Offset: 0x00095EAA
		internal TokenizerStream()
		{
			this.m_countTokens = 0;
			this.m_headTokens = new TokenizerShortBlock();
			this.m_headStrings = new TokenizerStringBlock();
			this.Reset();
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00097CD8 File Offset: 0x00095ED8
		internal void AddToken(short token)
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_currentTokens.m_next = new TokenizerShortBlock();
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			this.m_countTokens++;
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			block[indexTokens] = token;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x00097D50 File Offset: 0x00095F50
		internal void AddString(string str)
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings.m_next = new TokenizerStringBlock();
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			block[indexStrings] = str;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x00097DB8 File Offset: 0x00095FB8
		internal void Reset()
		{
			this.m_lastTokens = null;
			this.m_currentTokens = this.m_headTokens;
			this.m_currentStrings = this.m_headStrings;
			this.m_indexTokens = 0;
			this.m_indexStrings = 0;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x00097DE8 File Offset: 0x00095FE8
		internal short GetNextFullToken()
		{
			if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
			{
				this.m_lastTokens = this.m_currentTokens;
				this.m_currentTokens = this.m_currentTokens.m_next;
				this.m_indexTokens = 0;
			}
			short[] block = this.m_currentTokens.m_block;
			int indexTokens = this.m_indexTokens;
			this.m_indexTokens = indexTokens + 1;
			return block[indexTokens];
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x00097E4B File Offset: 0x0009604B
		internal short GetNextToken()
		{
			return this.GetNextFullToken() & 255;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x00097E5C File Offset: 0x0009605C
		internal string GetNextString()
		{
			if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
			{
				this.m_currentStrings = this.m_currentStrings.m_next;
				this.m_indexStrings = 0;
			}
			string[] block = this.m_currentStrings.m_block;
			int indexStrings = this.m_indexStrings;
			this.m_indexStrings = indexStrings + 1;
			return block[indexStrings];
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00097EB3 File Offset: 0x000960B3
		internal void ThrowAwayNextString()
		{
			this.GetNextString();
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x00097EBC File Offset: 0x000960BC
		internal void TagLastToken(short tag)
		{
			if (this.m_indexTokens == 0)
			{
				this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short)((ushort)this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (ushort)tag);
				return;
			}
			this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short)((ushort)this.m_currentTokens.m_block[this.m_indexTokens - 1] | (ushort)tag);
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x00097F3A File Offset: 0x0009613A
		internal int GetTokenCount()
		{
			return this.m_countTokens;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00097F44 File Offset: 0x00096144
		internal void GoToPosition(int position)
		{
			this.Reset();
			for (int i = 0; i < position; i++)
			{
				if (this.GetNextToken() == 3)
				{
					this.ThrowAwayNextString();
				}
			}
		}

		// Token: 0x04001E0C RID: 7692
		private int m_countTokens;

		// Token: 0x04001E0D RID: 7693
		private TokenizerShortBlock m_headTokens;

		// Token: 0x04001E0E RID: 7694
		private TokenizerShortBlock m_lastTokens;

		// Token: 0x04001E0F RID: 7695
		private TokenizerShortBlock m_currentTokens;

		// Token: 0x04001E10 RID: 7696
		private int m_indexTokens;

		// Token: 0x04001E11 RID: 7697
		private TokenizerStringBlock m_headStrings;

		// Token: 0x04001E12 RID: 7698
		private TokenizerStringBlock m_currentStrings;

		// Token: 0x04001E13 RID: 7699
		private int m_indexStrings;
	}
}
