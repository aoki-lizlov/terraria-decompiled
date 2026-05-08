using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Permissions
{
	// Token: 0x0200041C RID: 1052
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002C3F RID: 11327 RVA: 0x000A0030 File Offset: 0x0009E230
		public PrincipalPermission(PermissionState state)
		{
			this.principals = new ArrayList();
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(null, null, true);
				this.principals.Add(principalInfo);
			}
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000A006E File Offset: 0x0009E26E
		public PrincipalPermission(string name, string role)
			: this(name, role, true)
		{
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000A007C File Offset: 0x0009E27C
		public PrincipalPermission(string name, string role, bool isAuthenticated)
		{
			this.principals = new ArrayList();
			PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(name, role, isAuthenticated);
			this.principals.Add(principalInfo);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000A00B0 File Offset: 0x0009E2B0
		internal PrincipalPermission(ArrayList principals)
		{
			this.principals = (ArrayList)principals.Clone();
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000A00C9 File Offset: 0x0009E2C9
		public IPermission Copy()
		{
			return new PrincipalPermission(this.principals);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000A00D8 File Offset: 0x0009E2D8
		public void Demand()
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null)
			{
				throw new SecurityException("no Principal");
			}
			if (this.principals.Count > 0)
			{
				bool flag = false;
				foreach (object obj in this.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
					if ((principalInfo.Name == null || principalInfo.Name == currentPrincipal.Identity.Name) && (principalInfo.Role == null || currentPrincipal.IsInRole(principalInfo.Role)) && ((principalInfo.IsAuthenticated && currentPrincipal.Identity.IsAuthenticated) || !principalInfo.IsAuthenticated))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new SecurityException("Demand for principal refused.");
				}
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000A01BC File Offset: 0x0009E3BC
		public void FromXml(SecurityElement elem)
		{
			this.CheckSecurityElement(elem, "elem", 1, 1);
			this.principals.Clear();
			if (elem.Children != null)
			{
				foreach (object obj in elem.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (securityElement.Tag != "Identity")
					{
						throw new ArgumentException("not IPermission/Identity");
					}
					string text = securityElement.Attribute("ID");
					string text2 = securityElement.Attribute("Role");
					string text3 = securityElement.Attribute("Authenticated");
					bool flag = false;
					if (text3 != null)
					{
						try
						{
							flag = bool.Parse(text3);
						}
						catch
						{
						}
					}
					PrincipalPermission.PrincipalInfo principalInfo = new PrincipalPermission.PrincipalInfo(text, text2, flag);
					this.principals.Add(principalInfo);
				}
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000A02B0 File Offset: 0x0009E4B0
		public IPermission Intersect(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.Copy();
			}
			if (principalPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(PermissionState.None);
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if (principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						string text = null;
						if (principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null)
						{
							text = principalInfo.Name;
						}
						else if (principalInfo.Name == null)
						{
							text = principalInfo2.Name;
						}
						string text2 = null;
						if (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null)
						{
							text2 = principalInfo.Role;
						}
						else if (principalInfo.Role == null)
						{
							text2 = principalInfo2.Role;
						}
						if (text != null || text2 != null)
						{
							PrincipalPermission.PrincipalInfo principalInfo3 = new PrincipalPermission.PrincipalInfo(text, text2, principalInfo.IsAuthenticated);
							principalPermission2.principals.Add(principalInfo3);
						}
					}
				}
			}
			if (principalPermission2.principals.Count <= 0)
			{
				return null;
			}
			return principalPermission2;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000A0464 File Offset: 0x0009E664
		public bool IsSubsetOf(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.IsUnrestricted();
			}
			if (principalPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				bool flag = false;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000A058C File Offset: 0x0009E78C
		public bool IsUnrestricted()
		{
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				if (principalInfo.Name == null && principalInfo.Role == null && principalInfo.IsAuthenticated)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000A0600 File Offset: 0x0009E800
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000A0610 File Offset: 0x0009E810
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Permission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", 1.ToString());
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				SecurityElement securityElement2 = new SecurityElement("Identity");
				if (principalInfo.Name != null)
				{
					securityElement2.AddAttribute("ID", principalInfo.Name);
				}
				if (principalInfo.Role != null)
				{
					securityElement2.AddAttribute("Role", principalInfo.Role);
				}
				if (principalInfo.IsAuthenticated)
				{
					securityElement2.AddAttribute("Authenticated", "true");
				}
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000A0724 File Offset: 0x0009E924
		public IPermission Union(IPermission other)
		{
			PrincipalPermission principalPermission = this.Cast(other);
			if (principalPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(this.principals);
			foreach (object obj in principalPermission.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				principalPermission2.principals.Add(principalInfo);
			}
			return principalPermission2;
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000A07BC File Offset: 0x0009E9BC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PrincipalPermission principalPermission = obj as PrincipalPermission;
			if (principalPermission == null)
			{
				return false;
			}
			if (this.principals.Count != principalPermission.principals.Count)
			{
				return false;
			}
			foreach (object obj2 in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj2;
				bool flag = false;
				foreach (object obj3 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj3;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x00093238 File Offset: 0x00091438
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x00048AA1 File Offset: 0x00046CA1
		int IBuiltInPermission.GetTokenIndex()
		{
			return 8;
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000A08E8 File Offset: 0x0009EAE8
		private PrincipalPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PrincipalPermission principalPermission = target as PrincipalPermission;
			if (principalPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PrincipalPermission));
			}
			return principalPermission;
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000A0908 File Offset: 0x0009EB08
		private bool IsEmpty()
		{
			return this.principals.Count == 0;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000A0918 File Offset: 0x0009EB18
		internal int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "Permission")
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}"), se.Tag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}']."), num, minimumVersion, maximumVersion), parameterName);
			}
			return num;
		}

		// Token: 0x04001F37 RID: 7991
		private const int version = 1;

		// Token: 0x04001F38 RID: 7992
		private ArrayList principals;

		// Token: 0x0200041D RID: 1053
		internal class PrincipalInfo
		{
			// Token: 0x06002C52 RID: 11346 RVA: 0x000A09D4 File Offset: 0x0009EBD4
			public PrincipalInfo(string name, string role, bool isAuthenticated)
			{
				this._name = name;
				this._role = role;
				this._isAuthenticated = isAuthenticated;
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x06002C53 RID: 11347 RVA: 0x000A09F1 File Offset: 0x0009EBF1
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000A09F9 File Offset: 0x0009EBF9
			public string Role
			{
				get
				{
					return this._role;
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06002C55 RID: 11349 RVA: 0x000A0A01 File Offset: 0x0009EC01
			public bool IsAuthenticated
			{
				get
				{
					return this._isAuthenticated;
				}
			}

			// Token: 0x04001F39 RID: 7993
			private string _name;

			// Token: 0x04001F3A RID: 7994
			private string _role;

			// Token: 0x04001F3B RID: 7995
			private bool _isAuthenticated;
		}
	}
}
