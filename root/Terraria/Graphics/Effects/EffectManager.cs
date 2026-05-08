using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001EB RID: 491
	public abstract class EffectManager<T> where T : GameEffect
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00522FCF File Offset: 0x005211CF
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		// Token: 0x1700032F RID: 815
		public T this[string key]
		{
			get
			{
				T t;
				if (this._effects.TryGetValue(key, out t))
				{
					return t;
				}
				return default(T);
			}
			set
			{
				this.Bind(key, value);
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0052300A File Offset: 0x0052120A
		public void Bind(string name, T effect)
		{
			this._effects[name] = effect;
			if (this._isLoaded)
			{
				effect.Load();
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0052302C File Offset: 0x0052122C
		public void Load()
		{
			if (this._isLoaded)
			{
				return;
			}
			this._isLoaded = true;
			foreach (T t in this._effects.Values)
			{
				t.Load();
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00523098 File Offset: 0x00521298
		public T Activate(string name, Vector2 position = default(Vector2), params object[] args)
		{
			if (!this._effects.ContainsKey(name))
			{
				throw new MissingEffectException(string.Concat(new object[]
				{
					"Unable to find effect named: ",
					name,
					". Type: ",
					typeof(T),
					"."
				}));
			}
			T t = this._effects[name];
			this.OnActivate(t, position);
			t.Activate(position, args);
			return t;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00523110 File Offset: 0x00521310
		public void Deactivate(string name, params object[] args)
		{
			if (!this._effects.ContainsKey(name))
			{
				throw new MissingEffectException(string.Concat(new object[]
				{
					"Unable to find effect named: ",
					name,
					". Type: ",
					typeof(T),
					"."
				}));
			}
			T t = this._effects[name];
			this.OnDeactivate(t);
			t.Deactivate(args);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnActivate(T effect, Vector2 position)
		{
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void OnDeactivate(T effect)
		{
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00523185 File Offset: 0x00521385
		protected EffectManager()
		{
		}

		// Token: 0x04004B14 RID: 19220
		protected bool _isLoaded;

		// Token: 0x04004B15 RID: 19221
		protected Dictionary<string, T> _effects = new Dictionary<string, T>();
	}
}
