using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D3 RID: 2003
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x060045AE RID: 17838 RVA: 0x000E52E2 File Offset: 0x000E34E2
		public RuntimeWrappedException(object thrownObject)
			: base("An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.")
		{
			base.HResult = -2146233026;
			this._wrappedException = thrownObject;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x000E5301 File Offset: 0x000E3501
		private RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x000E5326 File Offset: 0x000E3526
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this._wrappedException, typeof(object));
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x000E534B File Offset: 0x000E354B
		public object WrappedException
		{
			get
			{
				return this._wrappedException;
			}
		}

		// Token: 0x04002CC1 RID: 11457
		private object _wrappedException;
	}
}
