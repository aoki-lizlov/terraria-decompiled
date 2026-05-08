using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	// Token: 0x02000A66 RID: 2662
	[ComVisible(true)]
	[Obsolete]
	[Serializable]
	public struct AssemblyHash : ICloneable
	{
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600616F RID: 24943 RVA: 0x0014DA1B File Offset: 0x0014BC1B
		// (set) Token: 0x06006170 RID: 24944 RVA: 0x0014DA23 File Offset: 0x0014BC23
		[Obsolete]
		public AssemblyHashAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0014DA2C File Offset: 0x0014BC2C
		[Obsolete]
		public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
		{
			this._algorithm = algorithm;
			if (value != null)
			{
				this._value = (byte[])value.Clone();
				return;
			}
			this._value = null;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0014DA51 File Offset: 0x0014BC51
		[Obsolete]
		public AssemblyHash(byte[] value)
		{
			this = new AssemblyHash(AssemblyHashAlgorithm.SHA1, value);
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0014DA5F File Offset: 0x0014BC5F
		[Obsolete]
		public object Clone()
		{
			return new AssemblyHash(this._algorithm, this._value);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0014DA77 File Offset: 0x0014BC77
		[Obsolete]
		public byte[] GetValue()
		{
			return this._value;
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0014DA7F File Offset: 0x0014BC7F
		[Obsolete]
		public void SetValue(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0014DA88 File Offset: 0x0014BC88
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyHash()
		{
		}

		// Token: 0x04003A8E RID: 14990
		private AssemblyHashAlgorithm _algorithm;

		// Token: 0x04003A8F RID: 14991
		private byte[] _value;

		// Token: 0x04003A90 RID: 14992
		[Obsolete]
		public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, null);
	}
}
