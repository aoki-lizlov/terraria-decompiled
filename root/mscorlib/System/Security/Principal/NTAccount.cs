using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004B7 RID: 1207
	[ComVisible(false)]
	public sealed class NTAccount : IdentityReference
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x000B786C File Offset: 0x000B5A6C
		public NTAccount(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this._value = name;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000B78A8 File Offset: 0x000B5AA8
		public NTAccount(string domainName, string accountName)
		{
			if (accountName == null)
			{
				throw new ArgumentNullException("accountName");
			}
			if (accountName.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "accountName");
			}
			if (domainName == null)
			{
				this._value = accountName;
				return;
			}
			this._value = domainName + "\\" + accountName;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000B7903 File Offset: 0x000B5B03
		public override string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000B790C File Offset: 0x000B5B0C
		public override bool Equals(object o)
		{
			NTAccount ntaccount = o as NTAccount;
			return !(ntaccount == null) && ntaccount.Value == this.Value;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000B793C File Offset: 0x000B5B3C
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000B7949 File Offset: 0x000B5B49
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(NTAccount) || targetType == typeof(SecurityIdentifier);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000B7974 File Offset: 0x000B5B74
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000B797C File Offset: 0x000B5B7C
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(NTAccount))
			{
				return this;
			}
			if (!(targetType == typeof(SecurityIdentifier)))
			{
				throw new ArgumentException("Unknown type", "targetType");
			}
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupByName(this.Value);
			if (wellKnownAccount == null || wellKnownAccount.Sid == null)
			{
				throw new IdentityNotMappedException("Cannot map account name: " + this.Value);
			}
			return new SecurityIdentifier(wellKnownAccount.Sid);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000B76D8 File Offset: 0x000B58D8
		public static bool operator ==(NTAccount left, NTAccount right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000B76F8 File Offset: 0x000B58F8
		public static bool operator !=(NTAccount left, NTAccount right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}

		// Token: 0x04002246 RID: 8774
		private string _value;
	}
}
