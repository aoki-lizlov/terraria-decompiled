using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007C RID: 124
	public sealed class DirectionalLight
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x00024251 File Offset: 0x00022451
		// (set) Token: 0x06001105 RID: 4357 RVA: 0x00024259 File Offset: 0x00022459
		public Vector3 DiffuseColor
		{
			get
			{
				return this.INTERNAL_diffuseColor;
			}
			set
			{
				this.INTERNAL_diffuseColor = value;
				if (this.Enabled && this.diffuseColorParameter != null)
				{
					this.diffuseColorParameter.SetValue(this.INTERNAL_diffuseColor);
				}
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00024283 File Offset: 0x00022483
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x0002428B File Offset: 0x0002248B
		public Vector3 Direction
		{
			get
			{
				return this.INTERNAL_direction;
			}
			set
			{
				this.INTERNAL_direction = value;
				if (this.directionParameter != null)
				{
					this.directionParameter.SetValue(this.INTERNAL_direction);
				}
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x000242AD File Offset: 0x000224AD
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x000242B5 File Offset: 0x000224B5
		public Vector3 SpecularColor
		{
			get
			{
				return this.INTERNAL_specularColor;
			}
			set
			{
				this.INTERNAL_specularColor = value;
				if (this.Enabled && this.specularColorParameter != null)
				{
					this.specularColorParameter.SetValue(this.INTERNAL_specularColor);
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000242DF File Offset: 0x000224DF
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x000242E8 File Offset: 0x000224E8
		public bool Enabled
		{
			get
			{
				return this.INTERNAL_enabled;
			}
			set
			{
				if (this.INTERNAL_enabled != value)
				{
					this.INTERNAL_enabled = value;
					if (this.INTERNAL_enabled)
					{
						if (this.diffuseColorParameter != null)
						{
							this.diffuseColorParameter.SetValue(this.DiffuseColor);
						}
						if (this.specularColorParameter != null)
						{
							this.specularColorParameter.SetValue(this.SpecularColor);
							return;
						}
					}
					else
					{
						if (this.diffuseColorParameter != null)
						{
							this.diffuseColorParameter.SetValue(Vector3.Zero);
						}
						if (this.specularColorParameter != null)
						{
							this.specularColorParameter.SetValue(Vector3.Zero);
						}
					}
				}
			}
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00024370 File Offset: 0x00022570
		public DirectionalLight(EffectParameter directionParameter, EffectParameter diffuseColorParameter, EffectParameter specularColorParameter, DirectionalLight cloneSource)
		{
			this.diffuseColorParameter = diffuseColorParameter;
			this.directionParameter = directionParameter;
			this.specularColorParameter = specularColorParameter;
			if (cloneSource != null)
			{
				this.DiffuseColor = cloneSource.DiffuseColor;
				this.Direction = cloneSource.Direction;
				this.SpecularColor = cloneSource.SpecularColor;
				this.Enabled = cloneSource.Enabled;
			}
		}

		// Token: 0x040007C2 RID: 1986
		private Vector3 INTERNAL_diffuseColor;

		// Token: 0x040007C3 RID: 1987
		private Vector3 INTERNAL_direction;

		// Token: 0x040007C4 RID: 1988
		private Vector3 INTERNAL_specularColor;

		// Token: 0x040007C5 RID: 1989
		private bool INTERNAL_enabled;

		// Token: 0x040007C6 RID: 1990
		internal EffectParameter diffuseColorParameter;

		// Token: 0x040007C7 RID: 1991
		internal EffectParameter directionParameter;

		// Token: 0x040007C8 RID: 1992
		internal EffectParameter specularColorParameter;
	}
}
