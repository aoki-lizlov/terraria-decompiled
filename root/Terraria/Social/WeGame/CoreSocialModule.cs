using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using rail;
using ReLogic.OS;
using Terraria.Utilities;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000126 RID: 294
	public class CoreSocialModule : ISocialModule
	{
		// Token: 0x06001B9B RID: 7067
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001B9C RID: 7068 RVA: 0x004FB924 File Offset: 0x004F9B24
		// (remove) Token: 0x06001B9D RID: 7069 RVA: 0x004FB958 File Offset: 0x004F9B58
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

		// Token: 0x06001B9E RID: 7070 RVA: 0x004FB98C File Offset: 0x004F9B8C
		public void Initialize()
		{
			RailGameID railGameID = new RailGameID();
			railGameID.id_ = 2000328UL;
			string[] array;
			if (Main.dedServ)
			{
				array = Environment.GetCommandLineArgs();
			}
			else
			{
				array = new string[] { " " };
			}
			if (rail_api.RailNeedRestartAppForCheckingEnvironment(railGameID, array.Length, array))
			{
				Environment.Exit(1);
			}
			if (!rail_api.RailInitialize())
			{
				Environment.Exit(1);
			}
			this._callbackHelper.RegisterCallback(2, new RailEventCallBackHandler(CoreSocialModule.RailEventCallBack));
			this.isRailValid = true;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.TickThread), null);
			Main.OnTickForThirdPartySoftwareOnly += CoreSocialModule.RailEventTick;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x004FBA2B File Offset: 0x004F9C2B
		public static void RailEventTick()
		{
			rail_api.RailFireEvents();
			if (Monitor.TryEnter(CoreSocialModule._railTickLock))
			{
				Monitor.Pulse(CoreSocialModule._railTickLock);
				Monitor.Exit(CoreSocialModule._railTickLock);
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x004FBA52 File Offset: 0x004F9C52
		private void TickThread(object context)
		{
			Monitor.Enter(CoreSocialModule._railTickLock);
			while (this.isRailValid)
			{
				if (CoreSocialModule.OnTick != null)
				{
					CoreSocialModule.OnTick();
				}
				Monitor.Wait(CoreSocialModule._railTickLock);
			}
			Monitor.Exit(CoreSocialModule._railTickLock);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x004FBA90 File Offset: 0x004F9C90
		public void Shutdown()
		{
			if (Platform.IsWindows)
			{
				Application.ApplicationExit += delegate(object obj, EventArgs evt)
				{
					this.isRailValid = false;
				};
			}
			else
			{
				this.isRailValid = false;
				AppDomain.CurrentDomain.ProcessExit += delegate(object obj, EventArgs evt)
				{
					this.isRailValid = false;
				};
			}
			this._callbackHelper.UnregisterAllCallback();
			rail_api.RailFinalize();
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x004FBAE4 File Offset: 0x004F9CE4
		public static void RailEventCallBack(RAILEventID eventId, EventBase data)
		{
			if (eventId == 2)
			{
				CoreSocialModule.ProcessRailSystemStateChange(((RailSystemStateChanged)data).state);
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x004FBAFA File Offset: 0x004F9CFA
		public static void SaveAndQuitCallBack()
		{
			Main.WeGameRequireExitGame();
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x004FBB01 File Offset: 0x004F9D01
		private static void ProcessRailSystemStateChange(RailSystemState state)
		{
			if (state == 2 || state == 3)
			{
				Terraria.Utilities.MessageBox.Show("检测到WeGame异常，游戏将自动保存进度并退出游戏", "Terraria--WeGame Error", Terraria.Utilities.MessageBoxButtons.OK, Terraria.Utilities.MessageBoxIcon.Error);
				WorldGen.SaveAndQuit(new Action(CoreSocialModule.SaveAndQuitCallBack));
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x004FBB2F File Offset: 0x004F9D2F
		public CoreSocialModule()
		{
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x004FBB42 File Offset: 0x004F9D42
		// Note: this type is marked as 'beforefieldinit'.
		static CoreSocialModule()
		{
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x004FBB4E File Offset: 0x004F9D4E
		[CompilerGenerated]
		private void <Shutdown>b__10_0(object obj, EventArgs evt)
		{
			this.isRailValid = false;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x004FBB4E File Offset: 0x004F9D4E
		[CompilerGenerated]
		private void <Shutdown>b__10_1(object obj, EventArgs evt)
		{
			this.isRailValid = false;
		}

		// Token: 0x0400158D RID: 5517
		private RailCallBackHelper _callbackHelper = new RailCallBackHelper();

		// Token: 0x0400158E RID: 5518
		[CompilerGenerated]
		private static Action OnTick;

		// Token: 0x0400158F RID: 5519
		private static object _railTickLock = new object();

		// Token: 0x04001590 RID: 5520
		private bool isRailValid;
	}
}
