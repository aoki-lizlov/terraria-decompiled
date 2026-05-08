using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000563 RID: 1379
	public struct PlayerMovementAccsCache
	{
		// Token: 0x060037E5 RID: 14309 RVA: 0x00630468 File Offset: 0x0062E668
		public void CopyFrom(Player player)
		{
			if (this._readyToPaste)
			{
				return;
			}
			this._readyToPaste = true;
			this._mountPreventedFlight = !player.mount.CanUseWings;
			this._mountPreventedExtraJumps = player.mount.BlockExtraJumps;
			this.rocketTime = player.rocketTime;
			this.rocketDelay = player.rocketDelay;
			this.rocketDelay2 = player.rocketDelay2;
			this.wingTime = player.wingTime;
			this.jumpAgainCloud = player.canJumpAgain_Cloud;
			this.jumpAgainSandstorm = player.canJumpAgain_Sandstorm;
			this.jumpAgainBlizzard = player.canJumpAgain_Blizzard;
			this.jumpAgainFart = player.canJumpAgain_Fart;
			this.jumpAgainSail = player.canJumpAgain_Sail;
			this.jumpAgainUnicorn = player.canJumpAgain_Unicorn;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x00630524 File Offset: 0x0062E724
		public void PasteInto(Player player)
		{
			if (!this._readyToPaste)
			{
				return;
			}
			this._readyToPaste = false;
			if (this._mountPreventedFlight)
			{
				player.rocketTime = this.rocketTime;
				player.rocketDelay = this.rocketDelay;
				player.rocketDelay2 = this.rocketDelay2;
				player.wingTime = this.wingTime;
			}
			if (this._mountPreventedExtraJumps)
			{
				player.canJumpAgain_Cloud = this.jumpAgainCloud;
				player.canJumpAgain_Sandstorm = this.jumpAgainSandstorm;
				player.canJumpAgain_Blizzard = this.jumpAgainBlizzard;
				player.canJumpAgain_Fart = this.jumpAgainFart;
				player.canJumpAgain_Sail = this.jumpAgainSail;
				player.canJumpAgain_Unicorn = this.jumpAgainUnicorn;
			}
		}

		// Token: 0x04005C0D RID: 23565
		private bool _readyToPaste;

		// Token: 0x04005C0E RID: 23566
		private bool _mountPreventedFlight;

		// Token: 0x04005C0F RID: 23567
		private bool _mountPreventedExtraJumps;

		// Token: 0x04005C10 RID: 23568
		private int rocketTime;

		// Token: 0x04005C11 RID: 23569
		private float wingTime;

		// Token: 0x04005C12 RID: 23570
		private int rocketDelay;

		// Token: 0x04005C13 RID: 23571
		private int rocketDelay2;

		// Token: 0x04005C14 RID: 23572
		private bool jumpAgainCloud;

		// Token: 0x04005C15 RID: 23573
		private bool jumpAgainSandstorm;

		// Token: 0x04005C16 RID: 23574
		private bool jumpAgainBlizzard;

		// Token: 0x04005C17 RID: 23575
		private bool jumpAgainFart;

		// Token: 0x04005C18 RID: 23576
		private bool jumpAgainSail;

		// Token: 0x04005C19 RID: 23577
		private bool jumpAgainUnicorn;
	}
}
