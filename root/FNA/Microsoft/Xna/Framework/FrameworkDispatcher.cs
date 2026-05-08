using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001C RID: 28
	public static class FrameworkDispatcher
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x00011E9C File Offset: 0x0001009C
		public static void Update()
		{
			List<DynamicSoundEffectInstance> streams = FrameworkDispatcher.Streams;
			lock (streams)
			{
				for (int i = 0; i < FrameworkDispatcher.Streams.Count; i++)
				{
					DynamicSoundEffectInstance dynamicSoundEffectInstance = FrameworkDispatcher.Streams[i];
					dynamicSoundEffectInstance.Update();
					if (dynamicSoundEffectInstance.IsDisposed)
					{
						i--;
					}
				}
			}
			if (Microphone.micList != null)
			{
				for (int j = 0; j < Microphone.micList.Count; j++)
				{
					Microphone.micList[j].CheckBuffer();
				}
			}
			MediaPlayer.Update();
			if (FrameworkDispatcher.ActiveSongChanged)
			{
				MediaPlayer.OnActiveSongChanged();
				FrameworkDispatcher.ActiveSongChanged = false;
			}
			if (FrameworkDispatcher.MediaStateChanged)
			{
				MediaPlayer.OnMediaStateChanged();
				FrameworkDispatcher.MediaStateChanged = false;
			}
			if (TouchPanel.TouchDeviceExists)
			{
				TouchPanel.Update();
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00011F68 File Offset: 0x00010168
		// Note: this type is marked as 'beforefieldinit'.
		static FrameworkDispatcher()
		{
		}

		// Token: 0x0400052F RID: 1327
		internal static bool ActiveSongChanged = false;

		// Token: 0x04000530 RID: 1328
		internal static bool MediaStateChanged = false;

		// Token: 0x04000531 RID: 1329
		internal static List<DynamicSoundEffectInstance> Streams = new List<DynamicSoundEffectInstance>();
	}
}
