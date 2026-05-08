using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C0 RID: 192
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00029268 File Offset: 0x00027468
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00029270 File Offset: 0x00027470
		public List<int> Indexes
		{
			[CompilerGenerated]
			get
			{
				return this.<Indexes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Indexes>k__BackingField = value;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00029279 File Offset: 0x00027479
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				foreach (int num in this.Indexes)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, num);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00029260 File Offset: 0x00027460
		public ArrayMultipleIndexFilter()
		{
		}

		// Token: 0x04000378 RID: 888
		[CompilerGenerated]
		private List<int> <Indexes>k__BackingField;

		// Token: 0x02000166 RID: 358
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D97 RID: 3479 RVA: 0x00032D10 File Offset: 0x00030F10
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D98 RID: 3480 RVA: 0x00032D30 File Offset: 0x00030F30
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

			// Token: 0x06000D99 RID: 3481 RVA: 0x00032D8C File Offset: 0x00030F8C
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
						goto IL_00D0;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_00AA:
					while (enumerator2.MoveNext())
					{
						int num2 = enumerator2.Current;
						JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, num2);
						if (tokenIndex != null)
						{
							this.<>2__current = tokenIndex;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally2();
					enumerator2 = default(List<int>.Enumerator);
					t = null;
					IL_00D0:
					if (enumerator.MoveNext())
					{
						t = enumerator.Current;
						enumerator2 = this.Indexes.GetEnumerator();
						this.<>1__state = -4;
						goto IL_00AA;
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

			// Token: 0x06000D9A RID: 3482 RVA: 0x00032EA4 File Offset: 0x000310A4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000D9B RID: 3483 RVA: 0x00032EC0 File Offset: 0x000310C0
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				enumerator2.Dispose();
			}

			// Token: 0x170002A4 RID: 676
			// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00032EDB File Offset: 0x000310DB
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002A5 RID: 677
			// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00032EDB File Offset: 0x000310DB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D9F RID: 3487 RVA: 0x00032EE4 File Offset: 0x000310E4
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				ArrayMultipleIndexFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new ArrayMultipleIndexFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				<ExecuteFilter>d__.errorWhenNoMatch = errorWhenNoMatch;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x00032F44 File Offset: 0x00031144
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x0400052E RID: 1326
			private int <>1__state;

			// Token: 0x0400052F RID: 1327
			private JToken <>2__current;

			// Token: 0x04000530 RID: 1328
			private int <>l__initialThreadId;

			// Token: 0x04000531 RID: 1329
			private IEnumerable<JToken> current;

			// Token: 0x04000532 RID: 1330
			public IEnumerable<JToken> <>3__current;

			// Token: 0x04000533 RID: 1331
			public ArrayMultipleIndexFilter <>4__this;

			// Token: 0x04000534 RID: 1332
			private JToken <t>5__1;

			// Token: 0x04000535 RID: 1333
			private bool errorWhenNoMatch;

			// Token: 0x04000536 RID: 1334
			public bool <>3__errorWhenNoMatch;

			// Token: 0x04000537 RID: 1335
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x04000538 RID: 1336
			private List<int>.Enumerator <>7__wrap2;
		}
	}
}
