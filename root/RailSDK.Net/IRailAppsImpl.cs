using System;

namespace rail
{
	// Token: 0x02000009 RID: 9
	public class IRailAppsImpl : RailObject, IRailApps
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailAppsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00002214 File Offset: 0x00000414
		~IRailAppsImpl()
		{
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0000223C File Offset: 0x0000043C
		public virtual bool IsGameInstalled(RailGameID game_id)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailApps_IsGameInstalled(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return flag;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00002298 File Offset: 0x00000498
		public virtual RailResult AsyncQuerySubscribeWishPlayState(RailGameID game_id, string user_data)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailApps_AsyncQuerySubscribeWishPlayState(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return railResult;
		}
	}
}
