using System;

namespace System.Security.AccessControl
{
	// Token: 0x020004ED RID: 1261
	public sealed class CustomAce : GenericAce
	{
		// Token: 0x0600336E RID: 13166 RVA: 0x000BDA39 File Offset: 0x000BBC39
		public CustomAce(AceType type, AceFlags flags, byte[] opaque)
			: base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003370 RID: 13168 RVA: 0x000BDA4A File Offset: 0x000BBC4A
		public int OpaqueLength
		{
			get
			{
				return this.opaque.Length;
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000BDA54 File Offset: 0x000BBC54
		public byte[] GetOpaque()
		{
			return (byte[])this.opaque.Clone();
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000BDA66 File Offset: 0x000BBC66
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x00047E00 File Offset: 0x00046000
		internal override string GetSddlForm()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002408 RID: 9224
		private byte[] opaque;

		// Token: 0x04002409 RID: 9225
		[MonoTODO]
		public static readonly int MaxOpaqueLength;
	}
}
