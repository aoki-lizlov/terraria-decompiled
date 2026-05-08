using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x02000423 RID: 1059
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002C8B RID: 11403 RVA: 0x000A11CD File Offset: 0x0009F3CD
		public RegistryPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000A1203 File Offset: 0x0009F403
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this._state = PermissionState.None;
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.AddPathList(access, pathList);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000A123C File Offset: 0x0009F43C
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			if (!Enum.IsDefined(typeof(AccessControlActions), control))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), control), "AccessControlActions");
			}
			this._state = PermissionState.None;
			this.AddPathList(access, control, pathList);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000A1298 File Offset: 0x0009F498
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.AddWithUnionKey(this.readList, pathList);
				return;
			case RegistryPermissionAccess.Write:
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			case RegistryPermissionAccess.Create:
				this.AddWithUnionKey(this.createList, pathList);
				return;
			case RegistryPermissionAccess.AllAccess:
				this.AddWithUnionKey(this.createList, pathList);
				this.AddWithUnionKey(this.readList, pathList);
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000A1338 File Offset: 0x0009F538
		public string GetPathList(RegistryPermissionAccess access)
		{
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
			case RegistryPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(access, true);
				goto IL_0061;
			case RegistryPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case RegistryPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			case RegistryPermissionAccess.Create:
				return this.GetPathList(this.createList);
			}
			this.ThrowInvalidFlag(access, false);
			IL_0061:
			return null;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000A13A8 File Offset: 0x0009F5A8
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.readList.Clear();
				foreach (string text in pathList.Split(';', StringSplitOptions.None))
				{
					this.readList.Add(text);
				}
				return;
			case RegistryPermissionAccess.Write:
				this.writeList.Clear();
				foreach (string text2 in pathList.Split(';', StringSplitOptions.None))
				{
					this.writeList.Add(text2);
				}
				return;
			case RegistryPermissionAccess.Create:
				this.createList.Clear();
				foreach (string text3 in pathList.Split(';', StringSplitOptions.None))
				{
					this.createList.Add(text3);
				}
				return;
			case RegistryPermissionAccess.AllAccess:
				this.createList.Clear();
				this.readList.Clear();
				this.writeList.Clear();
				foreach (string text4 in pathList.Split(';', StringSplitOptions.None))
				{
					this.createList.Add(text4);
					this.readList.Add(text4);
					this.writeList.Add(text4);
				}
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000A1500 File Offset: 0x0009F700
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(this._state);
			string text = this.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission;
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000A1554 File Offset: 0x0009F754
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Create");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Create, text);
			}
			string text2 = esd.Attribute("Read");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Read, text2);
			}
			string text3 = esd.Attribute("Write");
			if (text3 != null && text3.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Write, text3);
			}
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000A15EC File Offset: 0x0009F7EC
		public override IPermission Intersect(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.Copy();
			}
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			RegistryPermission registryPermission2 = new RegistryPermission(PermissionState.None);
			this.IntersectKeys(this.createList, registryPermission.createList, registryPermission2.createList);
			this.IntersectKeys(this.readList, registryPermission.readList, registryPermission2.readList);
			this.IntersectKeys(this.writeList, registryPermission.writeList, registryPermission2.writeList);
			if (!registryPermission2.IsEmpty())
			{
				return registryPermission2;
			}
			return null;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000A1680 File Offset: 0x0009F880
		public override bool IsSubsetOf(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return false;
			}
			if (registryPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.IsUnrestricted();
			}
			return registryPermission.IsUnrestricted() || (this.KeyIsSubsetOf(this.createList, registryPermission.createList) && this.KeyIsSubsetOf(this.readList, registryPermission.readList) && this.KeyIsSubsetOf(this.writeList, registryPermission.writeList));
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000A1705 File Offset: 0x0009F905
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000A1710 File Offset: 0x0009F910
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string text = this.GetPathList(RegistryPermissionAccess.Create);
				if (text != null)
				{
					securityElement.AddAttribute("Create", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Read);
				if (text != null)
				{
					securityElement.AddAttribute("Read", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Write);
				if (text != null)
				{
					securityElement.AddAttribute("Write", text);
				}
			}
			return securityElement;
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000A1788 File Offset: 0x0009F988
		public override IPermission Union(IPermission other)
		{
			RegistryPermission registryPermission = this.Cast(other);
			if (registryPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && registryPermission.IsEmpty())
			{
				return null;
			}
			RegistryPermission registryPermission2 = (RegistryPermission)this.Copy();
			string text = registryPermission.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Create, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Read, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission2;
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000348A8 File Offset: 0x00032AA8
		int IBuiltInPermission.GetTokenIndex()
		{
			return 5;
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000A1816 File Offset: 0x0009FA16
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.createList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000A184A File Offset: 0x0009FA4A
		private RegistryPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(RegistryPermission));
			}
			return registryPermission;
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000A186C File Offset: 0x0009FA6C
		internal void ThrowInvalidFlag(RegistryPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000A18AC File Offset: 0x0009FAAC
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string text = (string)obj;
				stringBuilder.Append(text);
				stringBuilder.Append(";");
			}
			string text2 = stringBuilder.ToString();
			int length = text2.Length;
			if (length > 0)
			{
				return text2.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000A1958 File Offset: 0x0009FB58
		internal bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text.StartsWith(text2))
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

		// Token: 0x06002C9F RID: 11423 RVA: 0x000A1A04 File Offset: 0x0009FC04
		internal void AddWithUnionKey(IList list, string pathList)
		{
			foreach (string text in pathList.Split(';', StringSplitOptions.None))
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					for (int j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (text2.StartsWith(text))
						{
							list[j] = text;
						}
						else if (!text.StartsWith(text2))
						{
							list.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000A1A88 File Offset: 0x0009FC88
		internal void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (text2.StartsWith(text))
						{
							result.Add(text2);
						}
					}
					else if (text.StartsWith(text2))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x04001F4A RID: 8010
		private const int version = 1;

		// Token: 0x04001F4B RID: 8011
		private PermissionState _state;

		// Token: 0x04001F4C RID: 8012
		private ArrayList createList;

		// Token: 0x04001F4D RID: 8013
		private ArrayList readList;

		// Token: 0x04001F4E RID: 8014
		private ArrayList writeList;
	}
}
