using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020004A2 RID: 1186
	internal class TripleDESTransform : SymmetricTransform
	{
		// Token: 0x060030FB RID: 12539 RVA: 0x000B65F8 File Offset: 0x000B47F8
		public TripleDESTransform(TripleDES algo, bool encryption, byte[] key, byte[] iv)
			: base(algo, encryption, iv)
		{
			if (key == null)
			{
				key = TripleDESTransform.GetStrongKey();
			}
			if (TripleDES.IsWeakKey(key))
			{
				throw new CryptographicException(Locale.GetText("This is a known weak key."));
			}
			byte[] array = new byte[8];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			DES des = DES.Create();
			Buffer.BlockCopy(key, 0, array, 0, 8);
			Buffer.BlockCopy(key, 8, array2, 0, 8);
			if (key.Length == 16)
			{
				Buffer.BlockCopy(key, 0, array3, 0, 8);
			}
			else
			{
				Buffer.BlockCopy(key, 16, array3, 0, 8);
			}
			if (encryption || algo.Mode == CipherMode.CFB)
			{
				this.E1 = new DESTransform(des, true, array, iv);
				this.D2 = new DESTransform(des, false, array2, iv);
				this.E3 = new DESTransform(des, true, array3, iv);
				return;
			}
			this.D1 = new DESTransform(des, false, array3, iv);
			this.E2 = new DESTransform(des, true, array2, iv);
			this.D3 = new DESTransform(des, false, array, iv);
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000B66EC File Offset: 0x000B48EC
		protected override void ECB(byte[] input, byte[] output)
		{
			DESTransform.Permutation(input, output, DESTransform.ipTab, false);
			if (this.encrypt)
			{
				this.E1.ProcessBlock(output, output);
				this.D2.ProcessBlock(output, output);
				this.E3.ProcessBlock(output, output);
			}
			else
			{
				this.D1.ProcessBlock(output, output);
				this.E2.ProcessBlock(output, output);
				this.D3.ProcessBlock(output, output);
			}
			DESTransform.Permutation(output, output, DESTransform.fpTab, true);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000B676C File Offset: 0x000B496C
		internal static byte[] GetStrongKey()
		{
			int num = DESTransform.BLOCK_BYTE_SIZE * 3;
			byte[] array = KeyBuilder.Key(num);
			while (TripleDES.IsWeakKey(array))
			{
				array = KeyBuilder.Key(num);
			}
			return array;
		}

		// Token: 0x040021FD RID: 8701
		private DESTransform E1;

		// Token: 0x040021FE RID: 8702
		private DESTransform D2;

		// Token: 0x040021FF RID: 8703
		private DESTransform E3;

		// Token: 0x04002200 RID: 8704
		private DESTransform D1;

		// Token: 0x04002201 RID: 8705
		private DESTransform E2;

		// Token: 0x04002202 RID: 8706
		private DESTransform D3;
	}
}
