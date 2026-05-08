using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008DA RID: 2266
	public enum StackBehaviour
	{
		// Token: 0x04003027 RID: 12327
		Pop0,
		// Token: 0x04003028 RID: 12328
		Pop1,
		// Token: 0x04003029 RID: 12329
		Pop1_pop1,
		// Token: 0x0400302A RID: 12330
		Popi,
		// Token: 0x0400302B RID: 12331
		Popi_pop1,
		// Token: 0x0400302C RID: 12332
		Popi_popi,
		// Token: 0x0400302D RID: 12333
		Popi_popi8,
		// Token: 0x0400302E RID: 12334
		Popi_popi_popi,
		// Token: 0x0400302F RID: 12335
		Popi_popr4,
		// Token: 0x04003030 RID: 12336
		Popi_popr8,
		// Token: 0x04003031 RID: 12337
		Popref,
		// Token: 0x04003032 RID: 12338
		Popref_pop1,
		// Token: 0x04003033 RID: 12339
		Popref_popi,
		// Token: 0x04003034 RID: 12340
		Popref_popi_popi,
		// Token: 0x04003035 RID: 12341
		Popref_popi_popi8,
		// Token: 0x04003036 RID: 12342
		Popref_popi_popr4,
		// Token: 0x04003037 RID: 12343
		Popref_popi_popr8,
		// Token: 0x04003038 RID: 12344
		Popref_popi_popref,
		// Token: 0x04003039 RID: 12345
		Push0,
		// Token: 0x0400303A RID: 12346
		Push1,
		// Token: 0x0400303B RID: 12347
		Push1_push1,
		// Token: 0x0400303C RID: 12348
		Pushi,
		// Token: 0x0400303D RID: 12349
		Pushi8,
		// Token: 0x0400303E RID: 12350
		Pushr4,
		// Token: 0x0400303F RID: 12351
		Pushr8,
		// Token: 0x04003040 RID: 12352
		Pushref,
		// Token: 0x04003041 RID: 12353
		Varpop,
		// Token: 0x04003042 RID: 12354
		Varpush,
		// Token: 0x04003043 RID: 12355
		Popref_popi_pop1
	}
}
