using System;

namespace System.Threading
{
	// Token: 0x02000257 RID: 599
	public sealed class AsyncLocal<T> : IAsyncLocal
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x000025BE File Offset: 0x000007BE
		public AsyncLocal()
		{
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006E5C1 File Offset: 0x0006C7C1
		public AsyncLocal(Action<AsyncLocalValueChangedArgs<T>> valueChangedHandler)
		{
			this.m_valueChangedHandler = valueChangedHandler;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0006E5D0 File Offset: 0x0006C7D0
		// (set) Token: 0x06001D39 RID: 7481 RVA: 0x0006E5F7 File Offset: 0x0006C7F7
		public T Value
		{
			get
			{
				object localValue = ExecutionContext.GetLocalValue(this);
				if (localValue != null)
				{
					return (T)((object)localValue);
				}
				return default(T);
			}
			set
			{
				ExecutionContext.SetLocalValue(this, value, this.m_valueChangedHandler != null);
			}
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0006E610 File Offset: 0x0006C810
		void IAsyncLocal.OnValueChanged(object previousValueObj, object currentValueObj, bool contextChanged)
		{
			T t = ((previousValueObj == null) ? default(T) : ((T)((object)previousValueObj)));
			T t2 = ((currentValueObj == null) ? default(T) : ((T)((object)currentValueObj)));
			this.m_valueChangedHandler(new AsyncLocalValueChangedArgs<T>(t, t2, contextChanged));
		}

		// Token: 0x04001909 RID: 6409
		private readonly Action<AsyncLocalValueChangedArgs<T>> m_valueChangedHandler;
	}
}
