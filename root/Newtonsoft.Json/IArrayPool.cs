using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000005 RID: 5
	public interface IArrayPool<T>
	{
		// Token: 0x06000001 RID: 1
		T[] Rent(int minimumLength);

		// Token: 0x06000002 RID: 2
		void Return(T[] array);
	}
}
