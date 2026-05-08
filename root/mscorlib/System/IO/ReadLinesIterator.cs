using System;
using System.Text;

namespace System.IO
{
	// Token: 0x0200095E RID: 2398
	internal class ReadLinesIterator : Iterator<string>
	{
		// Token: 0x060056D3 RID: 22227 RVA: 0x001250E3 File Offset: 0x001232E3
		private ReadLinesIterator(string path, Encoding encoding, StreamReader reader)
		{
			this._path = path;
			this._encoding = encoding;
			this._reader = reader;
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x00125100 File Offset: 0x00123300
		public override bool MoveNext()
		{
			if (this._reader != null)
			{
				this.current = this._reader.ReadLine();
				if (this.current != null)
				{
					return true;
				}
				base.Dispose();
			}
			return false;
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x0012512C File Offset: 0x0012332C
		protected override Iterator<string> Clone()
		{
			return ReadLinesIterator.CreateIterator(this._path, this._encoding, this._reader);
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x00125148 File Offset: 0x00123348
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._reader != null)
				{
					this._reader.Dispose();
				}
			}
			finally
			{
				this._reader = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x0012518C File Offset: 0x0012338C
		internal static ReadLinesIterator CreateIterator(string path, Encoding encoding)
		{
			return ReadLinesIterator.CreateIterator(path, encoding, null);
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x00125196 File Offset: 0x00123396
		private static ReadLinesIterator CreateIterator(string path, Encoding encoding, StreamReader reader)
		{
			return new ReadLinesIterator(path, encoding, reader ?? new StreamReader(path, encoding));
		}

		// Token: 0x04003460 RID: 13408
		private readonly string _path;

		// Token: 0x04003461 RID: 13409
		private readonly Encoding _encoding;

		// Token: 0x04003462 RID: 13410
		private StreamReader _reader;
	}
}
