using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200046A RID: 1130
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		// Token: 0x06002ECC RID: 11980 RVA: 0x000A827C File Offset: 0x000A647C
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000A8304 File Offset: 0x000A6504
		public MACTripleDES(byte[] rgbKey)
			: this("System.Security.Cryptography.TripleDES", rgbKey)
		{
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000A8314 File Offset: 0x000A6514
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.des = TripleDES.Create();
			}
			else
			{
				this.des = TripleDES.Create(strTripleDES);
			}
			this.HashSizeValue = this.des.BlockSize;
			this.KeyValue = (byte[])rgbKey.Clone();
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000A83AF File Offset: 0x000A65AF
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000A83B8 File Offset: 0x000A65B8
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x000A83C5 File Offset: 0x000A65C5
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.des.Padding;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
				}
				this.des.Padding = value;
			}
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000A83EC File Offset: 0x000A65EC
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000A8464 File Offset: 0x000A6664
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000A84E4 File Offset: 0x000A66E4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04002049 RID: 8265
		private ICryptoTransform m_encryptor;

		// Token: 0x0400204A RID: 8266
		private CryptoStream _cs;

		// Token: 0x0400204B RID: 8267
		private TailStream _ts;

		// Token: 0x0400204C RID: 8268
		private const int m_bitsPerByte = 8;

		// Token: 0x0400204D RID: 8269
		private int m_bytesPerBlock;

		// Token: 0x0400204E RID: 8270
		private TripleDES des;
	}
}
