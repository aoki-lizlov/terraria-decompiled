using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020009F1 RID: 2545
	[ComVisible(true)]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06005E2E RID: 24110 RVA: 0x00141FFD File Offset: 0x001401FD
		internal TextElementEnumerator(string str, int startIndex, int strLen)
		{
			this.str = str;
			this.startIndex = startIndex;
			this.strLen = strLen;
			this.Reset();
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x00142020 File Offset: 0x00140220
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.charLen = -1;
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x0014202C File Offset: 0x0014022C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.strLen = this.endIndex + 1;
			this.currTextElementLen = this.nextTextElementLen;
			if (this.charLen == -1)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x00142079 File Offset: 0x00140279
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.endIndex = this.strLen - 1;
			this.nextTextElementLen = this.currTextElementLen;
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x00142098 File Offset: 0x00140298
		public bool MoveNext()
		{
			if (this.index >= this.strLen)
			{
				this.index = this.strLen + 1;
				return false;
			}
			this.currTextElementLen = StringInfo.GetCurrentTextElementLen(this.str, this.index, this.strLen, ref this.uc, ref this.charLen);
			this.index += this.currTextElementLen;
			return true;
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06005E33 RID: 24115 RVA: 0x00142100 File Offset: 0x00140300
		public object Current
		{
			get
			{
				return this.GetTextElement();
			}
		}

		// Token: 0x06005E34 RID: 24116 RVA: 0x00142108 File Offset: 0x00140308
		public string GetTextElement()
		{
			if (this.index == this.startIndex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
			}
			if (this.index > this.strLen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
			}
			return this.str.Substring(this.index - this.currTextElementLen, this.currTextElementLen);
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x0014216F File Offset: 0x0014036F
		public int ElementIndex
		{
			get
			{
				if (this.index == this.startIndex)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
				}
				return this.index - this.currTextElementLen;
			}
		}

		// Token: 0x06005E36 RID: 24118 RVA: 0x0014219C File Offset: 0x0014039C
		public void Reset()
		{
			this.index = this.startIndex;
			if (this.index < this.strLen)
			{
				this.uc = CharUnicodeInfo.InternalGetUnicodeCategory(this.str, this.index, out this.charLen);
			}
		}

		// Token: 0x040038CE RID: 14542
		private string str;

		// Token: 0x040038CF RID: 14543
		private int index;

		// Token: 0x040038D0 RID: 14544
		private int startIndex;

		// Token: 0x040038D1 RID: 14545
		[NonSerialized]
		private int strLen;

		// Token: 0x040038D2 RID: 14546
		[NonSerialized]
		private int currTextElementLen;

		// Token: 0x040038D3 RID: 14547
		[OptionalField(VersionAdded = 2)]
		private UnicodeCategory uc;

		// Token: 0x040038D4 RID: 14548
		[OptionalField(VersionAdded = 2)]
		private int charLen;

		// Token: 0x040038D5 RID: 14549
		private int endIndex;

		// Token: 0x040038D6 RID: 14550
		private int nextTextElementLen;
	}
}
