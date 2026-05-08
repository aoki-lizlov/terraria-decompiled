using System;
using System.Runtime.CompilerServices;
using System.Threading;
using ReLogic.OS;
using Steamworks;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
	// Token: 0x02000146 RID: 326
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001CC7 RID: 7367 RVA: 0x004FFA64 File Offset: 0x004FDC64
		// (remove) Token: 0x06001CC8 RID: 7368 RVA: 0x004FFA98 File Offset: 0x004FDC98
		public static event Action OnTick
		{
			[CompilerGenerated]
			add
			{
				Action action = CoreSocialModule.OnTick;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref CoreSocialModule.OnTick, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = CoreSocialModule.OnTick;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref CoreSocialModule.OnTick, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00009E46 File Offset: 0x00008046
		public static void SetSkipPulsing(bool shouldSkipPausing)
		{
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x004FFACC File Offset: 0x004FDCCC
		public void Initialize()
		{
			CoreSocialModule._instance = this;
			if (!Main.dedServ && SteamAPI.RestartAppIfNecessary(new AppId_t(105600U)))
			{
				Environment.Exit(1);
				return;
			}
			if (!SteamAPI.Init())
			{
				MessageBox.Show(Language.GetTextValue("Error.LaunchFromSteam"), Language.GetTextValue("Error.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
				Environment.Exit(1);
			}
			SteamInput.Init(false);
			this.IsSteamValid = true;
			new Thread(new ParameterizedThreadStart(this.SteamCallbackLoop))
			{
				IsBackground = true
			}.Start();
			new Thread(new ParameterizedThreadStart(this.SteamTickLoop))
			{
				IsBackground = true
			}.Start();
			Main.OnTickForThirdPartySoftwareOnly += this.PulseSteamTick;
			Main.OnTickForThirdPartySoftwareOnly += this.PulseSteamCallback;
			if (Platform.IsOSX && !Main.dedServ)
			{
				this._onOverlayActivated = Callback<GameOverlayActivated_t>.Create(new Callback<GameOverlayActivated_t>.DispatchDelegate(this.OnOverlayActivated));
			}
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x004FFBB7 File Offset: 0x004FDDB7
		public void PulseSteamTick()
		{
			if (Monitor.TryEnter(this._steamTickLock))
			{
				Monitor.Pulse(this._steamTickLock);
				Monitor.Exit(this._steamTickLock);
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x004FFBDC File Offset: 0x004FDDDC
		public void PulseSteamCallback()
		{
			if (Monitor.TryEnter(this._steamCallbackLock))
			{
				Monitor.Pulse(this._steamCallbackLock);
				Monitor.Exit(this._steamCallbackLock);
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x004FFC01 File Offset: 0x004FDE01
		public static void Pulse()
		{
			CoreSocialModule._instance.PulseSteamTick();
			CoreSocialModule._instance.PulseSteamCallback();
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x004FFC18 File Offset: 0x004FDE18
		private void SteamTickLoop(object context)
		{
			Monitor.Enter(this._steamTickLock);
			while (this.IsSteamValid)
			{
				if (this._skipPulsing)
				{
					Monitor.Wait(this._steamCallbackLock);
				}
				else
				{
					if (CoreSocialModule.OnTick != null)
					{
						CoreSocialModule.OnTick();
					}
					Monitor.Wait(this._steamTickLock);
				}
			}
			Monitor.Exit(this._steamTickLock);
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x004FFC78 File Offset: 0x004FDE78
		private void SteamCallbackLoop(object context)
		{
			Monitor.Enter(this._steamCallbackLock);
			while (this.IsSteamValid)
			{
				if (this._skipPulsing)
				{
					Monitor.Wait(this._steamCallbackLock);
				}
				else
				{
					SteamAPI.RunCallbacks();
					Monitor.Wait(this._steamCallbackLock);
				}
			}
			Monitor.Exit(this._steamCallbackLock);
			SteamAPI.Shutdown();
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x004FFCD1 File Offset: 0x004FDED1
		public void Shutdown()
		{
			this.IsSteamValid = false;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x004FFCDA File Offset: 0x004FDEDA
		public void OnOverlayActivated(GameOverlayActivated_t result)
		{
			Main.instance.IsMouseVisible = result.m_bActive == 1;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x004FFCEF File Offset: 0x004FDEEF
		public CoreSocialModule()
		{
		}

		// Token: 0x040015F1 RID: 5617
		private static CoreSocialModule _instance;

		// Token: 0x040015F2 RID: 5618
		public const int SteamAppId = 105600;

		// Token: 0x040015F3 RID: 5619
		private bool IsSteamValid;

		// Token: 0x040015F4 RID: 5620
		[CompilerGenerated]
		private static Action OnTick;

		// Token: 0x040015F5 RID: 5621
		private object _steamTickLock = new object();

		// Token: 0x040015F6 RID: 5622
		private object _steamCallbackLock = new object();

		// Token: 0x040015F7 RID: 5623
		private Callback<GameOverlayActivated_t> _onOverlayActivated;

		// Token: 0x040015F8 RID: 5624
		private bool _skipPulsing;
	}
}
