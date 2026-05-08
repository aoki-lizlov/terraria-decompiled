using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000415 RID: 1045
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002BEF RID: 11247 RVA: 0x0009F70A File Offset: 0x0009D90A
		public KeyContainerPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0009F727 File Offset: 0x0009D927
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			this.SetFlags(flags);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0009F738 File Offset: 0x0009D938
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			this.SetFlags(flags);
			if (accessList != null)
			{
				this._accessEntries = new KeyContainerPermissionAccessEntryCollection();
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in accessList)
				{
					this._accessEntries.Add(keyContainerPermissionAccessEntry);
				}
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0009F781 File Offset: 0x0009D981
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this._accessEntries;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x0009F789 File Offset: 0x0009D989
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0009F794 File Offset: 0x0009D994
		public override IPermission Copy()
		{
			if (this._accessEntries.Count == 0)
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[this._accessEntries.Count];
			this._accessEntries.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags, array);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x0009F7E4 File Offset: 0x0009D9E4
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
				return;
			}
			this._flags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), securityElement.Attribute("Flags"));
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		[MonoTODO("(2.0)")]
		public override IPermission Intersect(IPermission target)
		{
			return null;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("(2.0)")]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0009F838 File Offset: 0x0009DA38
		public bool IsUnrestricted()
		{
			return this._flags == KeyContainerPermissionFlags.AllFlags;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0009F848 File Offset: 0x0009DA48
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x0009F878 File Offset: 0x0009DA78
		public override IPermission Union(IPermission target)
		{
			KeyContainerPermission keyContainerPermission = this.Cast(target);
			if (keyContainerPermission == null)
			{
				return this.Copy();
			}
			KeyContainerPermissionAccessEntryCollection keyContainerPermissionAccessEntryCollection = new KeyContainerPermissionAccessEntryCollection();
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this._accessEntries)
			{
				keyContainerPermissionAccessEntryCollection.Add(keyContainerPermissionAccessEntry);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission._accessEntries)
			{
				if (this._accessEntries.IndexOf(keyContainerPermissionAccessEntry2) == -1)
				{
					keyContainerPermissionAccessEntryCollection.Add(keyContainerPermissionAccessEntry2);
				}
			}
			if (keyContainerPermissionAccessEntryCollection.Count == 0)
			{
				return new KeyContainerPermission(this._flags | keyContainerPermission._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[keyContainerPermissionAccessEntryCollection.Count];
			keyContainerPermissionAccessEntryCollection.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags | keyContainerPermission._flags, array);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x0001E875 File Offset: 0x0001CA75
		int IBuiltInPermission.GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0009F93D File Offset: 0x0009DB3D
		private void SetFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & KeyContainerPermissionFlags.AllFlags) == KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), flags), "KeyContainerPermissionFlags");
			}
			this._flags = flags;
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0009F96F File Offset: 0x0009DB6F
		private KeyContainerPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = target as KeyContainerPermission;
			if (keyContainerPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(KeyContainerPermission));
			}
			return keyContainerPermission;
		}

		// Token: 0x04001F15 RID: 7957
		private KeyContainerPermissionAccessEntryCollection _accessEntries;

		// Token: 0x04001F16 RID: 7958
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04001F17 RID: 7959
		private const int version = 1;
	}
}
