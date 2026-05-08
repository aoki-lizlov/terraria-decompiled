using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	// Token: 0x020004CC RID: 1228
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		// Token: 0x060032B9 RID: 12985 RVA: 0x000BC017 File Offset: 0x000BA217
		public PrivilegeNotHeldException()
			: base("The process does not possess some privilege required for this operation.")
		{
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000BC024 File Offset: 0x000BA224
		public PrivilegeNotHeldException(string privilege)
			: base(string.Format(CultureInfo.CurrentCulture, "The process does not possess the '{0}' privilege which is required for this operation.", privilege))
		{
			this._privilegeName = privilege;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000BC043 File Offset: 0x000BA243
		public PrivilegeNotHeldException(string privilege, Exception inner)
			: base(string.Format(CultureInfo.CurrentCulture, "The process does not possess the '{0}' privilege which is required for this operation.", privilege), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000BC063 File Offset: 0x000BA263
		private PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000BC07E File Offset: 0x000BA27E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000BC0A3 File Offset: 0x000BA2A3
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		// Token: 0x0400237D RID: 9085
		private readonly string _privilegeName;
	}
}
