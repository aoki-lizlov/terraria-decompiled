using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005DA RID: 1498
	public static class SoundInstanceGarbageCollector
	{
		// Token: 0x06003AD4 RID: 15060 RVA: 0x00659608 File Offset: 0x00657808
		public static void Track(SoundEffectInstance sound)
		{
			if (Program.IsFna)
			{
				SoundInstanceGarbageCollector._activeSounds.Add(sound);
			}
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x0065961C File Offset: 0x0065781C
		public static void Update()
		{
			for (int i = 0; i < SoundInstanceGarbageCollector._activeSounds.Count; i++)
			{
				if (SoundInstanceGarbageCollector._activeSounds[i] == null)
				{
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
				else if (SoundInstanceGarbageCollector._activeSounds[i].State == SoundState.Stopped)
				{
					SoundInstanceGarbageCollector._activeSounds[i].Dispose();
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x0065968E File Offset: 0x0065788E
		// Note: this type is marked as 'beforefieldinit'.
		static SoundInstanceGarbageCollector()
		{
		}

		// Token: 0x04005E49 RID: 24137
		private static readonly List<SoundEffectInstance> _activeSounds = new List<SoundEffectInstance>(128);
	}
}
