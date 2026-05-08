using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000059 RID: 89
	internal struct StringReference
	{
		// Token: 0x170000FD RID: 253
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00012383 File Offset: 0x00010583
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001238B File Offset: 0x0001058B
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00012393 File Offset: 0x00010593
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001239B File Offset: 0x0001059B
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000123B2 File Offset: 0x000105B2
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x04000208 RID: 520
		private readonly char[] _chars;

		// Token: 0x04000209 RID: 521
		private readonly int _startIndex;

		// Token: 0x0400020A RID: 522
		private readonly int _length;
	}
}
