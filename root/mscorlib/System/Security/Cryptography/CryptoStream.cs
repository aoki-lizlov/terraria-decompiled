using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	// Token: 0x02000439 RID: 1081
	public class CryptoStream : Stream, IDisposable
	{
		// Token: 0x06002D76 RID: 11638 RVA: 0x000A428C File Offset: 0x000A248C
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
			: this(stream, transform, mode, false)
		{
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000A4298 File Offset: 0x000A2498
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._transform = transform;
			this._leaveOpen = leaveOpen;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException("Argument {0} should be larger than {1}.");
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(SR.Format("Stream was not writable.", "stream"));
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(SR.Format("Stream was not readable.", "stream"));
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000A433F File Offset: 0x000A253F
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000A4347 File Offset: 0x000A2547
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000A434F File Offset: 0x000A254F
		public override long Length
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000A434F File Offset: 0x000A254F
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x000A434F File Offset: 0x000A254F
		public override long Position
		{
			get
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
			set
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000A435B File Offset: 0x000A255B
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000A4364 File Offset: 0x000A2564
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException("FlushFinalBlock() method was called twice on a CryptoStream. It can only be called once.");
			}
			byte[] array = this._transform.TransformFinalBlock(this._inputBuffer, 0, this._inputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._outputBufferIndex > 0)
			{
				this._stream.Write(this._outputBuffer, 0, this._outputBufferIndex);
				this._outputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._inputBuffer != null)
			{
				Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
			}
			if (this._outputBuffer != null)
			{
				Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00004088 File Offset: 0x00002288
		public override void Flush()
		{
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000A444E File Offset: 0x000A264E
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000A434F File Offset: 0x000A254F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000A434F File Offset: 0x000A254F
		public override void SetLength(long value)
		{
			throw new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000A4484 File Offset: 0x000A2684
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000A449A File Offset: 0x000A269A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000A44B3 File Offset: 0x000A26B3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000A44BC File Offset: 0x000A26BC
		private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SemaphoreSlim semaphore = this.AsyncActiveSemaphore;
			await semaphore.WaitAsync().ForceAsync();
			int num;
			try
			{
				num = await this.ReadAsyncCore(buffer, offset, count, cancellationToken, true);
			}
			finally
			{
				semaphore.Release();
			}
			return num;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000A4520 File Offset: 0x000A2720
		public override int ReadByte()
		{
			if (this._outputBufferIndex > 1)
			{
				int num = (int)this._outputBuffer[0];
				Buffer.BlockCopy(this._outputBuffer, 1, this._outputBuffer, 0, this._outputBufferIndex - 1);
				this._outputBufferIndex--;
				return num;
			}
			return base.ReadByte();
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000A4570 File Offset: 0x000A2770
		public override void WriteByte(byte value)
		{
			if (this._inputBufferIndex + 1 < this._inputBlockSize)
			{
				byte[] inputBuffer = this._inputBuffer;
				int inputBufferIndex = this._inputBufferIndex;
				this._inputBufferIndex = inputBufferIndex + 1;
				inputBuffer[inputBufferIndex] = value;
				return;
			}
			base.WriteByte(value);
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000A45B0 File Offset: 0x000A27B0
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckReadArguments(buffer, offset, count);
			return this.ReadAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000A45E8 File Offset: 0x000A27E8
		private void CheckReadArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000A4644 File Offset: 0x000A2844
		private async Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			int bytesToDeliver = count;
			int currentOutputIndex = offset;
			if (this._outputBufferIndex != 0)
			{
				if (this._outputBufferIndex > count)
				{
					Buffer.BlockCopy(this._outputBuffer, 0, buffer, offset, count);
					Buffer.BlockCopy(this._outputBuffer, count, this._outputBuffer, 0, this._outputBufferIndex - count);
					this._outputBufferIndex -= count;
					int num = this._outputBuffer.Length - this._outputBufferIndex;
					CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num));
					return count;
				}
				Buffer.BlockCopy(this._outputBuffer, 0, buffer, offset, this._outputBufferIndex);
				bytesToDeliver -= this._outputBufferIndex;
				currentOutputIndex += this._outputBufferIndex;
				int num2 = this._outputBuffer.Length - this._outputBufferIndex;
				CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num2));
				this._outputBufferIndex = 0;
			}
			int num3;
			if (this._finalBlockTransformed)
			{
				num3 = count - bytesToDeliver;
			}
			else
			{
				int num4 = bytesToDeliver / this._outputBlockSize;
				if (num4 > 1 && this._transform.CanTransformMultipleBlocks)
				{
					int numWholeBlocksInBytes = num4 * this._inputBlockSize;
					byte[] tempInputBuffer = ArrayPool<byte>.Shared.Rent(numWholeBlocksInBytes);
					byte[] tempOutputBuffer = null;
					try
					{
						int num5;
						if (useAsync)
						{
							num5 = await this._stream.ReadAsync(new Memory<byte>(tempInputBuffer, this._inputBufferIndex, numWholeBlocksInBytes - this._inputBufferIndex), cancellationToken);
						}
						else
						{
							num5 = this._stream.Read(tempInputBuffer, this._inputBufferIndex, numWholeBlocksInBytes - this._inputBufferIndex);
						}
						int num6 = num5;
						int num7 = this._inputBufferIndex + num6;
						if (num7 < this._inputBlockSize)
						{
							Buffer.BlockCopy(tempInputBuffer, this._inputBufferIndex, this._inputBuffer, this._inputBufferIndex, num6);
							this._inputBufferIndex = num7;
						}
						else
						{
							Buffer.BlockCopy(this._inputBuffer, 0, tempInputBuffer, 0, this._inputBufferIndex);
							CryptographicOperations.ZeroMemory(new Span<byte>(this._inputBuffer, 0, this._inputBufferIndex));
							num6 += this._inputBufferIndex;
							this._inputBufferIndex = 0;
							int num8 = num6 / this._inputBlockSize;
							int num9 = num8 * this._inputBlockSize;
							int num10 = num6 - num9;
							if (num10 != 0)
							{
								this._inputBufferIndex = num10;
								Buffer.BlockCopy(tempInputBuffer, num9, this._inputBuffer, 0, num10);
							}
							tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num8 * this._outputBlockSize);
							int num11 = this._transform.TransformBlock(tempInputBuffer, 0, num9, tempOutputBuffer, 0);
							Buffer.BlockCopy(tempOutputBuffer, 0, buffer, currentOutputIndex, num11);
							CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, num11));
							ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
							tempOutputBuffer = null;
							bytesToDeliver -= num11;
							currentOutputIndex += num11;
						}
					}
					finally
					{
						if (tempOutputBuffer != null)
						{
							CryptographicOperations.ZeroMemory(tempOutputBuffer);
							ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
							tempOutputBuffer = null;
						}
						CryptographicOperations.ZeroMemory(new Span<byte>(tempInputBuffer, 0, numWholeBlocksInBytes));
						ArrayPool<byte>.Shared.Return(tempInputBuffer, false);
						tempInputBuffer = null;
					}
					tempInputBuffer = null;
					tempOutputBuffer = null;
				}
				while (bytesToDeliver > 0)
				{
					while (this._inputBufferIndex < this._inputBlockSize)
					{
						int num5;
						if (useAsync)
						{
							num5 = await this._stream.ReadAsync(new Memory<byte>(this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex), cancellationToken);
						}
						else
						{
							num5 = this._stream.Read(this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex);
						}
						int num6 = num5;
						if (num6 != 0)
						{
							this._inputBufferIndex += num6;
						}
						else
						{
							byte[] array = this._transform.TransformFinalBlock(this._inputBuffer, 0, this._inputBufferIndex);
							this._outputBuffer = array;
							this._outputBufferIndex = array.Length;
							this._finalBlockTransformed = true;
							if (bytesToDeliver < this._outputBufferIndex)
							{
								Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
								this._outputBufferIndex -= bytesToDeliver;
								Buffer.BlockCopy(this._outputBuffer, bytesToDeliver, this._outputBuffer, 0, this._outputBufferIndex);
								int num12 = this._outputBuffer.Length - this._outputBufferIndex;
								CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num12));
								return count;
							}
							Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, this._outputBufferIndex);
							bytesToDeliver -= this._outputBufferIndex;
							this._outputBufferIndex = 0;
							CryptographicOperations.ZeroMemory(this._outputBuffer);
							return count - bytesToDeliver;
						}
					}
					int num11 = this._transform.TransformBlock(this._inputBuffer, 0, this._inputBlockSize, this._outputBuffer, 0);
					this._inputBufferIndex = 0;
					if (bytesToDeliver < num11)
					{
						Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
						this._outputBufferIndex = num11 - bytesToDeliver;
						Buffer.BlockCopy(this._outputBuffer, bytesToDeliver, this._outputBuffer, 0, this._outputBufferIndex);
						int num13 = this._outputBuffer.Length - this._outputBufferIndex;
						CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, this._outputBufferIndex, num13));
						return count;
					}
					Buffer.BlockCopy(this._outputBuffer, 0, buffer, currentOutputIndex, num11);
					CryptographicOperations.ZeroMemory(new Span<byte>(this._outputBuffer, 0, num11));
					currentOutputIndex += num11;
					bytesToDeliver -= num11;
				}
				num3 = count;
			}
			return num3;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000A46B1 File Offset: 0x000A28B1
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this.CheckWriteArguments(buffer, offset, count);
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000A46C7 File Offset: 0x000A28C7
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000A46E0 File Offset: 0x000A28E0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000A46E8 File Offset: 0x000A28E8
		private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			SemaphoreSlim semaphore = this.AsyncActiveSemaphore;
			await semaphore.WaitAsync().ForceAsync();
			try
			{
				await this.WriteAsyncCore(buffer, offset, count, cancellationToken, true);
			}
			finally
			{
				semaphore.Release();
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000A474C File Offset: 0x000A294C
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteArguments(buffer, offset, count);
			this.WriteAsyncCore(buffer, offset, count, default(CancellationToken), false).GetAwaiter().GetResult();
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000A4784 File Offset: 0x000A2984
		private void CheckWriteArguments(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000A47E0 File Offset: 0x000A29E0
		private async Task WriteAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool useAsync)
		{
			int bytesToWrite = count;
			int currentInputIndex = offset;
			if (this._inputBufferIndex > 0)
			{
				if (count < this._inputBlockSize - this._inputBufferIndex)
				{
					Buffer.BlockCopy(buffer, offset, this._inputBuffer, this._inputBufferIndex, count);
					this._inputBufferIndex += count;
					return;
				}
				Buffer.BlockCopy(buffer, offset, this._inputBuffer, this._inputBufferIndex, this._inputBlockSize - this._inputBufferIndex);
				currentInputIndex += this._inputBlockSize - this._inputBufferIndex;
				bytesToWrite -= this._inputBlockSize - this._inputBufferIndex;
				this._inputBufferIndex = this._inputBlockSize;
			}
			if (this._outputBufferIndex > 0)
			{
				if (useAsync)
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, this._outputBufferIndex), cancellationToken);
				}
				else
				{
					this._stream.Write(this._outputBuffer, 0, this._outputBufferIndex);
				}
				this._outputBufferIndex = 0;
			}
			if (this._inputBufferIndex == this._inputBlockSize)
			{
				int numOutputBytes = this._transform.TransformBlock(this._inputBuffer, 0, this._inputBlockSize, this._outputBuffer, 0);
				if (useAsync)
				{
					await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, numOutputBytes), cancellationToken);
				}
				else
				{
					this._stream.Write(this._outputBuffer, 0, numOutputBytes);
				}
				this._inputBufferIndex = 0;
			}
			while (bytesToWrite > 0)
			{
				if (bytesToWrite < this._inputBlockSize)
				{
					Buffer.BlockCopy(buffer, currentInputIndex, this._inputBuffer, 0, bytesToWrite);
					this._inputBufferIndex += bytesToWrite;
					break;
				}
				int num = bytesToWrite / this._inputBlockSize;
				if (this._transform.CanTransformMultipleBlocks && num > 1)
				{
					int numWholeBlocksInBytes = num * this._inputBlockSize;
					byte[] tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num * this._outputBlockSize);
					int numOutputBytes = 0;
					try
					{
						numOutputBytes = this._transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, tempOutputBuffer, 0);
						if (useAsync)
						{
							await this._stream.WriteAsync(new ReadOnlyMemory<byte>(tempOutputBuffer, 0, numOutputBytes), cancellationToken);
						}
						else
						{
							this._stream.Write(tempOutputBuffer, 0, numOutputBytes);
						}
						currentInputIndex += numWholeBlocksInBytes;
						bytesToWrite -= numWholeBlocksInBytes;
					}
					finally
					{
						CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, numOutputBytes));
						ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
						tempOutputBuffer = null;
					}
					tempOutputBuffer = null;
				}
				else
				{
					int numOutputBytes = this._transform.TransformBlock(buffer, currentInputIndex, this._inputBlockSize, this._outputBuffer, 0);
					if (useAsync)
					{
						await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._outputBuffer, 0, numOutputBytes), cancellationToken);
					}
					else
					{
						this._stream.Write(this._outputBuffer, 0, numOutputBytes);
					}
					currentInputIndex += this._inputBlockSize;
					bytesToWrite -= this._inputBlockSize;
				}
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000A484D File Offset: 0x000A2A4D
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000A4858 File Offset: 0x000A2A58
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					if (!this._leaveOpen)
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._inputBuffer != null)
					{
						Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
					}
					if (this._outputBuffer != null)
					{
						Array.Clear(this._outputBuffer, 0, this._outputBuffer.Length);
					}
					this._inputBuffer = null;
					this._outputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000A4910 File Offset: 0x000A2B10
		private void InitializeBuffer()
		{
			if (this._transform != null)
			{
				this._inputBlockSize = this._transform.InputBlockSize;
				this._inputBuffer = new byte[this._inputBlockSize];
				this._outputBlockSize = this._transform.OutputBlockSize;
				this._outputBuffer = new byte[this._outputBlockSize];
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000A4969 File Offset: 0x000A2B69
		private SemaphoreSlim AsyncActiveSemaphore
		{
			get
			{
				return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._lazyAsyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
			}
		}

		// Token: 0x04001F9C RID: 8092
		private readonly Stream _stream;

		// Token: 0x04001F9D RID: 8093
		private readonly ICryptoTransform _transform;

		// Token: 0x04001F9E RID: 8094
		private readonly CryptoStreamMode _transformMode;

		// Token: 0x04001F9F RID: 8095
		private byte[] _inputBuffer;

		// Token: 0x04001FA0 RID: 8096
		private int _inputBufferIndex;

		// Token: 0x04001FA1 RID: 8097
		private int _inputBlockSize;

		// Token: 0x04001FA2 RID: 8098
		private byte[] _outputBuffer;

		// Token: 0x04001FA3 RID: 8099
		private int _outputBufferIndex;

		// Token: 0x04001FA4 RID: 8100
		private int _outputBlockSize;

		// Token: 0x04001FA5 RID: 8101
		private bool _canRead;

		// Token: 0x04001FA6 RID: 8102
		private bool _canWrite;

		// Token: 0x04001FA7 RID: 8103
		private bool _finalBlockTransformed;

		// Token: 0x04001FA8 RID: 8104
		private SemaphoreSlim _lazyAsyncActiveSemaphore;

		// Token: 0x04001FA9 RID: 8105
		private readonly bool _leaveOpen;

		// Token: 0x0200043A RID: 1082
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__37 : IAsyncStateMachine
		{
			// Token: 0x06002D98 RID: 11672 RVA: 0x000A4998 File Offset: 0x000A2B98
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				int result;
				try
				{
					ForceAsyncAwaiter forceAsyncAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_008A;
						}
						semaphore = cryptoStream.AsyncActiveSemaphore;
						forceAsyncAwaiter = semaphore.WaitAsync().ForceAsync().GetAwaiter();
						if (!forceAsyncAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ForceAsyncAwaiter forceAsyncAwaiter2 = forceAsyncAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ForceAsyncAwaiter, CryptoStream.<ReadAsyncInternal>d__37>(ref forceAsyncAwaiter, ref this);
							return;
						}
					}
					else
					{
						ForceAsyncAwaiter forceAsyncAwaiter2;
						forceAsyncAwaiter = forceAsyncAwaiter2;
						forceAsyncAwaiter2 = default(ForceAsyncAwaiter);
						num = (num2 = -1);
					}
					forceAsyncAwaiter.GetResult();
					IL_008A:
					try
					{
						TaskAwaiter<int> taskAwaiter;
						if (num != 1)
						{
							taskAwaiter = cryptoStream.ReadAsyncCore(buffer, offset, count, cancellationToken, true).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 1);
								TaskAwaiter<int> taskAwaiter2 = taskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, CryptoStream.<ReadAsyncInternal>d__37>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							TaskAwaiter<int> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<int>);
							num = (num2 = -1);
						}
						result = taskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							semaphore.Release();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					semaphore = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				semaphore = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06002D99 RID: 11673 RVA: 0x000A4B2C File Offset: 0x000A2D2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FAA RID: 8106
			public int <>1__state;

			// Token: 0x04001FAB RID: 8107
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001FAC RID: 8108
			public CryptoStream <>4__this;

			// Token: 0x04001FAD RID: 8109
			public byte[] buffer;

			// Token: 0x04001FAE RID: 8110
			public int offset;

			// Token: 0x04001FAF RID: 8111
			public int count;

			// Token: 0x04001FB0 RID: 8112
			public CancellationToken cancellationToken;

			// Token: 0x04001FB1 RID: 8113
			private SemaphoreSlim <semaphore>5__2;

			// Token: 0x04001FB2 RID: 8114
			private ForceAsyncAwaiter <>u__1;

			// Token: 0x04001FB3 RID: 8115
			private TaskAwaiter<int> <>u__2;
		}

		// Token: 0x0200043B RID: 1083
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncCore>d__42 : IAsyncStateMachine
		{
			// Token: 0x06002D9A RID: 11674 RVA: 0x000A4B3C File Offset: 0x000A2D3C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				int num4;
				try
				{
					ValueTaskAwaiter<int> valueTaskAwaiter2;
					ValueTaskAwaiter<int> valueTaskAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							valueTaskAwaiter = valueTaskAwaiter2;
							valueTaskAwaiter2 = default(ValueTaskAwaiter<int>);
							num = (num2 = -1);
							goto IL_04E4;
						}
						bytesToDeliver = count;
						currentOutputIndex = offset;
						if (cryptoStream._outputBufferIndex != 0)
						{
							if (cryptoStream._outputBufferIndex > count)
							{
								Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, offset, count);
								Buffer.BlockCopy(cryptoStream._outputBuffer, count, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex - count);
								cryptoStream._outputBufferIndex -= count;
								int num3 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
								CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, num3));
								num4 = count;
								goto IL_078D;
							}
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, offset, cryptoStream._outputBufferIndex);
							bytesToDeliver -= cryptoStream._outputBufferIndex;
							currentOutputIndex += cryptoStream._outputBufferIndex;
							int num5 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, num5));
							cryptoStream._outputBufferIndex = 0;
						}
						if (cryptoStream._finalBlockTransformed)
						{
							num4 = count - bytesToDeliver;
							goto IL_078D;
						}
						int num6 = bytesToDeliver / cryptoStream._outputBlockSize;
						if (num6 <= 1 || !cryptoStream._transform.CanTransformMultipleBlocks)
						{
							goto IL_063F;
						}
						numWholeBlocksInBytes = num6 * cryptoStream._inputBlockSize;
						tempInputBuffer = ArrayPool<byte>.Shared.Rent(numWholeBlocksInBytes);
						tempOutputBuffer = null;
					}
					int num7;
					int num8;
					try
					{
						if (num != 0)
						{
							if (!useAsync)
							{
								num7 = cryptoStream._stream.Read(tempInputBuffer, cryptoStream._inputBufferIndex, numWholeBlocksInBytes - cryptoStream._inputBufferIndex);
								goto IL_0284;
							}
							valueTaskAwaiter = cryptoStream._stream.ReadAsync(new Memory<byte>(tempInputBuffer, cryptoStream._inputBufferIndex, numWholeBlocksInBytes - cryptoStream._inputBufferIndex), cancellationToken).GetAwaiter();
							if (!valueTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								valueTaskAwaiter2 = valueTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<int>, CryptoStream.<ReadAsyncCore>d__42>(ref valueTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							valueTaskAwaiter = valueTaskAwaiter2;
							valueTaskAwaiter2 = default(ValueTaskAwaiter<int>);
							num = (num2 = -1);
						}
						num7 = valueTaskAwaiter.GetResult();
						IL_0284:
						num8 = num7;
						int num9 = cryptoStream._inputBufferIndex + num8;
						if (num9 < cryptoStream._inputBlockSize)
						{
							Buffer.BlockCopy(tempInputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, num8);
							cryptoStream._inputBufferIndex = num9;
						}
						else
						{
							Buffer.BlockCopy(cryptoStream._inputBuffer, 0, tempInputBuffer, 0, cryptoStream._inputBufferIndex);
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._inputBuffer, 0, cryptoStream._inputBufferIndex));
							num8 += cryptoStream._inputBufferIndex;
							cryptoStream._inputBufferIndex = 0;
							int num10 = num8 / cryptoStream._inputBlockSize;
							int num11 = num10 * cryptoStream._inputBlockSize;
							int num12 = num8 - num11;
							if (num12 != 0)
							{
								cryptoStream._inputBufferIndex = num12;
								Buffer.BlockCopy(tempInputBuffer, num11, cryptoStream._inputBuffer, 0, num12);
							}
							tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num10 * cryptoStream._outputBlockSize);
							int num13 = cryptoStream._transform.TransformBlock(tempInputBuffer, 0, num11, tempOutputBuffer, 0);
							Buffer.BlockCopy(tempOutputBuffer, 0, buffer, currentOutputIndex, num13);
							CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, num13));
							ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
							tempOutputBuffer = null;
							bytesToDeliver -= num13;
							currentOutputIndex += num13;
						}
					}
					finally
					{
						if (num < 0)
						{
							if (tempOutputBuffer != null)
							{
								CryptographicOperations.ZeroMemory(tempOutputBuffer);
								ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
								tempOutputBuffer = null;
							}
							CryptographicOperations.ZeroMemory(new Span<byte>(tempInputBuffer, 0, numWholeBlocksInBytes));
							ArrayPool<byte>.Shared.Return(tempInputBuffer, false);
							tempInputBuffer = null;
						}
					}
					tempInputBuffer = null;
					tempOutputBuffer = null;
					goto IL_063F;
					IL_04E4:
					num7 = valueTaskAwaiter.GetResult();
					IL_0515:
					num8 = num7;
					if (num8 != 0)
					{
						cryptoStream._inputBufferIndex += num8;
					}
					else
					{
						byte[] array = cryptoStream._transform.TransformFinalBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBufferIndex);
						cryptoStream._outputBuffer = array;
						cryptoStream._outputBufferIndex = array.Length;
						cryptoStream._finalBlockTransformed = true;
						if (bytesToDeliver < cryptoStream._outputBufferIndex)
						{
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
							cryptoStream._outputBufferIndex -= bytesToDeliver;
							Buffer.BlockCopy(cryptoStream._outputBuffer, bytesToDeliver, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							int num14 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, num14));
							num4 = count;
							goto IL_078D;
						}
						Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, currentOutputIndex, cryptoStream._outputBufferIndex);
						bytesToDeliver -= cryptoStream._outputBufferIndex;
						cryptoStream._outputBufferIndex = 0;
						CryptographicOperations.ZeroMemory(cryptoStream._outputBuffer);
						num4 = count - bytesToDeliver;
						goto IL_078D;
					}
					IL_052C:
					if (cryptoStream._inputBufferIndex >= cryptoStream._inputBlockSize)
					{
						int num13 = cryptoStream._transform.TransformBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
						cryptoStream._inputBufferIndex = 0;
						if (bytesToDeliver < num13)
						{
							Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
							cryptoStream._outputBufferIndex = num13 - bytesToDeliver;
							Buffer.BlockCopy(cryptoStream._outputBuffer, bytesToDeliver, cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							int num15 = cryptoStream._outputBuffer.Length - cryptoStream._outputBufferIndex;
							CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, cryptoStream._outputBufferIndex, num15));
							num4 = count;
							goto IL_078D;
						}
						Buffer.BlockCopy(cryptoStream._outputBuffer, 0, buffer, currentOutputIndex, num13);
						CryptographicOperations.ZeroMemory(new Span<byte>(cryptoStream._outputBuffer, 0, num13));
						currentOutputIndex += num13;
						bytesToDeliver -= num13;
					}
					else
					{
						if (!useAsync)
						{
							num7 = cryptoStream._stream.Read(cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex);
							goto IL_0515;
						}
						valueTaskAwaiter = cryptoStream._stream.ReadAsync(new Memory<byte>(cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex), cancellationToken).GetAwaiter();
						if (!valueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 1);
							valueTaskAwaiter2 = valueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter<int>, CryptoStream.<ReadAsyncCore>d__42>(ref valueTaskAwaiter, ref this);
							return;
						}
						goto IL_04E4;
					}
					IL_063F:
					if (bytesToDeliver > 0)
					{
						goto IL_052C;
					}
					num4 = count;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_078D:
				num2 = -2;
				this.<>t__builder.SetResult(num4);
			}

			// Token: 0x06002D9B RID: 11675 RVA: 0x000A5320 File Offset: 0x000A3520
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FB4 RID: 8116
			public int <>1__state;

			// Token: 0x04001FB5 RID: 8117
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04001FB6 RID: 8118
			public int count;

			// Token: 0x04001FB7 RID: 8119
			public int offset;

			// Token: 0x04001FB8 RID: 8120
			public CryptoStream <>4__this;

			// Token: 0x04001FB9 RID: 8121
			public byte[] buffer;

			// Token: 0x04001FBA RID: 8122
			public bool useAsync;

			// Token: 0x04001FBB RID: 8123
			public CancellationToken cancellationToken;

			// Token: 0x04001FBC RID: 8124
			private int <bytesToDeliver>5__2;

			// Token: 0x04001FBD RID: 8125
			private int <currentOutputIndex>5__3;

			// Token: 0x04001FBE RID: 8126
			private int <numWholeBlocksInBytes>5__4;

			// Token: 0x04001FBF RID: 8127
			private byte[] <tempInputBuffer>5__5;

			// Token: 0x04001FC0 RID: 8128
			private byte[] <tempOutputBuffer>5__6;

			// Token: 0x04001FC1 RID: 8129
			private ValueTaskAwaiter<int> <>u__1;
		}

		// Token: 0x0200043C RID: 1084
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__46 : IAsyncStateMachine
		{
			// Token: 0x06002D9C RID: 11676 RVA: 0x000A5330 File Offset: 0x000A3530
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				try
				{
					ForceAsyncAwaiter forceAsyncAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_0089;
						}
						semaphore = cryptoStream.AsyncActiveSemaphore;
						forceAsyncAwaiter = semaphore.WaitAsync().ForceAsync().GetAwaiter();
						if (!forceAsyncAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ForceAsyncAwaiter forceAsyncAwaiter2 = forceAsyncAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ForceAsyncAwaiter, CryptoStream.<WriteAsyncInternal>d__46>(ref forceAsyncAwaiter, ref this);
							return;
						}
					}
					else
					{
						ForceAsyncAwaiter forceAsyncAwaiter2;
						forceAsyncAwaiter = forceAsyncAwaiter2;
						forceAsyncAwaiter2 = default(ForceAsyncAwaiter);
						num = (num2 = -1);
					}
					forceAsyncAwaiter.GetResult();
					IL_0089:
					try
					{
						TaskAwaiter taskAwaiter;
						if (num != 1)
						{
							taskAwaiter = cryptoStream.WriteAsyncCore(buffer, offset, count, cancellationToken, true).GetAwaiter();
							if (!taskAwaiter.IsCompleted)
							{
								num = (num2 = 1);
								TaskAwaiter taskAwaiter2 = taskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, CryptoStream.<WriteAsyncInternal>d__46>(ref taskAwaiter, ref this);
								return;
							}
						}
						else
						{
							TaskAwaiter taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter);
							num = (num2 = -1);
						}
						taskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							semaphore.Release();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					semaphore = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				semaphore = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06002D9D RID: 11677 RVA: 0x000A54C4 File Offset: 0x000A36C4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FC2 RID: 8130
			public int <>1__state;

			// Token: 0x04001FC3 RID: 8131
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001FC4 RID: 8132
			public CryptoStream <>4__this;

			// Token: 0x04001FC5 RID: 8133
			public byte[] buffer;

			// Token: 0x04001FC6 RID: 8134
			public int offset;

			// Token: 0x04001FC7 RID: 8135
			public int count;

			// Token: 0x04001FC8 RID: 8136
			public CancellationToken cancellationToken;

			// Token: 0x04001FC9 RID: 8137
			private SemaphoreSlim <semaphore>5__2;

			// Token: 0x04001FCA RID: 8138
			private ForceAsyncAwaiter <>u__1;

			// Token: 0x04001FCB RID: 8139
			private TaskAwaiter <>u__2;
		}

		// Token: 0x0200043D RID: 1085
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncCore>d__49 : IAsyncStateMachine
		{
			// Token: 0x06002D9E RID: 11678 RVA: 0x000A54D4 File Offset: 0x000A36D4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				try
				{
					ValueTaskAwaiter valueTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						ValueTaskAwaiter valueTaskAwaiter2;
						valueTaskAwaiter = valueTaskAwaiter2;
						valueTaskAwaiter2 = default(ValueTaskAwaiter);
						num = (num2 = -1);
						break;
					}
					case 1:
					{
						ValueTaskAwaiter valueTaskAwaiter2;
						valueTaskAwaiter = valueTaskAwaiter2;
						valueTaskAwaiter2 = default(ValueTaskAwaiter);
						num = (num2 = -1);
						goto IL_0266;
					}
					case 2:
						IL_02FA:
						try
						{
							if (num != 2)
							{
								numOutputBytes = cryptoStream._transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, tempOutputBuffer, 0);
								if (!useAsync)
								{
									cryptoStream._stream.Write(tempOutputBuffer, 0, numOutputBytes);
									goto IL_03C9;
								}
								valueTaskAwaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(tempOutputBuffer, 0, numOutputBytes), cancellationToken).GetAwaiter();
								if (!valueTaskAwaiter.IsCompleted)
								{
									num = (num2 = 2);
									ValueTaskAwaiter valueTaskAwaiter2 = valueTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref valueTaskAwaiter, ref this);
									return;
								}
							}
							else
							{
								ValueTaskAwaiter valueTaskAwaiter2;
								valueTaskAwaiter = valueTaskAwaiter2;
								valueTaskAwaiter2 = default(ValueTaskAwaiter);
								num = (num2 = -1);
							}
							valueTaskAwaiter.GetResult();
							IL_03C9:
							currentInputIndex += numWholeBlocksInBytes;
							bytesToWrite -= numWholeBlocksInBytes;
						}
						finally
						{
							if (num < 0)
							{
								CryptographicOperations.ZeroMemory(new Span<byte>(tempOutputBuffer, 0, numOutputBytes));
								ArrayPool<byte>.Shared.Return(tempOutputBuffer, false);
								tempOutputBuffer = null;
							}
						}
						tempOutputBuffer = null;
						goto IL_0553;
					case 3:
					{
						ValueTaskAwaiter valueTaskAwaiter2;
						valueTaskAwaiter = valueTaskAwaiter2;
						valueTaskAwaiter2 = default(ValueTaskAwaiter);
						num = (num2 = -1);
						goto IL_04D7;
					}
					default:
						bytesToWrite = count;
						currentInputIndex = offset;
						if (cryptoStream._inputBufferIndex > 0)
						{
							if (count < cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex)
							{
								Buffer.BlockCopy(buffer, offset, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, count);
								cryptoStream._inputBufferIndex += count;
								goto IL_057A;
							}
							Buffer.BlockCopy(buffer, offset, cryptoStream._inputBuffer, cryptoStream._inputBufferIndex, cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex);
							currentInputIndex += cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex;
							bytesToWrite -= cryptoStream._inputBlockSize - cryptoStream._inputBufferIndex;
							cryptoStream._inputBufferIndex = cryptoStream._inputBlockSize;
						}
						if (cryptoStream._outputBufferIndex <= 0)
						{
							goto IL_01B4;
						}
						if (!useAsync)
						{
							cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex);
							goto IL_01AD;
						}
						valueTaskAwaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, cryptoStream._outputBufferIndex), cancellationToken).GetAwaiter();
						if (!valueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ValueTaskAwaiter valueTaskAwaiter2 = valueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref valueTaskAwaiter, ref this);
							return;
						}
						break;
					}
					valueTaskAwaiter.GetResult();
					IL_01AD:
					cryptoStream._outputBufferIndex = 0;
					IL_01B4:
					if (cryptoStream._inputBufferIndex != cryptoStream._inputBlockSize)
					{
						goto IL_0553;
					}
					numOutputBytes = cryptoStream._transform.TransformBlock(cryptoStream._inputBuffer, 0, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
					if (!useAsync)
					{
						cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, numOutputBytes);
						goto IL_0287;
					}
					valueTaskAwaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, numOutputBytes), cancellationToken).GetAwaiter();
					if (!valueTaskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						ValueTaskAwaiter valueTaskAwaiter2 = valueTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref valueTaskAwaiter, ref this);
						return;
					}
					IL_0266:
					valueTaskAwaiter.GetResult();
					IL_0287:
					cryptoStream._inputBufferIndex = 0;
					goto IL_0553;
					IL_04D7:
					valueTaskAwaiter.GetResult();
					IL_04F8:
					currentInputIndex += cryptoStream._inputBlockSize;
					bytesToWrite -= cryptoStream._inputBlockSize;
					IL_0553:
					if (bytesToWrite > 0)
					{
						if (bytesToWrite >= cryptoStream._inputBlockSize)
						{
							int num3 = bytesToWrite / cryptoStream._inputBlockSize;
							if (cryptoStream._transform.CanTransformMultipleBlocks && num3 > 1)
							{
								numWholeBlocksInBytes = num3 * cryptoStream._inputBlockSize;
								tempOutputBuffer = ArrayPool<byte>.Shared.Rent(num3 * cryptoStream._outputBlockSize);
								numOutputBytes = 0;
								goto IL_02FA;
							}
							numOutputBytes = cryptoStream._transform.TransformBlock(buffer, currentInputIndex, cryptoStream._inputBlockSize, cryptoStream._outputBuffer, 0);
							if (!useAsync)
							{
								cryptoStream._stream.Write(cryptoStream._outputBuffer, 0, numOutputBytes);
								goto IL_04F8;
							}
							valueTaskAwaiter = cryptoStream._stream.WriteAsync(new ReadOnlyMemory<byte>(cryptoStream._outputBuffer, 0, numOutputBytes), cancellationToken).GetAwaiter();
							if (!valueTaskAwaiter.IsCompleted)
							{
								num = (num2 = 3);
								ValueTaskAwaiter valueTaskAwaiter2 = valueTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, CryptoStream.<WriteAsyncCore>d__49>(ref valueTaskAwaiter, ref this);
								return;
							}
							goto IL_04D7;
						}
						else
						{
							Buffer.BlockCopy(buffer, currentInputIndex, cryptoStream._inputBuffer, 0, bytesToWrite);
							cryptoStream._inputBufferIndex += bytesToWrite;
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_057A:
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06002D9F RID: 11679 RVA: 0x000A5AA4 File Offset: 0x000A3CA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001FCC RID: 8140
			public int <>1__state;

			// Token: 0x04001FCD RID: 8141
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04001FCE RID: 8142
			public int count;

			// Token: 0x04001FCF RID: 8143
			public int offset;

			// Token: 0x04001FD0 RID: 8144
			public CryptoStream <>4__this;

			// Token: 0x04001FD1 RID: 8145
			public byte[] buffer;

			// Token: 0x04001FD2 RID: 8146
			public bool useAsync;

			// Token: 0x04001FD3 RID: 8147
			public CancellationToken cancellationToken;

			// Token: 0x04001FD4 RID: 8148
			private int <bytesToWrite>5__2;

			// Token: 0x04001FD5 RID: 8149
			private int <currentInputIndex>5__3;

			// Token: 0x04001FD6 RID: 8150
			private int <numOutputBytes>5__4;

			// Token: 0x04001FD7 RID: 8151
			private ValueTaskAwaiter <>u__1;

			// Token: 0x04001FD8 RID: 8152
			private int <numWholeBlocksInBytes>5__5;

			// Token: 0x04001FD9 RID: 8153
			private byte[] <tempOutputBuffer>5__6;
		}

		// Token: 0x0200043E RID: 1086
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002DA0 RID: 11680 RVA: 0x000A5AB2 File Offset: 0x000A3CB2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002DA1 RID: 11681 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06002DA2 RID: 11682 RVA: 0x000A5ABE File Offset: 0x000A3CBE
			internal SemaphoreSlim <get_AsyncActiveSemaphore>b__54_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x04001FDA RID: 8154
			public static readonly CryptoStream.<>c <>9 = new CryptoStream.<>c();

			// Token: 0x04001FDB RID: 8155
			public static Func<SemaphoreSlim> <>9__54_0;
		}
	}
}
