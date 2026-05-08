using System;

namespace Mono
{
	// Token: 0x0200002C RID: 44
	internal struct RuntimeEventHandle
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004292 File Offset: 0x00002492
		internal RuntimeEventHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000429B File Offset: 0x0000249B
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042A4 File Offset: 0x000024A4
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeEventHandle)obj).Value;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042EC File Offset: 0x000024EC
		public bool Equals(RuntimeEventHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004300 File Offset: 0x00002500
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000430D File Offset: 0x0000250D
		public static bool operator ==(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004317 File Offset: 0x00002517
		public static bool operator !=(RuntimeEventHandle left, RuntimeEventHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CE1 RID: 3297
		private IntPtr value;
	}
}
