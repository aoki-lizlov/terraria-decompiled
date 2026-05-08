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
	// Token: 0x020000C2 RID: 194
	internal class FieldFilter : PathFilter
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x000292F6 File Offset: 0x000274F6
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x000292FE File Offset: 0x000274FE
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00029307 File Offset: 0x00027507
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				JObject o = t as JObject;
				if (o != null)
				{
					if (this.Name != null)
					{
						JToken jtoken = o[this.Name];
						if (jtoken != null)
						{
							yield return jtoken;
						}
						else if (errorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, this.Name));
						}
					}
					else
					{
						foreach (KeyValuePair<string, JToken> keyValuePair in o)
						{
							yield return keyValuePair.Value;
						}
						IEnumerator<KeyValuePair<string, JToken>> enumerator2 = null;
					}
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, this.Name ?? "*", t.GetType().Name));
				}
				o = null;
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00029260 File Offset: 0x00027460
		public FieldFilter()
		{
		}

		// Token: 0x0400037C RID: 892
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x02000168 RID: 360
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DAA RID: 3498 RVA: 0x00033404 File Offset: 0x00031604
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x00033424 File Offset: 0x00031624
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

			// Token: 0x06000DAC RID: 3500 RVA: 0x00033498 File Offset: 0x00031698
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
						goto IL_019B;
					case 1:
						this.<>1__state = -3;
						goto IL_018D;
					case 2:
						this.<>1__state = -4;
						break;
					default:
						return false;
					}
					IL_0130:
					if (enumerator2.MoveNext())
					{
						KeyValuePair<string, JToken> keyValuePair = enumerator2.Current;
						this.<>2__current = keyValuePair.Value;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_018D:
					o = null;
					t = null;
					IL_019B:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						t = enumerator.Current;
						o = t as JObject;
						if (o != null)
						{
							if (this.Name == null)
							{
								enumerator2 = o.GetEnumerator();
								this.<>1__state = -4;
								goto IL_0130;
							}
							JToken jtoken = o[this.Name];
							if (jtoken != null)
							{
								this.<>2__current = jtoken;
								this.<>1__state = 1;
								flag = true;
							}
							else
							{
								if (errorWhenNoMatch)
								{
									throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, this.Name));
								}
								goto IL_018D;
							}
						}
						else
						{
							if (errorWhenNoMatch)
							{
								throw new JsonException("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, this.Name ?? "*", t.GetType().Name));
							}
							goto IL_018D;
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

			// Token: 0x06000DAD RID: 3501 RVA: 0x00033688 File Offset: 0x00031888
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000DAE RID: 3502 RVA: 0x000336A4 File Offset: 0x000318A4
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x170002A8 RID: 680
			// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000336C1 File Offset: 0x000318C1
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DB0 RID: 3504 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002A9 RID: 681
			// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000336C1 File Offset: 0x000318C1
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DB2 RID: 3506 RVA: 0x000336CC File Offset: 0x000318CC
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				FieldFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new FieldFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				<ExecuteFilter>d__.errorWhenNoMatch = errorWhenNoMatch;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DB3 RID: 3507 RVA: 0x0003372C File Offset: 0x0003192C
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000548 RID: 1352
			private int <>1__state;

			// Token: 0x04000549 RID: 1353
			private JToken <>2__current;

			// Token: 0x0400054A RID: 1354
			private int <>l__initialThreadId;

			// Token: 0x0400054B RID: 1355
			private IEnumerable<JToken> current;

			// Token: 0x0400054C RID: 1356
			public IEnumerable<JToken> <>3__current;

			// Token: 0x0400054D RID: 1357
			public FieldFilter <>4__this;

			// Token: 0x0400054E RID: 1358
			private bool errorWhenNoMatch;

			// Token: 0x0400054F RID: 1359
			public bool <>3__errorWhenNoMatch;

			// Token: 0x04000550 RID: 1360
			private JObject <o>5__1;

			// Token: 0x04000551 RID: 1361
			private JToken <t>5__2;

			// Token: 0x04000552 RID: 1362
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x04000553 RID: 1363
			private IEnumerator<KeyValuePair<string, JToken>> <>7__wrap2;
		}
	}
}
