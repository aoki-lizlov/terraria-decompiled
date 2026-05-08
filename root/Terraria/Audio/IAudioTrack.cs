using System;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005D2 RID: 1490
	public interface IAudioTrack : IDisposable
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06003A7B RID: 14971
		bool IsPlaying { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06003A7C RID: 14972
		bool IsStopped { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06003A7D RID: 14973
		bool IsPaused { get; }

		// Token: 0x06003A7E RID: 14974
		void Stop(AudioStopOptions options);

		// Token: 0x06003A7F RID: 14975
		void Play();

		// Token: 0x06003A80 RID: 14976
		void Pause();

		// Token: 0x06003A81 RID: 14977
		void SetVariable(string variableName, float value);

		// Token: 0x06003A82 RID: 14978
		void Resume();

		// Token: 0x06003A83 RID: 14979
		void Reuse();

		// Token: 0x06003A84 RID: 14980
		void Update();
	}
}
