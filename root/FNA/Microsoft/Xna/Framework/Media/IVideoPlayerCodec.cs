using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000056 RID: 86
	internal interface IVideoPlayerCodec : IDisposable
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000F60 RID: 3936
		// (set) Token: 0x06000F61 RID: 3937
		bool IsLooped { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000F62 RID: 3938
		// (set) Token: 0x06000F63 RID: 3939
		bool IsMuted { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000F64 RID: 3940
		TimeSpan PlayPosition { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000F65 RID: 3941
		MediaState State { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000F66 RID: 3942
		Video Video { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000F67 RID: 3943
		// (set) Token: 0x06000F68 RID: 3944
		float Volume { get; set; }

		// Token: 0x06000F69 RID: 3945
		Texture2D GetTexture();

		// Token: 0x06000F6A RID: 3946
		void Play(Video video);

		// Token: 0x06000F6B RID: 3947
		void Stop();

		// Token: 0x06000F6C RID: 3948
		void Pause();

		// Token: 0x06000F6D RID: 3949
		void Resume();

		// Token: 0x06000F6E RID: 3950
		void SetAudioTrackEXT(int track);

		// Token: 0x06000F6F RID: 3951
		void SetVideoTrackEXT(int track);
	}
}
