using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.Audio
{
	// Token: 0x020005DB RID: 1499
	public class SoundPlayer
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x006596A0 File Offset: 0x006578A0
		public SlotId Play(SoundStyle style, Vector2 position, SoundPlayOverrides overrides = default(SoundPlayOverrides))
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			if (Vector2.DistanceSquared(Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2)), position) > 100000000f)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style, position, overrides);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x0065970C File Offset: 0x0065790C
		public SlotId PlayLooped(SoundStyle style, Vector2 position, ActiveSound.LoopedPlayCondition loopingCondition, SoundPlayOverrides overrides = default(SoundPlayOverrides))
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			if (Vector2.DistanceSquared(Main.screenPosition + new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2)), position) > 100000000f)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style, position, loopingCondition, overrides);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0065977A File Offset: 0x0065797A
		public void Reload()
		{
			this.StopAll();
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00659784 File Offset: 0x00657984
		public SlotId Play(SoundStyle style)
		{
			if (Main.dedServ || style == null || !style.IsTrackable)
			{
				return SlotId.Invalid;
			}
			ActiveSound activeSound = new ActiveSound(style);
			return this._trackedSounds.Add(activeSound);
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x006597BC File Offset: 0x006579BC
		public ActiveSound GetActiveSound(SlotId id)
		{
			if (!this._trackedSounds.Has(id))
			{
				return null;
			}
			return this._trackedSounds[id];
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x006597DC File Offset: 0x006579DC
		public void PauseAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Pause();
			}
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x0065982C File Offset: 0x00657A2C
		public void ResumeAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Resume();
			}
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x0065987C File Offset: 0x00657A7C
		public void StopAll()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				itemPair.Value.Stop();
			}
			this._trackedSounds.Clear();
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x006598D8 File Offset: 0x00657AD8
		public void Update()
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				try
				{
					itemPair.Value.Update();
					if (!itemPair.Value.IsPlaying)
					{
						this._trackedSounds.Remove(itemPair.Id);
					}
				}
				catch
				{
					this._trackedSounds.Remove(itemPair.Id);
				}
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0065996C File Offset: 0x00657B6C
		public int GetActiveSoundCount(SoundStyle style)
		{
			int num = 0;
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				ActiveSound value = itemPair.Value;
				if (value.Style == style && value.IsPlaying)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x006599D0 File Offset: 0x00657BD0
		public ActiveSound FindActiveSound(SoundStyle style)
		{
			foreach (SlotVector<ActiveSound>.ItemPair itemPair in this._trackedSounds)
			{
				if (itemPair.Value.Style == style)
				{
					return itemPair.Value;
				}
			}
			return null;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00659A30 File Offset: 0x00657C30
		public SoundPlayer()
		{
		}

		// Token: 0x04005E4A RID: 24138
		private readonly SlotVector<ActiveSound> _trackedSounds = new SlotVector<ActiveSound>(4096);
	}
}
