using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CA RID: 202
	internal class QueryFilter : PathFilter
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002A83C File Offset: 0x00028A3C
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0002A844 File Offset: 0x00028A44
		public QueryExpression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expression>k__BackingField = value;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002A84D File Offset: 0x00028A4D
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken jtoken in current)
			{
				foreach (JToken jtoken2 in jtoken)
				{
					if (this.Expression.IsMatch(root, jtoken2))
					{
						yield return jtoken2;
					}
				}
				IEnumerator<JToken> enumerator2 = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00029260 File Offset: 0x00027460
		public QueryFilter()
		{
		}

		// Token: 0x04000390 RID: 912
		[CompilerGenerated]
		private QueryExpression <Expression>k__BackingField;

		// Token: 0x0200016B RID: 363
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DC1 RID: 3521 RVA: 0x00033A58 File Offset: 0x00031C58
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DC2 RID: 3522 RVA: 0x00033A78 File Offset: 0x00031C78
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -4 || num == -3 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000DC3 RID: 3523 RVA: 0x00033AD4 File Offset: 0x00031CD4
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = current.GetEnumerator();
						this.<>1__state = -3;
						goto IL_00B5;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_009B:
					while (enumerator2.MoveNext())
					{
						JToken jtoken = enumerator2.Current;
						if (this.Expression.IsMatch(root, jtoken))
						{
							this.<>2__current = jtoken;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_00B5:
					if (enumerator.MoveNext())
					{
						JToken jtoken2 = enumerator.Current;
						enumerator2 = jtoken2.GetEnumerator();
						this.<>1__state = -4;
						goto IL_009B;
					}
					this.<>m__Finally1();
					enumerator = null;
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x00033BD0 File Offset: 0x00031DD0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x00033BEC File Offset: 0x00031DEC
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170002AC RID: 684
			// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00033C09 File Offset: 0x00031E09
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DC7 RID: 3527 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002AD RID: 685
			// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00033C09 File Offset: 0x00031E09
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x00033C14 File Offset: 0x00031E14
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				QueryFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new QueryFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.root = root;
				<ExecuteFilter>d__.current = current;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DCA RID: 3530 RVA: 0x00033C74 File Offset: 0x00031E74
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000563 RID: 1379
			private int <>1__state;

			// Token: 0x04000564 RID: 1380
			private JToken <>2__current;

			// Token: 0x04000565 RID: 1381
			private int <>l__initialThreadId;

			// Token: 0x04000566 RID: 1382
			private IEnumerable<JToken> current;

			// Token: 0x04000567 RID: 1383
			public IEnumerable<JToken> <>3__current;

			// Token: 0x04000568 RID: 1384
			public QueryFilter <>4__this;

			// Token: 0x04000569 RID: 1385
			private JToken root;

			// Token: 0x0400056A RID: 1386
			public JToken <>3__root;

			// Token: 0x0400056B RID: 1387
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x0400056C RID: 1388
			private IEnumerator<JToken> <>7__wrap2;
		}
	}
}
