using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000BF RID: 191
	internal class ArrayIndexFilter : PathFilter
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00029231 File Offset: 0x00027431
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00029239 File Offset: 0x00027439
		public int? Index
		{
			[CompilerGenerated]
			get
			{
				return this.<Index>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Index>k__BackingField = value;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00029242 File Offset: 0x00027442
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				if (this.Index != null)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, this.Index.GetValueOrDefault());
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				else if (t is JArray || t is JConstructor)
				{
					foreach (JToken jtoken in t)
					{
						yield return jtoken;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Index * not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, t.GetType().Name));
				}
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00029260 File Offset: 0x00027460
		public ArrayIndexFilter()
		{
		}

		// Token: 0x04000377 RID: 887
		[CompilerGenerated]
		private int? <Index>k__BackingField;

		// Token: 0x02000165 RID: 357
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000D8D RID: 3469 RVA: 0x00032A13 File Offset: 0x00030C13
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000D8E RID: 3470 RVA: 0x00032A34 File Offset: 0x00030C34
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				switch (num)
				{
				case -4:
				case -3:
				case 1:
				case 2:
					try
					{
						if (num == -4 || num == 2)
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
					break;
				case -2:
				case -1:
				case 0:
					break;
				default:
					return;
				}
			}

			// Token: 0x06000D8F RID: 3471 RVA: 0x00032AA8 File Offset: 0x00030CA8
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = current.GetEnumerator();
						this.<>1__state = -3;
						goto IL_0168;
					case 1:
						this.<>1__state = -3;
						goto IL_0161;
					case 2:
						this.<>1__state = -4;
						break;
					default:
						return false;
					}
					IL_0118:
					if (enumerator2.MoveNext())
					{
						JToken jtoken = enumerator2.Current;
						this.<>2__current = jtoken;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_0161:
					t = null;
					IL_0168:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						t = enumerator.Current;
						if (this.Index != null)
						{
							JToken tokenIndex = PathFilter.GetTokenIndex(t, errorWhenNoMatch, this.Index.GetValueOrDefault());
							if (tokenIndex == null)
							{
								goto IL_0161;
							}
							this.<>2__current = tokenIndex;
							this.<>1__state = 1;
							flag = true;
						}
						else
						{
							if (t is JArray || t is JConstructor)
							{
								enumerator2 = t.GetEnumerator();
								this.<>1__state = -4;
								goto IL_0118;
							}
							if (errorWhenNoMatch)
							{
								throw new JsonException("Index * not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, t.GetType().Name));
							}
							goto IL_0161;
						}
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000D90 RID: 3472 RVA: 0x00032C64 File Offset: 0x00030E64
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000D91 RID: 3473 RVA: 0x00032C80 File Offset: 0x00030E80
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170002A2 RID: 674
			// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00032C9D File Offset: 0x00030E9D
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D93 RID: 3475 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002A3 RID: 675
			// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00032C9D File Offset: 0x00030E9D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000D95 RID: 3477 RVA: 0x00032CA8 File Offset: 0x00030EA8
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				ArrayIndexFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new ArrayIndexFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				<ExecuteFilter>d__.errorWhenNoMatch = errorWhenNoMatch;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000D96 RID: 3478 RVA: 0x00032D08 File Offset: 0x00030F08
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000523 RID: 1315
			private int <>1__state;

			// Token: 0x04000524 RID: 1316
			private JToken <>2__current;

			// Token: 0x04000525 RID: 1317
			private int <>l__initialThreadId;

			// Token: 0x04000526 RID: 1318
			private IEnumerable<JToken> current;

			// Token: 0x04000527 RID: 1319
			public IEnumerable<JToken> <>3__current;

			// Token: 0x04000528 RID: 1320
			public ArrayIndexFilter <>4__this;

			// Token: 0x04000529 RID: 1321
			private bool errorWhenNoMatch;

			// Token: 0x0400052A RID: 1322
			public bool <>3__errorWhenNoMatch;

			// Token: 0x0400052B RID: 1323
			private JToken <t>5__1;

			// Token: 0x0400052C RID: 1324
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x0400052D RID: 1325
			private IEnumerator<JToken> <>7__wrap2;
		}
	}
}
