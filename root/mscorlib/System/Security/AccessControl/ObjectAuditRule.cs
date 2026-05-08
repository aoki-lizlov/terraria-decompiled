using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200050E RID: 1294
	public abstract class ObjectAuditRule : AuditRule
	{
		// Token: 0x06003484 RID: 13444 RVA: 0x000BFE66 File Offset: 0x000BE066
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			this.object_type = objectType;
			this.inherited_object_type = inheritedObjectType;
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x000BFE87 File Offset: 0x000BE087
		public Guid InheritedObjectType
		{
			get
			{
				return this.inherited_object_type;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000BFE90 File Offset: 0x000BE090
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
				if (this.object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.ObjectAceTypePresent;
				}
				if (this.inherited_object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				}
				return objectAceFlags;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000BFECC File Offset: 0x000BE0CC
		public Guid ObjectType
		{
			get
			{
				return this.object_type;
			}
		}

		// Token: 0x04002450 RID: 9296
		private Guid inherited_object_type;

		// Token: 0x04002451 RID: 9297
		private Guid object_type;
	}
}
