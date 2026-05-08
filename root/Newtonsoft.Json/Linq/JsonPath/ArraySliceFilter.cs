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
	// Token: 0x020000C1 RID: 193
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00029297 File Offset: 0x00027497
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0002929F File Offset: 0x0002749F
		public int? Start
		{
			[CompilerGenerated]
			get
			{
				return this.<Start>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Start>k__BackingField = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000292A8 File Offset: 0x000274A8
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000292B0 File Offset: 0x000274B0
		public int? End
		{
			[CompilerGenerated]
			get
			{
				return this.<End>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<End>k__BackingField = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000292B9 File Offset: 0x000274B9
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x000292C1 File Offset: 0x000274C1
		public int? Step
		{
			[CompilerGenerated]
			get
			{
				return this.<Step>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Step>k__BackingField = value;
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000292CA File Offset: 0x000274CA
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, bool errorWhenNoMatch)
		{
			if (this.Step == 0)
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken t in current)
			{
				JArray a = t as JArray;
				if (a != null)
				{
					int stepCount = this.Step ?? 1;
					int num = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : (-1));
					if (this.Start < 0)
					{
						num = a.Count + num;
					}
					if (this.End < 0)
					{
						stopIndex = a.Count + stopIndex;
					}
					num = Math.Max(num, (stepCount > 0) ? 0 : int.MinValue);
					num = Math.Min(num, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num, stopIndex, positiveStep))
					{
						int i = num;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (errorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (errorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, t.GetType().Name));
				}
				a = null;
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000292E8 File Offset: 0x000274E8
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00029260 File Offset: 0x00027460
		public ArraySliceFilter()
		{
		}

		// Token: 0x04000379 RID: 889
		[CompilerGenerated]
		private int? <Start>k__BackingField;

		// Token: 0x0400037A RID: 890
		[CompilerGenerated]
		private int? <End>k__BackingField;

		// Token: 0x0400037B RID: 891
		[CompilerGenerated]
		private int? <Step>k__BackingField;

		// Token: 0x02000167 RID: 359
		[CompilerGenerated]
		private sealed class <ExecuteFilter>d__12 : IEnumerable<JToken>, IEnumerable, IEnumerator<JToken>, IDisposable, IEnumerator
		{
			// Token: 0x06000DA1 RID: 3489 RVA: 0x00032F4C File Offset: 0x0003114C
			[DebuggerHidden]
			public <ExecuteFilter>d__12(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x00032F6C File Offset: 0x0003116C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x00032FA4 File Offset: 0x000311A4
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						i += stepCount;
					}
					else
					{
						this.<>1__state = -1;
						if (this.Step == 0)
						{
							throw new JsonException("Step cannot be zero.");
						}
						enumerator = current.GetEnumerator();
						this.<>1__state = -3;
						goto IL_0381;
					}
					IL_028A:
					if (this.IsValid(i, stopIndex, positiveStep))
					{
						this.<>2__current = a[i];
						this.<>1__state = 1;
						return true;
					}
					IL_0373:
					a = null;
					t = null;
					IL_0381:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						flag = false;
					}
					else
					{
						t = enumerator.Current;
						a = t as JArray;
						if (a != null)
						{
							stepCount = this.Step ?? 1;
							int num2 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
							stopIndex = this.End ?? ((stepCount > 0) ? a.Count : (-1));
							if (this.Start < 0)
							{
								num2 = a.Count + num2;
							}
							if (this.End < 0)
							{
								stopIndex = a.Count + stopIndex;
							}
							num2 = Math.Max(num2, (stepCount > 0) ? 0 : int.MinValue);
							num2 = Math.Min(num2, (stepCount > 0) ? a.Count : (a.Count - 1));
							stopIndex = Math.Max(stopIndex, -1);
							stopIndex = Math.Min(stopIndex, a.Count);
							positiveStep = stepCount > 0;
							if (this.IsValid(num2, stopIndex, positiveStep))
							{
								i = num2;
								goto IL_028A;
							}
							if (errorWhenNoMatch)
							{
								throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
							}
							goto IL_0373;
						}
						else
						{
							if (errorWhenNoMatch)
							{
								throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, t.GetType().Name));
							}
							goto IL_0373;
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

			// Token: 0x06000DA4 RID: 3492 RVA: 0x00033378 File Offset: 0x00031578
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170002A6 RID: 678
			// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00033394 File Offset: 0x00031594
			JToken IEnumerator<JToken>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x00024289 File Offset: 0x00022489
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002A7 RID: 679
			// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00033394 File Offset: 0x00031594
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DA8 RID: 3496 RVA: 0x0003339C File Offset: 0x0003159C
			[DebuggerHidden]
			IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
			{
				ArraySliceFilter.<ExecuteFilter>d__12 <ExecuteFilter>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<ExecuteFilter>d__ = this;
				}
				else
				{
					<ExecuteFilter>d__ = new ArraySliceFilter.<ExecuteFilter>d__12(0);
					<ExecuteFilter>d__.<>4__this = this;
				}
				<ExecuteFilter>d__.current = current;
				<ExecuteFilter>d__.errorWhenNoMatch = errorWhenNoMatch;
				return <ExecuteFilter>d__;
			}

			// Token: 0x06000DA9 RID: 3497 RVA: 0x000333FC File Offset: 0x000315FC
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator();
			}

			// Token: 0x04000539 RID: 1337
			private int <>1__state;

			// Token: 0x0400053A RID: 1338
			private JToken <>2__current;

			// Token: 0x0400053B RID: 1339
			private int <>l__initialThreadId;

			// Token: 0x0400053C RID: 1340
			public ArraySliceFilter <>4__this;

			// Token: 0x0400053D RID: 1341
			private IEnumerable<JToken> current;

			// Token: 0x0400053E RID: 1342
			public IEnumerable<JToken> <>3__current;

			// Token: 0x0400053F RID: 1343
			private JArray <a>5__1;

			// Token: 0x04000540 RID: 1344
			private int <i>5__2;

			// Token: 0x04000541 RID: 1345
			private int <stepCount>5__3;

			// Token: 0x04000542 RID: 1346
			private int <stopIndex>5__4;

			// Token: 0x04000543 RID: 1347
			private bool <positiveStep>5__5;

			// Token: 0x04000544 RID: 1348
			private bool errorWhenNoMatch;

			// Token: 0x04000545 RID: 1349
			public bool <>3__errorWhenNoMatch;

			// Token: 0x04000546 RID: 1350
			private JToken <t>5__6;

			// Token: 0x04000547 RID: 1351
			private IEnumerator<JToken> <>7__wrap1;
		}
	}
}
