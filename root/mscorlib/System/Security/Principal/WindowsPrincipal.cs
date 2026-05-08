using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Mono;

namespace System.Security.Principal
{
	// Token: 0x020004BF RID: 1215
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : ClaimsPrincipal
	{
		// Token: 0x0600321E RID: 12830 RVA: 0x000B94B7 File Offset: 0x000B76B7
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000B94D4 File Offset: 0x000B76D4
		public override IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000B94DC File Offset: 0x000B76DC
		public virtual bool IsInRole(int rid)
		{
			if (Environment.IsUnix)
			{
				return WindowsPrincipal.IsMemberOfGroupId(this.Token, (IntPtr)rid);
			}
			string text;
			switch (rid)
			{
			case 544:
				text = "BUILTIN\\Administrators";
				break;
			case 545:
				text = "BUILTIN\\Users";
				break;
			case 546:
				text = "BUILTIN\\Guests";
				break;
			case 547:
				text = "BUILTIN\\Power Users";
				break;
			case 548:
				text = "BUILTIN\\Account Operators";
				break;
			case 549:
				text = "BUILTIN\\System Operators";
				break;
			case 550:
				text = "BUILTIN\\Print Operators";
				break;
			case 551:
				text = "BUILTIN\\Backup Operators";
				break;
			case 552:
				text = "BUILTIN\\Replicator";
				break;
			default:
				return false;
			}
			return this.IsInRole(text);
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000B9588 File Offset: 0x000B7788
		public override bool IsInRole(string role)
		{
			if (role == null)
			{
				return false;
			}
			if (Environment.IsUnix)
			{
				using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(role))
				{
					return WindowsPrincipal.IsMemberOfGroupName(this.Token, safeStringMarshal.Value);
				}
			}
			if (this.m_roles == null)
			{
				this.m_roles = WindowsIdentity._GetRoles(this.Token);
			}
			role = role.ToUpperInvariant();
			foreach (string text in this.m_roles)
			{
				if (text != null && role == text.ToUpperInvariant())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000B9630 File Offset: 0x000B7830
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (!Environment.IsUnix)
			{
				return this.IsInRole((int)role);
			}
			if (role == WindowsBuiltInRole.Administrator)
			{
				string text = "root";
				return this.IsInRole(text);
			}
			return false;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000B9667 File Offset: 0x000B7867
		private IntPtr Token
		{
			get
			{
				return this._identity.Token;
			}
		}

		// Token: 0x06003225 RID: 12837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupId(IntPtr user, IntPtr group);

		// Token: 0x06003226 RID: 12838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupName(IntPtr user, IntPtr group);

		// Token: 0x040022CB RID: 8907
		private WindowsIdentity _identity;

		// Token: 0x040022CC RID: 8908
		private string[] m_roles;
	}
}
