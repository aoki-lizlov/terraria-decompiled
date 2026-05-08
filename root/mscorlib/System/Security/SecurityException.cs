using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	// Token: 0x020003B8 RID: 952
	[ComVisible(true)]
	[Serializable]
	public class SecurityException : SystemException
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x00095BF4 File Offset: 0x00093DF4
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x00095BFC File Offset: 0x00093DFC
		[ComVisible(false)]
		public SecurityAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				this._action = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x00095C05 File Offset: 0x00093E05
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x00095C0D File Offset: 0x00093E0D
		[ComVisible(false)]
		public object DenySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._denyset;
			}
			set
			{
				this._denyset = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x00095C16 File Offset: 0x00093E16
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x00095C1E File Offset: 0x00093E1E
		[ComVisible(false)]
		public AssemblyName FailedAssemblyInfo
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x00095C27 File Offset: 0x00093E27
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x00095C2F File Offset: 0x00093E2F
		[ComVisible(false)]
		public MethodInfo Method
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._method;
			}
			set
			{
				this._method = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x00095C38 File Offset: 0x00093E38
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x00095C40 File Offset: 0x00093E40
		[ComVisible(false)]
		public object PermitOnlySetInstance
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._permitset;
			}
			set
			{
				this._permitset = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x00095C49 File Offset: 0x00093E49
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x00095C51 File Offset: 0x00093E51
		public string Url
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x00095C5A File Offset: 0x00093E5A
		// (set) Token: 0x060028F0 RID: 10480 RVA: 0x00095C62 File Offset: 0x00093E62
		public SecurityZone Zone
		{
			get
			{
				return this._zone;
			}
			set
			{
				this._zone = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x00095C6B File Offset: 0x00093E6B
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x00095C73 File Offset: 0x00093E73
		[ComVisible(false)]
		public object Demanded
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._demanded;
			}
			set
			{
				this._demanded = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x00095C7C File Offset: 0x00093E7C
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x00095C84 File Offset: 0x00093E84
		public IPermission FirstPermissionThatFailed
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._firstperm;
			}
			set
			{
				this._firstperm = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x00095C8D File Offset: 0x00093E8D
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x00095C95 File Offset: 0x00093E95
		public string PermissionState
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this.permissionState;
			}
			set
			{
				this.permissionState = value;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x00095C9E File Offset: 0x00093E9E
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x00095CA6 File Offset: 0x00093EA6
		public Type PermissionType
		{
			get
			{
				return this.permissionType;
			}
			set
			{
				this.permissionType = value;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x00095CAF File Offset: 0x00093EAF
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x00095CB7 File Offset: 0x00093EB7
		public string GrantedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._granted;
			}
			set
			{
				this._granted = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x00095CC0 File Offset: 0x00093EC0
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x00095CC8 File Offset: 0x00093EC8
		public string RefusedSet
		{
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true, ControlPolicy = true)]
			get
			{
				return this._refused;
			}
			set
			{
				this._refused = value;
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00095CD1 File Offset: 0x00093ED1
		public SecurityException()
			: this(Locale.GetText("A security error has been detected."))
		{
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00095CE3 File Offset: 0x00093EE3
		public SecurityException(string message)
			: base(message)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00095CF8 File Offset: 0x00093EF8
		protected SecurityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			base.HResult = -2146233078;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "PermissionState")
				{
					this.permissionState = (string)enumerator.Value;
					return;
				}
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00095D4D File Offset: 0x00093F4D
		public SecurityException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233078;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00095D62 File Offset: 0x00093F62
		public SecurityException(string message, Type type)
			: base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00095D7D File Offset: 0x00093F7D
		public SecurityException(string message, Type type, string state)
			: base(message)
		{
			base.HResult = -2146233078;
			this.permissionType = type;
			this.permissionState = state;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00095D9F File Offset: 0x00093F9F
		internal SecurityException(string message, PermissionSet granted, PermissionSet refused)
			: base(message)
		{
			base.HResult = -2146233078;
			this._granted = granted.ToString();
			this._refused = refused.ToString();
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00095DCB File Offset: 0x00093FCB
		public SecurityException(string message, object deny, object permitOnly, MethodInfo method, object demanded, IPermission permThatFailed)
			: base(message)
		{
			base.HResult = -2146233078;
			this._denyset = deny;
			this._permitset = permitOnly;
			this._method = method;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x00095E08 File Offset: 0x00094008
		public SecurityException(string message, AssemblyName assemblyName, PermissionSet grant, PermissionSet refused, MethodInfo method, SecurityAction action, object demanded, IPermission permThatFailed, Evidence evidence)
			: base(message)
		{
			base.HResult = -2146233078;
			this._assembly = assemblyName;
			this._granted = ((grant == null) ? string.Empty : grant.ToString());
			this._refused = ((refused == null) ? string.Empty : refused.ToString());
			this._method = method;
			this._action = action;
			this._demanded = demanded;
			this._firstperm = permThatFailed;
			if (this._firstperm != null)
			{
				this.permissionType = this._firstperm.GetType();
			}
			this._evidence = evidence;
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00095EA0 File Offset: 0x000940A0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			try
			{
				info.AddValue("PermissionState", this.permissionState);
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00095EDC File Offset: 0x000940DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			try
			{
				if (this.permissionType != null)
				{
					stringBuilder.AppendFormat("{0}Type: {1}", Environment.NewLine, this.PermissionType);
				}
				if (this._method != null)
				{
					string text = this._method.ToString();
					int num = text.IndexOf(" ") + 1;
					stringBuilder.AppendFormat("{0}Method: {1} {2}.{3}", new object[]
					{
						Environment.NewLine,
						this._method.ReturnType.Name,
						this._method.ReflectedType,
						text.Substring(num)
					});
				}
				if (this.permissionState != null)
				{
					stringBuilder.AppendFormat("{0}State: {1}", Environment.NewLine, this.PermissionState);
				}
				if (this._granted != null && this._granted.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Granted: {1}", Environment.NewLine, this.GrantedSet);
				}
				if (this._refused != null && this._refused.Length > 0)
				{
					stringBuilder.AppendFormat("{0}Refused: {1}", Environment.NewLine, this.RefusedSet);
				}
				if (this._demanded != null)
				{
					stringBuilder.AppendFormat("{0}Demanded: {1}", Environment.NewLine, this.Demanded);
				}
				if (this._firstperm != null)
				{
					stringBuilder.AppendFormat("{0}Failed Permission: {1}", Environment.NewLine, this.FirstPermissionThatFailed);
				}
				if (this._evidence != null)
				{
					stringBuilder.AppendFormat("{0}Evidences:", Environment.NewLine);
					foreach (object obj in this._evidence)
					{
						if (!(obj is Hash))
						{
							stringBuilder.AppendFormat("{0}\t{1}", Environment.NewLine, obj);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001DAC RID: 7596
		private string permissionState;

		// Token: 0x04001DAD RID: 7597
		private Type permissionType;

		// Token: 0x04001DAE RID: 7598
		private string _granted;

		// Token: 0x04001DAF RID: 7599
		private string _refused;

		// Token: 0x04001DB0 RID: 7600
		private object _demanded;

		// Token: 0x04001DB1 RID: 7601
		private IPermission _firstperm;

		// Token: 0x04001DB2 RID: 7602
		private MethodInfo _method;

		// Token: 0x04001DB3 RID: 7603
		private Evidence _evidence;

		// Token: 0x04001DB4 RID: 7604
		private SecurityAction _action;

		// Token: 0x04001DB5 RID: 7605
		private object _denyset;

		// Token: 0x04001DB6 RID: 7606
		private object _permitset;

		// Token: 0x04001DB7 RID: 7607
		private AssemblyName _assembly;

		// Token: 0x04001DB8 RID: 7608
		private string _url;

		// Token: 0x04001DB9 RID: 7609
		private SecurityZone _zone;
	}
}
