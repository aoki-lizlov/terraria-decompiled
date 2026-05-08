using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200040B RID: 1035
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002BA6 RID: 11174 RVA: 0x0009E1FA File Offset: 0x0009C3FA
		public GacIdentityPermission()
		{
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0009ED64 File Offset: 0x0009CF64
		public GacIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x0009A19E File Offset: 0x0009839E
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x0009ED74 File Offset: 0x0009CF74
		public override IPermission Intersect(IPermission target)
		{
			if (this.Cast(target) == null)
			{
				return null;
			}
			return this.Copy();
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0009ED87 File Offset: 0x0009CF87
		public override bool IsSubsetOf(IPermission target)
		{
			return this.Cast(target) != null;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x0009ED93 File Offset: 0x0009CF93
		public override IPermission Union(IPermission target)
		{
			this.Cast(target);
			return this.Copy();
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x0009EDA3 File Offset: 0x0009CFA3
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0009EDB3 File Offset: 0x0009CFB3
		public override SecurityElement ToXml()
		{
			return base.Element(1);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x0006BAF6 File Offset: 0x00069CF6
		int IBuiltInPermission.GetTokenIndex()
		{
			return 15;
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x0009EDBC File Offset: 0x0009CFBC
		private GacIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			GacIdentityPermission gacIdentityPermission = target as GacIdentityPermission;
			if (gacIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(GacIdentityPermission));
			}
			return gacIdentityPermission;
		}

		// Token: 0x04001EF7 RID: 7927
		private const int version = 1;
	}
}
