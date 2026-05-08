using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000503 RID: 1283
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		// Token: 0x0600343B RID: 13371 RVA: 0x000BF327 File Offset: 0x000BD527
		internal NativeObjectSecurity(CommonSecurityDescriptor securityDescriptor, ResourceType resourceType)
			: base(securityDescriptor)
		{
			this.resource_type = resourceType;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000BF337 File Offset: 0x000BD537
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType)
			: this(isContainer, resourceType, null, null)
		{
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000BF343 File Offset: 0x000BD543
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer)
		{
			this.exception_from_error_code = exceptionFromErrorCode;
			this.resource_type = resourceType;
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000BF35A File Offset: 0x000BD55A
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections)
			: this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000BF369 File Offset: 0x000BD569
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: this(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000BF378 File Offset: 0x000BD578
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(handle, includeSections), null, handle, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000BF39F File Offset: 0x000BD59F
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: this(isContainer, resourceType, exceptionFromErrorCode, exceptionContext)
		{
			this.RaiseExceptionOnFailure(this.InternalGet(name, includeSections), name, null, exceptionContext);
			this.ClearAccessControlSectionsModified();
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000BF3C8 File Offset: 0x000BD5C8
		private void ClearAccessControlSectionsModified()
		{
			base.WriteLock();
			try
			{
				base.AccessControlSectionsModified = AccessControlSections.None;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000BF3FC File Offset: 0x000BD5FC
		protected sealed override void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			this.Persist(handle, includeSections, null);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000BF407 File Offset: 0x000BD607
		protected sealed override void Persist(string name, AccessControlSections includeSections)
		{
			this.Persist(name, includeSections, null);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000BF414 File Offset: 0x000BD614
		internal void PersistModifications(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				this.Persist(handle, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000BF450 File Offset: 0x000BD650
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(handle, includeSections), null, handle, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000BF49C File Offset: 0x000BD69C
		internal void PersistModifications(string name)
		{
			base.WriteLock();
			try
			{
				this.Persist(name, base.AccessControlSectionsModified, null);
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000BF4D8 File Offset: 0x000BD6D8
		protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			base.WriteLock();
			try
			{
				this.RaiseExceptionOnFailure(this.InternalSet(name, includeSections), name, null, exceptionContext);
				base.AccessControlSectionsModified &= ~includeSections;
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000BF534 File Offset: 0x000BD734
		internal static Exception DefaultExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			switch (errorCode)
			{
			case 2:
				return new FileNotFoundException();
			case 3:
				return new DirectoryNotFoundException();
			case 4:
				break;
			case 5:
				return new UnauthorizedAccessException();
			default:
				if (errorCode == 1314)
				{
					return new PrivilegeNotHeldException();
				}
				break;
			}
			return new InvalidOperationException("OS error code " + errorCode.ToString());
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x000BF591 File Offset: 0x000BD791
		private void RaiseExceptionOnFailure(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 0)
			{
				return;
			}
			throw (this.exception_from_error_code ?? new NativeObjectSecurity.ExceptionFromErrorCode(NativeObjectSecurity.DefaultExceptionFromErrorCode))(errorCode, name, handle, context);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000BF5B8 File Offset: 0x000BD7B8
		internal virtual int InternalGet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(handle, this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000BF600 File Offset: 0x000BD800
		internal virtual int InternalGet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32GetHelper(delegate(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}, includeSections);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000BF648 File Offset: 0x000BD848
		internal virtual int InternalSet(SafeHandle handle, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetSecurityInfo(handle, this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000BF690 File Offset: 0x000BD890
		internal virtual int InternalSet(string name, AccessControlSections includeSections)
		{
			if (Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException();
			}
			return this.Win32SetHelper((SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl) => NativeObjectSecurity.SetNamedSecurityInfo(this.Win32FixName(name), this.ResourceType, securityInfos, owner, group, dacl, sacl), includeSections);
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000BF6D7 File Offset: 0x000BD8D7
		internal ResourceType ResourceType
		{
			get
			{
				return this.resource_type;
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000BF6E0 File Offset: 0x000BD8E0
		private int Win32GetHelper(NativeObjectSecurity.GetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			bool flag = (includeSections & AccessControlSections.Owner) > AccessControlSections.None;
			bool flag2 = (includeSections & AccessControlSections.Group) > AccessControlSections.None;
			bool flag3 = (includeSections & AccessControlSections.Access) > AccessControlSections.None;
			bool flag4 = (includeSections & AccessControlSections.Audit) > AccessControlSections.None;
			SecurityInfos securityInfos = (SecurityInfos)0;
			if (flag)
			{
				securityInfos |= SecurityInfos.Owner;
			}
			if (flag2)
			{
				securityInfos |= SecurityInfos.Group;
			}
			if (flag3)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
			}
			if (flag4)
			{
				securityInfos |= SecurityInfos.SystemAcl;
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			IntPtr intPtr4;
			IntPtr intPtr5;
			int num = nativeCall(securityInfos, out intPtr, out intPtr2, out intPtr3, out intPtr4, out intPtr5);
			if (num != 0)
			{
				return num;
			}
			try
			{
				int num2 = 0;
				if (NativeObjectSecurity.IsValidSecurityDescriptor(intPtr5))
				{
					num2 = NativeObjectSecurity.GetSecurityDescriptorLength(intPtr5);
				}
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr5, array, 0, num2);
				base.SetSecurityDescriptorBinaryForm(array, includeSections);
			}
			finally
			{
				NativeObjectSecurity.LocalFree(intPtr5);
			}
			return 0;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000BF790 File Offset: 0x000BD990
		private int Win32SetHelper(NativeObjectSecurity.SetSecurityInfoNativeCall nativeCall, AccessControlSections includeSections)
		{
			if (includeSections == AccessControlSections.None)
			{
				return 0;
			}
			SecurityInfos securityInfos = (SecurityInfos)0;
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			byte[] array4 = null;
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Owner;
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)base.GetOwner(typeof(SecurityIdentifier));
				if (null != securityIdentifier)
				{
					array = new byte[securityIdentifier.BinaryLength];
					securityIdentifier.GetBinaryForm(array, 0);
				}
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.Group;
				SecurityIdentifier securityIdentifier2 = (SecurityIdentifier)base.GetGroup(typeof(SecurityIdentifier));
				if (null != securityIdentifier2)
				{
					array2 = new byte[securityIdentifier2.BinaryLength];
					securityIdentifier2.GetBinaryForm(array2, 0);
				}
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				securityInfos |= SecurityInfos.DiscretionaryAcl;
				if (base.AreAccessRulesProtected)
				{
					securityInfos |= (SecurityInfos)(-2147483648);
				}
				else
				{
					securityInfos |= (SecurityInfos)536870912;
				}
				array3 = new byte[this.descriptor.DiscretionaryAcl.BinaryLength];
				this.descriptor.DiscretionaryAcl.GetBinaryForm(array3, 0);
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.descriptor.SystemAcl != null)
			{
				securityInfos |= SecurityInfos.SystemAcl;
				if (base.AreAuditRulesProtected)
				{
					securityInfos |= (SecurityInfos)1073741824;
				}
				else
				{
					securityInfos |= (SecurityInfos)268435456;
				}
				array4 = new byte[this.descriptor.SystemAcl.BinaryLength];
				this.descriptor.SystemAcl.GetBinaryForm(array4, 0);
			}
			return nativeCall(securityInfos, array, array2, array3, array4);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000BF8DE File Offset: 0x000BDADE
		private string Win32FixName(string name)
		{
			if (this.ResourceType == ResourceType.RegistryKey)
			{
				if (!name.StartsWith("HKEY_"))
				{
					throw new InvalidOperationException();
				}
				name = name.Substring("HKEY_".Length);
			}
			return name;
		}

		// Token: 0x06003453 RID: 13395
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x06003454 RID: 13396
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x06003455 RID: 13397
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06003456 RID: 13398
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetSecurityInfo(SafeHandle handle, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x06003457 RID: 13399
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int SetNamedSecurityInfo(string name, ResourceType resourceType, SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x06003458 RID: 13400
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int GetSecurityDescriptorLength(IntPtr descriptor);

		// Token: 0x06003459 RID: 13401
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsValidSecurityDescriptor(IntPtr descriptor);

		// Token: 0x0400243D RID: 9277
		private NativeObjectSecurity.ExceptionFromErrorCode exception_from_error_code;

		// Token: 0x0400243E RID: 9278
		private ResourceType resource_type;

		// Token: 0x02000504 RID: 1284
		// (Invoke) Token: 0x0600345B RID: 13403
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);

		// Token: 0x02000505 RID: 1285
		// (Invoke) Token: 0x0600345F RID: 13407
		private delegate int GetSecurityInfoNativeCall(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor);

		// Token: 0x02000506 RID: 1286
		// (Invoke) Token: 0x06003463 RID: 13411
		private delegate int SetSecurityInfoNativeCall(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl);

		// Token: 0x02000507 RID: 1287
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06003466 RID: 13414 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06003467 RID: 13415 RVA: 0x000BF90F File Offset: 0x000BDB0F
			internal int <InternalGet>b__0(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetSecurityInfo(this.handle, this.<>4__this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}

			// Token: 0x0400243F RID: 9279
			public SafeHandle handle;

			// Token: 0x04002440 RID: 9280
			public NativeObjectSecurity <>4__this;
		}

		// Token: 0x02000508 RID: 1288
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x06003468 RID: 13416 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06003469 RID: 13417 RVA: 0x000BF930 File Offset: 0x000BDB30
			internal int <InternalGet>b__0(SecurityInfos securityInfos, out IntPtr owner, out IntPtr group, out IntPtr dacl, out IntPtr sacl, out IntPtr descriptor)
			{
				return NativeObjectSecurity.GetNamedSecurityInfo(this.<>4__this.Win32FixName(this.name), this.<>4__this.ResourceType, securityInfos, out owner, out group, out dacl, out sacl, out descriptor);
			}

			// Token: 0x04002441 RID: 9281
			public NativeObjectSecurity <>4__this;

			// Token: 0x04002442 RID: 9282
			public string name;
		}

		// Token: 0x02000509 RID: 1289
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x0600346A RID: 13418 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x0600346B RID: 13419 RVA: 0x000BF95C File Offset: 0x000BDB5C
			internal int <InternalSet>b__0(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl)
			{
				return NativeObjectSecurity.SetSecurityInfo(this.handle, this.<>4__this.ResourceType, securityInfos, owner, group, dacl, sacl);
			}

			// Token: 0x04002443 RID: 9283
			public SafeHandle handle;

			// Token: 0x04002444 RID: 9284
			public NativeObjectSecurity <>4__this;
		}

		// Token: 0x0200050A RID: 1290
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x0600346C RID: 13420 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x0600346D RID: 13421 RVA: 0x000BF97B File Offset: 0x000BDB7B
			internal int <InternalSet>b__0(SecurityInfos securityInfos, byte[] owner, byte[] group, byte[] dacl, byte[] sacl)
			{
				return NativeObjectSecurity.SetNamedSecurityInfo(this.<>4__this.Win32FixName(this.name), this.<>4__this.ResourceType, securityInfos, owner, group, dacl, sacl);
			}

			// Token: 0x04002445 RID: 9285
			public NativeObjectSecurity <>4__this;

			// Token: 0x04002446 RID: 9286
			public string name;
		}
	}
}
