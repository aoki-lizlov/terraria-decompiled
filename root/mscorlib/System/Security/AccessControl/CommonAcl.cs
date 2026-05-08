using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004DD RID: 1245
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x060032E8 RID: 13032 RVA: 0x000BC5AC File Offset: 0x000BA7AC
		internal CommonAcl(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			if (rawAcl == null)
			{
				rawAcl = new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 10);
			}
			else
			{
				byte[] array = new byte[rawAcl.BinaryLength];
				rawAcl.GetBinaryForm(array, 0);
				rawAcl = new RawAcl(array, 0);
			}
			this.Init(isContainer, isDS, rawAcl);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000BC602 File Offset: 0x000BA802
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this.Init(isContainer, isDS, new RawAcl(revision, capacity));
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000BC61A File Offset: 0x000BA81A
		internal CommonAcl(bool isContainer, bool isDS, int capacity)
			: this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000BC634 File Offset: 0x000BA834
		private void Init(bool isContainer, bool isDS, RawAcl rawAcl)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.raw_acl = rawAcl;
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000BC651 File Offset: 0x000BA851
		public sealed override int BinaryLength
		{
			get
			{
				return this.raw_acl.BinaryLength;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000BC65E File Offset: 0x000BA85E
		public sealed override int Count
		{
			get
			{
				return this.raw_acl.Count;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x000BC66B File Offset: 0x000BA86B
		public bool IsCanonical
		{
			get
			{
				return this.is_canonical;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000BC673 File Offset: 0x000BA873
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000BC67B File Offset: 0x000BA87B
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000BC683 File Offset: 0x000BA883
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x000BC68B File Offset: 0x000BA88B
		internal bool IsAefa
		{
			get
			{
				return this.is_aefa;
			}
			set
			{
				this.is_aefa = value;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000BC694 File Offset: 0x000BA894
		public sealed override byte Revision
		{
			get
			{
				return this.raw_acl.Revision;
			}
		}

		// Token: 0x170006F4 RID: 1780
		public sealed override GenericAce this[int index]
		{
			get
			{
				return CommonAcl.CopyAce(this.raw_acl[index]);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000BC6B4 File Offset: 0x000BA8B4
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			this.raw_acl.GetBinaryForm(binaryForm, offset);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000BC6C4 File Offset: 0x000BA8C4
		public void Purge(SecurityIdentifier sid)
		{
			this.RequireCanonicity();
			this.RemoveAces<KnownAce>((KnownAce ace) => ace.SecurityIdentifier == sid);
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000BC6F6 File Offset: 0x000BA8F6
		public void RemoveInheritedAces()
		{
			this.RequireCanonicity();
			this.RemoveAces<GenericAce>((GenericAce ace) => ace.IsInherited);
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000BC723 File Offset: 0x000BA923
		internal void RequireCanonicity()
		{
			if (!this.IsCanonical)
			{
				throw new InvalidOperationException("ACL is not canonical.");
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000BC738 File Offset: 0x000BA938
		internal void CanonicalizeAndClearAefa()
		{
			this.RemoveAces<GenericAce>(new CommonAcl.RemoveAcesCallback<GenericAce>(this.IsAceMeaningless));
			this.is_canonical = this.TestCanonicity();
			if (this.IsCanonical)
			{
				this.ApplyCanonicalSortToExplicitAces();
				this.MergeExplicitAces();
			}
			this.IsAefa = false;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000BC774 File Offset: 0x000BA974
		internal virtual bool IsAceMeaningless(GenericAce ace)
		{
			AceFlags aceFlags = ace.AceFlags;
			KnownAce knownAce = ace as KnownAce;
			if (knownAce != null)
			{
				if (knownAce.AccessMask == 0)
				{
					return true;
				}
				if ((aceFlags & AceFlags.InheritOnly) != AceFlags.None)
				{
					if (knownAce is ObjectAce)
					{
						return true;
					}
					if (!this.IsContainer)
					{
						return true;
					}
					if ((aceFlags & (AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == AceFlags.None)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000BC7C4 File Offset: 0x000BA9C4
		private bool TestCanonicity()
		{
			AceEnumerator aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (!(aceEnumerator.Current is QualifiedAce))
				{
					return false;
				}
			}
			bool flag = false;
			aceEnumerator = base.GetEnumerator();
			while (aceEnumerator.MoveNext())
			{
				if (((QualifiedAce)aceEnumerator.Current).IsInherited)
				{
					flag = true;
				}
				else if (flag)
				{
					return false;
				}
			}
			bool flag2 = false;
			foreach (GenericAce genericAce in this)
			{
				QualifiedAce qualifiedAce = (QualifiedAce)genericAce;
				if (qualifiedAce.IsInherited)
				{
					break;
				}
				if (qualifiedAce.AceQualifier == AceQualifier.AccessAllowed)
				{
					flag2 = true;
				}
				else if (AceQualifier.AccessDenied == qualifiedAce.AceQualifier && flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000BC864 File Offset: 0x000BAA64
		internal int GetCanonicalExplicitDenyAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				QualifiedAce qualifiedAce = this.raw_acl[num] as QualifiedAce;
				if (qualifiedAce == null || qualifiedAce.AceQualifier != AceQualifier.AccessDenied)
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000BC8BC File Offset: 0x000BAABC
		internal int GetCanonicalExplicitAceCount()
		{
			int num = 0;
			while (num < this.Count && !this.raw_acl[num].IsInherited)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000BC8F0 File Offset: 0x000BAAF0
		private void MergeExplicitAces()
		{
			int num = this.GetCanonicalExplicitAceCount();
			int i = 0;
			while (i < num - 1)
			{
				GenericAce genericAce = this.MergeExplicitAcePair(this.raw_acl[i], this.raw_acl[i + 1]);
				if (null != genericAce)
				{
					this.raw_acl[i] = genericAce;
					this.raw_acl.RemoveAce(i + 1);
					num--;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000BC960 File Offset: 0x000BAB60
		private GenericAce MergeExplicitAcePair(GenericAce ace1, GenericAce ace2)
		{
			QualifiedAce qualifiedAce = ace1 as QualifiedAce;
			QualifiedAce qualifiedAce2 = ace2 as QualifiedAce;
			if (!(null != qualifiedAce) || !(null != qualifiedAce2))
			{
				return null;
			}
			if (qualifiedAce.AceQualifier != qualifiedAce2.AceQualifier)
			{
				return null;
			}
			if (!(qualifiedAce.SecurityIdentifier == qualifiedAce2.SecurityIdentifier))
			{
				return null;
			}
			AceFlags aceFlags = qualifiedAce.AceFlags;
			AceFlags aceFlags2 = qualifiedAce2.AceFlags;
			int accessMask = qualifiedAce.AccessMask;
			int accessMask2 = qualifiedAce2.AccessMask;
			if (!this.IsContainer)
			{
				aceFlags &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
				aceFlags2 &= ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit | AceFlags.NoPropagateInherit | AceFlags.InheritOnly);
			}
			AceFlags aceFlags3;
			int num;
			if (aceFlags != aceFlags2)
			{
				if (accessMask != accessMask2)
				{
					return null;
				}
				if ((aceFlags & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)) == (aceFlags2 & ~(AceFlags.ObjectInherit | AceFlags.ContainerInherit)))
				{
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
				else
				{
					if ((aceFlags & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)) != (aceFlags2 & ~(AceFlags.SuccessfulAccess | AceFlags.FailedAccess)))
					{
						return null;
					}
					aceFlags3 = aceFlags | aceFlags2;
					num = accessMask;
				}
			}
			else
			{
				aceFlags3 = aceFlags;
				num = accessMask | accessMask2;
			}
			CommonAce commonAce = ace1 as CommonAce;
			CommonAce commonAce2 = ace2 as CommonAce;
			if (null != commonAce && null != commonAce2)
			{
				return new CommonAce(aceFlags3, commonAce.AceQualifier, num, commonAce.SecurityIdentifier, commonAce.IsCallback, commonAce.GetOpaque());
			}
			ObjectAce objectAce = ace1 as ObjectAce;
			ObjectAce objectAce2 = ace2 as ObjectAce;
			if (null != objectAce && null != objectAce2)
			{
				Guid guid;
				Guid guid2;
				CommonAcl.GetObjectAceTypeGuids(objectAce, out guid, out guid2);
				Guid guid3;
				Guid guid4;
				CommonAcl.GetObjectAceTypeGuids(objectAce2, out guid3, out guid4);
				if (guid == guid3 && guid2 == guid4)
				{
					return new ObjectAce(aceFlags3, objectAce.AceQualifier, num, objectAce.SecurityIdentifier, objectAce.ObjectAceFlags, objectAce.ObjectAceType, objectAce.InheritedObjectAceType, objectAce.IsCallback, objectAce.GetOpaque());
				}
			}
			return null;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000BCB08 File Offset: 0x000BAD08
		private static void GetObjectAceTypeGuids(ObjectAce ace, out Guid type, out Guid inheritedType)
		{
			type = Guid.Empty;
			inheritedType = Guid.Empty;
			if ((ace.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				type = ace.ObjectAceType;
			}
			if ((ace.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				inheritedType = ace.InheritedObjectAceType;
			}
		}

		// Token: 0x06003302 RID: 13058
		internal abstract void ApplyCanonicalSortToExplicitAces();

		// Token: 0x06003303 RID: 13059 RVA: 0x000BCB58 File Offset: 0x000BAD58
		internal void ApplyCanonicalSortToExplicitAces(int start, int count)
		{
			for (int i = start + 1; i < start + count; i++)
			{
				KnownAce knownAce = (KnownAce)this.raw_acl[i];
				SecurityIdentifier securityIdentifier = knownAce.SecurityIdentifier;
				int num = i;
				while (num > start && ((KnownAce)this.raw_acl[num - 1]).SecurityIdentifier.CompareTo(securityIdentifier) > 0)
				{
					this.raw_acl[num] = this.raw_acl[num - 1];
					num--;
				}
				this.raw_acl[num] = knownAce;
			}
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000BCBE2 File Offset: 0x000BADE2
		internal override string GetSddlForm(ControlFlags sdFlags, bool isDacl)
		{
			return this.raw_acl.GetSddlForm(sdFlags, isDacl);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000BCBF4 File Offset: 0x000BADF4
		internal void RemoveAces<T>(CommonAcl.RemoveAcesCallback<T> callback) where T : GenericAce
		{
			int i = 0;
			while (i < this.raw_acl.Count)
			{
				if (this.raw_acl[i] is T && callback((T)((object)this.raw_acl[i])))
				{
					this.raw_acl.RemoveAce(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000BCC54 File Offset: 0x000BAE54
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.AddAce(qualifiedAce);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000BCC78 File Offset: 0x000BAE78
		internal void AddAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.AddAce(qualifiedAce);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000BCCA4 File Offset: 0x000BAEA4
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				return this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			}
			return new ObjectAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, objectFlags, objectType, inheritedObjectType, false, null);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000BCCF4 File Offset: 0x000BAEF4
		private QualifiedAce AddAceGetQualifiedAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			return new CommonAce(this.GetAceFlags(inheritanceFlags, propagationFlags, auditFlags), aceQualifier, accessMask, sid, false, null);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000BCD0C File Offset: 0x000BAF0C
		private void AddAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			int aceInsertPosition = this.GetAceInsertPosition(newAce.AceQualifier);
			this.raw_acl.InsertAce(aceInsertPosition, CommonAcl.CopyAce(newAce));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000BCD44 File Offset: 0x000BAF44
		private static GenericAce CopyAce(GenericAce ace)
		{
			byte[] array = new byte[ace.BinaryLength];
			ace.GetBinaryForm(array, 0);
			return GenericAce.CreateFromBinaryForm(array, 0);
		}

		// Token: 0x0600330C RID: 13068
		internal abstract int GetAceInsertPosition(AceQualifier aceQualifier);

		// Token: 0x0600330D RID: 13069 RVA: 0x000BCD6C File Offset: 0x000BAF6C
		private AceFlags GetAceFlags(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			if (inheritanceFlags != InheritanceFlags.None && !this.IsContainer)
			{
				throw new ArgumentException("Flags only work with containers.", "inheritanceFlags");
			}
			if (inheritanceFlags == InheritanceFlags.None && propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException("Propagation flags need inheritance flags.", "propagationFlags");
			}
			AceFlags aceFlags = AceFlags.None;
			if ((InheritanceFlags.ContainerInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ContainerInherit;
			}
			if ((InheritanceFlags.ObjectInherit & inheritanceFlags) != InheritanceFlags.None)
			{
				aceFlags |= AceFlags.ObjectInherit;
			}
			if ((PropagationFlags.InheritOnly & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.InheritOnly;
			}
			if ((PropagationFlags.NoPropagateInherit & propagationFlags) != PropagationFlags.None)
			{
				aceFlags |= AceFlags.NoPropagateInherit;
			}
			if ((AuditFlags.Success & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.SuccessfulAccess;
			}
			if ((AuditFlags.Failure & auditFlags) != AuditFlags.None)
			{
				aceFlags |= AceFlags.FailedAccess;
			}
			return aceFlags;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000BCDE8 File Offset: 0x000BAFE8
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			this.RequireCanonicity();
			this.RemoveAces<CommonAce>((CommonAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags);
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000BCE48 File Offset: 0x000BB048
		internal void RemoveAceSpecific(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!this.IsDS)
			{
				throw new InvalidOperationException("For this overload, IsDS must be true.");
			}
			if (objectFlags == ObjectAceFlags.None)
			{
				this.RemoveAceSpecific(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
				return;
			}
			this.RequireCanonicity();
			this.RemoveAces<ObjectAce>((ObjectAce ace) => ace.AccessMask == accessMask && ace.AceQualifier == aceQualifier && !(ace.SecurityIdentifier != sid) && ace.InheritanceFlags == inheritanceFlags && (inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == propagationFlags) && ace.AuditFlags == auditFlags && ace.ObjectAceFlags == objectFlags && ((objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || !(ace.ObjectAceType != objectType)) && ((objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || !(ace.InheritedObjectAceType != objectType)));
			this.CanonicalizeAndClearAefa();
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000BCEFC File Offset: 0x000BB0FC
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags);
			this.SetAce(qualifiedAce);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000BCF20 File Offset: 0x000BB120
		internal void SetAce(AceQualifier aceQualifier, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags auditFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			QualifiedAce qualifiedAce = this.AddAceGetQualifiedAce(aceQualifier, sid, accessMask, inheritanceFlags, propagationFlags, auditFlags, objectFlags, objectType, inheritedObjectType);
			this.SetAce(qualifiedAce);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000BCF4C File Offset: 0x000BB14C
		private void SetAce(QualifiedAce newAce)
		{
			this.RequireCanonicity();
			this.RemoveAces<QualifiedAce>((QualifiedAce oldAce) => oldAce.AceQualifier == newAce.AceQualifier && oldAce.SecurityIdentifier == newAce.SecurityIdentifier);
			this.CanonicalizeAndClearAefa();
			this.AddAce(newAce);
		}

		// Token: 0x040023C3 RID: 9155
		private const int default_capacity = 10;

		// Token: 0x040023C4 RID: 9156
		private bool is_aefa;

		// Token: 0x040023C5 RID: 9157
		private bool is_canonical;

		// Token: 0x040023C6 RID: 9158
		private bool is_container;

		// Token: 0x040023C7 RID: 9159
		private bool is_ds;

		// Token: 0x040023C8 RID: 9160
		internal RawAcl raw_acl;

		// Token: 0x020004DE RID: 1246
		// (Invoke) Token: 0x06003314 RID: 13076
		internal delegate bool RemoveAcesCallback<T>(T ace);

		// Token: 0x020004DF RID: 1247
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x06003317 RID: 13079 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06003318 RID: 13080 RVA: 0x000BCF90 File Offset: 0x000BB190
			internal bool <Purge>b__0(KnownAce ace)
			{
				return ace.SecurityIdentifier == this.sid;
			}

			// Token: 0x040023C9 RID: 9161
			public SecurityIdentifier sid;
		}

		// Token: 0x020004E0 RID: 1248
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003319 RID: 13081 RVA: 0x000BCFA3 File Offset: 0x000BB1A3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600331A RID: 13082 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600331B RID: 13083 RVA: 0x000BCFAF File Offset: 0x000BB1AF
			internal bool <RemoveInheritedAces>b__31_0(GenericAce ace)
			{
				return ace.IsInherited;
			}

			// Token: 0x040023CA RID: 9162
			public static readonly CommonAcl.<>c <>9 = new CommonAcl.<>c();

			// Token: 0x040023CB RID: 9163
			public static CommonAcl.RemoveAcesCallback<GenericAce> <>9__31_0;
		}

		// Token: 0x020004E1 RID: 1249
		[CompilerGenerated]
		private sealed class <>c__DisplayClass53_0
		{
			// Token: 0x0600331C RID: 13084 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass53_0()
			{
			}

			// Token: 0x0600331D RID: 13085 RVA: 0x000BCFB8 File Offset: 0x000BB1B8
			internal bool <RemoveAceSpecific>b__0(CommonAce ace)
			{
				return ace.AccessMask == this.accessMask && ace.AceQualifier == this.aceQualifier && !(ace.SecurityIdentifier != this.sid) && ace.InheritanceFlags == this.inheritanceFlags && (this.inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == this.propagationFlags) && ace.AuditFlags == this.auditFlags;
			}

			// Token: 0x040023CC RID: 9164
			public int accessMask;

			// Token: 0x040023CD RID: 9165
			public AceQualifier aceQualifier;

			// Token: 0x040023CE RID: 9166
			public SecurityIdentifier sid;

			// Token: 0x040023CF RID: 9167
			public InheritanceFlags inheritanceFlags;

			// Token: 0x040023D0 RID: 9168
			public PropagationFlags propagationFlags;

			// Token: 0x040023D1 RID: 9169
			public AuditFlags auditFlags;
		}

		// Token: 0x020004E2 RID: 1250
		[CompilerGenerated]
		private sealed class <>c__DisplayClass54_0
		{
			// Token: 0x0600331E RID: 13086 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass54_0()
			{
			}

			// Token: 0x0600331F RID: 13087 RVA: 0x000BD034 File Offset: 0x000BB234
			internal bool <RemoveAceSpecific>b__0(ObjectAce ace)
			{
				return ace.AccessMask == this.accessMask && ace.AceQualifier == this.aceQualifier && !(ace.SecurityIdentifier != this.sid) && ace.InheritanceFlags == this.inheritanceFlags && (this.inheritanceFlags == InheritanceFlags.None || ace.PropagationFlags == this.propagationFlags) && ace.AuditFlags == this.auditFlags && ace.ObjectAceFlags == this.objectFlags && ((this.objectFlags & ObjectAceFlags.ObjectAceTypePresent) == ObjectAceFlags.None || !(ace.ObjectAceType != this.objectType)) && ((this.objectFlags & ObjectAceFlags.InheritedObjectAceTypePresent) == ObjectAceFlags.None || !(ace.InheritedObjectAceType != this.objectType));
			}

			// Token: 0x040023D2 RID: 9170
			public int accessMask;

			// Token: 0x040023D3 RID: 9171
			public AceQualifier aceQualifier;

			// Token: 0x040023D4 RID: 9172
			public SecurityIdentifier sid;

			// Token: 0x040023D5 RID: 9173
			public InheritanceFlags inheritanceFlags;

			// Token: 0x040023D6 RID: 9174
			public PropagationFlags propagationFlags;

			// Token: 0x040023D7 RID: 9175
			public AuditFlags auditFlags;

			// Token: 0x040023D8 RID: 9176
			public ObjectAceFlags objectFlags;

			// Token: 0x040023D9 RID: 9177
			public Guid objectType;
		}

		// Token: 0x020004E3 RID: 1251
		[CompilerGenerated]
		private sealed class <>c__DisplayClass57_0
		{
			// Token: 0x06003320 RID: 13088 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass57_0()
			{
			}

			// Token: 0x06003321 RID: 13089 RVA: 0x000BD0FD File Offset: 0x000BB2FD
			internal bool <SetAce>b__0(QualifiedAce oldAce)
			{
				return oldAce.AceQualifier == this.newAce.AceQualifier && oldAce.SecurityIdentifier == this.newAce.SecurityIdentifier;
			}

			// Token: 0x040023DA RID: 9178
			public QualifiedAce newAce;
		}
	}
}
