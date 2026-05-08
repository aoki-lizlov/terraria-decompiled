using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005BA RID: 1466
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapHexBinary : ISoapXsd
	{
		// Token: 0x060038FE RID: 14590 RVA: 0x000CB3D4 File Offset: 0x000C95D4
		public SoapHexBinary()
		{
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000CB3E7 File Offset: 0x000C95E7
		public SoapHexBinary(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000CB401 File Offset: 0x000C9601
		// (set) Token: 0x06003901 RID: 14593 RVA: 0x000CB409 File Offset: 0x000C9609
		public byte[] Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003902 RID: 14594 RVA: 0x000CB412 File Offset: 0x000C9612
		public static string XsdType
		{
			get
			{
				return "hexBinary";
			}
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000CB419 File Offset: 0x000C9619
		public string GetXsdType()
		{
			return SoapHexBinary.XsdType;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000CB420 File Offset: 0x000C9620
		public static SoapHexBinary Parse(string value)
		{
			return new SoapHexBinary(SoapHexBinary.FromBinHexString(value));
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000CB430 File Offset: 0x000C9630
		internal static byte[] FromBinHexString(string value)
		{
			char[] array = value.ToCharArray();
			byte[] array2 = new byte[array.Length / 2 + array.Length % 2];
			int num = array.Length;
			if (num % 2 != 0)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			int num2 = 0;
			for (int i = 0; i < num - 1; i += 2)
			{
				array2[num2] = SoapHexBinary.FromHex(array[i], value);
				byte[] array3 = array2;
				int num3 = num2;
				array3[num3] = (byte)(array3[num3] << 4);
				byte[] array4 = array2;
				int num4 = num2;
				array4[num4] += SoapHexBinary.FromHex(array[i + 1], value);
				num2++;
			}
			return array2;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000CB4B0 File Offset: 0x000C96B0
		private static byte FromHex(char hexDigit, string value)
		{
			byte b;
			try
			{
				b = byte.Parse(hexDigit.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				throw SoapHexBinary.CreateInvalidValueException(value);
			}
			return b;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000CB4F0 File Offset: 0x000C96F0
		private static Exception CreateInvalidValueException(string value)
		{
			return new RemotingException(string.Format(CultureInfo.InvariantCulture, "Invalid value '{0}' for xsd:{1}.", value, SoapHexBinary.XsdType));
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000CB50C File Offset: 0x000C970C
		public override string ToString()
		{
			this.sb.Length = 0;
			foreach (byte b in this._value)
			{
				this.sb.Append(b.ToString("X2"));
			}
			return this.sb.ToString();
		}

		// Token: 0x040025AD RID: 9645
		private byte[] _value;

		// Token: 0x040025AE RID: 9646
		private StringBuilder sb = new StringBuilder();
	}
}
