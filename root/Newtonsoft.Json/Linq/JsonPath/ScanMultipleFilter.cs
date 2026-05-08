using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CD RID: 205
	internal class ScanMultipleFilter : PathFilter
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0002A89F File Offset: 0x00028A9F
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0002A8A7 File Offset: 0x00028AA7
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

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			foreach (JToken c in current)
			{
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
					JProperty e = value as JProperty;
					if (e != null)
					{
						foreach (string text in this.Names)
						{
							if (e.Name == text)
							{
								yield return e.Value;
							}
						}
						List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
					}
					jtoken = value as JContainer;
					e = null;
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00029260 File Offset: 0x00027460
		public ScanMultipleFilter()
		{
		}

		// Token: 0x04000393 RID: 915
		[CompilerGenerated]
		private List<string> <Names>k__BackingField;

		// Token: 0x0200016D RID: 365
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__4 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DD4 RID: 3540 RVA: 0x00033F78 File Offset: 0x00032178
			[DebuggerHidden]
			public <ExecuteFilter>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DD5 RID: 3541 RVA: 0x00033F98 File Offset: 0x00032198
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

			// Token: 0x06000DD6 RID: 3542 RVA: 0x00033FF4 File Offset: 0x000321F4
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
						goto IL_01AA;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_0165:
					while (enumerator2.MoveNext())
					{
						string text = enumerator2.Current;
						if (e.Name == text)
						{
							this.<>2__current = e.Value;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally2();
					enumerator2 = default(List<string>.Enumerator);
					goto IL_0184;
					IL_0061:
					JToken jtoken;
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
							goto IL_01AA;
						}
						value = value.Next;
					}
					e = value as JProperty;
					if (e != null)
					{
						enumerator2 = this.Names.GetEnumerator();
						this.<>1__state = -4;
						goto IL_0165;
					}
					IL_0184:
					jtoken = value as JContainer;
					e = null;
					goto IL_0061;
					IL_01AA:
					if (enumerator.MoveNext())
					{
						c = enumerator.Current;
						value = c;
						jtoken = c;
						goto IL_0061;
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

			// Token: 0x06000DD7 RID: 3543 RVA: 0x000341F0 File Offset: 0x000323F0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06000DD8 RID: 3544 RVA: 0x0003420C File Offset: 0x0003240C
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				enumerator2.Dispose();
			}

			// Token: 0x170002B0 RID: 688
			// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00034227 File Offset: 0x00032427
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DDA RID: 3546 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002B1 RID: 689
			// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00034227 File Offset: 0x00032427
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x00034230 File Offset: 0x00032430
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				ScanMultipleFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new ScanMultipleFilter.<ExecuteFilter>d__4(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DDD RID: 3549 RVA: 0x00034284 File Offset: 0x00032484
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000576 RID: 1398
			private int <>1__state;

			// Token: 0x04000577 RID: 1399
			private JToken <>2__current;

			// Token: 0x04000578 RID: 1400
			private int <>l__initialThreadId;

			// Token: 0x04000579 RID: 1401
			private IEnumerable<JToken> current;

			// Token: 0x0400057A RID: 1402
			public IEnumerable<JToken> <>3__current;

			// Token: 0x0400057B RID: 1403
			private JToken <value>5__1;

			// Token: 0x0400057C RID: 1404
			private JToken <c>5__2;

			// Token: 0x0400057D RID: 1405
			public ScanMultipleFilter <>4__this;

			// Token: 0x0400057E RID: 1406
			private JProperty <e>5__3;

			// Token: 0x0400057F RID: 1407
			private IEnumerator<JToken> <>7__wrap1;

			// Token: 0x04000580 RID: 1408
			private List<string>.Enumerator <>7__wrap2;
		}
	}
}
