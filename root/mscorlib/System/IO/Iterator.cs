using System;
using System.Collections;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200095B RID: 2395
	internal abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
	{
		// Token: 0x060056C9 RID: 22217 RVA: 0x00125062 File Offset: 0x00123262
		public Iterator()
		{
			this._threadId = Environment.CurrentManagedThreadId;
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060056CA RID: 22218 RVA: 0x00125075 File Offset: 0x00123275
		public TSource Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x060056CB RID: 22219
		protected abstract Iterator<TSource> Clone();

		// Token: 0x060056CC RID: 22220 RVA: 0x0012507D File Offset: 0x0012327D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x0012508C File Offset: 0x0012328C
		protected virtual void Dispose(bool disposing)
		{
			this.current = default(TSource);
			this.state = -1;
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x001250A1 File Offset: 0x001232A1
		public IEnumerator<TSource> GetEnumerator()
		{
			if (this.state == 0 && this._threadId == Environment.CurrentManagedThreadId)
			{
				this.state = 1;
				return this;
			}
			Iterator<TSource> iterator = this.Clone();
			iterator.state = 1;
			return iterator;
		}

		// Token: 0x060056CF RID: 22223
		public abstract bool MoveNext();

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060056D0 RID: 22224 RVA: 0x001250CE File Offset: 0x001232CE
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x001250DB File Offset: 0x001232DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x00047E00 File Offset: 0x00046000
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003456 RID: 13398
		private int _threadId;

		// Token: 0x04003457 RID: 13399
		internal int state;

		// Token: 0x04003458 RID: 13400
		internal TSource current;
	}
}
