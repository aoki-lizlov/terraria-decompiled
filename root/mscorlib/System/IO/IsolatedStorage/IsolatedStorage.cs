using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000992 RID: 2450
	[ComVisible(true)]
	public abstract class IsolatedStorage : MarshalByRefObject
	{
		// Token: 0x06005947 RID: 22855 RVA: 0x000543BD File Offset: 0x000525BD
		protected IsolatedStorage()
		{
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x0012F00C File Offset: 0x0012D20C
		[MonoTODO("Does not currently use the manifest support")]
		[ComVisible(false)]
		public object ApplicationIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Application) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._applicationIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				throw new NotImplementedException(Locale.GetText("CAS related"));
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x0012F05B File Offset: 0x0012D25B
		public object AssemblyIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Assembly) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._assemblyIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._assemblyIdentity;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600594A RID: 22858 RVA: 0x0012F095 File Offset: 0x0012D295
		[CLSCompliant(false)]
		[Obsolete]
		public virtual ulong CurrentSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset CurrentSize."));
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x0600594B RID: 22859 RVA: 0x0012F0A6 File Offset: 0x0012D2A6
		public object DomainIdentity
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if ((this.storage_scope & IsolatedStorageScope.Domain) == IsolatedStorageScope.None)
				{
					throw new InvalidOperationException(Locale.GetText("Invalid Isolation Scope."));
				}
				if (this._domainIdentity == null)
				{
					throw new InvalidOperationException(Locale.GetText("Identity unavailable."));
				}
				return this._domainIdentity;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600594C RID: 22860 RVA: 0x0012F0E0 File Offset: 0x0012D2E0
		[CLSCompliant(false)]
		[Obsolete]
		public virtual ulong MaximumSize
		{
			get
			{
				throw new InvalidOperationException(Locale.GetText("IsolatedStorage does not have a preset MaximumSize."));
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x0012F0F1 File Offset: 0x0012D2F1
		public IsolatedStorageScope Scope
		{
			get
			{
				return this.storage_scope;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600594E RID: 22862 RVA: 0x0012F0F9 File Offset: 0x0012D2F9
		[ComVisible(false)]
		public virtual long AvailableFreeSpace
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x0600594F RID: 22863 RVA: 0x0012F0F9 File Offset: 0x0012D2F9
		[ComVisible(false)]
		public virtual long Quota
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06005950 RID: 22864 RVA: 0x0012F0F9 File Offset: 0x0012D2F9
		[ComVisible(false)]
		public virtual long UsedSize
		{
			get
			{
				throw new InvalidOperationException("This property is not defined for this store.");
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x0012F105 File Offset: 0x0012D305
		protected virtual char SeparatorExternal
		{
			get
			{
				return Path.DirectorySeparatorChar;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06005952 RID: 22866 RVA: 0x0012F10C File Offset: 0x0012D30C
		protected virtual char SeparatorInternal
		{
			get
			{
				return '.';
			}
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		protected virtual IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			return null;
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x0012F110 File Offset: 0x0012D310
		protected void InitStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly) || scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
			{
				throw new NotImplementedException(scope.ToString());
			}
			throw new ArgumentException(scope.ToString());
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x0012F13F File Offset: 0x0012D33F
		[MonoTODO("requires manifest support")]
		protected void InitStore(IsolatedStorageScope scope, Type appEvidenceType)
		{
			if (AppDomain.CurrentDomain.ApplicationIdentity == null)
			{
				throw new IsolatedStorageException(Locale.GetText("No ApplicationIdentity available for AppDomain."));
			}
			appEvidenceType == null;
			this.storage_scope = scope;
		}

		// Token: 0x06005956 RID: 22870
		public abstract void Remove();

		// Token: 0x06005957 RID: 22871 RVA: 0x0000408A File Offset: 0x0000228A
		[ComVisible(false)]
		public virtual bool IncreaseQuotaTo(long newQuotaSize)
		{
			return false;
		}

		// Token: 0x04003578 RID: 13688
		internal IsolatedStorageScope storage_scope;

		// Token: 0x04003579 RID: 13689
		internal object _assemblyIdentity;

		// Token: 0x0400357A RID: 13690
		internal object _domainIdentity;

		// Token: 0x0400357B RID: 13691
		internal object _applicationIdentity;
	}
}
