using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CC RID: 204
	internal class ScanFilter : PathFilter
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0002A877 File Offset: 0x00028A77
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0002A87F File Offset: 0x00028A7F
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

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002A888 File Offset: 0x00028A88
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken c in current)
			{
				if (this.Name == null)
				{
					yield return c;
				}
				JToken value = c;
				JToken jtoken = c;
				for (;;)
				{
					if (jtoken != null && jtoken.HasValues)
					{
						value = jtoken.First;
					}
					else
					{
						while (value != null && value != c && value == value.Parent.Last)
						{
							value = value.Parent;
						}
						if (value == null || value == c)
						{
							break;
						}
						value = value.Next;
					}
					JProperty jproperty = value as JProperty;
					if (jproperty != null)
					{
						if (jproperty.Name == this.Name)
						{
							yield return jproperty.Value;
						}
					}
					else if (this.Name == null)
					{
						yield return value;
					}
					jtoken = value as JContainer;
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00029260 File Offset: 0x00027460
		public ScanFilter()
		{
		}

		// Token: 0x04000392 RID: 914
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0200016C RID: 364
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DCB RID: 3531 RVA: 0x00033C7C File Offset: 0x00031E7C
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DCC RID: 3532 RVA: 0x00033C9C File Offset: 0x00031E9C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -3:
				case 1:
				case 2:
				case 3:
					try
					{
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

			// Token: 0x06000DCD RID: 3533 RVA: 0x00033CF0 File Offset: 0x00031EF0
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
						goto IL_01B4;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -3;
						goto IL_0195;
					case 3:
						this.<>1__state = -3;
						goto IL_0195;
					default:
						return false;
					}
					IL_0089:
					value = c;
					JToken jtoken = c;
					IL_009C:
					if (jtoken != null && jtoken.HasValues)
					{
						value = jtoken.First;
					}
					else
					{
						while (value != null && value != c && value == value.Parent.Last)
						{
							value = value.Parent;
						}
						if (value == null || value == c)
						{
							value = null;
							c = null;
							goto IL_01B4;
						}
						value = value.Next;
					}
					JProperty jproperty = value as JProperty;
					if (jproperty != null)
					{
						if (jproperty.Name == this.Name)
						{
							this.<>2__current = jproperty.Value;
							this.<>1__state = 2;
							return true;
						}
					}
					else if (this.Name == null)
					{
						this.<>2__current = value;
						this.<>1__state = 3;
						return true;
					}
					IL_0195:
					jtoken = value as JContainer;
					goto IL_009C;
					IL_01B4:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						c = enumerator.Current;
						if (this.Name != null)
						{
							goto IL_0089;
						}
						this.<>2__current = c;
						this.<>1__state = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x00033EF8 File Offset: 0x000320F8
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170002AE RID: 686
			// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00033F14 File Offset: 0x00032114
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002AF RID: 687
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00033F14 File Offset: 0x00032114
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x00033F1C File Offset: 0x0003211C
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				ScanFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new ScanFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x00033F70 File Offset: 0x00032170
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x0400056D RID: 1389
			private int <>1__state;

			// Token: 0x0400056E RID: 1390
			private JToken <>2__current;

			// Token: 0x0400056F RID: 1391
			private int <>l__initialThreadId;

			// Token: 0x04000570 RID: 1392
			private IEnumerable<JToken> current;

			// Token: 0x04000571 RID: 1393
			public IEnumerable<JToken> <>3__current;

			// Token: 0x04000572 RID: 1394
			public ScanFilter <>4__this;

			// Token: 0x04000573 RID: 1395
			private JToken <c>5__1;

			// Token: 0x04000574 RID: 1396
			private JToken <value>5__2;

			// Token: 0x04000575 RID: 1397
			private IEnumerator<JToken> <>7__wrap1;
		}
	}
}
