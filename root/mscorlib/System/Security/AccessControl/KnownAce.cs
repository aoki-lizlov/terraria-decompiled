using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace System.Security.AccessControl
{
	// Token: 0x020004FE RID: 1278
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x0600341A RID: 13338 RVA: 0x000BF1D6 File Offset: 0x000BD3D6
		internal KnownAce(AceType type, AceFlags flags)
			: base(type, flags)
		{
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000BF1E0 File Offset: 0x000BD3E0
		internal KnownAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000BF1EA File Offset: 0x000BD3EA
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x000BF1F2 File Offset: 0x000BD3F2
		public int AccessMask
		{
			get
			{
				return this.access_mask;
			}
			set
			{
				this.access_mask = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000BF1FB File Offset: 0x000BD3FB
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000BF203 File Offset: 0x000BD403
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000BF20C File Offset: 0x000BD40C
		internal static string GetSddlAccessRights(int accessMask)
		{
			string sddlAliasRights = KnownAce.GetSddlAliasRights(accessMask);
			if (!string.IsNullOrEmpty(sddlAliasRights))
			{
				return sddlAliasRights;
			}
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x}", accessMask);
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000BF240 File Offset: 0x000BD440
		private static string GetSddlAliasRights(int accessMask)
		{
			SddlAccessRight[] array = SddlAccessRight.Decompose(accessMask);
			if (array == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SddlAccessRight sddlAccessRight in array)
			{
				stringBuilder.Append(sddlAccessRight.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002433 RID: 9267
		private int access_mask;

		// Token: 0x04002434 RID: 9268
		private SecurityIdentifier identifier;
	}
}
