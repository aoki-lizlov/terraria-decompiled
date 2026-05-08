using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001F9 RID: 505
	[ComVisible(false)]
	[Serializable]
	public sealed class ActivationContext : IDisposable, ISerializable
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x0005E2C4 File Offset: 0x0005C4C4
		private ActivationContext(ApplicationIdentity identity)
		{
			this._appid = identity;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0005E2D4 File Offset: 0x0005C4D4
		~ActivationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x0005E304 File Offset: 0x0005C504
		public ActivationContext.ContextForm Form
		{
			get
			{
				return this._form;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001855 RID: 6229 RVA: 0x0005E30C File Offset: 0x0005C50C
		public ApplicationIdentity Identity
		{
			get
			{
				return this._appid;
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005E314 File Offset: 0x0005C514
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			return new ActivationContext(identity);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0005E32A File Offset: 0x0005C52A
		[MonoTODO("Missing validation")]
		public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (manifestPaths == null)
			{
				throw new ArgumentNullException("manifestPaths");
			}
			return new ActivationContext(identity);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005E34E File Offset: 0x0005C54E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005E35D File Offset: 0x0005C55D
		private void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				this._disposed = true;
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0005E370 File Offset: 0x0005C570
		[MonoTODO("Missing serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x0400159B RID: 5531
		private ApplicationIdentity _appid;

		// Token: 0x0400159C RID: 5532
		private ActivationContext.ContextForm _form;

		// Token: 0x0400159D RID: 5533
		private bool _disposed;

		// Token: 0x020001FA RID: 506
		public enum ContextForm
		{
			// Token: 0x0400159F RID: 5535
			Loose,
			// Token: 0x040015A0 RID: 5536
			StoreBounded
		}
	}
}
