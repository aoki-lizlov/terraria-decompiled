using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding.Decoders
{
	// Token: 0x0200001D RID: 29
	public abstract class ASubband
	{
		// Token: 0x06000115 RID: 277
		internal abstract void ReadAllocation(Bitstream stream, Header header, Crc16 crc);

		// Token: 0x06000116 RID: 278
		internal abstract void ReadScaleFactor(Bitstream stream, Header header);

		// Token: 0x06000117 RID: 279
		internal abstract bool ReadSampleData(Bitstream stream);

		// Token: 0x06000118 RID: 280
		internal abstract bool PutNextSample(int channels, SynthesisFilter filter1, SynthesisFilter filter2);

		// Token: 0x06000119 RID: 281 RVA: 0x0001551B File Offset: 0x0001371B
		protected ASubband()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00015523 File Offset: 0x00013723
		// Note: this type is marked as 'beforefieldinit'.
		static ASubband()
		{
		}

		// Token: 0x040000E7 RID: 231
		internal static readonly float[] ScaleFactors = new float[]
		{
			2f, 1.587401f, 1.2599211f, 1f, 0.7937005f, 0.62996054f, 0.5f, 0.39685026f, 0.31498027f, 0.25f,
			0.19842513f, 0.15749013f, 0.125f, 0.099212565f, 0.07874507f, 0.0625f, 0.049606282f, 0.039372534f, 0.03125f, 0.024803141f,
			0.019686267f, 0.015625f, 0.012401571f, 0.009843133f, 0.0078125f, 0.0062007853f, 0.0049215667f, 0.00390625f, 0.0031003926f, 0.0024607833f,
			0.001953125f, 0.0015501963f, 0.0012303917f, 0.0009765625f, 0.00077509816f, 0.00061519584f, 0.00048828125f, 0.00038754908f, 0.00030759792f, 0.00024414062f,
			0.00019377454f, 0.00015379896f, 0.00012207031f, 9.688727E-05f, 7.689948E-05f, 6.1035156E-05f, 4.8443635E-05f, 3.844974E-05f, 3.0517578E-05f, 2.4221818E-05f,
			1.922487E-05f, 1.5258789E-05f, 1.2110909E-05f, 9.612435E-06f, 7.6293945E-06f, 6.0554544E-06f, 4.8062175E-06f, 3.8146973E-06f, 3.0277272E-06f, 2.4031087E-06f,
			1.9073486E-06f, 1.5138636E-06f, 1.2015544E-06f, 0f
		};
	}
}
