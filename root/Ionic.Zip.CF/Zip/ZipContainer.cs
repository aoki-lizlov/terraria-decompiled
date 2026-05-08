using System;
using System.IO;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200002D RID: 45
	internal class ZipContainer
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000D30E File Offset: 0x0000B50E
		public ZipContainer(object o)
		{
			this._zf = o as ZipFile;
			this._zos = o as ZipOutputStream;
			this._zis = o as ZipInputStream;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000D33A File Offset: 0x0000B53A
		public ZipFile ZipFile
		{
			get
			{
				return this._zf;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000D342 File Offset: 0x0000B542
		public ZipOutputStream ZipOutputStream
		{
			get
			{
				return this._zos;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D34A File Offset: 0x0000B54A
		public string Name
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Name;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos.Name;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000D379 File Offset: 0x0000B579
		public string Password
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._Password;
				}
				if (this._zis != null)
				{
					return this._zis._Password;
				}
				return this._zos._password;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000D3AE File Offset: 0x0000B5AE
		public Zip64Option Zip64
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._zip64;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos._zip64;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		public int BufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.BufferSize;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return 0;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000D402 File Offset: 0x0000B602
		public int CodecBufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.CodecBufferSize;
				}
				if (this._zis != null)
				{
					return this._zis.CodecBufferSize;
				}
				return this._zos.CodecBufferSize;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000D437 File Offset: 0x0000B637
		public CompressionStrategy Strategy
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Strategy;
				}
				return this._zos.Strategy;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000D458 File Offset: 0x0000B658
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.UseZip64WhenSaving;
				}
				return this._zos.EnableZip64;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000D479 File Offset: 0x0000B679
		public Encoding AlternateEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncoding;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		public Encoding DefaultEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return ZipFile.DefaultEncoding;
				}
				if (this._zos != null)
				{
					return ZipOutputStream.DefaultEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000D4C3 File Offset: 0x0000B6C3
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncodingUsage;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncodingUsage;
				}
				return ZipOption.Default;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000D4EE File Offset: 0x0000B6EE
		public Stream ReadStream
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ReadStream;
				}
				return this._zis.ReadStream;
			}
		}

		// Token: 0x0400012B RID: 299
		private ZipFile _zf;

		// Token: 0x0400012C RID: 300
		private ZipOutputStream _zos;

		// Token: 0x0400012D RID: 301
		private ZipInputStream _zis;
	}
}
