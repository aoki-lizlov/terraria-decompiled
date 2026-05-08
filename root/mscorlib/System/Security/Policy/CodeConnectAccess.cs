using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003D6 RID: 982
	[ComVisible(true)]
	[Serializable]
	public class CodeConnectAccess
	{
		// Token: 0x060029C7 RID: 10695 RVA: 0x00098AE4 File Offset: 0x00096CE4
		[MonoTODO("(2.0) validations incomplete")]
		public CodeConnectAccess(string allowScheme, int allowPort)
		{
			if (allowScheme == null || allowScheme.Length == 0)
			{
				throw new ArgumentOutOfRangeException("allowScheme");
			}
			if (allowPort < 0 || allowPort > 65535)
			{
				throw new ArgumentOutOfRangeException("allowPort");
			}
			this._scheme = allowScheme;
			this._port = allowPort;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x00098B32 File Offset: 0x00096D32
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x00098B3A File Offset: 0x00096D3A
		public string Scheme
		{
			get
			{
				return this._scheme;
			}
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00098B44 File Offset: 0x00096D44
		public override bool Equals(object o)
		{
			CodeConnectAccess codeConnectAccess = o as CodeConnectAccess;
			return codeConnectAccess != null && this._scheme == codeConnectAccess._scheme && this._port == codeConnectAccess._port;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00098B80 File Offset: 0x00096D80
		public override int GetHashCode()
		{
			return this._scheme.GetHashCode() ^ this._port;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00098B94 File Offset: 0x00096D94
		public static CodeConnectAccess CreateAnySchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.AnyScheme, allowPort);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00098BA1 File Offset: 0x00096DA1
		public static CodeConnectAccess CreateOriginSchemeAccess(int allowPort)
		{
			return new CodeConnectAccess(CodeConnectAccess.OriginScheme, allowPort);
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x00098BAE File Offset: 0x00096DAE
		// Note: this type is marked as 'beforefieldinit'.
		static CodeConnectAccess()
		{
		}

		// Token: 0x04001E2F RID: 7727
		public static readonly string AnyScheme = "*";

		// Token: 0x04001E30 RID: 7728
		public static readonly int DefaultPort = -3;

		// Token: 0x04001E31 RID: 7729
		public static readonly int OriginPort = -4;

		// Token: 0x04001E32 RID: 7730
		public static readonly string OriginScheme = "$origin";

		// Token: 0x04001E33 RID: 7731
		private string _scheme;

		// Token: 0x04001E34 RID: 7732
		private int _port;
	}
}
