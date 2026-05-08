using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B29 RID: 2857
	public abstract class ArrayPool<T>
	{
		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060068D4 RID: 26836 RVA: 0x001635B0 File Offset: 0x001617B0
		public static ArrayPool<T> Shared
		{
			[CompilerGenerated]
			get
			{
				return ArrayPool<T>.<Shared>k__BackingField;
			}
		} = new TlsOverPerCoreLockedStacksArrayPool<T>();

		// Token: 0x060068D5 RID: 26837 RVA: 0x001635B7 File Offset: 0x001617B7
		public static ArrayPool<T> Create()
		{
			return new ConfigurableArrayPool<T>();
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x001635BE File Offset: 0x001617BE
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new ConfigurableArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x060068D7 RID: 26839
		public abstract T[] Rent(int minimumLength);

		// Token: 0x060068D8 RID: 26840
		public abstract void Return(T[] array, bool clearArray = false);

		// Token: 0x060068D9 RID: 26841 RVA: 0x000025BE File Offset: 0x000007BE
		protected ArrayPool()
		{
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x001635C7 File Offset: 0x001617C7
		// Note: this type is marked as 'beforefieldinit'.
		static ArrayPool()
		{
		}

		// Token: 0x04003C64 RID: 15460
		[CompilerGenerated]
		private static readonly ArrayPool<T> <Shared>k__BackingField;
	}
}
