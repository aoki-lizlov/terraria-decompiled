using System;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A9 RID: 169
	public class OcclusionQuery : GraphicsResource
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0002E168 File Offset: 0x0002C368
		public bool IsComplete
		{
			get
			{
				return FNA3D.FNA3D_QueryComplete(base.GraphicsDevice.GLDevice, this.query) == 1;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0002E183 File Offset: 0x0002C383
		public int PixelCount
		{
			get
			{
				return FNA3D.FNA3D_QueryPixelCount(base.GraphicsDevice.GLDevice, this.query);
			}
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0002E19B File Offset: 0x0002C39B
		public OcclusionQuery(GraphicsDevice graphicsDevice)
		{
			base.GraphicsDevice = graphicsDevice;
			this.query = FNA3D.FNA3D_CreateQuery(base.GraphicsDevice.GLDevice);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0002E1C0 File Offset: 0x0002C3C0
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this.query, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeQuery(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0002E20B File Offset: 0x0002C40B
		public void Begin()
		{
			FNA3D.FNA3D_QueryBegin(base.GraphicsDevice.GLDevice, this.query);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0002E223 File Offset: 0x0002C423
		public void End()
		{
			FNA3D.FNA3D_QueryEnd(base.GraphicsDevice.GLDevice, this.query);
		}

		// Token: 0x04000916 RID: 2326
		private IntPtr query;
	}
}
