using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000422 RID: 1058
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002C7F RID: 11391 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public ReflectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000A1094 File Offset: 0x0009F294
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000A109C File Offset: 0x0009F29C
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
				this.memberAccess = (this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess;
				this.reflectionEmit = (this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit;
				this.typeInfo = (this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000A10D8 File Offset: 0x0009F2D8
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x000A10E0 File Offset: 0x0009F2E0
		public bool MemberAccess
		{
			get
			{
				return this.memberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.MemberAccess;
				}
				else
				{
					this.flags -= 2;
				}
				this.memberAccess = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000A110A File Offset: 0x0009F30A
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000A1112 File Offset: 0x0009F312
		[Obsolete]
		public bool ReflectionEmit
		{
			get
			{
				return this.reflectionEmit;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.ReflectionEmit;
				}
				else
				{
					this.flags -= 4;
				}
				this.reflectionEmit = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000A113C File Offset: 0x0009F33C
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000A1149 File Offset: 0x0009F349
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.flags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.RestrictedMemberAccess;
					return;
				}
				this.flags -= 8;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000A116B File Offset: 0x0009F36B
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x000A1173 File Offset: 0x0009F373
		[Obsolete("not enforced in 2.0+")]
		public bool TypeInformation
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.TypeInformation;
				}
				else
				{
					this.flags--;
				}
				this.typeInfo = value;
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000A11A0 File Offset: 0x0009F3A0
		public override IPermission CreatePermission()
		{
			ReflectionPermission reflectionPermission;
			if (base.Unrestricted)
			{
				reflectionPermission = new ReflectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				reflectionPermission = new ReflectionPermission(this.flags);
			}
			return reflectionPermission;
		}

		// Token: 0x04001F46 RID: 8006
		private ReflectionPermissionFlag flags;

		// Token: 0x04001F47 RID: 8007
		private bool memberAccess;

		// Token: 0x04001F48 RID: 8008
		private bool reflectionEmit;

		// Token: 0x04001F49 RID: 8009
		private bool typeInfo;
	}
}
