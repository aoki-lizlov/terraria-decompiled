using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000937 RID: 2359
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x06005423 RID: 21539 RVA: 0x0011C152 File Offset: 0x0011A352
		private void CheckAsyncTaskInProgress()
		{
			if (!this._asyncWriteTask.IsCompleted)
			{
				StreamWriter.ThrowAsyncIOInProgress();
			}
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x0011A243 File Offset: 0x00118443
		private static void ThrowAsyncIOInProgress()
		{
			throw new InvalidOperationException("The stream is currently in use by a previous operation on the stream.");
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0011C166 File Offset: 0x0011A366
		private static Encoding UTF8NoBOM
		{
			get
			{
				return EncodingHelper.UTF8Unmarked;
			}
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x0011C16D File Offset: 0x0011A36D
		internal StreamWriter()
			: base(null)
		{
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x0011C181 File Offset: 0x0011A381
		public StreamWriter(Stream stream)
			: this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x0011C195 File Offset: 0x0011A395
		public StreamWriter(Stream stream, Encoding encoding)
			: this(stream, encoding, 1024, false)
		{
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0011C1A5 File Offset: 0x0011A3A5
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
			: this(stream, encoding, bufferSize, false)
		{
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0011C1B4 File Offset: 0x0011A3B4
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
			: base(null)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream was not writable.");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			this.Init(stream, encoding, bufferSize, leaveOpen);
		}

		// Token: 0x0600542B RID: 21547 RVA: 0x0011C220 File Offset: 0x0011A420
		public StreamWriter(string path)
			: this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x0600542C RID: 21548 RVA: 0x0011C234 File Offset: 0x0011A434
		public StreamWriter(string path, bool append)
			: this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0011C248 File Offset: 0x0011A448
		public StreamWriter(string path, bool append, Encoding encoding)
			: this(path, append, encoding, 1024)
		{
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x0011C258 File Offset: 0x0011A458
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
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
			Stream stream = new FileStream(path, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan);
			this.Init(stream, encoding, bufferSize, false);
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x0011C2E0 File Offset: 0x0011A4E0
		private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
		{
			this._stream = streamArg;
			this._encoding = encodingArg;
			this._encoder = this._encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this._charBuffer = new char[bufferSize];
			this._byteBuffer = new byte[this._encoding.GetMaxByteCount(bufferSize)];
			this._charLen = bufferSize;
			if (this._stream.CanSeek && this._stream.Position > 0L)
			{
				this._haveWrittenPreamble = true;
			}
			this._closable = !shouldLeaveOpen;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x0011C373 File Offset: 0x0011A573
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x0011C384 File Offset: 0x0011A584
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._stream != null && disposing)
				{
					this.CheckAsyncTaskInProgress();
					this.Flush(true, true);
				}
			}
			finally
			{
				if (!this.LeaveOpen && this._stream != null)
				{
					try
					{
						if (disposing)
						{
							this._stream.Close();
						}
					}
					finally
					{
						this._stream = null;
						this._byteBuffer = null;
						this._charBuffer = null;
						this._encoding = null;
						this._encoder = null;
						this._charLen = 0;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x0011C41C File Offset: 0x0011A61C
		public override ValueTask DisposeAsync()
		{
			if (!(base.GetType() != typeof(StreamWriter)))
			{
				return this.DisposeAsyncCore();
			}
			return base.DisposeAsync();
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x0011C444 File Offset: 0x0011A644
		private async ValueTask DisposeAsyncCore()
		{
			try
			{
				if (this._stream != null)
				{
					await this.FlushAsync().ConfigureAwait(false);
				}
			}
			finally
			{
				this.CloseStreamFromDispose(true);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x0011C488 File Offset: 0x0011A688
		private void CloseStreamFromDispose(bool disposing)
		{
			if (!this.LeaveOpen && this._stream != null)
			{
				try
				{
					if (disposing)
					{
						this._stream.Close();
					}
				}
				finally
				{
					this._stream = null;
					this._byteBuffer = null;
					this._charBuffer = null;
					this._encoding = null;
					this._encoder = null;
					this._charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x0011C4F8 File Offset: 0x0011A6F8
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x0011C508 File Offset: 0x0011A708
		private void Flush(bool flushStream, bool flushEncoder)
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			if (this._charPos == 0 && !flushStream && !flushEncoder)
			{
				return;
			}
			if (!this._haveWrittenPreamble)
			{
				this._haveWrittenPreamble = true;
				ReadOnlySpan<byte> preamble = this._encoding.Preamble;
				if (preamble.Length > 0)
				{
					this._stream.Write(preamble);
				}
			}
			int bytes = this._encoder.GetBytes(this._charBuffer, 0, this._charPos, this._byteBuffer, 0, flushEncoder);
			this._charPos = 0;
			if (bytes > 0)
			{
				this._stream.Write(this._byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this._stream.Flush();
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x0011C5B6 File Offset: 0x0011A7B6
		// (set) Token: 0x06005438 RID: 21560 RVA: 0x0011C5BE File Offset: 0x0011A7BE
		public virtual bool AutoFlush
		{
			get
			{
				return this._autoFlush;
			}
			set
			{
				this.CheckAsyncTaskInProgress();
				this._autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x0011C5D8 File Offset: 0x0011A7D8
		public virtual Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x0011C5E0 File Offset: 0x0011A7E0
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (set) Token: 0x0600543B RID: 21563 RVA: 0x0011C5EB File Offset: 0x0011A7EB
		internal bool HaveWrittenPreamble
		{
			set
			{
				this._haveWrittenPreamble = value;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x0011C5F4 File Offset: 0x0011A7F4
		public override Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x0011C5FC File Offset: 0x0011A7FC
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this._charPos == this._charLen)
			{
				this.Flush(false, false);
			}
			this._charBuffer[this._charPos] = value;
			this._charPos++;
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0011C651 File Offset: 0x0011A851
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer)
		{
			this.WriteSpan(buffer, false);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0011C660 File Offset: 0x0011A860
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.WriteSpan(buffer.AsSpan(index, count), false);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0011C6CF File Offset: 0x0011A8CF
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(ReadOnlySpan<char> buffer)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.WriteSpan(buffer, false);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x0011C6F8 File Offset: 0x0011A8F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe void WriteSpan(ReadOnlySpan<char> buffer, bool appendNewLine)
		{
			this.CheckAsyncTaskInProgress();
			if (buffer.Length <= 4 && buffer.Length <= this._charLen - this._charPos)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					char[] charBuffer = this._charBuffer;
					int charPos = this._charPos;
					this._charPos = charPos + 1;
					charBuffer[charPos] = *buffer[i];
				}
			}
			else
			{
				char[] charBuffer2 = this._charBuffer;
				if (charBuffer2 == null)
				{
					throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
				}
				fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
				{
					char* ptr = reference;
					fixed (char* ptr2 = &charBuffer2[0])
					{
						char* ptr3 = ptr2;
						char* ptr4 = ptr;
						int j = buffer.Length;
						int num = this._charPos;
						while (j > 0)
						{
							if (num == charBuffer2.Length)
							{
								this.Flush(false, false);
								num = 0;
							}
							int num2 = Math.Min(charBuffer2.Length - num, j);
							int num3 = num2 * 2;
							Buffer.MemoryCopy((void*)ptr4, (void*)(ptr3 + num), (long)num3, (long)num3);
							this._charPos += num2;
							num += num2;
							ptr4 += num2;
							j -= num2;
						}
					}
				}
			}
			if (appendNewLine)
			{
				char[] coreNewLine = this.CoreNewLine;
				for (int k = 0; k < coreNewLine.Length; k++)
				{
					if (this._charPos == this._charLen)
					{
						this.Flush(false, false);
					}
					this._charBuffer[this._charPos] = coreNewLine[k];
					this._charPos++;
				}
			}
			if (this._autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0011C878 File Offset: 0x0011AA78
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void Write(string value)
		{
			this.WriteSpan(value, false);
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x0011C887 File Offset: 0x0011AA87
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(string value)
		{
			this.CheckAsyncTaskInProgress();
			this.WriteSpan(value, true);
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0011C89C File Offset: 0x0011AA9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override void WriteLine(ReadOnlySpan<char> value)
		{
			if (base.GetType() == typeof(StreamWriter))
			{
				this.CheckAsyncTaskInProgress();
				this.WriteSpan(value, true);
				return;
			}
			base.WriteLine(value);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0011C8CC File Offset: 0x0011AACC
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0011C944 File Offset: 0x0011AB44
		private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			if (charPos == charLen)
			{
				await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			charBuffer[charPos] = value;
			charPos++;
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		public override Task WriteAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value == null)
			{
				return Task.CompletedTask;
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0011CA44 File Offset: 0x0011AC44
		private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			int count = value.Length;
			int index = 0;
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				value.CopyTo(index, charBuffer, charPos, num);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0011CAC4 File Offset: 0x0011ACC4
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0011CB9C File Offset: 0x0011AD9C
		public override Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, false, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0011CC24 File Offset: 0x0011AE24
		private static async Task WriteAsyncInternal(StreamWriter _this, ReadOnlyMemory<char> source, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine, CancellationToken cancellationToken)
		{
			int num;
			for (int copied = 0; copied < source.Length; copied += num)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
					charPos = 0;
				}
				num = Math.Min(charLen - charPos, source.Length - copied);
				source.Span.Slice(copied, num).CopyTo(new Span<char>(charBuffer, charPos, num));
				charPos += num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0011CCAC File Offset: 0x0011AEAC
		public override Task WriteLineAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, ReadOnlyMemory<char>.Empty, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0011CD30 File Offset: 0x0011AF30
		public override Task WriteLineAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0011CDA8 File Offset: 0x0011AFA8
		public override Task WriteLineAsync(string value)
		{
			if (value == null)
			{
				return this.WriteLineAsync();
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0011CE28 File Offset: 0x0011B028
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, index, count);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, new ReadOnlyMemory<char>(buffer, index, count), this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0011CF00 File Offset: 0x0011B100
		public override Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, cancellationToken);
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, this._charBuffer, this._charPos, this._charLen, this.CoreNewLine, this._autoFlush, true, cancellationToken);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0011CF88 File Offset: 0x0011B188
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Can not write to a closed TextWriter.");
			}
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this._charBuffer, this._charPos, default(CancellationToken));
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x17000DEF RID: 3567
		// (set) Token: 0x06005452 RID: 21586 RVA: 0x0011CFF3 File Offset: 0x0011B1F3
		private int CharPos_Prop
		{
			set
			{
				this._charPos = value;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (set) Token: 0x06005453 RID: 21587 RVA: 0x0011C5EB File Offset: 0x0011A7EB
		private bool HaveWrittenPreamble_Prop
		{
			set
			{
				this._haveWrittenPreamble = value;
			}
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0011CFFC File Offset: 0x0011B1FC
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this._haveWrittenPreamble, this._encoding, this._encoder, this._byteBuffer, this._stream, cancellationToken);
			this._charPos = 0;
			return task;
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x0011D05C File Offset: 0x0011B25C
		private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream, CancellationToken cancellationToken)
		{
			if (!haveWrittenPreamble)
			{
				_this.HaveWrittenPreamble_Prop = true;
				byte[] preamble = encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					await stream.WriteAsync(new ReadOnlyMemory<byte>(preamble), cancellationToken).ConfigureAwait(false);
				}
			}
			int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
			if (bytes > 0)
			{
				await stream.WriteAsync(new ReadOnlyMemory<byte>(byteBuffer, 0, bytes), cancellationToken).ConfigureAwait(false);
			}
			if (flushStream)
			{
				await stream.FlushAsync(cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0011D0F6 File Offset: 0x0011B2F6
		// Note: this type is marked as 'beforefieldinit'.
		static StreamWriter()
		{
		}

		// Token: 0x04003379 RID: 13177
		internal const int DefaultBufferSize = 1024;

		// Token: 0x0400337A RID: 13178
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x0400337B RID: 13179
		private const int MinBufferSize = 128;

		// Token: 0x0400337C RID: 13180
		private const int DontCopyOnWriteLineThreshold = 512;

		// Token: 0x0400337D RID: 13181
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, StreamWriter.UTF8NoBOM, 128, true);

		// Token: 0x0400337E RID: 13182
		private Stream _stream;

		// Token: 0x0400337F RID: 13183
		private Encoding _encoding;

		// Token: 0x04003380 RID: 13184
		private Encoder _encoder;

		// Token: 0x04003381 RID: 13185
		private byte[] _byteBuffer;

		// Token: 0x04003382 RID: 13186
		private char[] _charBuffer;

		// Token: 0x04003383 RID: 13187
		private int _charPos;

		// Token: 0x04003384 RID: 13188
		private int _charLen;

		// Token: 0x04003385 RID: 13189
		private bool _autoFlush;

		// Token: 0x04003386 RID: 13190
		private bool _haveWrittenPreamble;

		// Token: 0x04003387 RID: 13191
		private bool _closable;

		// Token: 0x04003388 RID: 13192
		private Task _asyncWriteTask = Task.CompletedTask;

		// Token: 0x02000938 RID: 2360
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DisposeAsyncCore>d__33 : IAsyncStateMachine
		{
			// Token: 0x06005457 RID: 21591 RVA: 0x0011D114 File Offset: 0x0011B314
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamWriter streamWriter = this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num != 0)
						{
							if (streamWriter._stream == null)
							{
								goto IL_007D;
							}
							configuredTaskAwaiter = streamWriter.FlushAsync().ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<DisposeAsyncCore>d__33>(ref configuredTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						configuredTaskAwaiter.GetResult();
						IL_007D:;
					}
					finally
					{
						if (num < 0)
						{
							streamWriter.CloseStreamFromDispose(true);
						}
					}
					GC.SuppressFinalize(streamWriter);
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005458 RID: 21592 RVA: 0x0011D1FC File Offset: 0x0011B3FC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003389 RID: 13193
			public int <>1__state;

			// Token: 0x0400338A RID: 13194
			public AsyncValueTaskMethodBuilder <>t__builder;

			// Token: 0x0400338B RID: 13195
			public StreamWriter <>4__this;

			// Token: 0x0400338C RID: 13196
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000939 RID: 2361
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__57 : IAsyncStateMachine
		{
			// Token: 0x06005459 RID: 21593 RVA: 0x0011D20C File Offset: 0x0011B40C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_0177;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_0257;
					}
					default:
						if (charPos != charLen)
						{
							goto IL_00B1;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter, ref this);
							return;
						}
						break;
					}
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_00B1:
					charBuffer[charPos] = value;
					int num3 = charPos;
					charPos = num3 + 1;
					if (appendNewLine)
					{
						i = 0;
						goto IL_01C3;
					}
					goto IL_01D6;
					IL_0177:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_0185:
					charBuffer[charPos] = coreNewLine[i];
					num3 = charPos;
					charPos = num3 + 1;
					num3 = i;
					i = num3 + 1;
					IL_01C3:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_0185;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_0177;
					}
					IL_01D6:
					if (!autoFlush)
					{
						goto IL_0265;
					}
					configuredTaskAwaiter = _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_0257:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_0265:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600545A RID: 21594 RVA: 0x0011D4DC File Offset: 0x0011B6DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400338D RID: 13197
			public int <>1__state;

			// Token: 0x0400338E RID: 13198
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400338F RID: 13199
			public int charPos;

			// Token: 0x04003390 RID: 13200
			public int charLen;

			// Token: 0x04003391 RID: 13201
			public StreamWriter _this;

			// Token: 0x04003392 RID: 13202
			public char[] charBuffer;

			// Token: 0x04003393 RID: 13203
			public char value;

			// Token: 0x04003394 RID: 13204
			public bool appendNewLine;

			// Token: 0x04003395 RID: 13205
			public char[] coreNewLine;

			// Token: 0x04003396 RID: 13206
			public bool autoFlush;

			// Token: 0x04003397 RID: 13207
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003398 RID: 13208
			private int <i>5__2;
		}

		// Token: 0x0200093A RID: 2362
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__59 : IAsyncStateMachine
		{
			// Token: 0x0600545B RID: 21595 RVA: 0x0011D4EC File Offset: 0x0011B6EC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_01E3;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_02C4;
					}
					default:
						count = value.Length;
						index = 0;
						goto IL_0135;
					}
					IL_00C1:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_00CF:
					int num3 = charLen - charPos;
					if (num3 > count)
					{
						num3 = count;
					}
					value.CopyTo(index, charBuffer, charPos, num3);
					charPos += num3;
					index += num3;
					count -= num3;
					IL_0135:
					if (count <= 0)
					{
						if (appendNewLine)
						{
							i = 0;
							goto IL_022F;
						}
						goto IL_0242;
					}
					else
					{
						if (charPos != charLen)
						{
							goto IL_00CF;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_00C1;
					}
					IL_01E3:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_01F1:
					charBuffer[charPos] = coreNewLine[i];
					int num4 = charPos;
					charPos = num4 + 1;
					num4 = i;
					i = num4 + 1;
					IL_022F:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_01F1;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_01E3;
					}
					IL_0242:
					if (!autoFlush)
					{
						goto IL_02D2;
					}
					configuredTaskAwaiter = _this.FlushAsyncInternal(true, false, charBuffer, charPos, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__59>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_02C4:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_02D2:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600545C RID: 21596 RVA: 0x0011D828 File Offset: 0x0011BA28
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003399 RID: 13209
			public int <>1__state;

			// Token: 0x0400339A RID: 13210
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400339B RID: 13211
			public string value;

			// Token: 0x0400339C RID: 13212
			public int charPos;

			// Token: 0x0400339D RID: 13213
			public int charLen;

			// Token: 0x0400339E RID: 13214
			public StreamWriter _this;

			// Token: 0x0400339F RID: 13215
			public char[] charBuffer;

			// Token: 0x040033A0 RID: 13216
			public bool appendNewLine;

			// Token: 0x040033A1 RID: 13217
			public char[] coreNewLine;

			// Token: 0x040033A2 RID: 13218
			public bool autoFlush;

			// Token: 0x040033A3 RID: 13219
			private int <count>5__2;

			// Token: 0x040033A4 RID: 13220
			private int <index>5__3;

			// Token: 0x040033A5 RID: 13221
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040033A6 RID: 13222
			private int <i>5__4;
		}

		// Token: 0x0200093B RID: 2363
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__62 : IAsyncStateMachine
		{
			// Token: 0x0600545D RID: 21597 RVA: 0x0011D838 File Offset: 0x0011BA38
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_01E5;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_02C2;
					}
					default:
						copied = 0;
						goto IL_0131;
					}
					IL_00AC:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_00BA:
					int num3 = Math.Min(charLen - charPos, source.Length - copied);
					source.Span.Slice(copied, num3).CopyTo(new Span<char>(charBuffer, charPos, num3));
					charPos += num3;
					copied += num3;
					IL_0131:
					if (copied >= source.Length)
					{
						if (appendNewLine)
						{
							i = 0;
							goto IL_0231;
						}
						goto IL_0244;
					}
					else
					{
						if (charPos != charLen)
						{
							goto IL_00BA;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_00AC;
					}
					IL_01E5:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_01F3:
					charBuffer[charPos] = coreNewLine[i];
					int num4 = charPos;
					charPos = num4 + 1;
					num4 = i;
					i = num4 + 1;
					IL_0231:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_01F3;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_01E5;
					}
					IL_0244:
					if (!autoFlush)
					{
						goto IL_02D0;
					}
					configuredTaskAwaiter = _this.FlushAsyncInternal(true, false, charBuffer, charPos, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__62>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_02C2:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_02D0:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600545E RID: 21598 RVA: 0x0011DB70 File Offset: 0x0011BD70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033A7 RID: 13223
			public int <>1__state;

			// Token: 0x040033A8 RID: 13224
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040033A9 RID: 13225
			public int charPos;

			// Token: 0x040033AA RID: 13226
			public int charLen;

			// Token: 0x040033AB RID: 13227
			public StreamWriter _this;

			// Token: 0x040033AC RID: 13228
			public char[] charBuffer;

			// Token: 0x040033AD RID: 13229
			public CancellationToken cancellationToken;

			// Token: 0x040033AE RID: 13230
			public ReadOnlyMemory<char> source;

			// Token: 0x040033AF RID: 13231
			public bool appendNewLine;

			// Token: 0x040033B0 RID: 13232
			public char[] coreNewLine;

			// Token: 0x040033B1 RID: 13233
			public bool autoFlush;

			// Token: 0x040033B2 RID: 13234
			private int <copied>5__2;

			// Token: 0x040033B3 RID: 13235
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040033B4 RID: 13236
			private int <i>5__3;
		}

		// Token: 0x0200093C RID: 2364
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsyncInternal>d__74 : IAsyncStateMachine
		{
			// Token: 0x0600545F RID: 21599 RVA: 0x0011DB80 File Offset: 0x0011BD80
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_0161;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_01D9;
					}
					default:
					{
						if (haveWrittenPreamble)
						{
							goto IL_00BA;
						}
						_this.HaveWrittenPreamble_Prop = true;
						byte[] preamble = encoding.GetPreamble();
						if (preamble.Length == 0)
						{
							goto IL_00BA;
						}
						configuredValueTaskAwaiter = stream.WriteAsync(new ReadOnlyMemory<byte>(preamble), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						break;
					}
					}
					configuredValueTaskAwaiter.GetResult();
					IL_00BA:
					int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
					if (bytes <= 0)
					{
						goto IL_0168;
					}
					configuredValueTaskAwaiter = stream.WriteAsync(new ReadOnlyMemory<byte>(byteBuffer, 0, bytes), cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredValueTaskAwaiter.IsCompleted)
					{
						num2 = 1;
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref configuredValueTaskAwaiter, ref this);
						return;
					}
					IL_0161:
					configuredValueTaskAwaiter.GetResult();
					IL_0168:
					if (!flushStream)
					{
						goto IL_01E0;
					}
					configuredTaskAwaiter = stream.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__74>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_01D9:
					configuredTaskAwaiter.GetResult();
					IL_01E0:;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005460 RID: 21600 RVA: 0x0011DDB8 File Offset: 0x0011BFB8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033B5 RID: 13237
			public int <>1__state;

			// Token: 0x040033B6 RID: 13238
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040033B7 RID: 13239
			public bool haveWrittenPreamble;

			// Token: 0x040033B8 RID: 13240
			public StreamWriter _this;

			// Token: 0x040033B9 RID: 13241
			public Encoding encoding;

			// Token: 0x040033BA RID: 13242
			public Stream stream;

			// Token: 0x040033BB RID: 13243
			public CancellationToken cancellationToken;

			// Token: 0x040033BC RID: 13244
			public Encoder encoder;

			// Token: 0x040033BD RID: 13245
			public char[] charBuffer;

			// Token: 0x040033BE RID: 13246
			public int charPos;

			// Token: 0x040033BF RID: 13247
			public byte[] byteBuffer;

			// Token: 0x040033C0 RID: 13248
			public bool flushEncoder;

			// Token: 0x040033C1 RID: 13249
			public bool flushStream;

			// Token: 0x040033C2 RID: 13250
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x040033C3 RID: 13251
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
