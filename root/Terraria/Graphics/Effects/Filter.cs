using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001ED RID: 493
	public class Filter : GameEffect
	{
		// Token: 0x060020A3 RID: 8355 RVA: 0x00523198 File Offset: 0x00521398
		public Filter(ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow)
		{
			this._shader = shader;
			this._priority = priority;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x005231AE File Offset: 0x005213AE
		public void Update(GameTime gameTime)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.Update(gameTime);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x005231CE File Offset: 0x005213CE
		public void Apply(Vector2 textureSize, Vector2 sceneSize, Vector2 sceneOffset)
		{
			this._shader.UseSceneSize(sceneSize).UseSceneOffset(sceneOffset).UseImageSize0(textureSize)
				.Apply();
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x005231ED File Offset: 0x005213ED
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x005231F5 File Offset: 0x005213F5
		public override void Activate(Vector2 position, params object[] args)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(position);
			this.Active = true;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0052321D File Offset: 0x0052141D
		public override void Deactivate(params object[] args)
		{
			this.Active = false;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00523226 File Offset: 0x00521426
		public bool IsInUse()
		{
			return this.Active || this.Opacity > 0f;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0052323F File Offset: 0x0052143F
		public bool IsActive()
		{
			return this.Active;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00523247 File Offset: 0x00521447
		public override bool IsVisible()
		{
			return this.GetShader().CombinedOpacity > 0f && !this.IsHidden;
		}

		// Token: 0x04004B1C RID: 19228
		public bool Active;

		// Token: 0x04004B1D RID: 19229
		private ScreenShaderData _shader;

		// Token: 0x04004B1E RID: 19230
		public bool IsHidden;
	}
}
