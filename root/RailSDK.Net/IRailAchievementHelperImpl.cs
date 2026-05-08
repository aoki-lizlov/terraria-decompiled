using System;

namespace rail
{
	// Token: 0x02000008 RID: 8
	public class IRailAchievementHelperImpl : RailObject, IRailAchievementHelper
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailAchievementHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00002148 File Offset: 0x00000348
		~IRailAchievementHelperImpl()
		{
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00002170 File Offset: 0x00000370
		public virtual IRailPlayerAchievement CreatePlayerAchievement(RailID player)
		{
			IntPtr intPtr = ((player == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player != null)
			{
				RailConverter.Csharp2Cpp(player, intPtr);
			}
			IRailPlayerAchievement railPlayerAchievement;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailAchievementHelper_CreatePlayerAchievement(this.swigCPtr_, intPtr);
				railPlayerAchievement = ((intPtr2 == IntPtr.Zero) ? null : new IRailPlayerAchievementImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railPlayerAchievement;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000021E4 File Offset: 0x000003E4
		public virtual IRailGlobalAchievement GetGlobalAchievement()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailAchievementHelper_GetGlobalAchievement(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGlobalAchievementImpl(intPtr);
			}
			return null;
		}
	}
}
