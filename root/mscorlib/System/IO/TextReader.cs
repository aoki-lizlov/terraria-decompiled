using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200093D RID: 2365
	[Serializable]
	public abstract class TextReader : MarshalByRefObject, IDisposable
	{
		// Token: 0x06005461 RID: 21601 RVA: 0x000543BD File Offset: 0x000525BD
		protected TextReader()
		{
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0011DDC6 File Offset: 0x0011BFC6
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0011DDC6 File Offset: 0x0011BFC6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0011B48C File Offset: 0x0011968C
		public virtual int Peek()
		{
			return -1;
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x0011B48C File Offset: 0x0011968C
		public virtual int Read()
		{
			return -1;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x0011DDD8 File Offset: 0x0011BFD8
		public virtual int Read(char[] buffer, int index, int count)
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
			int i;
			for (i = 0; i < count; i++)
			{
				int num = this.Read();
				if (num == -1)
				{
					break;
				}
				buffer[index + i] = (char)num;
			}
			return i;
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x0011DE54 File Offset: 0x0011C054
		public virtual int Read(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("The read operation returned an invalid length.");
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x0011DED0 File Offset: 0x0011C0D0
		public virtual string ReadToEnd()
		{
			char[] array = new char[4096];
			StringBuilder stringBuilder = new StringBuilder(4096);
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				stringBuilder.Append(array, 0, num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x0011DF14 File Offset: 0x0011C114
		public virtual int ReadBlock(char[] buffer, int index, int count)
		{
			int num = 0;
			int num2;
			do
			{
				num += (num2 = this.Read(buffer, index + num, count - num));
			}
			while (num2 > 0 && num < count);
			return num;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x0011DF40 File Offset: 0x0011C140
		public virtual int ReadBlock(Span<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.ReadBlock(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("The read operation returned an invalid length.");
				}
				new Span<char>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0011DFBC File Offset: 0x0011C1BC
		public virtual string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				num = this.Read();
				if (num == -1)
				{
					goto IL_0043;
				}
				if (num == 13 || num == 10)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			if (num == 13 && this.Peek() == 10)
			{
				this.Read();
			}
			return stringBuilder.ToString();
			IL_0043:
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0011E01D File Offset: 0x0011C21D
		public virtual Task<string> ReadLineAsync()
		{
			return Task<string>.Factory.StartNew((object state) => ((TextReader)state).ReadLine(), this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0011E054 File Offset: 0x0011C254
		public virtual async Task<string> ReadToEndAsync()
		{
			StringBuilder sb = new StringBuilder(4096);
			char[] chars = ArrayPool<char>.Shared.Rent(4096);
			try
			{
				int num;
				while ((num = await this.ReadAsyncInternal(chars, default(CancellationToken)).ConfigureAwait(false)) != 0)
				{
					sb.Append(chars, 0, num);
				}
			}
			finally
			{
				ArrayPool<char>.Shared.Return(chars, false);
			}
			return sb.ToString();
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0011E098 File Offset: 0x0011C298
		public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
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
			return this.ReadAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0011E110 File Offset: 0x0011C310
		public virtual ValueTask<int> ReadAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.Read(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0011E188 File Offset: 0x0011C388
		internal virtual ValueTask<int> ReadAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			Tuple<TextReader, Memory<char>> tuple = new Tuple<TextReader, Memory<char>>(this, buffer);
			return new ValueTask<int>(Task<int>.Factory.StartNew(delegate(object state)
			{
				Tuple<TextReader, Memory<char>> tuple2 = (Tuple<TextReader, Memory<char>>)state;
				return tuple2.Item1.Read(tuple2.Item2.Span);
			}, tuple, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default));
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x0011E1D4 File Offset: 0x0011C3D4
		public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
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
			return this.ReadBlockAsyncInternal(new Memory<char>(buffer, index, count), default(CancellationToken)).AsTask();
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0011E24C File Offset: 0x0011C44C
		public virtual ValueTask<int> ReadBlockAsync(Memory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			Task<int> task;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				task = Task<int>.Factory.StartNew(delegate(object state)
				{
					Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
					return tuple.Item1.ReadBlock(tuple.Item2.Span);
				}, Tuple.Create<TextReader, Memory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			else
			{
				task = this.ReadBlockAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
			return new ValueTask<int>(task);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0011E2C4 File Offset: 0x0011C4C4
		internal async ValueTask<int> ReadBlockAsyncInternal(Memory<char> buffer, CancellationToken cancellationToken)
		{
			int i = 0;
			int num;
			do
			{
				num = await this.ReadAsyncInternal(buffer.Slice(i), cancellationToken).ConfigureAwait(false);
				i += num;
			}
			while (num > 0 && i < buffer.Length);
			return i;
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x0011E317 File Offset: 0x0011C517
		public static TextReader Synchronized(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (!(reader is TextReader.SyncTextReader))
			{
				return new TextReader.SyncTextReader(reader);
			}
			return reader;
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0011E337 File Offset: 0x0011C537
		// Note: this type is marked as 'beforefieldinit'.
		static TextReader()
		{
		}

		// Token: 0x040033C4 RID: 13252
		public static readonly TextReader Null = new TextReader.NullTextReader();

		// Token: 0x0200093E RID: 2366
		[Serializable]
		private sealed class NullTextReader : TextReader
		{
			// Token: 0x06005477 RID: 21623 RVA: 0x0011E343 File Offset: 0x0011C543
			public NullTextReader()
			{
			}

			// Token: 0x06005478 RID: 21624 RVA: 0x0000408A File Offset: 0x0000228A
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06005479 RID: 21625 RVA: 0x0000A9B6 File Offset: 0x00008BB6
			public override string ReadLine()
			{
				return null;
			}
		}

		// Token: 0x0200093F RID: 2367
		[Serializable]
		internal sealed class SyncTextReader : TextReader
		{
			// Token: 0x0600547A RID: 21626 RVA: 0x0011E34B File Offset: 0x0011C54B
			internal SyncTextReader(TextReader t)
			{
				this._in = t;
			}

			// Token: 0x0600547B RID: 21627 RVA: 0x0011E35A File Offset: 0x0011C55A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._in.Close();
			}

			// Token: 0x0600547C RID: 21628 RVA: 0x0011E367 File Offset: 0x0011C567
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._in).Dispose();
				}
			}

			// Token: 0x0600547D RID: 21629 RVA: 0x0011E377 File Offset: 0x0011C577
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Peek()
			{
				return this._in.Peek();
			}

			// Token: 0x0600547E RID: 21630 RVA: 0x0011E384 File Offset: 0x0011C584
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read()
			{
				return this._in.Read();
			}

			// Token: 0x0600547F RID: 21631 RVA: 0x0011E391 File Offset: 0x0011C591
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int Read(char[] buffer, int index, int count)
			{
				return this._in.Read(buffer, index, count);
			}

			// Token: 0x06005480 RID: 21632 RVA: 0x0011E3A1 File Offset: 0x0011C5A1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override int ReadBlock(char[] buffer, int index, int count)
			{
				return this._in.ReadBlock(buffer, index, count);
			}

			// Token: 0x06005481 RID: 21633 RVA: 0x0011E3B1 File Offset: 0x0011C5B1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadLine()
			{
				return this._in.ReadLine();
			}

			// Token: 0x06005482 RID: 21634 RVA: 0x0011E3BE File Offset: 0x0011C5BE
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override string ReadToEnd()
			{
				return this._in.ReadToEnd();
			}

			// Token: 0x06005483 RID: 21635 RVA: 0x0011E3CB File Offset: 0x0011C5CB
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadLineAsync()
			{
				return Task.FromResult<string>(this.ReadLine());
			}

			// Token: 0x06005484 RID: 21636 RVA: 0x0011E3D8 File Offset: 0x0011C5D8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task<string> ReadToEndAsync()
			{
				return Task.FromResult<string>(this.ReadToEnd());
			}

			// Token: 0x06005485 RID: 21637 RVA: 0x0011E3E8 File Offset: 0x0011C5E8
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.ReadBlock(buffer, index, count));
			}

			// Token: 0x06005486 RID: 21638 RVA: 0x0011E44C File Offset: 0x0011C64C
			[MethodImpl(MethodImplOptions.Synchronized)]
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
				return Task.FromResult<int>(this.Read(buffer, index, count));
			}

			// Token: 0x040033C5 RID: 13253
			internal readonly TextReader _in;
		}

		// Token: 0x02000940 RID: 2368
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06005487 RID: 21639 RVA: 0x0011E4B0 File Offset: 0x0011C6B0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06005488 RID: 21640 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06005489 RID: 21641 RVA: 0x0011E4BC File Offset: 0x0011C6BC
			internal string <ReadLineAsync>b__13_0(object state)
			{
				return ((TextReader)state).ReadLine();
			}

			// Token: 0x0600548A RID: 21642 RVA: 0x0011E4CC File Offset: 0x0011C6CC
			internal int <ReadAsync>b__16_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}

			// Token: 0x0600548B RID: 21643 RVA: 0x0011E4FC File Offset: 0x0011C6FC
			internal int <ReadAsyncInternal>b__17_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.Read(tuple.Item2.Span);
			}

			// Token: 0x0600548C RID: 21644 RVA: 0x0011E52C File Offset: 0x0011C72C
			internal int <ReadBlockAsync>b__19_0(object state)
			{
				Tuple<TextReader, Memory<char>> tuple = (Tuple<TextReader, Memory<char>>)state;
				return tuple.Item1.ReadBlock(tuple.Item2.Span);
			}

			// Token: 0x040033C6 RID: 13254
			public static readonly TextReader.<>c <>9 = new TextReader.<>c();

			// Token: 0x040033C7 RID: 13255
			public static Func<object, string> <>9__13_0;

			// Token: 0x040033C8 RID: 13256
			public static Func<object, int> <>9__16_0;

			// Token: 0x040033C9 RID: 13257
			public static Func<object, int> <>9__17_0;

			// Token: 0x040033CA RID: 13258
			public static Func<object, int> <>9__19_0;
		}

		// Token: 0x02000941 RID: 2369
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsync>d__14 : IAsyncStateMachine
		{
			// Token: 0x0600548D RID: 21645 RVA: 0x0011E55C File Offset: 0x0011C75C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TextReader textReader = this;
				string text;
				try
				{
					if (num != 0)
					{
						sb = new StringBuilder(4096);
						chars = ArrayPool<char>.Shared.Rent(4096);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num == 0)
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_00CA;
						}
						IL_0050:
						configuredValueTaskAwaiter = textReader.ReadAsyncInternal(chars, default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, TextReader.<ReadToEndAsync>d__14>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_00CA:
						int result;
						if ((result = configuredValueTaskAwaiter.GetResult()) != 0)
						{
							sb.Append(chars, 0, result);
							goto IL_0050;
						}
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<char>.Shared.Return(chars, false);
						}
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					sb = null;
					chars = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				sb = null;
				chars = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x0600548E RID: 21646 RVA: 0x0011E6CC File Offset: 0x0011C8CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033CB RID: 13259
			public int <>1__state;

			// Token: 0x040033CC RID: 13260
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x040033CD RID: 13261
			public TextReader <>4__this;

			// Token: 0x040033CE RID: 13262
			private StringBuilder <sb>5__2;

			// Token: 0x040033CF RID: 13263
			private char[] <chars>5__3;

			// Token: 0x040033D0 RID: 13264
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}

		// Token: 0x02000942 RID: 2370
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBlockAsyncInternal>d__20 : IAsyncStateMachine
		{
			// Token: 0x0600548F RID: 21647 RVA: 0x0011E6DC File Offset: 0x0011C8DC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				TextReader textReader = this;
				int num3;
				try
				{
					ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
					if (num == 0)
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
						num2 = -1;
						goto IL_0094;
					}
					i = 0;
					IL_0018:
					configuredValueTaskAwaiter = textReader.ReadAsyncInternal(buffer.Slice(i), cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredValueTaskAwaiter.IsCompleted)
					{
						num2 = 0;
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, TextReader.<ReadBlockAsyncInternal>d__20>(ref configuredValueTaskAwaiter, ref this);
						return;
					}
					IL_0094:
					int result = configuredValueTaskAwaiter.GetResult();
					i += result;
					if (result > 0 && i < buffer.Length)
					{
						goto IL_0018;
					}
					num3 = i;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x06005490 RID: 21648 RVA: 0x0011E7F4 File Offset: 0x0011C9F4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033D1 RID: 13265
			public int <>1__state;

			// Token: 0x040033D2 RID: 13266
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040033D3 RID: 13267
			public TextReader <>4__this;

			// Token: 0x040033D4 RID: 13268
			public Memory<char> buffer;

			// Token: 0x040033D5 RID: 13269
			public CancellationToken cancellationToken;

			// Token: 0x040033D6 RID: 13270
			private int <n>5__2;

			// Token: 0x040033D7 RID: 13271
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;
		}
	}
}
