using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x0200042F RID: 1071
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNamePublicKeyBlob
	{
		// Token: 0x06002D1F RID: 11551 RVA: 0x000A3190 File Offset: 0x000A1390
		public StrongNamePublicKeyBlob(byte[] publicKey)
		{
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			this.pubkey = publicKey;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000A31B0 File Offset: 0x000A13B0
		internal static StrongNamePublicKeyBlob FromString(string s)
		{
			if (s == null || s.Length == 0)
			{
				return null;
			}
			byte[] array = new byte[s.Length / 2];
			int i = 0;
			int num = 0;
			while (i < s.Length)
			{
				byte b = StrongNamePublicKeyBlob.CharToByte(s[i]);
				byte b2 = StrongNamePublicKeyBlob.CharToByte(s[i + 1]);
				array[num] = Convert.ToByte((int)(b * 16 + b2));
				i += 2;
				num++;
			}
			return new StrongNamePublicKeyBlob(array);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000A3224 File Offset: 0x000A1424
		private static byte CharToByte(char c)
		{
			char c2 = char.ToLowerInvariant(c);
			if (char.IsDigit(c2))
			{
				return (byte)(c2 - '0');
			}
			return (byte)(c2 - 'a' + '\n');
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000A3250 File Offset: 0x000A1450
		public override bool Equals(object obj)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = obj as StrongNamePublicKeyBlob;
			if (strongNamePublicKeyBlob == null)
			{
				return false;
			}
			bool flag = this.pubkey.Length == strongNamePublicKeyBlob.pubkey.Length;
			if (flag)
			{
				for (int i = 0; i < this.pubkey.Length; i++)
				{
					if (this.pubkey[i] != strongNamePublicKeyBlob.pubkey[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000A32A8 File Offset: 0x000A14A8
		public override int GetHashCode()
		{
			int num = 0;
			int i = 0;
			int num2 = Math.Min(this.pubkey.Length, 4);
			while (i < num2)
			{
				num = (num << 8) + (int)this.pubkey[i++];
			}
			return num;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000A32E0 File Offset: 0x000A14E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.pubkey.Length; i++)
			{
				stringBuilder.Append(this.pubkey[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F82 RID: 8066
		internal byte[] pubkey;
	}
}
