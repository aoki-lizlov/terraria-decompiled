using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200097A RID: 2426
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		// Token: 0x06005817 RID: 22551 RVA: 0x0012A633 File Offset: 0x00128833
		public StringWriter()
			: this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06005818 RID: 22552 RVA: 0x0012A645 File Offset: 0x00128845
		public StringWriter(IFormatProvider formatProvider)
			: this(new StringBuilder(), formatProvider)
		{
		}

		// Token: 0x06005819 RID: 22553 RVA: 0x0012A653 File Offset: 0x00128853
		public StringWriter(StringBuilder sb)
			: this(sb, CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x0600581A RID: 22554 RVA: 0x0012A661 File Offset: 0x00128861
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider)
			: base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("Buffer cannot be null."));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		// Token: 0x0600581B RID: 22555 RVA: 0x0012A690 File Offset: 0x00128890
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600581C RID: 22556 RVA: 0x0012A699 File Offset: 0x00128899
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600581D RID: 22557 RVA: 0x0012A6A9 File Offset: 0x001288A9
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x0012A6C9 File Offset: 0x001288C9
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		// Token: 0x0600581F RID: 22559 RVA: 0x0012A6D1 File Offset: 0x001288D1
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		// Token: 0x06005820 RID: 22560 RVA: 0x0012A6F0 File Offset: 0x001288F0
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(buffer, index, count);
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x0012A77B File Offset: 0x0012897B
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0011F288 File Offset: 0x0011D488
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x0011F296 File Offset: 0x0011D496
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x0011F2A4 File Offset: 0x0011D4A4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x0011F2B4 File Offset: 0x0011D4B4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x0011F2C2 File Offset: 0x0011D4C2
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x0011F2D0 File Offset: 0x0011D4D0
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x000789AA File Offset: 0x00076BAA
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x0012A79A File Offset: 0x0012899A
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x0400350A RID: 13578
		private static volatile UnicodeEncoding m_encoding;

		// Token: 0x0400350B RID: 13579
		private StringBuilder _sb;

		// Token: 0x0400350C RID: 13580
		private bool _isOpen;
	}
}
