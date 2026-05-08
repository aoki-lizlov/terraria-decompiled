using System;
using System.Collections.Generic;

namespace ReLogic.Utilities
{
	// Token: 0x02000006 RID: 6
	internal class TypeInstanceCollection<BaseType> : IDisposable where BaseType : class
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000024A0 File Offset: 0x000006A0
		public void Register<T>(T instance) where T : BaseType
		{
			this._services.Add(typeof(T), (BaseType)((object)instance));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024C4 File Offset: 0x000006C4
		public T Get<T>() where T : BaseType
		{
			BaseType baseType;
			if (this._services.TryGetValue(typeof(T), ref baseType))
			{
				return (T)((object)baseType);
			}
			return default(T);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024FF File Offset: 0x000006FF
		public void Remove<T>() where T : BaseType
		{
			this._services.Remove(typeof(T));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002517 File Offset: 0x00000717
		public bool Has<T>() where T : BaseType
		{
			return this._services.ContainsKey(typeof(T));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000252E File Offset: 0x0000072E
		public bool Has(Type type)
		{
			return this._services.ContainsKey(type);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000253C File Offset: 0x0000073C
		public void IfHas<T>(Action<T> callback) where T : BaseType
		{
			BaseType baseType;
			if (this._services.TryGetValue(typeof(T), ref baseType))
			{
				callback.Invoke((T)((object)baseType));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002574 File Offset: 0x00000774
		public U IfHas<T, U>(Func<T, U> callback) where T : BaseType
		{
			BaseType baseType;
			if (this._services.TryGetValue(typeof(T), ref baseType))
			{
				return callback.Invoke((T)((object)baseType));
			}
			return default(U);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025B8 File Offset: 0x000007B8
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					foreach (BaseType baseType in this._services.Values)
					{
						IDisposable disposable = baseType as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					this._services.Clear();
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000263C File Offset: 0x0000083C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002645 File Offset: 0x00000845
		public TypeInstanceCollection()
		{
		}

		// Token: 0x04000006 RID: 6
		private Dictionary<Type, BaseType> _services = new Dictionary<Type, BaseType>();

		// Token: 0x04000007 RID: 7
		private bool _disposedValue;
	}
}
