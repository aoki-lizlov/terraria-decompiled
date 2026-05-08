using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000495 RID: 1173
	[ComVisible(true)]
	public sealed class CryptoAPITransform : ICryptoTransform, IDisposable
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x000B1B27 File Offset: 0x000AFD27
		internal CryptoAPITransform()
		{
			this.m_disposed = false;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x0000408A File Offset: 0x0000228A
		public int InputBlockSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000B1B36 File Offset: 0x000AFD36
		public IntPtr KeyHandle
		{
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0000408A File Offset: 0x0000228A
		public int OutputBlockSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000B1B3D File Offset: 0x000AFD3D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000B1B4C File Offset: 0x000AFD4C
		public void Clear()
		{
			this.Dispose(false);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000B1B55 File Offset: 0x000AFD55
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
			}
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0000408A File Offset: 0x0000228A
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return 0;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			return null;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00004088 File Offset: 0x00002288
		[ComVisible(false)]
		public void Reset()
		{
		}

		// Token: 0x0400210E RID: 8462
		private bool m_disposed;
	}
}
