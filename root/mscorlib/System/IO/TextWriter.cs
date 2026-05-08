using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000943 RID: 2371
	[Serializable]
	public abstract class TextWriter : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x06005491 RID: 21649 RVA: 0x0011E802 File Offset: 0x0011CA02
		protected TextWriter()
		{
			this._internalFormatProvider = null;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0011E827 File Offset: 0x0011CA27
		protected TextWriter(IFormatProvider formatProvider)
		{
			this._internalFormatProvider = formatProvider;
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x0011E84C File Offset: 0x0011CA4C
		public virtual IFormatProvider FormatProvider
		{
			get
			{
				if (this._internalFormatProvider == null)
				{
					return CultureInfo.CurrentCulture;
				}
				return this._internalFormatProvider;
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0011C373 File Offset: 0x0011A573
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0011C373 File Offset: 0x0011A573
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0011E864 File Offset: 0x0011CA64
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				this.Dispose();
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask(Task.FromException(ex));
			}
			return valueTask;
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Flush()
		{
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06005499 RID: 21657
		public abstract Encoding Encoding { get; }

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x0011E8A4 File Offset: 0x0011CAA4
		// (set) Token: 0x0600549B RID: 21659 RVA: 0x0011E8AC File Offset: 0x0011CAAC
		public virtual string NewLine
		{
			get
			{
				return this.CoreNewLineStr;
			}
			set
			{
				if (value == null)
				{
					value = Environment.NewLine;
				}
				this.CoreNewLineStr = value;
				this.CoreNewLine = value.ToCharArray();
			}
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Write(char value)
		{
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0011E8CB File Offset: 0x0011CACB
		public virtual void Write(char[] buffer)
		{
			if (buffer != null)
			{
				this.Write(buffer, 0, buffer.Length);
			}
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x0011E8DC File Offset: 0x0011CADC
		public virtual void Write(char[] buffer, int index, int count)
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
			for (int i = 0; i < count; i++)
			{
				this.Write(buffer[index + i]);
			}
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x0011E950 File Offset: 0x0011CB50
		public virtual void Write(ReadOnlySpan<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(new Span<char>(array));
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0011E9AC File Offset: 0x0011CBAC
		public virtual void Write(bool value)
		{
			this.Write(value ? "True" : "False");
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0011E9C3 File Offset: 0x0011CBC3
		public virtual void Write(int value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x0011E9D8 File Offset: 0x0011CBD8
		[CLSCompliant(false)]
		public virtual void Write(uint value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0011E9ED File Offset: 0x0011CBED
		public virtual void Write(long value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x0011EA02 File Offset: 0x0011CC02
		[CLSCompliant(false)]
		public virtual void Write(ulong value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x0011EA17 File Offset: 0x0011CC17
		public virtual void Write(float value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0011EA2C File Offset: 0x0011CC2C
		public virtual void Write(double value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0011EA41 File Offset: 0x0011CC41
		public virtual void Write(decimal value)
		{
			this.Write(value.ToString(this.FormatProvider));
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x0011EA56 File Offset: 0x0011CC56
		public virtual void Write(string value)
		{
			if (value != null)
			{
				this.Write(value.ToCharArray());
			}
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x0011EA68 File Offset: 0x0011CC68
		public virtual void Write(object value)
		{
			if (value != null)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					this.Write(formattable.ToString(null, this.FormatProvider));
					return;
				}
				this.Write(value.ToString());
			}
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0011EAA2 File Offset: 0x0011CCA2
		public virtual void Write(string format, object arg0)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0011EAB7 File Offset: 0x0011CCB7
		public virtual void Write(string format, object arg0, object arg1)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0011EACD File Offset: 0x0011CCCD
		public virtual void Write(string format, object arg0, object arg1, object arg2)
		{
			this.Write(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x0011EAE5 File Offset: 0x0011CCE5
		public virtual void Write(string format, params object[] arg)
		{
			this.Write(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0011EAFA File Offset: 0x0011CCFA
		public virtual void WriteLine()
		{
			this.Write(this.CoreNewLine);
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0011EB08 File Offset: 0x0011CD08
		public virtual void WriteLine(char value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x0011EB17 File Offset: 0x0011CD17
		public virtual void WriteLine(char[] buffer)
		{
			this.Write(buffer);
			this.WriteLine();
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0011EB26 File Offset: 0x0011CD26
		public virtual void WriteLine(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			this.WriteLine();
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0011EB38 File Offset: 0x0011CD38
		public virtual void WriteLine(ReadOnlySpan<char> buffer)
		{
			char[] array = ArrayPool<char>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(new Span<char>(array));
				this.WriteLine(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0011EB94 File Offset: 0x0011CD94
		public virtual void WriteLine(bool value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0011EBA3 File Offset: 0x0011CDA3
		public virtual void WriteLine(int value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x0011EBB2 File Offset: 0x0011CDB2
		[CLSCompliant(false)]
		public virtual void WriteLine(uint value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0011EBC1 File Offset: 0x0011CDC1
		public virtual void WriteLine(long value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0011EBD0 File Offset: 0x0011CDD0
		[CLSCompliant(false)]
		public virtual void WriteLine(ulong value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0011EBDF File Offset: 0x0011CDDF
		public virtual void WriteLine(float value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0011EBEE File Offset: 0x0011CDEE
		public virtual void WriteLine(double value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0011EBFD File Offset: 0x0011CDFD
		public virtual void WriteLine(decimal value)
		{
			this.Write(value);
			this.WriteLine();
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0011EC0C File Offset: 0x0011CE0C
		public virtual void WriteLine(string value)
		{
			if (value != null)
			{
				this.Write(value);
			}
			this.Write(this.CoreNewLineStr);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x0011EC24 File Offset: 0x0011CE24
		public virtual void WriteLine(object value)
		{
			if (value == null)
			{
				this.WriteLine();
				return;
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				this.WriteLine(formattable.ToString(null, this.FormatProvider));
				return;
			}
			this.WriteLine(value.ToString());
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x0011EC65 File Offset: 0x0011CE65
		public virtual void WriteLine(string format, object arg0)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0));
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x0011EC7A File Offset: 0x0011CE7A
		public virtual void WriteLine(string format, object arg0, object arg1)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1));
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x0011EC90 File Offset: 0x0011CE90
		public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg0, arg1, arg2));
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0011ECA8 File Offset: 0x0011CEA8
		public virtual void WriteLine(string format, params object[] arg)
		{
			this.WriteLine(string.Format(this.FormatProvider, format, arg));
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x0011ECC0 File Offset: 0x0011CEC0
		public virtual Task WriteAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char> tuple2 = (Tuple<TextWriter, char>)state;
				tuple2.Item1.Write(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x0011ED0C File Offset: 0x0011CF0C
		public virtual Task WriteAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, string> tuple2 = (Tuple<TextWriter, string>)state;
				tuple2.Item1.Write(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0011ED56 File Offset: 0x0011CF56
		public Task WriteAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return Task.CompletedTask;
			}
			return this.WriteAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x060054C4 RID: 21700 RVA: 0x0011ED6C File Offset: 0x0011CF6C
		public virtual Task WriteAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple2 = (Tuple<TextWriter, char[], int, int>)state;
				tuple2.Item1.Write(tuple2.Item2, tuple2.Item3, tuple2.Item4);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054C5 RID: 21701 RVA: 0x0011EDB8 File Offset: 0x0011CFB8
		public virtual Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				return Task.Factory.StartNew(delegate(object state)
				{
					Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
					tuple.Item1.Write(tuple.Item2.Span);
				}, Tuple.Create<TextWriter, ReadOnlyMemory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			return this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0011EE24 File Offset: 0x0011D024
		public virtual Task WriteLineAsync(char value)
		{
			Tuple<TextWriter, char> tuple = new Tuple<TextWriter, char>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char> tuple2 = (Tuple<TextWriter, char>)state;
				tuple2.Item1.WriteLine(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0011EE70 File Offset: 0x0011D070
		public virtual Task WriteLineAsync(string value)
		{
			Tuple<TextWriter, string> tuple = new Tuple<TextWriter, string>(this, value);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, string> tuple2 = (Tuple<TextWriter, string>)state;
				tuple2.Item1.WriteLine(tuple2.Item2);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0011EEBA File Offset: 0x0011D0BA
		public Task WriteLineAsync(char[] buffer)
		{
			if (buffer == null)
			{
				return this.WriteLineAsync();
			}
			return this.WriteLineAsync(buffer, 0, buffer.Length);
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x0011EED4 File Offset: 0x0011D0D4
		public virtual Task WriteLineAsync(char[] buffer, int index, int count)
		{
			Tuple<TextWriter, char[], int, int> tuple = new Tuple<TextWriter, char[], int, int>(this, buffer, index, count);
			return Task.Factory.StartNew(delegate(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple2 = (Tuple<TextWriter, char[], int, int>)state;
				tuple2.Item1.WriteLine(tuple2.Item2, tuple2.Item3, tuple2.Item4);
			}, tuple, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x0011EF20 File Offset: 0x0011D120
		public virtual Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<char> arraySegment;
			if (!MemoryMarshal.TryGetArray<char>(buffer, out arraySegment))
			{
				return Task.Factory.StartNew(delegate(object state)
				{
					Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
					tuple.Item1.WriteLine(tuple.Item2.Span);
				}, Tuple.Create<TextWriter, ReadOnlyMemory<char>>(this, buffer), cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			}
			return this.WriteLineAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0011EF8A File Offset: 0x0011D18A
		public virtual Task WriteLineAsync()
		{
			return this.WriteAsync(this.CoreNewLine);
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0011EF98 File Offset: 0x0011D198
		public virtual Task FlushAsync()
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((TextWriter)state).Flush();
			}, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0011EFCF File Offset: 0x0011D1CF
		public static TextWriter Synchronized(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (!(writer is TextWriter.SyncTextWriter))
			{
				return new TextWriter.SyncTextWriter(writer);
			}
			return writer;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0011EFEF File Offset: 0x0011D1EF
		// Note: this type is marked as 'beforefieldinit'.
		static TextWriter()
		{
		}

		// Token: 0x040033D8 RID: 13272
		public static readonly TextWriter Null = new TextWriter.NullTextWriter();

		// Token: 0x040033D9 RID: 13273
		private static readonly char[] s_coreNewLine = Environment.NewLine.ToCharArray();

		// Token: 0x040033DA RID: 13274
		protected char[] CoreNewLine = TextWriter.s_coreNewLine;

		// Token: 0x040033DB RID: 13275
		private string CoreNewLineStr = Environment.NewLine;

		// Token: 0x040033DC RID: 13276
		private IFormatProvider _internalFormatProvider;

		// Token: 0x02000944 RID: 2372
		[Serializable]
		private sealed class NullTextWriter : TextWriter
		{
			// Token: 0x060054CF RID: 21711 RVA: 0x0011F00A File Offset: 0x0011D20A
			internal NullTextWriter()
				: base(CultureInfo.InvariantCulture)
			{
			}

			// Token: 0x17000DF4 RID: 3572
			// (get) Token: 0x060054D0 RID: 21712 RVA: 0x0011B485 File Offset: 0x00119685
			public override Encoding Encoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x060054D1 RID: 21713 RVA: 0x00004088 File Offset: 0x00002288
			public override void Write(char[] buffer, int index, int count)
			{
			}

			// Token: 0x060054D2 RID: 21714 RVA: 0x00004088 File Offset: 0x00002288
			public override void Write(string value)
			{
			}

			// Token: 0x060054D3 RID: 21715 RVA: 0x00004088 File Offset: 0x00002288
			public override void WriteLine()
			{
			}

			// Token: 0x060054D4 RID: 21716 RVA: 0x00004088 File Offset: 0x00002288
			public override void WriteLine(string value)
			{
			}

			// Token: 0x060054D5 RID: 21717 RVA: 0x00004088 File Offset: 0x00002288
			public override void WriteLine(object value)
			{
			}

			// Token: 0x060054D6 RID: 21718 RVA: 0x00004088 File Offset: 0x00002288
			public override void Write(char value)
			{
			}
		}

		// Token: 0x02000945 RID: 2373
		[Serializable]
		internal sealed class SyncTextWriter : TextWriter, IDisposable
		{
			// Token: 0x060054D7 RID: 21719 RVA: 0x0011F017 File Offset: 0x0011D217
			internal SyncTextWriter(TextWriter t)
				: base(t.FormatProvider)
			{
				this._out = t;
			}

			// Token: 0x17000DF5 RID: 3573
			// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0011F02C File Offset: 0x0011D22C
			public override Encoding Encoding
			{
				get
				{
					return this._out.Encoding;
				}
			}

			// Token: 0x17000DF6 RID: 3574
			// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0011F039 File Offset: 0x0011D239
			public override IFormatProvider FormatProvider
			{
				get
				{
					return this._out.FormatProvider;
				}
			}

			// Token: 0x17000DF7 RID: 3575
			// (get) Token: 0x060054DA RID: 21722 RVA: 0x0011F046 File Offset: 0x0011D246
			// (set) Token: 0x060054DB RID: 21723 RVA: 0x0011F053 File Offset: 0x0011D253
			public override string NewLine
			{
				[MethodImpl(MethodImplOptions.Synchronized)]
				get
				{
					return this._out.NewLine;
				}
				[MethodImpl(MethodImplOptions.Synchronized)]
				set
				{
					this._out.NewLine = value;
				}
			}

			// Token: 0x060054DC RID: 21724 RVA: 0x0011F061 File Offset: 0x0011D261
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Close()
			{
				this._out.Close();
			}

			// Token: 0x060054DD RID: 21725 RVA: 0x0011F06E File Offset: 0x0011D26E
			[MethodImpl(MethodImplOptions.Synchronized)]
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					((IDisposable)this._out).Dispose();
				}
			}

			// Token: 0x060054DE RID: 21726 RVA: 0x0011F07E File Offset: 0x0011D27E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Flush()
			{
				this._out.Flush();
			}

			// Token: 0x060054DF RID: 21727 RVA: 0x0011F08B File Offset: 0x0011D28B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E0 RID: 21728 RVA: 0x0011F099 File Offset: 0x0011D299
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer)
			{
				this._out.Write(buffer);
			}

			// Token: 0x060054E1 RID: 21729 RVA: 0x0011F0A7 File Offset: 0x0011D2A7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(char[] buffer, int index, int count)
			{
				this._out.Write(buffer, index, count);
			}

			// Token: 0x060054E2 RID: 21730 RVA: 0x0011F0B7 File Offset: 0x0011D2B7
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(bool value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E3 RID: 21731 RVA: 0x0011F0C5 File Offset: 0x0011D2C5
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(int value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E4 RID: 21732 RVA: 0x0011F0D3 File Offset: 0x0011D2D3
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(uint value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E5 RID: 21733 RVA: 0x0011F0E1 File Offset: 0x0011D2E1
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(long value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E6 RID: 21734 RVA: 0x0011F0EF File Offset: 0x0011D2EF
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(ulong value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E7 RID: 21735 RVA: 0x0011F0FD File Offset: 0x0011D2FD
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(float value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E8 RID: 21736 RVA: 0x0011F10B File Offset: 0x0011D30B
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(double value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054E9 RID: 21737 RVA: 0x0011F119 File Offset: 0x0011D319
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(decimal value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054EA RID: 21738 RVA: 0x0011F127 File Offset: 0x0011D327
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054EB RID: 21739 RVA: 0x0011F135 File Offset: 0x0011D335
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(object value)
			{
				this._out.Write(value);
			}

			// Token: 0x060054EC RID: 21740 RVA: 0x0011F143 File Offset: 0x0011D343
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0)
			{
				this._out.Write(format, arg0);
			}

			// Token: 0x060054ED RID: 21741 RVA: 0x0011F152 File Offset: 0x0011D352
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1)
			{
				this._out.Write(format, arg0, arg1);
			}

			// Token: 0x060054EE RID: 21742 RVA: 0x0011F162 File Offset: 0x0011D362
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, object arg0, object arg1, object arg2)
			{
				this._out.Write(format, arg0, arg1, arg2);
			}

			// Token: 0x060054EF RID: 21743 RVA: 0x0011F174 File Offset: 0x0011D374
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void Write(string format, params object[] arg)
			{
				this._out.Write(format, arg);
			}

			// Token: 0x060054F0 RID: 21744 RVA: 0x0011F183 File Offset: 0x0011D383
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine()
			{
				this._out.WriteLine();
			}

			// Token: 0x060054F1 RID: 21745 RVA: 0x0011F190 File Offset: 0x0011D390
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F2 RID: 21746 RVA: 0x0011F19E File Offset: 0x0011D39E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(decimal value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F3 RID: 21747 RVA: 0x0011F1AC File Offset: 0x0011D3AC
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer)
			{
				this._out.WriteLine(buffer);
			}

			// Token: 0x060054F4 RID: 21748 RVA: 0x0011F1BA File Offset: 0x0011D3BA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(char[] buffer, int index, int count)
			{
				this._out.WriteLine(buffer, index, count);
			}

			// Token: 0x060054F5 RID: 21749 RVA: 0x0011F1CA File Offset: 0x0011D3CA
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(bool value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F6 RID: 21750 RVA: 0x0011F1D8 File Offset: 0x0011D3D8
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(int value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F7 RID: 21751 RVA: 0x0011F1E6 File Offset: 0x0011D3E6
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(uint value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F8 RID: 21752 RVA: 0x0011F1F4 File Offset: 0x0011D3F4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(long value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054F9 RID: 21753 RVA: 0x0011F202 File Offset: 0x0011D402
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(ulong value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054FA RID: 21754 RVA: 0x0011F210 File Offset: 0x0011D410
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(float value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054FB RID: 21755 RVA: 0x0011F21E File Offset: 0x0011D41E
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(double value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054FC RID: 21756 RVA: 0x0011F22C File Offset: 0x0011D42C
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054FD RID: 21757 RVA: 0x0011F23A File Offset: 0x0011D43A
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(object value)
			{
				this._out.WriteLine(value);
			}

			// Token: 0x060054FE RID: 21758 RVA: 0x0011F248 File Offset: 0x0011D448
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0)
			{
				this._out.WriteLine(format, arg0);
			}

			// Token: 0x060054FF RID: 21759 RVA: 0x0011F257 File Offset: 0x0011D457
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1)
			{
				this._out.WriteLine(format, arg0, arg1);
			}

			// Token: 0x06005500 RID: 21760 RVA: 0x0011F267 File Offset: 0x0011D467
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, object arg0, object arg1, object arg2)
			{
				this._out.WriteLine(format, arg0, arg1, arg2);
			}

			// Token: 0x06005501 RID: 21761 RVA: 0x0011F279 File Offset: 0x0011D479
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override void WriteLine(string format, params object[] arg)
			{
				this._out.WriteLine(format, arg);
			}

			// Token: 0x06005502 RID: 21762 RVA: 0x0011F288 File Offset: 0x0011D488
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06005503 RID: 21763 RVA: 0x0011F296 File Offset: 0x0011D496
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(string value)
			{
				this.Write(value);
				return Task.CompletedTask;
			}

			// Token: 0x06005504 RID: 21764 RVA: 0x0011F2A4 File Offset: 0x0011D4A4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteAsync(char[] buffer, int index, int count)
			{
				this.Write(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06005505 RID: 21765 RVA: 0x0011F2B4 File Offset: 0x0011D4B4
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06005506 RID: 21766 RVA: 0x0011F2C2 File Offset: 0x0011D4C2
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(string value)
			{
				this.WriteLine(value);
				return Task.CompletedTask;
			}

			// Token: 0x06005507 RID: 21767 RVA: 0x0011F2D0 File Offset: 0x0011D4D0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task WriteLineAsync(char[] buffer, int index, int count)
			{
				this.WriteLine(buffer, index, count);
				return Task.CompletedTask;
			}

			// Token: 0x06005508 RID: 21768 RVA: 0x0011F2E0 File Offset: 0x0011D4E0
			[MethodImpl(MethodImplOptions.Synchronized)]
			public override Task FlushAsync()
			{
				this.Flush();
				return Task.CompletedTask;
			}

			// Token: 0x040033DD RID: 13277
			private readonly TextWriter _out;
		}

		// Token: 0x02000946 RID: 2374
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06005509 RID: 21769 RVA: 0x0011F2ED File Offset: 0x0011D4ED
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600550A RID: 21770 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600550B RID: 21771 RVA: 0x0011F2FC File Offset: 0x0011D4FC
			internal void <WriteAsync>b__56_0(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.Write(tuple.Item2);
			}

			// Token: 0x0600550C RID: 21772 RVA: 0x0011F324 File Offset: 0x0011D524
			internal void <WriteAsync>b__57_0(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.Write(tuple.Item2);
			}

			// Token: 0x0600550D RID: 21773 RVA: 0x0011F34C File Offset: 0x0011D54C
			internal void <WriteAsync>b__59_0(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.Write(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x0600550E RID: 21774 RVA: 0x0011F380 File Offset: 0x0011D580
			internal void <WriteAsync>b__60_0(object state)
			{
				Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
				tuple.Item1.Write(tuple.Item2.Span);
			}

			// Token: 0x0600550F RID: 21775 RVA: 0x0011F3B0 File Offset: 0x0011D5B0
			internal void <WriteLineAsync>b__61_0(object state)
			{
				Tuple<TextWriter, char> tuple = (Tuple<TextWriter, char>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}

			// Token: 0x06005510 RID: 21776 RVA: 0x0011F3D8 File Offset: 0x0011D5D8
			internal void <WriteLineAsync>b__62_0(object state)
			{
				Tuple<TextWriter, string> tuple = (Tuple<TextWriter, string>)state;
				tuple.Item1.WriteLine(tuple.Item2);
			}

			// Token: 0x06005511 RID: 21777 RVA: 0x0011F400 File Offset: 0x0011D600
			internal void <WriteLineAsync>b__64_0(object state)
			{
				Tuple<TextWriter, char[], int, int> tuple = (Tuple<TextWriter, char[], int, int>)state;
				tuple.Item1.WriteLine(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x06005512 RID: 21778 RVA: 0x0011F434 File Offset: 0x0011D634
			internal void <WriteLineAsync>b__65_0(object state)
			{
				Tuple<TextWriter, ReadOnlyMemory<char>> tuple = (Tuple<TextWriter, ReadOnlyMemory<char>>)state;
				tuple.Item1.WriteLine(tuple.Item2.Span);
			}

			// Token: 0x06005513 RID: 21779 RVA: 0x0011F461 File Offset: 0x0011D661
			internal void <FlushAsync>b__67_0(object state)
			{
				((TextWriter)state).Flush();
			}

			// Token: 0x040033DE RID: 13278
			public static readonly TextWriter.<>c <>9 = new TextWriter.<>c();

			// Token: 0x040033DF RID: 13279
			public static Action<object> <>9__56_0;

			// Token: 0x040033E0 RID: 13280
			public static Action<object> <>9__57_0;

			// Token: 0x040033E1 RID: 13281
			public static Action<object> <>9__59_0;

			// Token: 0x040033E2 RID: 13282
			public static Action<object> <>9__60_0;

			// Token: 0x040033E3 RID: 13283
			public static Action<object> <>9__61_0;

			// Token: 0x040033E4 RID: 13284
			public static Action<object> <>9__62_0;

			// Token: 0x040033E5 RID: 13285
			public static Action<object> <>9__64_0;

			// Token: 0x040033E6 RID: 13286
			public static Action<object> <>9__65_0;

			// Token: 0x040033E7 RID: 13287
			public static Action<object> <>9__67_0;
		}
	}
}
