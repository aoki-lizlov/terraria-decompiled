using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004A3 RID: 1187
	public sealed class AesCcm : IDisposable
	{
		// Token: 0x060030FE RID: 12542 RVA: 0x000785B9 File Offset: 0x000767B9
		public AesCcm(byte[] key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000785B9 File Offset: 0x000767B9
		public AesCcm(ReadOnlySpan<byte> key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static KeySizes NonceByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static KeySizes TagByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Decrypt(byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plaintext, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Decrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> tag, Span<byte> plaintext, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Encrypt(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Encrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> plaintext, Span<byte> ciphertext, Span<byte> tag, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
}
