using System;

namespace rail
{
	// Token: 0x02000199 RID: 409
	public interface IRailVoiceHelper
	{
		// Token: 0x060018D0 RID: 6352
		IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options, string channel_name, string user_data, out RailResult result);

		// Token: 0x060018D1 RID: 6353
		IRailVoiceChannel OpenVoiceChannel(RailVoiceChannelID voice_channel_id, string channel_name, out RailResult result);

		// Token: 0x060018D2 RID: 6354
		EnumRailVoiceChannelSpeakerState GetSpeakerState();

		// Token: 0x060018D3 RID: 6355
		RailResult MuteSpeaker();

		// Token: 0x060018D4 RID: 6356
		RailResult ResumeSpeaker();

		// Token: 0x060018D5 RID: 6357
		RailResult SetupVoiceCapture(RailVoiceCaptureOption options, RailCaptureVoiceCallback callback);

		// Token: 0x060018D6 RID: 6358
		RailResult SetupVoiceCapture(RailVoiceCaptureOption options);

		// Token: 0x060018D7 RID: 6359
		RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat);

		// Token: 0x060018D8 RID: 6360
		RailResult StartVoiceCapturing(uint duration_milliseconds);

		// Token: 0x060018D9 RID: 6361
		RailResult StartVoiceCapturing();

		// Token: 0x060018DA RID: 6362
		RailResult StopVoiceCapturing();

		// Token: 0x060018DB RID: 6363
		RailResult GetCapturedVoiceData(byte[] buffer, uint buffer_length, out uint encoded_bytes_written);

		// Token: 0x060018DC RID: 6364
		RailResult DecodeVoice(byte[] encoded_buffer, uint encoded_length, byte[] pcm_buffer, uint pcm_buffer_length, out uint pcm_buffer_written);

		// Token: 0x060018DD RID: 6365
		RailResult GetVoiceCaptureSpecification(RailVoiceCaptureSpecification spec);

		// Token: 0x060018DE RID: 6366
		RailResult EnableInGameVoiceSpeaking(bool can_speaking);

		// Token: 0x060018DF RID: 6367
		RailResult SetPlayerNicknameInVoiceChannel(string nickname);

		// Token: 0x060018E0 RID: 6368
		RailResult SetPushToTalkKeyInVoiceChannel(uint push_to_talk_hot_key);

		// Token: 0x060018E1 RID: 6369
		uint GetPushToTalkKeyInVoiceChannel();

		// Token: 0x060018E2 RID: 6370
		RailResult ShowOverlayUI(bool show);
	}
}
