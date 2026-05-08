using System;

namespace System
{
	// Token: 0x02000130 RID: 304
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00032841 File Offset: 0x00030A41
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00032857 File Offset: 0x00030A57
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003286D File Offset: 0x00030A6D
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00032883 File Offset: 0x00030A83
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0003288B File Offset: 0x00030A8B
		public bool IsError
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x04001129 RID: 4393
		private string _message;

		// Token: 0x0400112A RID: 4394
		private bool _error;
	}
}
