using System;

namespace rail
{
	// Token: 0x02000010 RID: 16
	public class IRailFactoryImpl : RailObject, IRailFactory
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailFactoryImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00002DD4 File Offset: 0x00000FD4
		~IRailFactoryImpl()
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00002DFC File Offset: 0x00000FFC
		public virtual IRailPlayer RailPlayer()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailPlayer(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailPlayerImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00002E2C File Offset: 0x0000102C
		public virtual IRailUsersHelper RailUsersHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUsersHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUsersHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00002E5C File Offset: 0x0000105C
		public virtual IRailFriends RailFriends()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailFriends(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFriendsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00002E8C File Offset: 0x0000108C
		public virtual IRailFloatingWindow RailFloatingWindow()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailFloatingWindow(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFloatingWindowImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00002EBC File Offset: 0x000010BC
		public virtual IRailBrowserHelper RailBrowserHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailBrowserHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00002EEC File Offset: 0x000010EC
		public virtual IRailInGamePurchase RailInGamePurchase()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailInGamePurchase(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailInGamePurchaseImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00002F1C File Offset: 0x0000111C
		public virtual IRailZoneHelper RailZoneHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailZoneHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailZoneHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00002F4C File Offset: 0x0000114C
		public virtual IRailRoomHelper RailRoomHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailRoomHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailRoomHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00002F7C File Offset: 0x0000117C
		public virtual IRailGameServerHelper RailGameServerHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailGameServerHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameServerHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00002FAC File Offset: 0x000011AC
		public virtual IRailStorageHelper RailStorageHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailStorageHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailStorageHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00002FDC File Offset: 0x000011DC
		public virtual IRailUserSpaceHelper RailUserSpaceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUserSpaceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUserSpaceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0000300C File Offset: 0x0000120C
		public virtual IRailStatisticHelper RailStatisticHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailStatisticHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailStatisticHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0000303C File Offset: 0x0000123C
		public virtual IRailLeaderboardHelper RailLeaderboardHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailLeaderboardHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0000306C File Offset: 0x0000126C
		public virtual IRailAchievementHelper RailAchievementHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailAchievementHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAchievementHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0000309C File Offset: 0x0000129C
		public virtual IRailNetwork RailNetworkHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailNetworkHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailNetworkImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000030CC File Offset: 0x000012CC
		public virtual IRailApps RailApps()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailApps(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAppsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000030FC File Offset: 0x000012FC
		public virtual IRailGame RailGame()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailGame(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0000312C File Offset: 0x0000132C
		public virtual IRailUtils RailUtils()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUtils(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUtilsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0000315C File Offset: 0x0000135C
		public virtual IRailAssetsHelper RailAssetsHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailAssetsHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAssetsHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0000318C File Offset: 0x0000138C
		public virtual IRailDlcHelper RailDlcHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailDlcHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailDlcHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x000031BC File Offset: 0x000013BC
		public virtual IRailScreenshotHelper RailScreenshotHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailScreenshotHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000031EC File Offset: 0x000013EC
		public virtual IRailVoiceHelper RailVoiceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailVoiceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailVoiceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0000321C File Offset: 0x0000141C
		public virtual IRailSystemHelper RailSystemHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailSystemHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSystemHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0000324C File Offset: 0x0000144C
		public virtual IRailTextInputHelper RailTextInputHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailTextInputHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailTextInputHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0000327C File Offset: 0x0000147C
		public virtual IRailIMEHelper RailIMETextInputHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailIMETextInputHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailIMEHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x000032AC File Offset: 0x000014AC
		public virtual IRailHttpSessionHelper RailHttpSessionHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailHttpSessionHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpSessionHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x000032DC File Offset: 0x000014DC
		public virtual IRailSmallObjectServiceHelper RailSmallObjectServiceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailSmallObjectServiceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSmallObjectServiceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0000330C File Offset: 0x0000150C
		public virtual IRailZoneServerHelper RailZoneServerHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailZoneServerHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailZoneServerHelperImpl(intPtr);
			}
			return null;
		}
	}
}
