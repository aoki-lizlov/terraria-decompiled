using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001F0 RID: 496
	public abstract class GameEffect
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00523827 File Offset: 0x00521A27
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0052382F File Offset: 0x00521A2F
		public EffectPriority Priority
		{
			get
			{
				return this._priority;
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00523837 File Offset: 0x00521A37
		public void Load()
		{
			if (this._isLoaded)
			{
				return;
			}
			this._isLoaded = true;
			this.OnLoad();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnLoad()
		{
		}

		// Token: 0x060020BC RID: 8380
		public abstract bool IsVisible();

		// Token: 0x060020BD RID: 8381
		public abstract void Activate(Vector2 position, params object[] args);

		// Token: 0x060020BE RID: 8382
		public abstract void Deactivate(params object[] args);

		// Token: 0x060020BF RID: 8383 RVA: 0x0000357B File Offset: 0x0000177B
		protected GameEffect()
		{
		}

		// Token: 0x04004B26 RID: 19238
		public float Opacity;

		// Token: 0x04004B27 RID: 19239
		protected bool _isLoaded;

		// Token: 0x04004B28 RID: 19240
		protected EffectPriority _priority;
	}
}
