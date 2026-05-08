using System;

namespace System.Text
{
	// Token: 0x02000372 RID: 882
	public abstract class EncoderFallbackBuffer
	{
		// Token: 0x060025D6 RID: 9686
		public abstract bool Fallback(char charUnknown, int index);

		// Token: 0x060025D7 RID: 9687
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		// Token: 0x060025D8 RID: 9688
		public abstract char GetNextChar();

		// Token: 0x060025D9 RID: 9689
		public abstract bool MovePrevious();

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060025DA RID: 9690
		public abstract int Remaining { get; }

		// Token: 0x060025DB RID: 9691 RVA: 0x00087355 File Offset: 0x00085555
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0008735F File Offset: 0x0008555F
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0008737D File Offset: 0x0008557D
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000873B4 File Offset: 0x000855B4
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = nextChar > '\0';
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000873E0 File Offset: 0x000855E0
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int num = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder._charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num2 = this.iRecursionCount;
							this.iRecursionCount = num2 + 1;
							if (num2 > 250)
							{
								this.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, num);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num2 = this.iRecursionCount;
				this.iRecursionCount = num2 + 1;
				if (num2 > 250)
				{
					this.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, num);
			return this.bFallingBack;
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000874DE File Offset: 0x000856DE
		internal void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(SR.Format("Recursive fallback not allowed for character \\\\u{0:X4}.", charRecursive), "chars");
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000025BE File Offset: 0x000007BE
		protected EncoderFallbackBuffer()
		{
		}

		// Token: 0x04001C7E RID: 7294
		internal unsafe char* charStart;

		// Token: 0x04001C7F RID: 7295
		internal unsafe char* charEnd;

		// Token: 0x04001C80 RID: 7296
		internal EncoderNLS encoder;

		// Token: 0x04001C81 RID: 7297
		internal bool setEncoder;

		// Token: 0x04001C82 RID: 7298
		internal bool bUsedEncoder;

		// Token: 0x04001C83 RID: 7299
		internal bool bFallingBack;

		// Token: 0x04001C84 RID: 7300
		internal int iRecursionCount;

		// Token: 0x04001C85 RID: 7301
		private const int iMaxRecursion = 250;
	}
}
