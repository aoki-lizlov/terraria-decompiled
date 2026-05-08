using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000445 RID: 1093
	public sealed class RSASignaturePadding : IEquatable<RSASignaturePadding>
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x000A6115 File Offset: 0x000A4315
		private RSASignaturePadding(RSASignaturePaddingMode mode)
		{
			this._mode = mode;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000A6124 File Offset: 0x000A4324
		public static RSASignaturePadding Pkcs1
		{
			get
			{
				return RSASignaturePadding.s_pkcs1;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000A612B File Offset: 0x000A432B
		public static RSASignaturePadding Pss
		{
			get
			{
				return RSASignaturePadding.s_pss;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000A6132 File Offset: 0x000A4332
		public RSASignaturePaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000A613A File Offset: 0x000A433A
		public override int GetHashCode()
		{
			return this._mode.GetHashCode();
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000A614D File Offset: 0x000A434D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSASignaturePadding);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000A615B File Offset: 0x000A435B
		public bool Equals(RSASignaturePadding other)
		{
			return other != null && this._mode == other._mode;
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x000A6176 File Offset: 0x000A4376
		public static bool operator ==(RSASignaturePadding left, RSASignaturePadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000A6187 File Offset: 0x000A4387
		public static bool operator !=(RSASignaturePadding left, RSASignaturePadding right)
		{
			return !(left == right);
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000A6193 File Offset: 0x000A4393
		public override string ToString()
		{
			return this._mode.ToString();
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000A61A6 File Offset: 0x000A43A6
		// Note: this type is marked as 'beforefieldinit'.
		static RSASignaturePadding()
		{
		}

		// Token: 0x04001FEE RID: 8174
		private static readonly RSASignaturePadding s_pkcs1 = new RSASignaturePadding(RSASignaturePaddingMode.Pkcs1);

		// Token: 0x04001FEF RID: 8175
		private static readonly RSASignaturePadding s_pss = new RSASignaturePadding(RSASignaturePaddingMode.Pss);

		// Token: 0x04001FF0 RID: 8176
		private readonly RSASignaturePaddingMode _mode;
	}
}
