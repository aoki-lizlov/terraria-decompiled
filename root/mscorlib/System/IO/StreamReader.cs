using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000931 RID: 2353
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x060053E0 RID: 21472 RVA: 0x0011A22F File Offset: 0x0011842F
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncReadTask.IsCompleted)
			{
				StreamReader.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x060053E1 RID: 21473 RVA: 0x0011A243 File Offset: 0x00118443
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x060053E2 RID: 21474 RVA: 0x0011A24F File Offset: 0x0011844F
		internal StreamReader()
		{
		}

		// Token: 0x060053E3 RID: 21475 RVA: 0x0011A262 File Offset: 0x00118462
		public StreamReader(Stream stream)
			: this(stream, true)
		{
		}

		// Token: 0x060053E4 RID: 21476 RVA: 0x0011A26C File Offset: 0x0011846C
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		// Token: 0x060053E5 RID: 21477 RVA: 0x0011A281 File Offset: 0x00118481
		public StreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, true, 1024, false)
		{
		}

		// Token: 0x060053E6 RID: 21478 RVA: 0x0011A292 File Offset: 0x00118492
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, 1024, false)
		{
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x0011A2A3 File Offset: 0x001184A3
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x0011A2B4 File Offset: 0x001184B4
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream was not readable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x0011A322 File Offset: 0x00118522
		public StreamReader(string path)
			: this(path, true)
		{
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x0011A32C File Offset: 0x0011852C
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x0011A340 File Offset: 0x00118540
		public StreamReader(string path, Encoding encoding)
			: this(path, encoding, true, 1024)
		{
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x0011A350 File Offset: 0x00118550
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, 1024)
		{
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x0011A360 File Offset: 0x00118560
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Empty path name is not legal.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x0011A3E4 File Offset: 0x001185E4
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this._stream = stream;
			this._encoding = encoding;
			this._decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this._charBuffer = new char[this._maxCharsPerBuffer];
			this._byteLen = 0;
			this._bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._checkPreamble = encoding.Preamble.Length > 0;
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x0011A485 File Offset: 0x00118685
		internal void Init(Stream stream)
		{
			this._stream = stream;
			this._closable = true;
		}

		// Token: 0x060053F0 RID: 21488 RVA: 0x0011A495 File Offset: 0x00118695
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060053F1 RID: 21489 RVA: 0x0011A4A0 File Offset: 0x001186A0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this._stream != null)
				{
					this._stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					this._stream = null;
					this._encoding = null;
					this._decoder = null;
					this._byteBuffer = null;
					this._charBuffer = null;
					this._charPos = 0;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x0011A528 File Offset: 0x00118728
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060053F3 RID: 21491 RVA: 0x0011A530 File Offset: 0x00118730
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060053F4 RID: 21492 RVA: 0x0011A538 File Offset: 0x00118738
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x0011A543 File Offset: 0x00118743
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this._byteLen = 0;
			this._charLen = 0;
			this._charPos = 0;
			if (this._encoding != null)
			{
				this._decoder = this._encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x0011A580 File Offset: 0x00118780
		public bool EndOfStream
		{
			get
			{
				if (this._stream == null)
				{
					throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
				}
				this.CheckAsyncTaskInProgress();
				return this._charPos >= this._charLen && this.ReadBuffer() == 0;
			}
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x0011A5B8 File Offset: 0x001187B8
		public override int Peek()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this._charBuffer[this._charPos];
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0011A60C File Offset: 0x0011880C
		public override int Read()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int num = (int)this._charBuffer[this._charPos];
			this._charPos++;
			return num;
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0011A668 File Offset: 0x00118868
		public override int Read(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.ReadSpan(new Span<char>(buffer, index, count));
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0011A6CC File Offset: 0x001188CC
		public override int Read(Span<char> buffer)
		{
			if (!(base.GetType() == typeof(StreamReader)))
			{
				return base.Read(buffer);
			}
			return this.ReadSpan(buffer);
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0011A6F4 File Offset: 0x001188F4
		private int ReadSpan(Span<char> buffer)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			int i = buffer.Length;
			while (i > 0)
			{
				int num2 = this._charLen - this._charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer.Slice(num), out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > i)
				{
					num2 = i;
				}
				if (!flag)
				{
					new Span<char>(this._charBuffer, this._charPos, num2).CopyTo(buffer.Slice(num));
					this._charPos += num2;
				}
				num += num2;
				i -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0011A7A0 File Offset: 0x001189A0
		public override string ReadToEnd()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this._charLen - this._charPos);
			do
			{
				stringBuilder.Append(this._charBuffer, this._charPos, this._charLen - this._charPos);
				this._charPos = this._charLen;
				this.ReadBuffer();
			}
			while (this._charLen > 0);
			return stringBuilder.ToString();
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0011A81C File Offset: 0x00118A1C
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0011A898 File Offset: 0x00118A98
		public override int ReadBlock(Span<char> buffer)
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlock(buffer);
			}
			int num = 0;
			int num2;
			do
			{
				num2 = this.ReadSpan(buffer.Slice(num));
				num += num2;
			}
			while (num2 > 0 && num < buffer.Length);
			return num;
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0011A8E8 File Offset: 0x00118AE8
		private void CompressBuffer(int n)
		{
			Buffer.BlockCopy(this._byteBuffer, n, this._byteBuffer, 0, this._byteLen - n);
			this._byteLen -= n;
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0011A914 File Offset: 0x00118B14
		private void DetectEncoding()
		{
			if (this._byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this._byteBuffer[0] == 254 && this._byteBuffer[1] == 255)
			{
				this._encoding = Encoding.BigEndianUnicode;
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this._byteBuffer[0] == 255 && this._byteBuffer[1] == 254)
			{
				if (this._byteLen < 4 || this._byteBuffer[2] != 0 || this._byteBuffer[3] != 0)
				{
					this._encoding = Encoding.Unicode;
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this._encoding = Encoding.UTF32;
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this._byteLen >= 3 && this._byteBuffer[0] == 239 && this._byteBuffer[1] == 187 && this._byteBuffer[2] == 191)
			{
				this._encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this._byteLen >= 4 && this._byteBuffer[0] == 0 && this._byteBuffer[1] == 0 && this._byteBuffer[2] == 254 && this._byteBuffer[3] == 255)
			{
				this._encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this._byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this._decoder = this._encoding.GetDecoder();
				int maxCharCount = this._encoding.GetMaxCharCount(this._byteBuffer.Length);
				if (maxCharCount > this._maxCharsPerBuffer)
				{
					this._charBuffer = new char[maxCharCount];
				}
				this._maxCharsPerBuffer = maxCharCount;
			}
		}

		// Token: 0x06005401 RID: 21505 RVA: 0x0011AACC File Offset: 0x00118CCC
		private unsafe bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			ReadOnlySpan<byte> preamble = this._encoding.Preamble;
			int num = ((this._byteLen >= preamble.Length) ? (preamble.Length - this._bytePos) : (this._byteLen - this._bytePos));
			int i = 0;
			while (i < num)
			{
				if (this._byteBuffer[this._bytePos] != *preamble[this._bytePos])
				{
					this._bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this._bytePos++;
			}
			if (this._checkPreamble && this._bytePos == preamble.Length)
			{
				this.CompressBuffer(preamble.Length);
				this._bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x06005402 RID: 21506 RVA: 0x0011ABA8 File Offset: 0x00118DA8
		internal virtual int ReadBuffer()
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this._byteLen < this._byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = (this._byteLen = 0);
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0011AD10 File Offset: 0x00118F10
		private int ReadBuffer(Span<char> userBuffer, out bool readToUserBuffer)
		{
			this._charLen = 0;
			this._charPos = 0;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = userBuffer.Length >= this._maxCharsPerBuffer;
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this._stream.Read(this._byteBuffer, this._bytePos, this._byteBuffer.Length - this._bytePos);
					if (num2 == 0)
					{
						break;
					}
					this._byteLen += num2;
				}
				else
				{
					this._byteLen = this._stream.Read(this._byteBuffer, 0, this._byteBuffer.Length);
					if (this._byteLen == 0)
					{
						goto IL_01CD;
					}
				}
				this._isBlocked = this._byteLen < this._byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = userBuffer.Length >= this._maxCharsPerBuffer;
					}
					this._charPos = 0;
					if (readToUserBuffer)
					{
						num += this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
						this._charLen = 0;
					}
					else
					{
						num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
						this._charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_01CD;
				}
			}
			if (this._byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this._decoder.GetChars(new ReadOnlySpan<byte>(this._byteBuffer, 0, this._byteLen), userBuffer.Slice(num), false);
					this._charLen = 0;
				}
				else
				{
					num = this._decoder.GetChars(this._byteBuffer, 0, this._byteLen, this._charBuffer, num);
					this._charLen += num;
				}
			}
			return num;
			IL_01CD:
			this._isBlocked &= num < userBuffer.Length;
			return num;
		}

		// Token: 0x06005404 RID: 21508 RVA: 0x0011AF04 File Offset: 0x00119104
		public override string ReadLine()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this._charPos;
				do
				{
					c = this._charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_0051;
					}
					num++;
				}
				while (num < this._charLen);
				num = this._charLen - this._charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this._charBuffer, this._charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_0051:
			string text;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this._charBuffer, this._charPos, num - this._charPos);
				text = stringBuilder.ToString();
			}
			else
			{
				text = new string(this._charBuffer, this._charPos, num - this._charPos);
			}
			this._charPos = num + 1;
			if (c == '\r' && (this._charPos < this._charLen || this.ReadBuffer() > 0) && this._charBuffer[this._charPos] == '\n')
			{
				this._charPos++;
			}
			return text;
			Block_11:
			return stringBuilder.ToString();
		}

		// Token: 0x06005405 RID: 21509 RVA: 0x0011B03C File Offset: 0x0011923C
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06005406 RID: 21510 RVA: 0x0011B090 File Offset: 0x00119290
		private async Task<string> ReadLineAsyncInternal()
		{
			bool flag = this._charPos == this._charLen;
			ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (flag)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() == 0;
			}
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				StringBuilder sb = null;
				char[] charBuffer;
				int charLen;
				int num2;
				int num;
				char c;
				for (;;)
				{
					charBuffer = this._charBuffer;
					charLen = this._charLen;
					num = (num2 = this._charPos);
					do
					{
						c = charBuffer[num2];
						if (c == '\r' || c == '\n')
						{
							goto IL_00E1;
						}
						num2++;
					}
					while (num2 < charLen);
					num2 = charLen - num;
					if (sb == null)
					{
						sb = new StringBuilder(num2 + 80);
					}
					sb.Append(charBuffer, num, num2);
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						await configuredTaskAwaiter;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					}
					if (configuredTaskAwaiter.GetResult() <= 0)
					{
						goto Block_14;
					}
				}
				IL_00E1:
				string s;
				if (sb != null)
				{
					sb.Append(charBuffer, num, num2 - num);
					s = sb.ToString();
				}
				else
				{
					s = new string(charBuffer, num, num2 - num);
				}
				int num3 = num2 + 1;
				this._charPos = num3;
				flag = c == '\r';
				if (flag)
				{
					bool flag2 = num3 < charLen;
					if (!flag2)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							await configuredTaskAwaiter;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						}
						flag2 = configuredTaskAwaiter.GetResult() > 0;
					}
					flag = flag2;
				}
				if (flag)
				{
					num = this._charPos;
					if (this._charBuffer[num] == '\n')
					{
						this._charPos = num + 1;
					}
				}
				return s;
				Block_14:
				text = sb.ToString();
			}
			return text;
		}

		// Token: 0x06005407 RID: 21511 RVA: 0x0011B0D4 File Offset: 0x001192D4
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x06005408 RID: 21512 RVA: 0x0011B128 File Offset: 0x00119328
		private async Task<string> ReadToEndAsyncInternal()
		{
			StringBuilder sb = new StringBuilder(this._charLen - this._charPos);
			do
			{
				int charPos = this._charPos;
				sb.Append(this._charBuffer, charPos, this._charLen - charPos);
				this._charPos = this._charLen;
				await this.ReadBufferAsync().ConfigureAwait(false);
			}
			while (this._charLen > 0);
			return sb.ToString();
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0011B16C File Offset: 0x0011936C
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x0011B228 File Offset: 0x00119428
		public override ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			return this.ReadAsyncInternal(buffer, cancellationToken);
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0011B28C File Offset: 0x0011948C
		internal override async ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			bool flag = this._charPos == this._charLen;
			if (flag)
			{
				ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter = this.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() == 0;
			}
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int charsRead = 0;
				bool readToUserBuffer = false;
				byte[] tmpByteBuffer = this._byteBuffer;
				Stream tmpStream = this._stream;
				int count = buffer.Length;
				while (count > 0)
				{
					int i = this._charLen - this._charPos;
					if (i == 0)
					{
						this._charLen = 0;
						this._charPos = 0;
						if (!this._checkPreamble)
						{
							this._byteLen = 0;
						}
						readToUserBuffer = count >= this._maxCharsPerBuffer;
						do
						{
							if (this._checkPreamble)
							{
								int bytePos = this._bytePos;
								int num2 = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), cancellationToken).ConfigureAwait(false);
								if (num2 == 0)
								{
									goto Block_7;
								}
								this._byteLen += num2;
							}
							else
							{
								this._byteLen = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), cancellationToken).ConfigureAwait(false);
								if (this._byteLen == 0)
								{
									goto Block_10;
								}
							}
							this._isBlocked = this._byteLen < tmpByteBuffer.Length;
							if (!this.IsPreamble())
							{
								if (this._detectEncoding && this._byteLen >= 2)
								{
									this.DetectEncoding();
									readToUserBuffer = count >= this._maxCharsPerBuffer;
								}
								this._charPos = 0;
								if (readToUserBuffer)
								{
									i += this._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, this._byteLen), buffer.Span.Slice(charsRead), false);
									this._charLen = 0;
								}
								else
								{
									i = this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, 0);
									this._charLen += i;
								}
							}
						}
						while (i == 0);
						IL_0423:
						if (i != 0)
						{
							goto IL_042E;
						}
						break;
						Block_10:
						this._isBlocked = true;
						goto IL_0423;
						Block_7:
						if (this._byteLen > 0)
						{
							if (readToUserBuffer)
							{
								i = this._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, this._byteLen), buffer.Span.Slice(charsRead), false);
								this._charLen = 0;
							}
							else
							{
								i = this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, 0);
								this._charLen += i;
							}
						}
						this._isBlocked = true;
						goto IL_0423;
					}
					IL_042E:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						new Span<char>(this._charBuffer, this._charPos, i).CopyTo(buffer.Span.Slice(charsRead));
						this._charPos += i;
					}
					charsRead += i;
					count -= i;
					if (this._isBlocked)
					{
						break;
					}
				}
				num = charsRead;
			}
			return num;
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0011B2E0 File Offset: 0x001194E0
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0011B384 File Offset: 0x00119584
		public override ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot read from a closed TextReader.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> valueTask = base.ReadBlockAsyncInternal(buffer, cancellationToken);
			if (valueTask.IsCompletedSuccessfully)
			{
				return valueTask;
			}
			Task<int> task = valueTask.AsTask();
			this._asyncReadTask = task;
			return new ValueTask<int>(task);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0011B40C File Offset: 0x0011960C
		private async Task<int> ReadBufferAsync()
		{
			this._charLen = 0;
			this._charPos = 0;
			byte[] tmpByteBuffer = this._byteBuffer;
			Stream tmpStream = this._stream;
			if (!this._checkPreamble)
			{
				this._byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int bytePos = this._bytePos;
					int num = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), default(CancellationToken)).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					this._byteLen += num;
				}
				else
				{
					this._byteLen = await tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), default(CancellationToken)).ConfigureAwait(false);
					if (this._byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this._byteLen < tmpByteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this._byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this._charLen += this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				}
				if (this._charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this._byteLen > 0)
			{
				this._charLen += this._decoder.GetChars(tmpByteBuffer, 0, this._byteLen, this._charBuffer, this._charLen);
				this._bytePos = 0;
				this._byteLen = 0;
			}
			return this._charLen;
			Block_5:
			return this._charLen;
			Block_9:
			return this._charLen;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x0011B44F File Offset: 0x0011964F
		internal bool DataAvailable()
		{
			return this._charPos < this._charLen;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0011B45F File Offset: 0x0011965F
		// Note: this type is marked as 'beforefieldinit'.
		static StreamReader()
		{
		}

		// Token: 0x04003348 RID: 13128
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x04003349 RID: 13129
		private const int DefaultBufferSize = 1024;

		// Token: 0x0400334A RID: 13130
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x0400334B RID: 13131
		private const int MinBufferSize = 128;

		// Token: 0x0400334C RID: 13132
		private Stream _stream;

		// Token: 0x0400334D RID: 13133
		private Encoding _encoding;

		// Token: 0x0400334E RID: 13134
		private Decoder _decoder;

		// Token: 0x0400334F RID: 13135
		private byte[] _byteBuffer;

		// Token: 0x04003350 RID: 13136
		private char[] _charBuffer;

		// Token: 0x04003351 RID: 13137
		private int _charPos;

		// Token: 0x04003352 RID: 13138
		private int _charLen;

		// Token: 0x04003353 RID: 13139
		private int _byteLen;

		// Token: 0x04003354 RID: 13140
		private int _bytePos;

		// Token: 0x04003355 RID: 13141
		private int _maxCharsPerBuffer;

		// Token: 0x04003356 RID: 13142
		private bool _detectEncoding;

		// Token: 0x04003357 RID: 13143
		private bool _checkPreamble;

		// Token: 0x04003358 RID: 13144
		private bool _isBlocked;

		// Token: 0x04003359 RID: 13145
		private bool _closable;

		// Token: 0x0400335A RID: 13146
		private Task _asyncReadTask = Task.CompletedTask;

		// Token: 0x02000932 RID: 2354
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06005411 RID: 21521 RVA: 0x0011B46B File Offset: 0x0011966B
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x17000DE7 RID: 3559
			// (get) Token: 0x06005412 RID: 21522 RVA: 0x0011B47E File Offset: 0x0011967E
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x06005413 RID: 21523 RVA: 0x0011B485 File Offset: 0x00119685
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06005414 RID: 21524 RVA: 0x00004088 File Offset: 0x00002288
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06005415 RID: 21525 RVA: 0x0011B48C File Offset: 0x0011968C
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06005416 RID: 21526 RVA: 0x0011B48C File Offset: 0x0011968C
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06005417 RID: 21527 RVA: 0x0000408A File Offset: 0x0000228A
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06005418 RID: 21528 RVA: 0x0000A9B6 File Offset: 0x00008BB6
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06005419 RID: 21529 RVA: 0x00004091 File Offset: 0x00002291
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x0600541A RID: 21530 RVA: 0x0000408A File Offset: 0x0000228A
			internal override int ReadBuffer()
			{
				return 0;
			}
		}

		// Token: 0x02000933 RID: 2355
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadLineAsyncInternal>d__61 : IAsyncStateMachine
		{
			// Token: 0x0600541B RID: 21531 RVA: 0x0011B490 File Offset: 0x00119690
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				string text;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					bool flag;
					switch (num)
					{
					case 0:
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					case 1:
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_01A9;
					case 2:
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_0287;
					default:
						flag = streamReader._charPos == streamReader._charLen;
						if (!flag)
						{
							goto IL_009E;
						}
						configuredTaskAwaiter3 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 0;
							configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						break;
					}
					flag = configuredTaskAwaiter3.GetResult() == 0;
					IL_009E:
					if (flag)
					{
						text = null;
						goto IL_02C2;
					}
					sb = null;
					IL_00AF:
					char[] charBuffer = streamReader._charBuffer;
					int charLen = streamReader._charLen;
					int num3 = streamReader._charPos;
					int num4 = num3;
					char c;
					for (;;)
					{
						c = charBuffer[num4];
						if (c == '\r' || c == '\n')
						{
							break;
						}
						num4++;
						if (num4 >= charLen)
						{
							goto Block_14;
						}
					}
					if (sb != null)
					{
						sb.Append(charBuffer, num3, num4 - num3);
						s = sb.ToString();
					}
					else
					{
						s = new string(charBuffer, num3, num4 - num3);
					}
					num3 = (streamReader._charPos = num4 + 1);
					flag = c == '\r';
					if (!flag)
					{
						goto IL_01B8;
					}
					bool flag2 = num3 < charLen;
					if (flag2)
					{
						goto IL_01B5;
					}
					configuredTaskAwaiter3 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						num2 = 1;
						configuredTaskAwaiter2 = configuredTaskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref configuredTaskAwaiter3, ref this);
						return;
					}
					goto IL_01A9;
					Block_14:
					num4 = charLen - num3;
					if (sb == null)
					{
						sb = new StringBuilder(num4 + 80);
					}
					sb.Append(charBuffer, num3, num4);
					configuredTaskAwaiter3 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						num2 = 2;
						configuredTaskAwaiter2 = configuredTaskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__61>(ref configuredTaskAwaiter3, ref this);
						return;
					}
					goto IL_0287;
					IL_01A9:
					flag2 = configuredTaskAwaiter3.GetResult() > 0;
					IL_01B5:
					flag = flag2;
					IL_01B8:
					if (flag)
					{
						num3 = streamReader._charPos;
						if (streamReader._charBuffer[num3] == '\n')
						{
							streamReader._charPos = num3 + 1;
						}
					}
					text = s;
					goto IL_02C2;
					IL_0287:
					if (configuredTaskAwaiter3.GetResult() > 0)
					{
						goto IL_00AF;
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					sb = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_02C2:
				num2 = -2;
				sb = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x0600541C RID: 21532 RVA: 0x0011B798 File Offset: 0x00119998
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400335B RID: 13147
			public int <>1__state;

			// Token: 0x0400335C RID: 13148
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400335D RID: 13149
			public StreamReader <>4__this;

			// Token: 0x0400335E RID: 13150
			private StringBuilder <sb>5__2;

			// Token: 0x0400335F RID: 13151
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003360 RID: 13152
			private string <s>5__3;
		}

		// Token: 0x02000934 RID: 2356
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsyncInternal>d__63 : IAsyncStateMachine
		{
			// Token: 0x0600541D RID: 21533 RVA: 0x0011B7A8 File Offset: 0x001199A8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				string text;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num == 0)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_00B8;
					}
					sb = new StringBuilder(streamReader._charLen - streamReader._charPos);
					IL_002C:
					int charPos = streamReader._charPos;
					sb.Append(streamReader._charBuffer, charPos, streamReader._charLen - charPos);
					streamReader._charPos = streamReader._charLen;
					configuredTaskAwaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 0;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadToEndAsyncInternal>d__63>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_00B8:
					configuredTaskAwaiter.GetResult();
					if (streamReader._charLen > 0)
					{
						goto IL_002C;
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					sb = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				sb = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x0600541E RID: 21534 RVA: 0x0011B8DC File Offset: 0x00119ADC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003361 RID: 13153
			public int <>1__state;

			// Token: 0x04003362 RID: 13154
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04003363 RID: 13155
			public StreamReader <>4__this;

			// Token: 0x04003364 RID: 13156
			private StringBuilder <sb>5__2;

			// Token: 0x04003365 RID: 13157
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000935 RID: 2357
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__66 : IAsyncStateMachine
		{
			// Token: 0x0600541F RID: 21535 RVA: 0x0011B8EC File Offset: 0x00119AEC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				int num3;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
					bool flag;
					switch (num)
					{
					case 0:
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					case 1:
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_01D1;
					}
					case 2:
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_030F;
					}
					default:
						flag = streamReader._charPos == streamReader._charLen;
						if (!flag)
						{
							goto IL_009E;
						}
						configuredTaskAwaiter3 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 0;
							configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						break;
					}
					flag = configuredTaskAwaiter3.GetResult() == 0;
					IL_009E:
					if (flag)
					{
						num3 = 0;
						goto IL_0507;
					}
					charsRead = 0;
					readToUserBuffer = false;
					tmpByteBuffer = streamReader._byteBuffer;
					tmpStream = streamReader._stream;
					count = buffer.Length;
					goto IL_04CB;
					IL_0136:
					if (streamReader._checkPreamble)
					{
						int bytePos = streamReader._bytePos;
						configuredValueTaskAwaiter = tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 1;
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredValueTaskAwaiter = tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 2;
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadAsyncInternal>d__66>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						goto IL_030F;
					}
					IL_01D1:
					int result = configuredValueTaskAwaiter.GetResult();
					if (result == 0)
					{
						if (streamReader._byteLen > 0)
						{
							if (readToUserBuffer)
							{
								i = streamReader._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, streamReader._byteLen), buffer.Span.Slice(charsRead), false);
								streamReader._charLen = 0;
							}
							else
							{
								i = streamReader._decoder.GetChars(tmpByteBuffer, 0, streamReader._byteLen, streamReader._charBuffer, 0);
								streamReader._charLen += i;
							}
						}
						streamReader._isBlocked = true;
						goto IL_0423;
					}
					streamReader._byteLen += result;
					goto IL_0334;
					IL_030F:
					int result2 = configuredValueTaskAwaiter.GetResult();
					streamReader._byteLen = result2;
					if (streamReader._byteLen == 0)
					{
						streamReader._isBlocked = true;
						goto IL_0423;
					}
					IL_0334:
					streamReader._isBlocked = streamReader._byteLen < tmpByteBuffer.Length;
					if (!streamReader.IsPreamble())
					{
						if (streamReader._detectEncoding && streamReader._byteLen >= 2)
						{
							streamReader.DetectEncoding();
							readToUserBuffer = count >= streamReader._maxCharsPerBuffer;
						}
						streamReader._charPos = 0;
						if (readToUserBuffer)
						{
							i += streamReader._decoder.GetChars(new ReadOnlySpan<byte>(tmpByteBuffer, 0, streamReader._byteLen), buffer.Span.Slice(charsRead), false);
							streamReader._charLen = 0;
						}
						else
						{
							i = streamReader._decoder.GetChars(tmpByteBuffer, 0, streamReader._byteLen, streamReader._charBuffer, 0);
							streamReader._charLen += i;
						}
					}
					if (i == 0)
					{
						goto IL_0136;
					}
					IL_0423:
					if (i == 0)
					{
						goto IL_04D7;
					}
					IL_042E:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						new Span<char>(streamReader._charBuffer, streamReader._charPos, i).CopyTo(buffer.Span.Slice(charsRead));
						streamReader._charPos += i;
					}
					charsRead += i;
					count -= i;
					if (streamReader._isBlocked)
					{
						goto IL_04D7;
					}
					IL_04CB:
					if (count > 0)
					{
						i = streamReader._charLen - streamReader._charPos;
						if (i == 0)
						{
							streamReader._charLen = 0;
							streamReader._charPos = 0;
							if (!streamReader._checkPreamble)
							{
								streamReader._byteLen = 0;
							}
							readToUserBuffer = count >= streamReader._maxCharsPerBuffer;
							goto IL_0136;
						}
						goto IL_042E;
					}
					IL_04D7:
					num3 = charsRead;
				}
				catch (Exception ex)
				{
					num2 = -2;
					tmpByteBuffer = null;
					tmpStream = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_0507:
				num2 = -2;
				tmpByteBuffer = null;
				tmpStream = null;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x06005420 RID: 21536 RVA: 0x0011BE40 File Offset: 0x0011A040
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003366 RID: 13158
			public int <>1__state;

			// Token: 0x04003367 RID: 13159
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003368 RID: 13160
			public StreamReader <>4__this;

			// Token: 0x04003369 RID: 13161
			public Memory<char> buffer;

			// Token: 0x0400336A RID: 13162
			public CancellationToken cancellationToken;

			// Token: 0x0400336B RID: 13163
			private int <charsRead>5__2;

			// Token: 0x0400336C RID: 13164
			private bool <readToUserBuffer>5__3;

			// Token: 0x0400336D RID: 13165
			private byte[] <tmpByteBuffer>5__4;

			// Token: 0x0400336E RID: 13166
			private Stream <tmpStream>5__5;

			// Token: 0x0400336F RID: 13167
			private int <count>5__6;

			// Token: 0x04003370 RID: 13168
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003371 RID: 13169
			private int <n>5__7;

			// Token: 0x04003372 RID: 13170
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000936 RID: 2358
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBufferAsync>d__69 : IAsyncStateMachine
		{
			// Token: 0x06005421 RID: 21537 RVA: 0x0011BE50 File Offset: 0x0011A050
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				int num3;
				try
				{
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
					if (num == 0)
					{
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_00EC;
					}
					if (num == 1)
					{
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_01E0;
					}
					streamReader._charLen = 0;
					streamReader._charPos = 0;
					tmpByteBuffer = streamReader._byteBuffer;
					tmpStream = streamReader._stream;
					if (!streamReader._checkPreamble)
					{
						streamReader._byteLen = 0;
					}
					IL_0050:
					if (streamReader._checkPreamble)
					{
						int bytePos = streamReader._bytePos;
						configuredValueTaskAwaiter = tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer, bytePos, tmpByteBuffer.Length - bytePos), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadBufferAsync>d__69>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredValueTaskAwaiter = tmpStream.ReadAsync(new Memory<byte>(tmpByteBuffer), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 1;
							configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, StreamReader.<ReadBufferAsync>d__69>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						goto IL_01E0;
					}
					IL_00EC:
					int result = configuredValueTaskAwaiter.GetResult();
					if (result == 0)
					{
						if (streamReader._byteLen > 0)
						{
							streamReader._charLen += streamReader._decoder.GetChars(tmpByteBuffer, 0, streamReader._byteLen, streamReader._charBuffer, streamReader._charLen);
							streamReader._bytePos = 0;
							streamReader._byteLen = 0;
						}
						num3 = streamReader._charLen;
						goto IL_02A6;
					}
					streamReader._byteLen += result;
					goto IL_0205;
					IL_01E0:
					int result2 = configuredValueTaskAwaiter.GetResult();
					streamReader._byteLen = result2;
					if (streamReader._byteLen == 0)
					{
						num3 = streamReader._charLen;
						goto IL_02A6;
					}
					IL_0205:
					streamReader._isBlocked = streamReader._byteLen < tmpByteBuffer.Length;
					if (!streamReader.IsPreamble())
					{
						if (streamReader._detectEncoding && streamReader._byteLen >= 2)
						{
							streamReader.DetectEncoding();
						}
						streamReader._charLen += streamReader._decoder.GetChars(tmpByteBuffer, 0, streamReader._byteLen, streamReader._charBuffer, streamReader._charLen);
					}
					if (streamReader._charLen == 0)
					{
						goto IL_0050;
					}
					num3 = streamReader._charLen;
				}
				catch (Exception ex)
				{
					num2 = -2;
					tmpByteBuffer = null;
					tmpStream = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_02A6:
				num2 = -2;
				tmpByteBuffer = null;
				tmpStream = null;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x06005422 RID: 21538 RVA: 0x0011C144 File Offset: 0x0011A344
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003373 RID: 13171
			public int <>1__state;

			// Token: 0x04003374 RID: 13172
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04003375 RID: 13173
			public StreamReader <>4__this;

			// Token: 0x04003376 RID: 13174
			private byte[] <tmpByteBuffer>5__2;

			// Token: 0x04003377 RID: 13175
			private Stream <tmpStream>5__3;

			// Token: 0x04003378 RID: 13176
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}
	}
}
