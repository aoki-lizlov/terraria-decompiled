using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004A4 RID: 1188
	public sealed class AesGcm : IDisposable
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x000785B9 File Offset: 0x000767B9
		public AesGcm(byte[] key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000785B9 File Offset: 0x000767B9
		public AesGcm(ReadOnlySpan<byte> key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static KeySizes NonceByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static KeySizes TagByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Decrypt(byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plaintext, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Decrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> tag, Span<byte> plaintext, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x00004088 File Offset: 0x00002288
		public void Dispose()
		{
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Encrypt(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public void Encrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> plaintext, Span<byte> ciphertext, Span<byte> tag, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
}
