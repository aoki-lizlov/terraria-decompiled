using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x02000403 RID: 1027
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002B3F RID: 11071 RVA: 0x0009D6D4 File Offset: 0x0009B8D4
		public EnvironmentPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x0009D6FF File Offset: 0x0009B8FF
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.SetPathList(flag, pathList);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0009D728 File Offset: 0x0009B928
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
				foreach (string text in pathList.Split(';', StringSplitOptions.None))
				{
					if (!this.readList.Contains(text))
					{
						this.readList.Add(text);
					}
				}
				return;
			case EnvironmentPermissionAccess.Write:
				foreach (string text2 in pathList.Split(';', StringSplitOptions.None))
				{
					if (!this.writeList.Contains(text2))
					{
						this.writeList.Add(text2);
					}
				}
				return;
			case EnvironmentPermissionAccess.AllAccess:
				foreach (string text3 in pathList.Split(';', StringSplitOptions.None))
				{
					if (!this.readList.Contains(text3))
					{
						this.readList.Add(text3);
					}
					if (!this.writeList.Contains(text3))
					{
						this.writeList.Add(text3);
					}
				}
				return;
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0009D82C File Offset: 0x0009BA2C
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(this._state);
			string text = this.GetPathList(EnvironmentPermissionAccess.Read);
			if (text != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			text = this.GetPathList(EnvironmentPermissionAccess.Write);
			if (text != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, text);
			}
			return environmentPermission;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0009D86C File Offset: 0x0009BA6C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Read");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			string text2 = esd.Attribute("Write");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Write, text2);
			}
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0009D8D8 File Offset: 0x0009BAD8
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
			case EnvironmentPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(flag, true);
				break;
			case EnvironmentPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case EnvironmentPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
			return null;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0009D92C File Offset: 0x0009BB2C
		public override IPermission Intersect(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.Copy();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			int num = 0;
			EnvironmentPermission environmentPermission2 = new EnvironmentPermission(PermissionState.None);
			string pathList = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (pathList != null)
			{
				foreach (string text in pathList.Split(';', StringSplitOptions.None))
				{
					if (this.readList.Contains(text))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, text);
						num++;
					}
				}
			}
			string pathList2 = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (pathList2 != null)
			{
				foreach (string text2 in pathList2.Split(';', StringSplitOptions.None))
				{
					if (this.writeList.Contains(text2))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, text2);
						num++;
					}
				}
			}
			if (num <= 0)
			{
				return null;
			}
			return environmentPermission2;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x0009DA10 File Offset: 0x0009BC10
		public override bool IsSubsetOf(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return false;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.IsUnrestricted();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.readList)
			{
				string text = (string)obj;
				if (!environmentPermission.readList.Contains(text))
				{
					return false;
				}
			}
			foreach (object obj2 in this.writeList)
			{
				string text2 = (string)obj2;
				if (!environmentPermission.writeList.Contains(text2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x0009DAF8 File Offset: 0x0009BCF8
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0009DB04 File Offset: 0x0009BD04
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
				this.readList.Clear();
				foreach (string text in pathList.Split(';', StringSplitOptions.None))
				{
					this.readList.Add(text);
				}
				return;
			case EnvironmentPermissionAccess.Write:
				this.writeList.Clear();
				foreach (string text2 in pathList.Split(';', StringSplitOptions.None))
				{
					this.writeList.Add(text2);
				}
				return;
			case EnvironmentPermissionAccess.AllAccess:
				this.readList.Clear();
				this.writeList.Clear();
				foreach (string text3 in pathList.Split(';', StringSplitOptions.None))
				{
					this.readList.Add(text3);
					this.writeList.Add(text3);
				}
				return;
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x0009DBFC File Offset: 0x0009BDFC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string text = this.GetPathList(EnvironmentPermissionAccess.Read);
				if (text != null)
				{
					securityElement.AddAttribute("Read", text);
				}
				text = this.GetPathList(EnvironmentPermissionAccess.Write);
				if (text != null)
				{
					securityElement.AddAttribute("Write", text);
				}
			}
			return securityElement;
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x0009DC5C File Offset: 0x0009BE5C
		public override IPermission Union(IPermission other)
		{
			EnvironmentPermission environmentPermission = this.Cast(other);
			if (environmentPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && environmentPermission.IsEmpty())
			{
				return null;
			}
			EnvironmentPermission environmentPermission2 = (EnvironmentPermission)this.Copy();
			string text = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (text != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, text);
			}
			text = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (text != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, text);
			}
			return environmentPermission2;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0000408A File Offset: 0x0000228A
		int IBuiltInPermission.GetTokenIndex()
		{
			return 0;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0009DCD7 File Offset: 0x0009BED7
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0009DCFE File Offset: 0x0009BEFE
		private EnvironmentPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			EnvironmentPermission environmentPermission = target as EnvironmentPermission;
			if (environmentPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(EnvironmentPermission));
			}
			return environmentPermission;
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0009DD20 File Offset: 0x0009BF20
		internal void ThrowInvalidFlag(EnvironmentPermissionAccess flag, bool context)
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

		// Token: 0x06002B4F RID: 11087 RVA: 0x0009DD60 File Offset: 0x0009BF60
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

		// Token: 0x04001ECF RID: 7887
		private const int version = 1;

		// Token: 0x04001ED0 RID: 7888
		private PermissionState _state;

		// Token: 0x04001ED1 RID: 7889
		private ArrayList readList;

		// Token: 0x04001ED2 RID: 7890
		private ArrayList writeList;
	}
}
