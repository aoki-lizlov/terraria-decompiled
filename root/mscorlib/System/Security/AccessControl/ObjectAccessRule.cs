using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200050B RID: 1291
	public abstract class ObjectAccessRule : AccessRule
	{
		// Token: 0x0600346E RID: 13422 RVA: 0x000BF9A5 File Offset: 0x000BDBA5
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
			this.object_type = objectType;
			this.inherited_object_type = inheritedObjectType;
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000BF9C6 File Offset: 0x000BDBC6
		public Guid InheritedObjectType
		{
			get
			{
				return this.inherited_object_type;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000BF9D0 File Offset: 0x000BDBD0
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

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000BFA0C File Offset: 0x000BDC0C
		public Guid ObjectType
		{
			get
			{
				return this.object_type;
			}
		}

		// Token: 0x04002447 RID: 9287
		private Guid object_type;

		// Token: 0x04002448 RID: 9288
		private Guid inherited_object_type;
	}
}
