using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x0200082D RID: 2093
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x060046D7 RID: 18135 RVA: 0x000E7BB4 File Offset: 0x000E5DB4
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x000E7BE1 File Offset: 0x000E5DE1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x000E7BE9 File Offset: 0x000E5DE9
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_00CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_00CE;
		}

		// Token: 0x04002D24 RID: 11556
		private CultureInfo m_startingCulture;

		// Token: 0x04002D25 RID: 11557
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x04002D26 RID: 11558
		private bool m_useParents;

		// Token: 0x0200082E RID: 2094
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__5 : IEnumerator<CultureInfo>, IDisposable, IEnumerator
		{
			// Token: 0x060046DA RID: 18138 RVA: 0x000E7BF8 File Offset: 0x000E5DF8
			[DebuggerHidden]
			public <GetEnumerator>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060046DB RID: 18139 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060046DC RID: 18140 RVA: 0x000E7C08 File Offset: 0x000E5E08
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ResourceFallbackManager resourceFallbackManager = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = false;
					currentCulture = resourceFallbackManager.m_startingCulture;
					break;
				case 1:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = true;
					goto IL_00CE;
				case 2:
					this.<>1__state = -1;
					currentCulture = currentCulture.Parent;
					if (!resourceFallbackManager.m_useParents || currentCulture.HasInvariantCultureName)
					{
						goto IL_00CE;
					}
					break;
				case 3:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (resourceFallbackManager.m_neutralResourcesCulture != null && currentCulture.Name == resourceFallbackManager.m_neutralResourcesCulture.Name)
				{
					this.<>2__current = CultureInfo.InvariantCulture;
					this.<>1__state = 1;
					return true;
				}
				this.<>2__current = currentCulture;
				this.<>1__state = 2;
				return true;
				IL_00CE:
				if (!resourceFallbackManager.m_useParents || resourceFallbackManager.m_startingCulture.HasInvariantCultureName)
				{
					return false;
				}
				if (reachedNeutralResourcesCulture)
				{
					return false;
				}
				this.<>2__current = CultureInfo.InvariantCulture;
				this.<>1__state = 3;
				return true;
			}

			// Token: 0x17000AE8 RID: 2792
			// (get) Token: 0x060046DD RID: 18141 RVA: 0x000E7D20 File Offset: 0x000E5F20
			CultureInfo IEnumerator<CultureInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060046DE RID: 18142 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000AE9 RID: 2793
			// (get) Token: 0x060046DF RID: 18143 RVA: 0x000E7D20 File Offset: 0x000E5F20
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002D27 RID: 11559
			private int <>1__state;

			// Token: 0x04002D28 RID: 11560
			private CultureInfo <>2__current;

			// Token: 0x04002D29 RID: 11561
			public ResourceFallbackManager <>4__this;

			// Token: 0x04002D2A RID: 11562
			private bool <reachedNeutralResourcesCulture>5__2;

			// Token: 0x04002D2B RID: 11563
			private CultureInfo <currentCulture>5__3;
		}
	}
}
