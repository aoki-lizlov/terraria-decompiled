using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000298 RID: 664
	public class WaterShaderData : ScreenShaderData
	{
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002543 RID: 9539 RVA: 0x00554388 File Offset: 0x00552588
		// (remove) Token: 0x06002544 RID: 9540 RVA: 0x005543C0 File Offset: 0x005525C0
		public event Action<TileBatch> OnWaveDraw
		{
			[CompilerGenerated]
			add
			{
				Action<TileBatch> action = this.OnWaveDraw;
				Action<TileBatch> action2;
				do
				{
					action2 = action;
					Action<TileBatch> action3 = (Action<TileBatch>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<TileBatch>>(ref this.OnWaveDraw, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<TileBatch> action = this.OnWaveDraw;
				Action<TileBatch> action2;
				do
				{
					action2 = action;
					Action<TileBatch> action3 = (Action<TileBatch>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<TileBatch>>(ref this.OnWaveDraw, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x005543F8 File Offset: 0x005525F8
		public WaterShaderData(string passName)
			: base(passName)
		{
			Main.OnRenderTargetsInitialized += this.InitRenderTargets;
			Main.OnRenderTargetsReleased += this.ReleaseRenderTargets;
			this._rippleShapeTexture = Main.Assets.Request<Texture2D>("Images/Misc/Ripples", 1);
			Main.OnPreDraw += this.PreDraw;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x005544BC File Offset: 0x005526BC
		public override void Update(GameTime gameTime)
		{
			this._useViscosityFilter = Main.WaveQuality >= 3;
			this._useProjectileWaves = Main.WaveQuality >= 3;
			this._usePlayerWaves = Main.WaveQuality >= 2;
			this._useRippleWaves = Main.WaveQuality >= 2;
			this._useCustomWaves = Main.WaveQuality >= 2;
			if (FocusHelper.PauseLiquidRenderer)
			{
				return;
			}
			this._progress += (float)gameTime.ElapsedGameTime.TotalSeconds * base.Intensity * 0.75f;
			this._progress %= 86400f;
			if (this._useProjectileWaves || this._useRippleWaves || this._useCustomWaves || this._usePlayerWaves)
			{
				this._queuedSteps++;
			}
			base.Update(gameTime);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x00554598 File Offset: 0x00552798
		private void StepLiquids()
		{
			this._isWaveBufferDirty = true;
			Vector2 vector = (Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			Vector2 vector2 = vector - Main.screenPosition;
			TileBatch tileBatch = Main.tileBatch;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			graphicsDevice.SetRenderTarget(this._distortionTarget);
			if (this._clearNextFrame)
			{
				graphicsDevice.Clear(new Color(0.5f, 0.5f, 0f, 1f));
				this._clearNextFrame = false;
			}
			this.DrawWaves();
			graphicsDevice.SetRenderTarget(this._distortionTargetSwap);
			graphicsDevice.Clear(new Color(0.5f, 0.5f, 0.5f, 1f));
			Main.tileBatch.Begin();
			vector2 *= 0.25f;
			vector2.X = (float)Math.Floor((double)vector2.X);
			vector2.Y = (float)Math.Floor((double)vector2.Y);
			Vector2 vector3 = vector2 - this._lastDistortionDrawOffset;
			this._lastDistortionDrawOffset = vector2;
			tileBatch.Draw(this._distortionTarget, new Vector4(vector3.X, vector3.Y, (float)this._distortionTarget.Width, (float)this._distortionTarget.Height), new VertexColors(Color.White));
			GameShaders.Misc["WaterProcessor"].Apply(new DrawData?(new DrawData(this._distortionTarget, Vector2.Zero, Color.White)));
			tileBatch.End();
			RenderTarget2D distortionTarget = this._distortionTarget;
			this._distortionTarget = this._distortionTargetSwap;
			this._distortionTargetSwap = distortionTarget;
			if (this._useViscosityFilter)
			{
				LiquidRenderer.Instance.SetWaveMaskData(ref this._viscosityMaskChain[this._activeViscosityMask]);
				tileBatch.Begin();
				Rectangle cachedDrawArea = LiquidRenderer.Instance.GetCachedDrawArea();
				Rectangle rectangle = new Rectangle(0, 0, cachedDrawArea.Height, cachedDrawArea.Width);
				Vector4 vector4 = new Vector4((float)(cachedDrawArea.X + cachedDrawArea.Width), (float)cachedDrawArea.Y, (float)cachedDrawArea.Height, (float)cachedDrawArea.Width);
				vector4 *= 16f;
				vector4.X -= vector.X;
				vector4.Y -= vector.Y;
				vector4 *= 0.25f;
				vector4.X += vector2.X;
				vector4.Y += vector2.Y;
				graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
				tileBatch.Draw(this._viscosityMaskChain[this._activeViscosityMask], vector4, new Rectangle?(rectangle), new VertexColors(Color.White), rectangle.Size(), SpriteEffects.FlipHorizontally, -1.5707964f);
				tileBatch.End();
				this._activeViscosityMask++;
				this._activeViscosityMask %= this._viscosityMaskChain.Length;
			}
			graphicsDevice.SetRenderTarget(null);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0055488C File Offset: 0x00552A8C
		private void DrawWaves()
		{
			Vector2 screenPosition = Main.screenPosition;
			Vector2 vector = (Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			Vector2 vector2 = -this._lastDistortionDrawOffset / 0.25f + vector;
			TileBatch tileBatch = Main.tileBatch;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			Vector2 vector3 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
			Vector2 vector4 = new Vector2(16f, 16f);
			tileBatch.Begin();
			GameShaders.Misc["WaterDistortionObject"].Apply(null);
			if (this._useNPCWaves)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (Main.npc[i] != null && Main.npc[i].active && (Main.npc[i].wet || Main.npc[i].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, vector3, Main.npc[i].position - vector4, Main.npc[i].Size + vector4))
					{
						NPC npc = Main.npc[i];
						Vector2 vector5 = npc.Center - vector2;
						Vector2 vector6 = npc.velocity.RotatedBy((double)(-(double)npc.rotation), default(Vector2)) / new Vector2((float)npc.height, (float)npc.width);
						float num = vector6.LengthSquared();
						num = num * 0.3f + 0.7f * num * (1024f / (float)(npc.height * npc.width));
						num = Math.Min(num, 0.08f);
						num += (npc.velocity - npc.oldVelocity).Length() * 0.5f;
						vector6.Normalize();
						Vector2 velocity = npc.velocity;
						velocity.Normalize();
						vector5 -= velocity * 10f;
						if (!this._useViscosityFilter && (npc.honeyWet || npc.lavaWet))
						{
							num *= 0.3f;
						}
						if (npc.wet)
						{
							tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector5.X, vector5.Y, (float)npc.width * 2f, (float)npc.height * 2f) * 0.25f, null, new VertexColors(new Color(vector6.X * 0.5f + 0.5f, vector6.Y * 0.5f + 0.5f, 0.5f * num)), new Vector2((float)TextureAssets.MagicPixel.Width() / 2f, (float)TextureAssets.MagicPixel.Height() / 2f), SpriteEffects.None, npc.rotation);
						}
						if (npc.wetCount != 0)
						{
							num = npc.velocity.Length();
							num = 0.195f * (float)Math.Sqrt((double)num);
							float num2 = 5f;
							if (!npc.wet)
							{
								num2 = -20f;
							}
							this.QueueRipple(npc.Center + velocity * num2, new Color(0.5f, (npc.wet ? num : (-num)) * 0.5f + 0.5f, 0f, 1f) * 0.5f, new Vector2((float)npc.width, (float)npc.height * ((float)npc.wetCount / 9f)) * MathHelper.Clamp(num * 10f, 0f, 1f), RippleShape.Circle, 0f);
						}
					}
				}
			}
			if (this._usePlayerWaves)
			{
				for (int j = 0; j < 255; j++)
				{
					if (Main.player[j] != null && Main.player[j].active && (Main.player[j].wet || Main.player[j].wetCount != 0) && Collision.CheckAABBvAABBCollision(screenPosition, vector3, Main.player[j].position - vector4, Main.player[j].Size + vector4))
					{
						Player player = Main.player[j];
						Vector2 vector7 = player.Center - vector2;
						float num3 = player.velocity.Length();
						num3 = 0.05f * (float)Math.Sqrt((double)num3);
						Vector2 velocity2 = player.velocity;
						velocity2.Normalize();
						vector7 -= velocity2 * 10f;
						if (!this._useViscosityFilter && (player.honeyWet || player.lavaWet))
						{
							num3 *= 0.3f;
						}
						if (player.wet)
						{
							tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector7.X - (float)player.width * 2f * 0.5f, vector7.Y - (float)player.height * 2f * 0.5f, (float)player.width * 2f, (float)player.height * 2f) * 0.25f, new VertexColors(new Color(velocity2.X * 0.5f + 0.5f, velocity2.Y * 0.5f + 0.5f, 0.5f * num3)));
						}
						if (player.wetCount != 0)
						{
							float num4 = 5f;
							if (!player.wet)
							{
								num4 = -20f;
							}
							num3 *= 3f;
							this.QueueRipple(player.Center + velocity2 * num4, player.wet ? num3 : (-num3), new Vector2((float)player.width, (float)player.height * ((float)player.wetCount / 9f)) * MathHelper.Clamp(num3 * 10f, 0f, 1f), RippleShape.Circle, 0f);
						}
					}
				}
			}
			if (this._useProjectileWaves)
			{
				for (int k = 0; k < 1000; k++)
				{
					Projectile projectile = Main.projectile[k];
					if (projectile.wet && !projectile.lavaWet)
					{
						bool flag = !projectile.honeyWet;
					}
					bool flag2 = projectile.lavaWet;
					bool flag3 = projectile.honeyWet;
					bool flag4 = projectile.wet;
					if (projectile.ignoreWater)
					{
						flag4 = true;
					}
					if (projectile != null && projectile.active && ProjectileID.Sets.CanDistortWater[projectile.type] && flag4 && !ProjectileID.Sets.NoLiquidDistortion[projectile.type] && Collision.CheckAABBvAABBCollision(screenPosition, vector3, projectile.position - vector4, projectile.Size + vector4))
					{
						if (projectile.ignoreWater)
						{
							bool flag5 = Collision.LavaCollision(projectile.position, projectile.width, projectile.height);
							flag2 = Collision.WetCollision(projectile.position, projectile.width, projectile.height);
							flag3 = Collision.honey;
							flag4 = flag5 || flag2 || flag3;
							if (!flag4)
							{
								goto IL_0879;
							}
						}
						Vector2 vector8 = projectile.Center - vector2;
						float num5 = projectile.velocity.Length();
						num5 = 2f * (float)Math.Sqrt((double)(0.05f * num5));
						Vector2 velocity3 = projectile.velocity;
						velocity3.Normalize();
						if (!this._useViscosityFilter && (flag3 || flag2))
						{
							num5 *= 0.3f;
						}
						float num6 = Math.Max(12f, (float)projectile.width * 0.75f);
						float num7 = Math.Max(12f, (float)projectile.height * 0.75f);
						tileBatch.Draw(TextureAssets.MagicPixel.Value, new Vector4(vector8.X - num6 * 0.5f, vector8.Y - num7 * 0.5f, num6, num7) * 0.25f, new VertexColors(new Color(velocity3.X * 0.5f + 0.5f, velocity3.Y * 0.5f + 0.5f, num5 * 0.5f)));
					}
					IL_0879:;
				}
			}
			tileBatch.End();
			if (this._useRippleWaves)
			{
				tileBatch.Begin();
				for (int l = 0; l < this._rippleQueueCount; l++)
				{
					Vector2 vector9 = this._rippleQueue[l].Position - vector2;
					Vector2 size = this._rippleQueue[l].Size;
					Rectangle sourceRectangle = this._rippleQueue[l].SourceRectangle;
					Texture2D value = this._rippleShapeTexture.Value;
					tileBatch.Draw(value, new Vector4(vector9.X, vector9.Y, size.X, size.Y) * 0.25f, new Rectangle?(sourceRectangle), new VertexColors(this._rippleQueue[l].WaveData), new Vector2((float)(sourceRectangle.Width / 2), (float)(sourceRectangle.Height / 2)), SpriteEffects.None, this._rippleQueue[l].Rotation);
				}
				tileBatch.End();
			}
			this._rippleQueueCount = 0;
			if (this._useCustomWaves && this.OnWaveDraw != null)
			{
				tileBatch.Begin();
				this.OnWaveDraw(tileBatch);
				tileBatch.End();
			}
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x0055525C File Offset: 0x0055345C
		private void PreDraw(GameTime gameTime)
		{
			this.ValidateRenderTargets();
			if (!this._usingRenderTargets || !Main.IsGraphicsDeviceAvailable)
			{
				return;
			}
			if (this._useProjectileWaves || this._useRippleWaves || this._useCustomWaves || this._usePlayerWaves)
			{
				for (int i = 0; i < Math.Min(this._queuedSteps, 2); i++)
				{
					this.StepLiquids();
				}
			}
			else if (this._isWaveBufferDirty || this._clearNextFrame)
			{
				GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
				graphicsDevice.SetRenderTarget(this._distortionTarget);
				graphicsDevice.Clear(new Color(0.5f, 0.5f, 0f, 1f));
				this._clearNextFrame = false;
				this._isWaveBufferDirty = false;
				graphicsDevice.SetRenderTarget(null);
			}
			this._queuedSteps = 0;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00555320 File Offset: 0x00553520
		public override void Apply()
		{
			if (!this._usingRenderTargets || !Main.IsGraphicsDeviceAvailable)
			{
				return;
			}
			base.UseProgress(this._progress);
			Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
			Vector2 vector = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			Vector2 unscaledScreenPosition = ScreenShaderData.UnscaledScreenPosition;
			Vector2 vector2 = (Main.drawToScreen ? Vector2.Zero : vector) - unscaledScreenPosition;
			base.UseImage(this.DrawRipples ? this._distortionTarget : this._noDistortionTexture, 1, null);
			base.UseImage(Main.waterTarget.Texture, 2, SamplerState.PointClamp);
			base.UseTargetPosition(unscaledScreenPosition + vector - Main.waterTarget.Position);
			base.UseImageOffset(-(vector2 - this._lastDistortionDrawOffset / 0.25f));
			base.Apply();
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00555410 File Offset: 0x00553610
		private void ValidateRenderTargets()
		{
			int backBufferWidth = Main.instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
			int backBufferHeight = Main.instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
			bool flag = !Main.drawToScreen;
			if (this._usingRenderTargets && !flag)
			{
				this.ReleaseRenderTargets();
				return;
			}
			if (!this._usingRenderTargets && flag)
			{
				this.InitRenderTargets(backBufferWidth, backBufferHeight);
				return;
			}
			if (this._usingRenderTargets && flag && (this._distortionTarget.IsContentLost || this._distortionTargetSwap.IsContentLost))
			{
				this._clearNextFrame = true;
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x005554A4 File Offset: 0x005536A4
		private void InitRenderTargets(int width, int height)
		{
			this._lastScreenWidth = width;
			this._lastScreenHeight = height;
			width = (int)((float)width * 0.25f);
			height = (int)((float)height * 0.25f);
			try
			{
				this._noDistortionTexture = new Texture2D(Main.instance.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
				this._noDistortionTexture.SetData<Color>(new Color[]
				{
					new Color(0.5f, 0.5f, 0f, 1f)
				});
				this._distortionTarget = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
				this._distortionTargetSwap = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
				this._usingRenderTargets = true;
				this._clearNextFrame = true;
			}
			catch (Exception ex)
			{
				Lighting.Mode = LightMode.Retro;
				this._usingRenderTargets = false;
				Console.WriteLine("Failed to create water distortion render targets. " + ex);
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00555598 File Offset: 0x00553798
		private void ReleaseRenderTargets()
		{
			try
			{
				if (this._distortionTarget != null)
				{
					this._distortionTarget.Dispose();
				}
				if (this._distortionTargetSwap != null)
				{
					this._distortionTargetSwap.Dispose();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error disposing of water distortion render targets. " + ex);
			}
			this._distortionTarget = null;
			this._distortionTargetSwap = null;
			this._usingRenderTargets = false;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00555608 File Offset: 0x00553808
		public void QueueRipple(Vector2 position, float strength = 1f, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			float num = strength * 0.5f + 0.5f;
			float num2 = Math.Min(Math.Abs(strength), 1f);
			this.QueueRipple(position, new Color(0.5f, num, 0f, 1f) * num2, new Vector2(4f * Math.Max(Math.Abs(strength), 1f)), shape, rotation);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00555674 File Offset: 0x00553874
		public void QueueRipple(Vector2 position, float strength, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			float num = strength * 0.5f + 0.5f;
			float num2 = Math.Min(Math.Abs(strength), 1f);
			this.QueueRipple(position, new Color(0.5f, num, 0f, 1f) * num2, size, shape, rotation);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x005556C8 File Offset: 0x005538C8
		public void QueueRipple(Vector2 position, Color waveData, Vector2 size, RippleShape shape = RippleShape.Square, float rotation = 0f)
		{
			if (!this._useRippleWaves || Main.drawToScreen)
			{
				this._rippleQueueCount = 0;
				return;
			}
			if (this._rippleQueueCount < this._rippleQueue.Length)
			{
				WaterShaderData.Ripple[] rippleQueue = this._rippleQueue;
				int rippleQueueCount = this._rippleQueueCount;
				this._rippleQueueCount = rippleQueueCount + 1;
				rippleQueue[rippleQueueCount] = new WaterShaderData.Ripple(position, waveData, size, shape, rotation);
			}
		}

		// Token: 0x04004F9B RID: 20379
		private const float DISTORTION_BUFFER_SCALE = 0.25f;

		// Token: 0x04004F9C RID: 20380
		private const float WAVE_FRAMERATE = 0.016666668f;

		// Token: 0x04004F9D RID: 20381
		private const int MAX_RIPPLES_QUEUED = 200;

		// Token: 0x04004F9E RID: 20382
		[CompilerGenerated]
		private Action<TileBatch> OnWaveDraw;

		// Token: 0x04004F9F RID: 20383
		public bool DrawRipples = true;

		// Token: 0x04004FA0 RID: 20384
		public bool _useViscosityFilter = true;

		// Token: 0x04004FA1 RID: 20385
		private RenderTarget2D _distortionTarget;

		// Token: 0x04004FA2 RID: 20386
		private RenderTarget2D _distortionTargetSwap;

		// Token: 0x04004FA3 RID: 20387
		private Texture2D _noDistortionTexture;

		// Token: 0x04004FA4 RID: 20388
		private bool _usingRenderTargets;

		// Token: 0x04004FA5 RID: 20389
		private Vector2 _lastDistortionDrawOffset = Vector2.Zero;

		// Token: 0x04004FA6 RID: 20390
		private float _progress;

		// Token: 0x04004FA7 RID: 20391
		private WaterShaderData.Ripple[] _rippleQueue = new WaterShaderData.Ripple[200];

		// Token: 0x04004FA8 RID: 20392
		private int _rippleQueueCount;

		// Token: 0x04004FA9 RID: 20393
		private int _lastScreenWidth;

		// Token: 0x04004FAA RID: 20394
		private int _lastScreenHeight;

		// Token: 0x04004FAB RID: 20395
		public bool _useProjectileWaves = true;

		// Token: 0x04004FAC RID: 20396
		private bool _useNPCWaves = true;

		// Token: 0x04004FAD RID: 20397
		private bool _usePlayerWaves = true;

		// Token: 0x04004FAE RID: 20398
		private bool _useRippleWaves = true;

		// Token: 0x04004FAF RID: 20399
		private bool _useCustomWaves = true;

		// Token: 0x04004FB0 RID: 20400
		private bool _clearNextFrame = true;

		// Token: 0x04004FB1 RID: 20401
		private Texture2D[] _viscosityMaskChain = new Texture2D[3];

		// Token: 0x04004FB2 RID: 20402
		private int _activeViscosityMask;

		// Token: 0x04004FB3 RID: 20403
		private Asset<Texture2D> _rippleShapeTexture;

		// Token: 0x04004FB4 RID: 20404
		private bool _isWaveBufferDirty = true;

		// Token: 0x04004FB5 RID: 20405
		private int _queuedSteps;

		// Token: 0x04004FB6 RID: 20406
		private const int MAX_QUEUED_STEPS = 2;

		// Token: 0x02000810 RID: 2064
		private struct Ripple
		{
			// Token: 0x1700053A RID: 1338
			// (get) Token: 0x060042ED RID: 17133 RVA: 0x006BFF71 File Offset: 0x006BE171
			public Rectangle SourceRectangle
			{
				get
				{
					return WaterShaderData.Ripple.RIPPLE_SHAPE_SOURCE_RECTS[(int)this.Shape];
				}
			}

			// Token: 0x060042EE RID: 17134 RVA: 0x006BFF83 File Offset: 0x006BE183
			public Ripple(Vector2 position, Color waveData, Vector2 size, RippleShape shape, float rotation)
			{
				this.Position = position;
				this.WaveData = waveData;
				this.Size = size;
				this.Shape = shape;
				this.Rotation = rotation;
			}

			// Token: 0x060042EF RID: 17135 RVA: 0x006BFFAC File Offset: 0x006BE1AC
			// Note: this type is marked as 'beforefieldinit'.
			static Ripple()
			{
			}

			// Token: 0x040071E8 RID: 29160
			private static readonly Rectangle[] RIPPLE_SHAPE_SOURCE_RECTS = new Rectangle[]
			{
				new Rectangle(0, 0, 0, 0),
				new Rectangle(1, 1, 62, 62),
				new Rectangle(1, 65, 62, 62)
			};

			// Token: 0x040071E9 RID: 29161
			public readonly Vector2 Position;

			// Token: 0x040071EA RID: 29162
			public readonly Color WaveData;

			// Token: 0x040071EB RID: 29163
			public readonly Vector2 Size;

			// Token: 0x040071EC RID: 29164
			public readonly RippleShape Shape;

			// Token: 0x040071ED RID: 29165
			public readonly float Rotation;
		}
	}
}
