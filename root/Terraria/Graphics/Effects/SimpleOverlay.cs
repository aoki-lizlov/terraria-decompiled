using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001F6 RID: 502
	public class SimpleOverlay : Overlay
	{
		// Token: 0x060020CA RID: 8394 RVA: 0x00523B83 File Offset: 0x00521D83
		public SimpleOverlay(string textureName, ScreenShaderData shader, EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All)
			: base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName, 1);
			this._shader = shader;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00523BBC File Offset: 0x00521DBC
		public SimpleOverlay(string textureName, string shaderName = "Default", EffectPriority priority = EffectPriority.VeryLow, RenderLayers layer = RenderLayers.All)
			: base(priority, layer)
		{
			this._texture = Main.Assets.Request<Texture2D>((textureName == null) ? "" : textureName, 1);
			this._shader = new ScreenShaderData(Main.ScreenShaderRef, shaderName);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00523C0A File Offset: 0x00521E0A
		public ScreenShaderData GetShader()
		{
			return this._shader;
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00523C14 File Offset: 0x00521E14
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._shader.UseGlobalOpacity(this.Opacity);
			this._shader.UseTargetPosition(this.TargetPosition);
			this._shader.Apply();
			spriteBatch.Draw(this._texture.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Main.ColorOfTheSkies);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x00523C77 File Offset: 0x00521E77
		public override void Update(GameTime gameTime)
		{
			this._shader.Update(gameTime);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00523C85 File Offset: 0x00521E85
		public override void Activate(Vector2 position, params object[] args)
		{
			this.TargetPosition = position;
			this.Mode = OverlayMode.FadeIn;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00523C95 File Offset: 0x00521E95
		public override void Deactivate(params object[] args)
		{
			this.Mode = OverlayMode.FadeOut;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00523C9E File Offset: 0x00521E9E
		public override bool IsVisible()
		{
			return this._shader.CombinedOpacity > 0f;
		}

		// Token: 0x04004B35 RID: 19253
		private Asset<Texture2D> _texture;

		// Token: 0x04004B36 RID: 19254
		private ScreenShaderData _shader;

		// Token: 0x04004B37 RID: 19255
		public Vector2 TargetPosition = Vector2.Zero;
	}
}
