using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using XPT.Core.Audio.MP3Sharp;

namespace Terraria.Audio
{
	// Token: 0x020005D7 RID: 1495
	public class MP3AudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06003ABC RID: 15036 RVA: 0x006591C4 File Offset: 0x006573C4
		public MP3AudioTrack(Stream stream)
		{
			this._stream = stream;
			MP3Stream mp3Stream = new MP3Stream(stream);
			int frequency = mp3Stream.Frequency;
			this._mp3Stream = mp3Stream;
			base.CreateSoundEffect(frequency, AudioChannels.Stereo);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x006591FB File Offset: 0x006573FB
		public override void Reuse()
		{
			this._mp3Stream.Position = 0L;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x0065920A File Offset: 0x0065740A
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._mp3Stream.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x00659230 File Offset: 0x00657430
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._mp3Stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(AudioStopOptions.Immediate);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x04005E40 RID: 24128
		private Stream _stream;

		// Token: 0x04005E41 RID: 24129
		private MP3Stream _mp3Stream;
	}
}
