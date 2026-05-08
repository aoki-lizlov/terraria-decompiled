using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x020005AD RID: 1453
	public class CinematicManager
	{
		// Token: 0x06003975 RID: 14709 RVA: 0x006522AC File Offset: 0x006504AC
		public void Update(GameTime gameTime)
		{
			if (this._films.Count > 0)
			{
				if (!this._films[0].IsActive)
				{
					this._films[0].OnBegin();
				}
				if (FocusHelper.UpdateVisualEffects && !this._films[0].OnUpdate(gameTime))
				{
					this._films[0].OnEnd();
					this._films.RemoveAt(0);
				}
			}
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x00652323 File Offset: 0x00650523
		public void PlayFilm(Film film)
		{
			this._films.Add(film);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00009E46 File Offset: 0x00008046
		public void StopAll()
		{
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00652331 File Offset: 0x00650531
		public CinematicManager()
		{
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00652344 File Offset: 0x00650544
		// Note: this type is marked as 'beforefieldinit'.
		static CinematicManager()
		{
		}

		// Token: 0x04005DA0 RID: 23968
		public static CinematicManager Instance = new CinematicManager();

		// Token: 0x04005DA1 RID: 23969
		private List<Film> _films = new List<Film>();
	}
}
