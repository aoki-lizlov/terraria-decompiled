using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CsvHelper.Configuration
{
	// Token: 0x0200003D RID: 61
	public class CsvPropertyNameCollection : IEnumerable<string>, IEnumerable
	{
		// Token: 0x1700006B RID: 107
		public string this[int index]
		{
			get
			{
				return this.Prefix + this.names[index];
			}
			set
			{
				this.names[index] = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007B40 File Offset: 0x00005D40
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00007B48 File Offset: 0x00005D48
		public string Prefix
		{
			[CompilerGenerated]
			get
			{
				return this.<Prefix>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Prefix>k__BackingField = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007B51 File Offset: 0x00005D51
		public List<string> Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007B59 File Offset: 0x00005D59
		public int Count
		{
			get
			{
				return this.names.Count;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007B66 File Offset: 0x00005D66
		public void Add(string name)
		{
			this.names.Add(name);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007B74 File Offset: 0x00005D74
		public void Clear()
		{
			this.names.Clear();
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007B81 File Offset: 0x00005D81
		public void AddRange(IEnumerable<string> names)
		{
			this.names.AddRange(names);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007B8F File Offset: 0x00005D8F
		public IEnumerator<string> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.names.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007B9E File Offset: 0x00005D9E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public CsvPropertyNameCollection()
		{
		}

		// Token: 0x04000071 RID: 113
		private readonly List<string> names = new List<string>();

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private string <Prefix>k__BackingField;

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__15 : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0000839B File Offset: 0x0000659B
			[DebuggerHidden]
			public <GetEnumerator>d__15(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000273 RID: 627 RVA: 0x000069AC File Offset: 0x00004BAC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000274 RID: 628 RVA: 0x000083AC File Offset: 0x000065AC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= this.names.Count)
				{
					return false;
				}
				this.<>2__current = this[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000275 RID: 629 RVA: 0x00008429 File Offset: 0x00006629
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00007E58 File Offset: 0x00006058
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000277 RID: 631 RVA: 0x00008429 File Offset: 0x00006629
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000099 RID: 153
			private int <>1__state;

			// Token: 0x0400009A RID: 154
			private string <>2__current;

			// Token: 0x0400009B RID: 155
			public CsvPropertyNameCollection <>4__this;

			// Token: 0x0400009C RID: 156
			private int <i>5__1;
		}
	}
}
