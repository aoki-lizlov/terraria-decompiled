using System;

namespace Mono
{
	// Token: 0x0200002D RID: 45
	internal struct RuntimePropertyHandle
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00004324 File Offset: 0x00002524
		internal RuntimePropertyHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000432D File Offset: 0x0000252D
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004338 File Offset: 0x00002538
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimePropertyHandle)obj).Value;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004380 File Offset: 0x00002580
		public bool Equals(RuntimePropertyHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004394 File Offset: 0x00002594
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000043A1 File Offset: 0x000025A1
		public static bool operator ==(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000043AB File Offset: 0x000025AB
		public static bool operator !=(RuntimePropertyHandle left, RuntimePropertyHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CE2 RID: 3298
		private IntPtr value;
	}
}
