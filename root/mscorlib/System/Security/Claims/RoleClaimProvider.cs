using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x020004CA RID: 1226
	[ComVisible(false)]
	internal class RoleClaimProvider
	{
		// Token: 0x060032AF RID: 12975 RVA: 0x000BBECB File Offset: 0x000BA0CB
		public RoleClaimProvider(string issuer, string[] roles, ClaimsIdentity subject)
		{
			this.m_issuer = issuer;
			this.m_roles = roles;
			this.m_subject = subject;
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000BBEE8 File Offset: 0x000BA0E8
		public IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_roles.Length; i = num + 1)
				{
					if (this.m_roles[i] != null)
					{
						yield return new Claim(this.m_subject.RoleClaimType, this.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", this.m_issuer, this.m_issuer, this.m_subject);
					}
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x04002375 RID: 9077
		private string m_issuer;

		// Token: 0x04002376 RID: 9078
		private string[] m_roles;

		// Token: 0x04002377 RID: 9079
		private ClaimsIdentity m_subject;

		// Token: 0x020004CB RID: 1227
		[CompilerGenerated]
		private sealed class <get_Claims>d__5 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x060032B1 RID: 12977 RVA: 0x000BBEF8 File Offset: 0x000BA0F8
			[DebuggerHidden]
			public <get_Claims>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060032B2 RID: 12978 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060032B3 RID: 12979 RVA: 0x000BBF14 File Offset: 0x000BA114
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				RoleClaimProvider roleClaimProvider = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					i = 0;
					goto IL_0090;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_0080:
				int num2 = i;
				i = num2 + 1;
				IL_0090:
				if (i >= roleClaimProvider.m_roles.Length)
				{
					return false;
				}
				if (roleClaimProvider.m_roles[i] != null)
				{
					this.<>2__current = new Claim(roleClaimProvider.m_subject.RoleClaimType, roleClaimProvider.m_roles[i], "http://www.w3.org/2001/XMLSchema#string", roleClaimProvider.m_issuer, roleClaimProvider.m_issuer, roleClaimProvider.m_subject);
					this.<>1__state = 1;
					return true;
				}
				goto IL_0080;
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x060032B4 RID: 12980 RVA: 0x000BBFC2 File Offset: 0x000BA1C2
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060032B5 RID: 12981 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x060032B6 RID: 12982 RVA: 0x000BBFC2 File Offset: 0x000BA1C2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060032B7 RID: 12983 RVA: 0x000BBFCC File Offset: 0x000BA1CC
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				RoleClaimProvider.<get_Claims>d__5 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new RoleClaimProvider.<get_Claims>d__5(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x060032B8 RID: 12984 RVA: 0x000BC00F File Offset: 0x000BA20F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x04002378 RID: 9080
			private int <>1__state;

			// Token: 0x04002379 RID: 9081
			private Claim <>2__current;

			// Token: 0x0400237A RID: 9082
			private int <>l__initialThreadId;

			// Token: 0x0400237B RID: 9083
			public RoleClaimProvider <>4__this;

			// Token: 0x0400237C RID: 9084
			private int <i>5__2;
		}
	}
}
