using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003AD RID: 941
	[ComVisible(true)]
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		// Token: 0x06002841 RID: 10305 RVA: 0x000025BE File Offset: 0x000007BE
		protected CodeAccessPermission()
		{
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000931C8 File Offset: 0x000913C8
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Assert()
		{
			new PermissionSet(this).Assert();
		}

		// Token: 0x06002843 RID: 10307
		public abstract IPermission Copy();

		// Token: 0x06002844 RID: 10308 RVA: 0x000931D5 File Offset: 0x000913D5
		public void Demand()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			new PermissionSet(this).CasOnlyDemand(3);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000931EB File Offset: 0x000913EB
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Deny()
		{
			new PermissionSet(this).Deny();
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000931F8 File Offset: 0x000913F8
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
			return this.IsSubsetOf(codeAccessPermission) && codeAccessPermission.IsSubsetOf(this);
		}

		// Token: 0x06002847 RID: 10311
		public abstract void FromXml(SecurityElement elem);

		// Token: 0x06002848 RID: 10312 RVA: 0x00093238 File Offset: 0x00091438
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002849 RID: 10313
		public abstract IPermission Intersect(IPermission target);

		// Token: 0x0600284A RID: 10314
		public abstract bool IsSubsetOf(IPermission target);

		// Token: 0x0600284B RID: 10315 RVA: 0x00093240 File Offset: 0x00091440
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x0600284C RID: 10316
		public abstract SecurityElement ToXml();

		// Token: 0x0600284D RID: 10317 RVA: 0x0009324D File Offset: 0x0009144D
		public virtual IPermission Union(IPermission other)
		{
			if (other != null)
			{
				throw new NotSupportedException();
			}
			return null;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x00093259 File Offset: 0x00091459
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void PermitOnly()
		{
			new PermissionSet(this).PermitOnly();
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00093266 File Offset: 0x00091466
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAll()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00093266 File Offset: 0x00091466
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAssert()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00093266 File Offset: 0x00091466
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertDeny()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00093266 File Offset: 0x00091466
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertPermitOnly()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			throw new NotImplementedException();
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00093278 File Offset: 0x00091478
		internal SecurityElement Element(int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000932D7 File Offset: 0x000914D7
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None && state != PermissionState.Unrestricted)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), state), "state");
			}
			return state;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00093304 File Offset: 0x00091504
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
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

		// Token: 0x06002856 RID: 10326 RVA: 0x000933C0 File Offset: 0x000915C0
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000933F2 File Offset: 0x000915F2
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			throw new ArgumentException(string.Format(Locale.GetText("Invalid permission type '{0}', expected type '{1}'."), target.GetType(), expected), "target");
		}
	}
}
