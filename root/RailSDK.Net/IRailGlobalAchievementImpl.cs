using System;

namespace rail
{
	// Token: 0x02000017 RID: 23
	public class IRailGlobalAchievementImpl : RailObject, IRailGlobalAchievement, IRailComponent
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailGlobalAchievementImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00004834 File Offset: 0x00002A34
		~IRailGlobalAchievementImpl()
		{
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0000485C File Offset: 0x00002A5C
		public virtual RailResult AsyncRequestAchievement(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_AsyncRequestAchievement(this.swigCPtr_, user_data);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0000486A File Offset: 0x00002A6A
		public virtual RailResult GetGlobalAchievedPercent(string name, out double percent)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercent(this.swigCPtr_, name, out percent);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0000487C File Offset: 0x00002A7C
		public virtual RailResult GetGlobalAchievedPercentDescending(int index, out string name, out double percent)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGlobalAchievement_GetGlobalAchievedPercentDescending(this.swigCPtr_, index, intPtr, out percent);
			}
			finally
			{
				name = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000025FC File Offset: 0x000007FC
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00002609 File Offset: 0x00000809
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
