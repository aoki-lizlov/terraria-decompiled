using System;

namespace System.Threading
{
	// Token: 0x02000264 RID: 612
	public static class LazyInitializer
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x0006EF4E File Offset: 0x0006D14E
		public static T EnsureInitialized<T>(ref T target) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target);
			}
			return t;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0006EF68 File Offset: 0x0006D168
		private static T EnsureInitializedCore<T>(ref T target) where T : class
		{
			try
			{
				Interlocked.CompareExchange<T>(ref target, Activator.CreateInstance<T>(), default(T));
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
			}
			return target;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0006EFB0 File Offset: 0x0006D1B0
		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
			}
			return t;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0006EFC8 File Offset: 0x0006D1C8
		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException("ValueFactory returned null.");
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0006F006 File Offset: 0x0006D206
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0006F020 File Offset: 0x0006D220
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					try
					{
						target = Activator.CreateInstance<T>();
					}
					catch (MissingMethodException)
					{
						throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
					}
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0006F094 File Offset: 0x0006D294
		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (Volatile.Read(ref initialized))
			{
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0006F0B0 File Offset: 0x0006D2B0
		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (!Volatile.Read(ref initialized))
				{
					target = valueFactory();
					Volatile.Write(ref initialized, true);
				}
			}
			return target;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0006F10C File Offset: 0x0006D30C
		public static T EnsureInitialized<T>(ref T target, ref object syncLock, Func<T> valueFactory) where T : class
		{
			T t;
			if ((t = Volatile.Read<T>(ref target)) == null)
			{
				t = LazyInitializer.EnsureInitializedCore<T>(ref target, ref syncLock, valueFactory);
			}
			return t;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006F128 File Offset: 0x0006D328
		private static T EnsureInitializedCore<T>(ref T target, ref object syncLock, Func<T> valueFactory) where T : class
		{
			object obj = LazyInitializer.EnsureLockInitialized(ref syncLock);
			lock (obj)
			{
				if (Volatile.Read<T>(ref target) == null)
				{
					Volatile.Write<T>(ref target, valueFactory());
					if (target == null)
					{
						throw new InvalidOperationException("ValueFactory returned null.");
					}
				}
			}
			return target;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0006F19C File Offset: 0x0006D39C
		private static object EnsureLockInitialized(ref object syncLock)
		{
			object obj;
			if ((obj = syncLock) == null)
			{
				obj = Interlocked.CompareExchange(ref syncLock, new object(), null) ?? syncLock;
			}
			return obj;
		}
	}
}
