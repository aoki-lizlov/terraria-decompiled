using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001E6 RID: 486
	[ComVisible(true)]
	[Serializable]
	public class TypeLoadException : SystemException, ISerializable
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x0005B382 File Offset: 0x00059582
		public TypeLoadException()
			: base(Environment.GetResourceString("Failure has occurred while loading a type."))
		{
			base.SetErrorCode(-2146233054);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005B39F File Offset: 0x0005959F
		public TypeLoadException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233054);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005B3B3 File Offset: 0x000595B3
		public TypeLoadException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233054);
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0005B3C8 File Offset: 0x000595C8
		public override string Message
		{
			[SecuritySafeCritical]
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005B3D8 File Offset: 0x000595D8
		[SecurityCritical]
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this.ClassName == null && this.ResourceId == 0)
				{
					this._message = Environment.GetResourceString("Failure has occurred while loading a type.");
					return;
				}
				if (this.AssemblyName == null)
				{
					this.AssemblyName = Environment.GetResourceString("[Unknown]");
				}
				if (this.ClassName == null)
				{
					this.ClassName = Environment.GetResourceString("[Unknown]");
				}
				string text = "Could not load type '{0}' from assembly '{1}'.";
				this._message = string.Format(CultureInfo.CurrentCulture, text, this.ClassName, this.AssemblyName, this.MessageArg);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0005B469 File Offset: 0x00059669
		public string TypeName
		{
			get
			{
				if (this.ClassName == null)
				{
					return string.Empty;
				}
				return this.ClassName;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005B47F File Offset: 0x0005967F
		private TypeLoadException(string className, string assemblyName)
			: this(className, assemblyName, null, 0)
		{
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005B48B File Offset: 0x0005968B
		[SecurityCritical]
		private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
			: base(null)
		{
			base.SetErrorCode(-2146233054);
			this.ClassName = className;
			this.AssemblyName = assemblyName;
			this.MessageArg = messageArg;
			this.ResourceId = resourceId;
			this.SetMessageField();
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005B4C4 File Offset: 0x000596C4
		protected TypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.ClassName = info.GetString("TypeLoadClassName");
			this.AssemblyName = info.GetString("TypeLoadAssemblyName");
			this.MessageArg = info.GetString("TypeLoadMessageArg");
			this.ResourceId = info.GetInt32("TypeLoadResourceID");
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0005B52C File Offset: 0x0005972C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this.ClassName, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this.AssemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", this.MessageArg, typeof(string));
			info.AddValue("TypeLoadResourceID", this.ResourceId);
		}

		// Token: 0x04001517 RID: 5399
		private string ClassName;

		// Token: 0x04001518 RID: 5400
		private string AssemblyName;

		// Token: 0x04001519 RID: 5401
		private string MessageArg;

		// Token: 0x0400151A RID: 5402
		internal int ResourceId;
	}
}
