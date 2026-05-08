using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C9 RID: 1993
	[AttributeUsage(AttributeTargets.Property, Inherited = true)]
	[Serializable]
	public sealed class IndexerNameAttribute : Attribute
	{
		// Token: 0x060045A0 RID: 17824 RVA: 0x00002050 File Offset: 0x00000250
		public IndexerNameAttribute(string indexerName)
		{
		}
	}
}
