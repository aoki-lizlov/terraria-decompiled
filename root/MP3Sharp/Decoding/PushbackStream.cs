using System;
using System.IO;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200001A RID: 26
	public class PushbackStream
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00012EB3 File Offset: 0x000110B3
		internal PushbackStream(Stream s, int backBufferSize)
		{
			this._Stream = s;
			this._BackBufferSize = backBufferSize;
			this._TemporaryBuffer = new byte[this._BackBufferSize];
			this._CircularByteBuffer = new CircularByteBuffer(this._BackBufferSize);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00012EEC File Offset: 0x000110EC
		internal int Read(sbyte[] toRead, int offset, int length)
		{
			int num = 0;
			bool flag = true;
			while (num < length && flag)
			{
				if (this._NumForwardBytesInBuffer > 0)
				{
					this._NumForwardBytesInBuffer--;
					toRead[offset + num] = (sbyte)this._CircularByteBuffer[this._NumForwardBytesInBuffer];
					num++;
				}
				else
				{
					int num2 = length - num;
					int num3 = this._Stream.Read(this._TemporaryBuffer, 0, num2);
					flag = num3 >= num2;
					for (int i = 0; i < num3; i++)
					{
						this._CircularByteBuffer.Push(this._TemporaryBuffer[i]);
						toRead[offset + num + i] = (sbyte)this._TemporaryBuffer[i];
					}
					num += num3;
				}
			}
			return num;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00012F9D File Offset: 0x0001119D
		internal void UnRead(int length)
		{
			this._NumForwardBytesInBuffer += length;
			if (this._NumForwardBytesInBuffer > this._BackBufferSize)
			{
				throw new Exception("The backstream cannot unread the requested number of bytes.");
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00012FC6 File Offset: 0x000111C6
		internal void Close()
		{
			this._Stream.Close();
		}

		// Token: 0x040000B2 RID: 178
		private readonly int _BackBufferSize;

		// Token: 0x040000B3 RID: 179
		private readonly CircularByteBuffer _CircularByteBuffer;

		// Token: 0x040000B4 RID: 180
		private readonly Stream _Stream;

		// Token: 0x040000B5 RID: 181
		private readonly byte[] _TemporaryBuffer;

		// Token: 0x040000B6 RID: 182
		private int _NumForwardBytesInBuffer;
	}
}
