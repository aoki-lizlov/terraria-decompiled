using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004F0 RID: 1264
	public sealed class DiscretionaryAcl : CommonAcl
	{
		// Token: 0x0600338D RID: 13197 RVA: 0x000BE0A8 File Offset: 0x000BC2A8
		public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
			: base(isContainer, isDS, capacity)
		{
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000BE0B3 File Offset: 0x000BC2B3
		public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: base(isContainer, isDS, rawAcl)
		{
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000BE0BE File Offset: 0x000BC2BE
		public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
			: base(isContainer, isDS, revision, capacity)
		{
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000BE0CB File Offset: 0x000BC2CB
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.AddAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000BE0E0 File Offset: 0x000BC2E0
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.AddAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000BE108 File Offset: 0x000BC308
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000BE144 File Offset: 0x000BC344
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000BE17D File Offset: 0x000BC37D
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.RemoveAceSpecific(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000BE194 File Offset: 0x000BC394
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.RemoveAceSpecific(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000BE1BC File Offset: 0x000BC3BC
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000BE1F5 File Offset: 0x000BC3F5
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.SetAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None);
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000BE20C File Offset: 0x000BC40C
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			base.SetAce(DiscretionaryAcl.GetAceQualifier(accessType), sid, accessMask, inheritanceFlags, propagationFlags, AuditFlags.None, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000BE234 File Offset: 0x000BC434
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x000BE270 File Offset: 0x000BC470
		internal override void ApplyCanonicalSortToExplicitAces()
		{
			int canonicalExplicitAceCount = base.GetCanonicalExplicitAceCount();
			int canonicalExplicitDenyAceCount = base.GetCanonicalExplicitDenyAceCount();
			base.ApplyCanonicalSortToExplicitAces(0, canonicalExplicitDenyAceCount);
			base.ApplyCanonicalSortToExplicitAces(canonicalExplicitDenyAceCount, canonicalExplicitAceCount - canonicalExplicitDenyAceCount);
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x000BE29D File Offset: 0x000BC49D
		internal override int GetAceInsertPosition(AceQualifier aceQualifier)
		{
			if (aceQualifier == AceQualifier.AccessAllowed)
			{
				return base.GetCanonicalExplicitDenyAceCount();
			}
			return 0;
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x000BE2AA File Offset: 0x000BC4AA
		private static AceQualifier GetAceQualifier(AccessControlType accessType)
		{
			if (accessType == AccessControlType.Allow)
			{
				return AceQualifier.AccessAllowed;
			}
			if (AccessControlType.Deny == accessType)
			{
				return AceQualifier.AccessDenied;
			}
			throw new ArgumentOutOfRangeException("accessType");
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000BE2C4 File Offset: 0x000BC4C4
		internal override bool IsAceMeaningless(GenericAce ace)
		{
			if (base.IsAceMeaningless(ace))
			{
				return true;
			}
			if (ace.AuditFlags != AuditFlags.None)
			{
				return true;
			}
			QualifiedAce qualifiedAce = ace as QualifiedAce;
			return null != qualifiedAce && qualifiedAce.AceQualifier != AceQualifier.AccessAllowed && AceQualifier.AccessDenied != qualifiedAce.AceQualifier;
		}
	}
}
