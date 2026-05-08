using System;
using System.Collections;
using System.Collections.Generic;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x020005D1 RID: 1489
	public interface IAudioSystem : IDisposable
	{
		// Token: 0x06003A6C RID: 14956
		void LoadCue(int cueIndex, string cueName);

		// Token: 0x06003A6D RID: 14957
		void PauseAll();

		// Token: 0x06003A6E RID: 14958
		void ResumeAll();

		// Token: 0x06003A6F RID: 14959
		void UpdateMisc();

		// Token: 0x06003A70 RID: 14960
		void UpdateAudioEngine();

		// Token: 0x06003A71 RID: 14961
		void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume);

		// Token: 0x06003A72 RID: 14962
		void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume);

		// Token: 0x06003A73 RID: 14963
		void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade);

		// Token: 0x06003A74 RID: 14964
		void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible);

		// Token: 0x06003A75 RID: 14965
		bool IsTrackPlaying(int trackIndex);

		// Token: 0x06003A76 RID: 14966
		void UseSources(List<IContentSource> sources);

		// Token: 0x06003A77 RID: 14967
		IEnumerator PrepareWaveBank();

		// Token: 0x06003A78 RID: 14968
		void LoadFromSources();

		// Token: 0x06003A79 RID: 14969
		void Update();

		// Token: 0x06003A7A RID: 14970
		void SetPlayCallback(int trackIndex, AudioTrackPlayCallback callback);
	}
}
