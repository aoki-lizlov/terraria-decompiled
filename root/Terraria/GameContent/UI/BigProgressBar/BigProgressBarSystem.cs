using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x02000388 RID: 904
	public class BigProgressBarSystem
	{
		// Token: 0x060029CA RID: 10698 RVA: 0x0057ECE1 File Offset: 0x0057CEE1
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_Save;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x0057ED07 File Offset: 0x0057CF07
		public void Update()
		{
			if (this._currentBar == null)
			{
				this.TryFindingNPCToTrack();
			}
			if (this._currentBar == null)
			{
				return;
			}
			if (!this._currentBar.ValidateAndCollectNecessaryInfo(ref this._info))
			{
				this._currentBar = null;
			}
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x0057ED3A File Offset: 0x0057CF3A
		public void Draw(SpriteBatch spriteBatch)
		{
			if (this._currentBar == null)
			{
				return;
			}
			this._currentBar.Draw(ref this._info, spriteBatch);
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x0057ED58 File Offset: 0x0057CF58
		private void TryFindingNPCToTrack()
		{
			Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
			rectangle.Inflate(5000, 5000);
			float num = float.PositiveInfinity;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.Hitbox.Intersects(rectangle))
				{
					float num2 = npc.Distance(Main.LocalPlayer.Center);
					if (num > num2 && this.TryTracking(i))
					{
						num = num2;
					}
				}
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x0057EDF8 File Offset: 0x0057CFF8
		public bool TryTracking(int npcIndex)
		{
			if (npcIndex < 0 || npcIndex > Main.maxNPCs)
			{
				return false;
			}
			NPC npc = Main.npc[npcIndex];
			if (!npc.active)
			{
				return false;
			}
			BigProgressBarInfo bigProgressBarInfo = new BigProgressBarInfo
			{
				npcIndexToAimAt = npcIndex
			};
			IBigProgressBar bigProgressBar = this._bossBar;
			IBigProgressBar bigProgressBar2;
			if (this._bossBarsByNpcNetId.TryGetValue(npc.netID, out bigProgressBar2))
			{
				bigProgressBar = bigProgressBar2;
			}
			if (!bigProgressBar.ValidateAndCollectNecessaryInfo(ref bigProgressBarInfo))
			{
				return false;
			}
			this._currentBar = bigProgressBar;
			bigProgressBarInfo.showText = true;
			this._info = bigProgressBarInfo;
			return true;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x0057EE79 File Offset: 0x0057D079
		private void Configuration_Save(Preferences obj)
		{
			obj.Put("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x0057EE90 File Offset: 0x0057D090
		private void Configuration_OnLoad(Preferences obj)
		{
			BigProgressBarSystem.ShowText = obj.Get<bool>("ShowBossBarHealthText", BigProgressBarSystem.ShowText);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x0057EEA7 File Offset: 0x0057D0A7
		public static void ToggleShowText()
		{
			BigProgressBarSystem.ShowText = !BigProgressBarSystem.ShowText;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x0057EEB8 File Offset: 0x0057D0B8
		public BigProgressBarSystem()
		{
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0057F070 File Offset: 0x0057D270
		// Note: this type is marked as 'beforefieldinit'.
		static BigProgressBarSystem()
		{
		}

		// Token: 0x040052B5 RID: 21173
		private IBigProgressBar _currentBar;

		// Token: 0x040052B6 RID: 21174
		private CommonBossBigProgressBar _bossBar = new CommonBossBigProgressBar();

		// Token: 0x040052B7 RID: 21175
		private BigProgressBarInfo _info;

		// Token: 0x040052B8 RID: 21176
		private static TwinsBigProgressBar _twinsBar = new TwinsBigProgressBar();

		// Token: 0x040052B9 RID: 21177
		private static EaterOfWorldsProgressBar _eaterOfWorldsBar = new EaterOfWorldsProgressBar();

		// Token: 0x040052BA RID: 21178
		private static BrainOfCthuluBigProgressBar _brainOfCthuluBar = new BrainOfCthuluBigProgressBar();

		// Token: 0x040052BB RID: 21179
		private static GolemHeadProgressBar _golemBar = new GolemHeadProgressBar();

		// Token: 0x040052BC RID: 21180
		private static MoonLordProgressBar _moonlordBar = new MoonLordProgressBar();

		// Token: 0x040052BD RID: 21181
		private static SolarFlarePillarBigProgressBar _solarPillarBar = new SolarFlarePillarBigProgressBar();

		// Token: 0x040052BE RID: 21182
		private static VortexPillarBigProgressBar _vortexPillarBar = new VortexPillarBigProgressBar();

		// Token: 0x040052BF RID: 21183
		private static NebulaPillarBigProgressBar _nebulaPillarBar = new NebulaPillarBigProgressBar();

		// Token: 0x040052C0 RID: 21184
		private static StardustPillarBigProgressBar _stardustPillarBar = new StardustPillarBigProgressBar();

		// Token: 0x040052C1 RID: 21185
		private static NeverValidProgressBar _neverValid = new NeverValidProgressBar();

		// Token: 0x040052C2 RID: 21186
		private static PirateShipBigProgressBar _pirateShipBar = new PirateShipBigProgressBar();

		// Token: 0x040052C3 RID: 21187
		private static MartianSaucerBigProgressBar _martianSaucerBar = new MartianSaucerBigProgressBar();

		// Token: 0x040052C4 RID: 21188
		private static DeerclopsBigProgressBar _deerclopsBar = new DeerclopsBigProgressBar();

		// Token: 0x040052C5 RID: 21189
		public static bool ShowText = true;

		// Token: 0x040052C6 RID: 21190
		private Dictionary<int, IBigProgressBar> _bossBarsByNpcNetId = new Dictionary<int, IBigProgressBar>
		{
			{
				125,
				BigProgressBarSystem._twinsBar
			},
			{
				126,
				BigProgressBarSystem._twinsBar
			},
			{
				13,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				14,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				15,
				BigProgressBarSystem._eaterOfWorldsBar
			},
			{
				266,
				BigProgressBarSystem._brainOfCthuluBar
			},
			{
				245,
				BigProgressBarSystem._golemBar
			},
			{
				246,
				BigProgressBarSystem._golemBar
			},
			{
				249,
				BigProgressBarSystem._neverValid
			},
			{
				517,
				BigProgressBarSystem._solarPillarBar
			},
			{
				422,
				BigProgressBarSystem._vortexPillarBar
			},
			{
				507,
				BigProgressBarSystem._nebulaPillarBar
			},
			{
				493,
				BigProgressBarSystem._stardustPillarBar
			},
			{
				398,
				BigProgressBarSystem._moonlordBar
			},
			{
				396,
				BigProgressBarSystem._moonlordBar
			},
			{
				397,
				BigProgressBarSystem._moonlordBar
			},
			{
				548,
				BigProgressBarSystem._neverValid
			},
			{
				549,
				BigProgressBarSystem._neverValid
			},
			{
				491,
				BigProgressBarSystem._pirateShipBar
			},
			{
				492,
				BigProgressBarSystem._pirateShipBar
			},
			{
				440,
				BigProgressBarSystem._neverValid
			},
			{
				395,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				393,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				394,
				BigProgressBarSystem._martianSaucerBar
			},
			{
				68,
				BigProgressBarSystem._neverValid
			},
			{
				668,
				BigProgressBarSystem._deerclopsBar
			}
		};

		// Token: 0x040052C7 RID: 21191
		private const string _preferencesKey = "ShowBossBarHealthText";
	}
}
