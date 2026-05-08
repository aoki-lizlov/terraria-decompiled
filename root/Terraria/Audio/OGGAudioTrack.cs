using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using NVorbis;

namespace Terraria.Audio
{
	// Token: 0x020005D9 RID: 1497
	public class OGGAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06003ACC RID: 15052 RVA: 0x00659462 File Offset: 0x00657662
		public OGGAudioTrack(Stream streamToRead)
		{
			this._vorbisReader = new VorbisReader(streamToRead, true);
			this.FindLoops();
			base.CreateSoundEffect(this._vorbisReader.SampleRate, (AudioChannels)this._vorbisReader.Channels);
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x00659499 File Offset: 0x00657699
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			this.PrepareBufferToSubmit();
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x006594B4 File Offset: 0x006576B4
		private void PrepareBufferToSubmit()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			float[] temporaryBuffer = this._temporaryBuffer;
			VorbisReader vorbisReader = this._vorbisReader;
			int num = vorbisReader.ReadSamples(temporaryBuffer, 0, temporaryBuffer.Length);
			bool flag = this._loopEnd > 0 && vorbisReader.DecodedPosition >= (long)this._loopEnd;
			bool flag2 = num < temporaryBuffer.Length;
			if (flag || flag2)
			{
				vorbisReader.DecodedPosition = (long)this._loopStart;
				vorbisReader.ReadSamples(temporaryBuffer, num, temporaryBuffer.Length - num);
			}
			OGGAudioTrack.ApplyTemporaryBufferTo(temporaryBuffer, bufferToSubmit);
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x00659530 File Offset: 0x00657730
		private static void ApplyTemporaryBufferTo(float[] temporaryBuffer, byte[] samplesBuffer)
		{
			for (int i = 0; i < temporaryBuffer.Length; i++)
			{
				short num = (short)(temporaryBuffer[i] * 32767f);
				samplesBuffer[i * 2] = (byte)num;
				samplesBuffer[i * 2 + 1] = (byte)(num >> 8);
			}
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x00659568 File Offset: 0x00657768
		public override void Reuse()
		{
			this._vorbisReader.SeekTo(0L, SeekOrigin.Begin);
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x00659578 File Offset: 0x00657778
		private void FindLoops()
		{
			IDictionary<string, IList<string>> all = this._vorbisReader.Tags.All;
			this.TryReadingTag(all, "LOOPSTART", ref this._loopStart);
			this.TryReadingTag(all, "LOOPEND", ref this._loopEnd);
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x006595BC File Offset: 0x006577BC
		private void TryReadingTag(IDictionary<string, IList<string>> tags, string entryName, ref int result)
		{
			IList<string> list;
			int num;
			if (tags.TryGetValue(entryName, out list) && list.Count > 0 && int.TryParse(list[0], out num))
			{
				result = num;
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x006595F0 File Offset: 0x006577F0
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._vorbisReader.Dispose();
		}

		// Token: 0x04005E46 RID: 24134
		private VorbisReader _vorbisReader;

		// Token: 0x04005E47 RID: 24135
		private int _loopStart;

		// Token: 0x04005E48 RID: 24136
		private int _loopEnd;
	}
}
