using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace ReLogic.IO
{
	// Token: 0x02000081 RID: 129
	public class ConsoleOutputMirror : IDisposable
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0000AF98 File Offset: 0x00009198
		public static void ToFile(string path)
		{
			if (ConsoleOutputMirror._instance != null)
			{
				ConsoleOutputMirror._instance.Dispose();
				ConsoleOutputMirror._instance = null;
			}
			try
			{
				ConsoleOutputMirror._instance = new ConsoleOutputMirror(path);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to bind console output to file: {0}\r\nException: {1}", path, ex);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000AFEC File Offset: 0x000091EC
		private ConsoleOutputMirror(string path)
		{
			this._oldConsoleOutput = Console.Out;
			Directory.CreateDirectory(Directory.GetParent(path).FullName);
			this._fileStream = File.Create(path);
			this._fileWriter = new StreamWriter(this._fileStream)
			{
				AutoFlush = true
			};
			this._newConsoleOutput = new ConsoleOutputMirror.DoubleWriter(this._fileWriter, this._oldConsoleOutput);
			Console.SetOut(this._newConsoleOutput);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B064 File Offset: 0x00009264
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", Justification = "DoubleWriter is not responsible for disposing of writers")]
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					Console.SetOut(this._oldConsoleOutput);
					if (this._fileWriter != null)
					{
						this._fileWriter.Flush();
						this._fileWriter.Close();
						this._fileWriter = null;
					}
					if (this._fileStream != null)
					{
						this._fileStream.Close();
						this._fileStream = null;
					}
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B0CD File Offset: 0x000092CD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x040004E1 RID: 1249
		private static ConsoleOutputMirror _instance;

		// Token: 0x040004E2 RID: 1250
		private FileStream _fileStream;

		// Token: 0x040004E3 RID: 1251
		private StreamWriter _fileWriter;

		// Token: 0x040004E4 RID: 1252
		private TextWriter _newConsoleOutput;

		// Token: 0x040004E5 RID: 1253
		private readonly TextWriter _oldConsoleOutput;

		// Token: 0x040004E6 RID: 1254
		private bool _disposedValue;

		// Token: 0x020000E5 RID: 229
		private class DoubleWriter : TextWriter
		{
			// Token: 0x06000470 RID: 1136 RVA: 0x0000E595 File Offset: 0x0000C795
			public DoubleWriter(TextWriter first, TextWriter second)
			{
				this._first = first;
				this._second = second;
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000E5AB File Offset: 0x0000C7AB
			public override Encoding Encoding
			{
				get
				{
					return this._first.Encoding;
				}
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
			public override void Flush()
			{
				this._first.Flush();
				this._second.Flush();
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
			public override void Write(char value)
			{
				this._first.Write(value);
				this._second.Write(value);
			}

			// Token: 0x04000612 RID: 1554
			private readonly TextWriter _first;

			// Token: 0x04000613 RID: 1555
			private readonly TextWriter _second;
		}
	}
}
