using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020001E8 RID: 488
	internal struct UnSafeCharBuffer
	{
		// Token: 0x06001735 RID: 5941 RVA: 0x0005BBF2 File Offset: 0x00059DF2
		[SecurityCritical]
		public unsafe UnSafeCharBuffer(char* buffer, int bufferSize)
		{
			this.m_buffer = buffer;
			this.m_totalSize = bufferSize;
			this.m_length = 0;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005BC0C File Offset: 0x00059E0C
		[SecuritySafeCritical]
		public unsafe void AppendString(string stringToAppend)
		{
			if (string.IsNullOrEmpty(stringToAppend))
			{
				return;
			}
			if (this.m_totalSize - this.m_length < stringToAppend.Length)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (string text = stringToAppend)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.Memcpy((byte*)(this.m_buffer + this.m_length), (byte*)ptr, stringToAppend.Length * 2);
			}
			this.m_length += stringToAppend.Length;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0005BC80 File Offset: 0x00059E80
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x0400152F RID: 5423
		[SecurityCritical]
		private unsafe char* m_buffer;

		// Token: 0x04001530 RID: 5424
		private int m_totalSize;

		// Token: 0x04001531 RID: 5425
		private int m_length;
	}
}
