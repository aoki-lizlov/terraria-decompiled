using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x020004BD RID: 1213
	[ComVisible(true)]
	[Serializable]
	public class WindowsIdentity : ClaimsIdentity, IIdentity, IDeserializationCallback, ISerializable, IDisposable
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x000B9061 File Offset: 0x000B7261
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken)
			: this(userToken, null, WindowsAccountType.Normal, false)
		{
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000B906D File Offset: 0x000B726D
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type)
			: this(userToken, type, WindowsAccountType.Normal, false)
		{
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000B9079 File Offset: 0x000B7279
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType)
			: this(userToken, type, acctType, false)
		{
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000B9085 File Offset: 0x000B7285
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(IntPtr userToken, string type, WindowsAccountType acctType, bool isAuthenticated)
		{
			this._type = type;
			this._account = acctType;
			this._authenticated = isAuthenticated;
			this._name = null;
			this.SetToken(userToken);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000B90B1 File Offset: 0x000B72B1
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(string sUserPrincipalName)
			: this(sUserPrincipalName, null)
		{
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000B90BC File Offset: 0x000B72BC
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(string sUserPrincipalName, string type)
		{
			if (sUserPrincipalName == null)
			{
				throw new NullReferenceException("sUserPrincipalName");
			}
			IntPtr userToken = WindowsIdentity.GetUserToken(sUserPrincipalName);
			if (!Environment.IsUnix && userToken == IntPtr.Zero)
			{
				throw new ArgumentException("only for Windows Server 2003 +");
			}
			this._authenticated = true;
			this._account = WindowsAccountType.Normal;
			this._type = type;
			this.SetToken(userToken);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000B911F File Offset: 0x000B731F
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public WindowsIdentity(SerializationInfo info, StreamingContext context)
		{
			this._info = info;
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000B912E File Offset: 0x000B732E
		internal WindowsIdentity(ClaimsIdentity claimsIdentity, IntPtr userToken)
			: base(claimsIdentity)
		{
			if (userToken != IntPtr.Zero && userToken.ToInt64() > 0L)
			{
				this.SetToken(userToken);
			}
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000B9156 File Offset: 0x000B7356
		[ComVisible(false)]
		public void Dispose()
		{
			this._token = IntPtr.Zero;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000B9156 File Offset: 0x000B7356
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			this._token = IntPtr.Zero;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000B9164 File Offset: 0x000B7364
		public static WindowsIdentity GetAnonymous()
		{
			WindowsIdentity windowsIdentity;
			if (Environment.IsUnix)
			{
				windowsIdentity = new WindowsIdentity("nobody");
				windowsIdentity._account = WindowsAccountType.Anonymous;
				windowsIdentity._authenticated = false;
				windowsIdentity._type = string.Empty;
			}
			else
			{
				windowsIdentity = new WindowsIdentity(IntPtr.Zero, string.Empty, WindowsAccountType.Anonymous, false);
				windowsIdentity._name = string.Empty;
			}
			return windowsIdentity;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000B91BE File Offset: 0x000B73BE
		public static WindowsIdentity GetCurrent()
		{
			return new WindowsIdentity(WindowsIdentity.GetCurrentToken(), null, WindowsAccountType.Normal, true);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(bool ifImpersonating)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("need icall changes")]
		public static WindowsIdentity GetCurrent(TokenAccessLevels desiredAccess)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000B91CD File Offset: 0x000B73CD
		public virtual WindowsImpersonationContext Impersonate()
		{
			return new WindowsImpersonationContext(this._token);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000B91DA File Offset: 0x000B73DA
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public static WindowsImpersonationContext Impersonate(IntPtr userToken)
		{
			return new WindowsImpersonationContext(userToken);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000174FB File Offset: 0x000156FB
		[SecuritySafeCritical]
		public static void RunImpersonated(SafeAccessTokenHandle safeAccessTokenHandle, Action action)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000174FB File Offset: 0x000156FB
		[SecuritySafeCritical]
		public static T RunImpersonated<T>(SafeAccessTokenHandle safeAccessTokenHandle, Func<T> func)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000B91E2 File Offset: 0x000B73E2
		public sealed override string AuthenticationType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000B91EA File Offset: 0x000B73EA
		public virtual bool IsAnonymous
		{
			get
			{
				return this._account == WindowsAccountType.Anonymous;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000B91F5 File Offset: 0x000B73F5
		public override bool IsAuthenticated
		{
			get
			{
				return this._authenticated;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000B91FD File Offset: 0x000B73FD
		public virtual bool IsGuest
		{
			get
			{
				return this._account == WindowsAccountType.Guest;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000B9208 File Offset: 0x000B7408
		public virtual bool IsSystem
		{
			get
			{
				return this._account == WindowsAccountType.System;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000B9213 File Offset: 0x000B7413
		public override string Name
		{
			get
			{
				if (this._name == null)
				{
					this._name = WindowsIdentity.GetTokenName(this._token);
				}
				return this._name;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000B9234 File Offset: 0x000B7434
		public virtual IntPtr Token
		{
			get
			{
				return this._token;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("not implemented")]
		public IdentityReferenceCollection Groups
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public SecurityIdentifier Owner
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public SecurityIdentifier User
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000B923C File Offset: 0x000B743C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this._token = (IntPtr)this._info.GetValue("m_userToken", typeof(IntPtr));
			this._name = this._info.GetString("m_name");
			if (this._name != null)
			{
				if (WindowsIdentity.GetTokenName(this._token) != this._name)
				{
					throw new SerializationException("Token-Name mismatch.");
				}
			}
			else
			{
				this._name = WindowsIdentity.GetTokenName(this._token);
				if (this._name == null)
				{
					throw new SerializationException("Token doesn't match a user.");
				}
			}
			this._type = this._info.GetString("m_type");
			this._account = (WindowsAccountType)this._info.GetValue("m_acctType", typeof(WindowsAccountType));
			this._authenticated = this._info.GetBoolean("m_isAuthenticated");
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000B9324 File Offset: 0x000B7524
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("m_userToken", this._token);
			info.AddValue("m_name", this._name);
			info.AddValue("m_type", this._type);
			info.AddValue("m_acctType", this._account);
			info.AddValue("m_isAuthenticated", this._authenticated);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000B9390 File Offset: 0x000B7590
		internal ClaimsIdentity CloneAsBase()
		{
			return base.Clone();
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000B9234 File Offset: 0x000B7434
		internal IntPtr GetTokenInternal()
		{
			return this._token;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000B9398 File Offset: 0x000B7598
		private void SetToken(IntPtr token)
		{
			if (Environment.IsUnix)
			{
				this._token = token;
				if (this._type == null)
				{
					this._type = "POSIX";
				}
				if (this._token == IntPtr.Zero)
				{
					this._account = WindowsAccountType.System;
					return;
				}
			}
			else
			{
				if (token == WindowsIdentity.invalidWindows && this._account != WindowsAccountType.Anonymous)
				{
					throw new ArgumentException("Invalid token");
				}
				this._token = token;
				if (this._type == null)
				{
					this._type = "NTLM";
				}
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000174FB File Offset: 0x000156FB
		public SafeAccessTokenHandle AccessToken
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06003211 RID: 12817
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] _GetRoles(IntPtr token);

		// Token: 0x06003212 RID: 12818
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCurrentToken();

		// Token: 0x06003213 RID: 12819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetTokenName(IntPtr token);

		// Token: 0x06003214 RID: 12820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUserToken(string username);

		// Token: 0x06003215 RID: 12821 RVA: 0x000B941B File Offset: 0x000B761B
		// Note: this type is marked as 'beforefieldinit'.
		static WindowsIdentity()
		{
		}

		// Token: 0x040022C1 RID: 8897
		private IntPtr _token;

		// Token: 0x040022C2 RID: 8898
		private string _type;

		// Token: 0x040022C3 RID: 8899
		private WindowsAccountType _account;

		// Token: 0x040022C4 RID: 8900
		private bool _authenticated;

		// Token: 0x040022C5 RID: 8901
		private string _name;

		// Token: 0x040022C6 RID: 8902
		private SerializationInfo _info;

		// Token: 0x040022C7 RID: 8903
		private static IntPtr invalidWindows = IntPtr.Zero;

		// Token: 0x040022C8 RID: 8904
		[NonSerialized]
		public new const string DefaultIssuer = "AD AUTHORITY";
	}
}
