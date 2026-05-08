using System;
using System.IO;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x0200000D RID: 13
	public class CsvSerializer : ICsvSerializer, IDisposable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004610 File Offset: 0x00002810
		public CsvConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004618 File Offset: 0x00002818
		public CsvSerializer(TextWriter writer)
			: this(writer, new CsvConfiguration())
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004626 File Offset: 0x00002826
		public CsvSerializer(TextWriter writer, CsvConfiguration configuration)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.writer = writer;
			this.configuration = configuration;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004658 File Offset: 0x00002858
		public void Write(string[] record)
		{
			this.CheckDisposed();
			string text = string.Join(this.configuration.Delimiter, record);
			this.writer.WriteLine(text);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004689 File Offset: 0x00002889
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004698 File Offset: 0x00002898
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.writer != null)
			{
				this.writer.Dispose();
			}
			this.disposed = true;
			this.writer = null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000046C7 File Offset: 0x000028C7
		protected virtual void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04000020 RID: 32
		private bool disposed;

		// Token: 0x04000021 RID: 33
		private readonly CsvConfiguration configuration;

		// Token: 0x04000022 RID: 34
		private TextWriter writer;
	}
}
