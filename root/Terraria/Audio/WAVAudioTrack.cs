using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x020005DE RID: 1502
	public class WAVAudioTrack : ASoundEffectBasedAudioTrack
	{
		// Token: 0x06003AED RID: 15085 RVA: 0x00659AD0 File Offset: 0x00657CD0
		public WAVAudioTrack(Stream stream)
		{
			this._stream = stream;
			BinaryReader binaryReader = new BinaryReader(stream);
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			AudioChannels audioChannels = AudioChannels.Mono;
			uint num = 0U;
			bool flag = false;
			int num2 = 0;
			while (!flag && num2 < 10)
			{
				uint num3 = binaryReader.ReadUInt32();
				int num4 = binaryReader.ReadInt32();
				if (num3 != 544501094U)
				{
					if (num3 == 1263424842U)
					{
						WAVAudioTrack.SkipJunk(binaryReader, num4);
					}
				}
				else
				{
					binaryReader.ReadInt16();
					audioChannels = (AudioChannels)binaryReader.ReadUInt16();
					num = binaryReader.ReadUInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadInt16();
					binaryReader.ReadInt16();
					flag = true;
				}
				if (!flag)
				{
					num2++;
				}
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			this._streamContentStartIndex = stream.Position;
			base.CreateSoundEffect((int)num, audioChannels);
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00659BA8 File Offset: 0x00657DA8
		private static void SkipJunk(BinaryReader reader, int chunkSize)
		{
			int num = chunkSize;
			if (num % 2 != 0)
			{
				num++;
			}
			reader.ReadBytes(num);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x00659BC8 File Offset: 0x00657DC8
		protected override void ReadAheadPutAChunkIntoTheBuffer()
		{
			byte[] bufferToSubmit = this._bufferToSubmit;
			if (this._stream.Read(bufferToSubmit, 0, bufferToSubmit.Length) < 1)
			{
				base.Stop(AudioStopOptions.Immediate);
				return;
			}
			this._soundEffectInstance.SubmitBuffer(this._bufferToSubmit);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x00659C08 File Offset: 0x00657E08
		public override void Reuse()
		{
			this._stream.Position = this._streamContentStartIndex;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x00659C1B File Offset: 0x00657E1B
		public override void Dispose()
		{
			this._soundEffectInstance.Dispose();
			this._stream.Dispose();
		}

		// Token: 0x04005E53 RID: 24147
		private long _streamContentStartIndex = -1L;

		// Token: 0x04005E54 RID: 24148
		private Stream _stream;

		// Token: 0x04005E55 RID: 24149
		private const uint JUNK = 1263424842U;

		// Token: 0x04005E56 RID: 24150
		private const uint FMT = 544501094U;
	}
}
