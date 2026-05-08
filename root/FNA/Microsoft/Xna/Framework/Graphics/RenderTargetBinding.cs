using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B2 RID: 178
	public struct RenderTargetBinding
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0002F917 File Offset: 0x0002DB17
		public Texture RenderTarget
		{
			get
			{
				return this.renderTarget;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0002F91F File Offset: 0x0002DB1F
		public CubeMapFace CubeMapFace
		{
			get
			{
				return this.cubeMapFace;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0002F927 File Offset: 0x0002DB27
		public RenderTargetBinding(RenderTarget2D renderTarget)
		{
			if (renderTarget == null)
			{
				throw new ArgumentNullException("renderTarget");
			}
			this.renderTarget = renderTarget;
			this.cubeMapFace = CubeMapFace.PositiveX;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0002F945 File Offset: 0x0002DB45
		public RenderTargetBinding(RenderTargetCube renderTarget, CubeMapFace cubeMapFace)
		{
			if (renderTarget == null)
			{
				throw new ArgumentNullException("renderTarget");
			}
			if (cubeMapFace < CubeMapFace.PositiveX || cubeMapFace > CubeMapFace.NegativeZ)
			{
				throw new ArgumentOutOfRangeException("cubeMapFace");
			}
			this.renderTarget = renderTarget;
			this.cubeMapFace = cubeMapFace;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0002F976 File Offset: 0x0002DB76
		public static implicit operator RenderTargetBinding(RenderTarget2D renderTarget)
		{
			return new RenderTargetBinding(renderTarget);
		}

		// Token: 0x0400097E RID: 2430
		private readonly Texture renderTarget;

		// Token: 0x0400097F RID: 2431
		private readonly CubeMapFace cubeMapFace;
	}
}
