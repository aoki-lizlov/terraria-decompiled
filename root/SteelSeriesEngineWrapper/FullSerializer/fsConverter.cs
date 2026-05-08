using System;

namespace FullSerializer
{
	// Token: 0x02000007 RID: 7
	public abstract class fsConverter : fsBaseConverter
	{
		// Token: 0x0600001B RID: 27
		public abstract bool CanProcess(Type type);

		// Token: 0x0600001C RID: 28 RVA: 0x00002869 File Offset: 0x00000A69
		protected fsConverter()
		{
		}
	}
}
