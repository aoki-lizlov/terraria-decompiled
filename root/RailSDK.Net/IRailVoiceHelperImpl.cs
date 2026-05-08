using System;

namespace rail
{
	// Token: 0x02000034 RID: 52
	public class IRailVoiceHelperImpl : RailObject, IRailVoiceHelper
	{
		// Token: 0x06001345 RID: 4933 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailVoiceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000084B0 File Offset: 0x000066B0
		~IRailVoiceHelperImpl()
		{
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000084D8 File Offset: 0x000066D8
		public virtual IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options, string channel_name, string user_data, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailVoiceChannel railVoiceChannel;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel(this.swigCPtr_, intPtr, channel_name, user_data, out result);
				railVoiceChannel = ((intPtr2 == IntPtr.Zero) ? null : new IRailVoiceChannelImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(intPtr);
			}
			return railVoiceChannel;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00008544 File Offset: 0x00006744
		public virtual IRailVoiceChannel OpenVoiceChannel(RailVoiceChannelID voice_channel_id, string channel_name, out RailResult result)
		{
			IntPtr intPtr = ((voice_channel_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceChannelID__SWIG_0());
			if (voice_channel_id != null)
			{
				RailConverter.Csharp2Cpp(voice_channel_id, intPtr);
			}
			IRailVoiceChannel railVoiceChannel;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailVoiceHelper_OpenVoiceChannel(this.swigCPtr_, intPtr, channel_name, out result);
				railVoiceChannel = ((intPtr2 == IntPtr.Zero) ? null : new IRailVoiceChannelImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceChannelID(intPtr);
			}
			return railVoiceChannel;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000085B8 File Offset: 0x000067B8
		public virtual EnumRailVoiceChannelSpeakerState GetSpeakerState()
		{
			return (EnumRailVoiceChannelSpeakerState)RAIL_API_PINVOKE.IRailVoiceHelper_GetSpeakerState(this.swigCPtr_);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000085C5 File Offset: 0x000067C5
		public virtual RailResult MuteSpeaker()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_MuteSpeaker(this.swigCPtr_);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000085D2 File Offset: 0x000067D2
		public virtual RailResult ResumeSpeaker()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_ResumeSpeaker(this.swigCPtr_);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000085E0 File Offset: 0x000067E0
		public virtual RailResult SetupVoiceCapture(RailVoiceCaptureOption options, RailCaptureVoiceCallback callback)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_0(this.swigCPtr_, intPtr, callback);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00008630 File Offset: 0x00006830
		public virtual RailResult SetupVoiceCapture(RailVoiceCaptureOption options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00008680 File Offset: 0x00006880
		public virtual RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_0(this.swigCPtr_, duration_milliseconds, repeat);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0000868F File Offset: 0x0000688F
		public virtual RailResult StartVoiceCapturing(uint duration_milliseconds)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_1(this.swigCPtr_, duration_milliseconds);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0000869D File Offset: 0x0000689D
		public virtual RailResult StartVoiceCapturing()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_2(this.swigCPtr_);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000086AA File Offset: 0x000068AA
		public virtual RailResult StopVoiceCapturing()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StopVoiceCapturing(this.swigCPtr_);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x000086B7 File Offset: 0x000068B7
		public virtual RailResult GetCapturedVoiceData(byte[] buffer, uint buffer_length, out uint encoded_bytes_written)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_GetCapturedVoiceData(this.swigCPtr_, buffer, buffer_length, out encoded_bytes_written);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000086C7 File Offset: 0x000068C7
		public virtual RailResult DecodeVoice(byte[] encoded_buffer, uint encoded_length, byte[] pcm_buffer, uint pcm_buffer_length, out uint pcm_buffer_written)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_DecodeVoice(this.swigCPtr_, encoded_buffer, encoded_length, pcm_buffer, pcm_buffer_length, out pcm_buffer_written);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000086DC File Offset: 0x000068DC
		public virtual RailResult GetVoiceCaptureSpecification(RailVoiceCaptureSpecification spec)
		{
			IntPtr intPtr = ((spec == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureSpecification__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_GetVoiceCaptureSpecification(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (spec != null)
				{
					RailConverter.Cpp2Csharp(intPtr, spec);
				}
				RAIL_API_PINVOKE.delete_RailVoiceCaptureSpecification(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0000872C File Offset: 0x0000692C
		public virtual RailResult EnableInGameVoiceSpeaking(bool can_speaking)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_EnableInGameVoiceSpeaking(this.swigCPtr_, can_speaking);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0000873A File Offset: 0x0000693A
		public virtual RailResult SetPlayerNicknameInVoiceChannel(string nickname)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetPlayerNicknameInVoiceChannel(this.swigCPtr_, nickname);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00008748 File Offset: 0x00006948
		public virtual RailResult SetPushToTalkKeyInVoiceChannel(uint push_to_talk_hot_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetPushToTalkKeyInVoiceChannel(this.swigCPtr_, push_to_talk_hot_key);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00008756 File Offset: 0x00006956
		public virtual uint GetPushToTalkKeyInVoiceChannel()
		{
			return RAIL_API_PINVOKE.IRailVoiceHelper_GetPushToTalkKeyInVoiceChannel(this.swigCPtr_);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00008763 File Offset: 0x00006963
		public virtual RailResult ShowOverlayUI(bool show)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_ShowOverlayUI(this.swigCPtr_, show);
		}
	}
}
