using System;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003B2 RID: 946
	internal static class PermissionBuilder
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x000936E8 File Offset: 0x000918E8
		public static IPermission Create(string fullname, PermissionState state)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", fullname);
			securityElement.AddAttribute("version", "1");
			if (state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return PermissionBuilder.CreatePermission(fullname, securityElement);
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00093748 File Offset: 0x00091948
		public static IPermission Create(SecurityElement se)
		{
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			string text = se.Attribute("class");
			if (text == null || text.Length == 0)
			{
				throw new ArgumentException("class");
			}
			return PermissionBuilder.CreatePermission(text, se);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0009378C File Offset: 0x0009198C
		public static IPermission Create(string fullname, SecurityElement se)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException("fullname");
			}
			if (se == null)
			{
				throw new ArgumentNullException("se");
			}
			return PermissionBuilder.CreatePermission(fullname, se);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000937B1 File Offset: 0x000919B1
		public static IPermission Create(Type type)
		{
			return (IPermission)Activator.CreateInstance(type, PermissionBuilder.psNone);
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x000937C3 File Offset: 0x000919C3
		internal static IPermission CreatePermission(string fullname, SecurityElement se)
		{
			Type type = Type.GetType(fullname);
			if (type == null)
			{
				throw new TypeLoadException(string.Format(Locale.GetText("Can't create an instance of permission class {0}."), fullname));
			}
			IPermission permission = PermissionBuilder.Create(type);
			permission.FromXml(se);
			return permission;
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000937F6 File Offset: 0x000919F6
		// Note: this type is marked as 'beforefieldinit'.
		static PermissionBuilder()
		{
		}

		// Token: 0x04001D8C RID: 7564
		private static object[] psNone = new object[] { PermissionState.None };
	}
}
