using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C3 RID: 195
	internal class FieldMultipleFilter : PathFilter
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00029325 File Offset: 0x00027525
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0002932D File Offset: 0x0002752D
		public List<string> Names
		{
			[CompilerGenerated]
			get
			{
				return this.<Names>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Names>k__BackingField = value;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00029336 File Offset: 0x00027536
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken t in current)
			{
				JObject o = t as JObject;
				if (o != null)
				{
					foreach (string name in this.Names)
					{
						JToken jtoken = o[name];
						if (jtoken != null)
						{
							yield return jtoken;
						}
						if (errorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, name));
						}
						name = null;
					}
					List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Properties {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", Enumerable.Select<string, string>(this.Names, (string n) => "'" + n + "'")), t.GetType().Name));
				}
				o = null;
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00029260 File Offset: 0x00027460
		public FieldMultipleFilter()
		{
		}

		// Token: 0x0400037D RID: 893
		[CompilerGenerated]
		private List<string> <Names>k__BackingField;

		// Token: 0x02000169 RID: 361
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000DB4 RID: 3508 RVA: 0x00033734 File Offset: 0x00031934
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000DB5 RID: 3509 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000DB6 RID: 3510 RVA: 0x00033740 File Offset: 0x00031940
			internal string <ExecuteFilter>b__4_0(string n)
			{
				return "'" + n + "'";
			}

			// Token: 0x04000554 RID: 1364
			public static readonly FieldMultipleFilter.<>c <>9 = new FieldMultipleFilter.<>c();

			// Token: 0x04000555 RID: 1365
			public static Func<string, string> <>9__4_0;
		}

		// Token: 0x0200016A RID: 362
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DB7 RID: 3511 RVA: 0x00033752 File Offset: 0x00031952
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DB8 RID: 3512 RVA: 0x00033774 File Offset: 0x00031974
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

			// Token: 0x06000DB9 RID: 3513 RVA: 0x000337D0 File Offset: 0x000319D0
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
						goto IL_018C;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_00CD:
					if (errorWhenNoMatch)
					{
						throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, name));
					}
					name = null;
					IL_00F7:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = default(List<string>.Enumerator);
					}
					else
					{
						name = enumerator2.Current;
						JToken jtoken = o[name];
						if (jtoken != null)
						{
							this.<>2__current = jtoken;
							this.<>1__state = 1;
							return true;
						}
						goto IL_00CD;
					}
					IL_017E:
					o = null;
					t = null;
					IL_018C:
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
							enumerator2 = this.Names.GetEnumerator();
							this.<>1__state = -4;
							goto IL_00F7;
						}
						if (errorWhenNoMatch)
						{
							throw new JsonException("Properties {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", Enumerable.Select<string, string>(this.Names, (string n) => "'" + n + "'")), t.GetType().Name));
						}
						goto IL_017E;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000DBA RID: 3514 RVA: 0x000339B0 File Offset: 0x00031BB0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000DBB RID: 3515 RVA: 0x000339CC File Offset: 0x00031BCC
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				enumerator2.Dispose();
			}

			// Token: 0x170002AA RID: 682
			// (get) Token: 0x06000DBC RID: 3516 RVA: 0x000339E7 File Offset: 0x00031BE7
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DBD RID: 3517 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002AB RID: 683
			// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000339E7 File Offset: 0x00031BE7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DBF RID: 3519 RVA: 0x000339F0 File Offset: 0x00031BF0
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				FieldMultipleFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new FieldMultipleFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				<ExecuteFilter>d__.errorWhenNoMatch = errorWhenNoMatch;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x00033A50 File Offset: 0x00031C50
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000556 RID: 1366
			private int <>1__state;

			// Token: 0x04000557 RID: 1367
			private JToken <>2__current;

			// Token: 0x04000558 RID: 1368
			private int <>l__initialThreadId;

			// Token: 0x04000559 RID: 1369
			private IEnumerable<JToken> current;

			// Token: 0x0400055A RID: 1370
			public IEnumerable<JToken> <>3__current;

			// Token: 0x0400055B RID: 1371
			public FieldMultipleFilter <>4__this;

			// Token: 0x0400055C RID: 1372
			private JObject <o>5__1;

			// Token: 0x0400055D RID: 1373
			private bool errorWhenNoMatch;

			// Token: 0x0400055E RID: 1374
			public bool <>3__errorWhenNoMatch;

			// Token: 0x0400055F RID: 1375
			private string <name>5__2;

			// Token: 0x04000560 RID: 1376
			private JToken <t>5__3;

			// Token: 0x04000561 RID: 1377
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x04000562 RID: 1378
			private List<string>.Enumerator <>7__wrap2;
		}
	}
}
