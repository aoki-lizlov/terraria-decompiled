using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008E1 RID: 2273
	internal struct MonoWin32Resource
	{
		// Token: 0x06004DFB RID: 19963 RVA: 0x000F6026 File Offset: 0x000F4226
		public MonoWin32Resource(int res_type, int res_id, int lang_id, byte[] data)
		{
			this.res_type = res_type;
			this.res_id = res_id;
			this.lang_id = lang_id;
			this.data = data;
		}

		// Token: 0x0400305D RID: 12381
		public int res_type;

		// Token: 0x0400305E RID: 12382
		public int res_id;

		// Token: 0x0400305F RID: 12383
		public int lang_id;

		// Token: 0x04003060 RID: 12384
		public byte[] data;
	}
}
