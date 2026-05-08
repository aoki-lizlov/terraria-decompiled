using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002F RID: 47
	public abstract class RgbDevice
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005C0B File Offset: 0x00003E0B
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005C13 File Offset: 0x00003E13
		public EffectDetailLevel PreferredLevelOfDetail
		{
			[CompilerGenerated]
			get
			{
				return this.<PreferredLevelOfDetail>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<PreferredLevelOfDetail>k__BackingField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005C1C File Offset: 0x00003E1C
		public int LedCount
		{
			get
			{
				return this._backBuffer.Count;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005C29 File Offset: 0x00003E29
		protected RgbDevice(RgbDeviceVendor vendor, RgbDeviceType type, Fragment fragment, DeviceColorProfile colorProfile)
		{
			this.PreferredLevelOfDetail = EffectDetailLevel.High;
			this.Vendor = vendor;
			this.Type = type;
			this._backBuffer = fragment;
			this._workingFragment = fragment.CreateCopy();
			this._colorProfile = colorProfile;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005C61 File Offset: 0x00003E61
		public virtual Fragment Rasterize()
		{
			return this._workingFragment;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005C6C File Offset: 0x00003E6C
		protected Vector4 GetProcessedLedColor(int index)
		{
			Vector4 vector = this._backBuffer.Colors[index];
			this._colorProfile.Apply(ref vector);
			return vector;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005C99 File Offset: 0x00003E99
		protected Vector4 GetUnprocessedLedColor(int index)
		{
			return this._backBuffer.Colors[index];
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005CAC File Offset: 0x00003EAC
		protected Color ProcessLedColor(Color color)
		{
			Vector3 vector = color.ToVector3();
			this._colorProfile.Apply(ref vector);
			return new Color(vector);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public void SetLedColor(int index, Vector4 color)
		{
			this._backBuffer.Colors[index] = color;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005CE8 File Offset: 0x00003EE8
		public Vector2 GetLedCanvasPosition(int index)
		{
			return this._backBuffer.GetCanvasPositionOfIndex(index);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005CF6 File Offset: 0x00003EF6
		public Point GetLedGridPosition(int index)
		{
			return this._backBuffer.GetGridPositionOfIndex(index);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005D04 File Offset: 0x00003F04
		public virtual void Render(Fragment fragment, ShaderBlendState blendState)
		{
			BlendMode mode = blendState.Mode;
			if (mode == BlendMode.GlobalOpacityOnly)
			{
				for (int i = 0; i < fragment.Count; i++)
				{
					this._backBuffer.Colors[i] = Vector4.Lerp(this._backBuffer.Colors[i], fragment.Colors[i], blendState.GlobalOpacity);
				}
				return;
			}
			if (mode == BlendMode.PerPixelOpacity)
			{
				for (int j = 0; j < fragment.Count; j++)
				{
					this._backBuffer.Colors[j] = Vector4.Lerp(this._backBuffer.Colors[j], fragment.Colors[j], blendState.GlobalOpacity * fragment.Colors[j].W);
				}
				return;
			}
			for (int k = 0; k < fragment.Count; k++)
			{
				this._backBuffer.Colors[k] = fragment.Colors[k];
			}
		}

		// Token: 0x06000162 RID: 354
		public abstract void Present();

		// Token: 0x06000163 RID: 355 RVA: 0x00005DFC File Offset: 0x00003FFC
		public virtual void DebugDraw(IDebugDrawer drawer, Vector2 position, float scale)
		{
			for (int i = 0; i < this.LedCount; i++)
			{
				Vector2 canvasPositionOfIndex = this._backBuffer.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._backBuffer.Colors[i];
				vector.X *= vector.W;
				vector.Y *= vector.W;
				vector.Z *= vector.W;
				drawer.DrawSquare(new Vector4(canvasPositionOfIndex * scale + position, scale / 10f, scale / 10f), new Color(vector));
			}
		}

		// Token: 0x04000082 RID: 130
		public readonly RgbDeviceType Type;

		// Token: 0x04000083 RID: 131
		public readonly RgbDeviceVendor Vendor;

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private EffectDetailLevel <PreferredLevelOfDetail>k__BackingField;

		// Token: 0x04000085 RID: 133
		private readonly Fragment _backBuffer;

		// Token: 0x04000086 RID: 134
		private readonly Fragment _workingFragment;

		// Token: 0x04000087 RID: 135
		private readonly DeviceColorProfile _colorProfile;
	}
}
