using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;

namespace Terraria.Testing
{
	// Token: 0x02000112 RID: 274
	public static class LockstepDebug
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x004F85DC File Offset: 0x004F67DC
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x004F85E3 File Offset: 0x004F67E3
		public static bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return LockstepDebug.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				LockstepDebug.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x004F85EC File Offset: 0x004F67EC
		private static void Init()
		{
			if (LockstepDebug._init)
			{
				return;
			}
			PipeStream pipeStream;
			try
			{
				NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(LockstepDebug.Identifier);
				namedPipeClientStream.Connect(1);
				pipeStream = namedPipeClientStream;
				Trace.WriteLine("LockstepDebug connected to server.");
			}
			catch (TimeoutException)
			{
				Trace.WriteLine("LockstepDebug waiting for connection from client.");
				LockstepDebug.isHost = true;
				NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(LockstepDebug.Identifier, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.WriteThrough, LockstepDebug.BufSize, LockstepDebug.BufSize);
				namedPipeServerStream.WaitForConnection();
				pipeStream = namedPipeServerStream;
			}
			LockstepDebug._reader = new BinaryReader(pipeStream);
			LockstepDebug._writer = new BinaryWriter(pipeStream);
			LockstepDebug.WriteStep("Init");
			if (LockstepDebug.ReadStep() != "Init")
			{
				throw new Exception("Shared memory communication failed");
			}
			LockstepDebug._init = true;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x004F86A8 File Offset: 0x004F68A8
		public static void Enable()
		{
			LockstepDebug.Init();
			LockstepDebug.Enabled = true;
			bool flag = false;
			if (LockstepDebug.lastSuccessfulStep == 0L)
			{
				Trace.WriteLine("LockstepDebug enabled.");
			}
			else if (LockstepDebug.stepCount == LockstepDebug.lastSuccessfulStep + 1L)
			{
				LockstepDebug.stopAtLastStep = true;
				Trace.WriteLine("LockstepDebug rerun detected. Skipping to and stopping at step " + LockstepDebug.lastSuccessfulStep);
			}
			else
			{
				Trace.WriteLine(string.Format("LockstepDebug rerun detected. Skipping to step {0}. Up to {1} steps to find mismatch.", LockstepDebug.lastSuccessfulStep, LockstepDebug.stepCount - LockstepDebug.lastSuccessfulStep));
				if (LockstepDebug.expensiveStepState.stepRateIncreaseCount == 1)
				{
					flag = true;
				}
			}
			LockstepDebug.WriteStep("Enable");
			if (LockstepDebug.ReadStep() != "Enable")
			{
				throw new Exception("Enable sync failed");
			}
			LockstepDebug.stepCount = 0L;
			LockstepDebug.expensiveStepState = new LockstepDebug.ExpensiveStepRateState(flag);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x004F8778 File Offset: 0x004F6978
		public static void ExpensiveStep<T>(Func<T> expensiveArg, params object[] args)
		{
			if (!LockstepDebug.Enabled)
			{
				return;
			}
			if (LockstepDebug.stepCount + 1L < LockstepDebug.lastSuccessfulStep)
			{
				LockstepDebug.stepCount += 1L;
				return;
			}
			if (LockstepDebug.expensiveStepState.remainingSkips > 0L)
			{
				LockstepDebug.stepCount += 1L;
				LockstepDebug.expensiveStepState.remainingSkips = LockstepDebug.expensiveStepState.remainingSkips - 1L;
				return;
			}
			if (LockstepDebug.isHost)
			{
				LockstepDebug.expensiveStepState.CheckAndIncreaseStepRate();
			}
			LockstepDebug.expensiveStepState.stepTime.Start();
			LockstepDebug.Step(new object[]
			{
				expensiveArg(),
				string.Join(", ", args)
			});
			LockstepDebug.expensiveStepState.stepTime.Stop();
			LockstepDebug.expensiveStepState.timedStepCount = LockstepDebug.expensiveStepState.timedStepCount + 1L;
			LockstepDebug.expensiveStepState.remainingSkips = LockstepDebug.expensiveStepState.stepRate - 1L;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x004F8853 File Offset: 0x004F6A53
		public static void Step(params object[] args)
		{
			LockstepDebug.Step(string.Join(", ", args));
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x004F8868 File Offset: 0x004F6A68
		public static void Step(string state)
		{
			if (!LockstepDebug.Enabled)
			{
				return;
			}
			if (state.Length > LockstepDebug.BufSize / 2)
			{
				throw new ArgumentException("String too large");
			}
			LockstepDebug.stepCount += 1L;
			if (LockstepDebug.stepCount < LockstepDebug.lastSuccessfulStep)
			{
				return;
			}
			LockstepDebug.WriteStep(state);
			string text = LockstepDebug.ReadStep();
			if (!(text != state))
			{
				if (LockstepDebug.stepCount == LockstepDebug.lastSuccessfulStep && LockstepDebug.stopAtLastStep)
				{
					Trace.WriteLine("LockstepDebug reached the last match from the previous run. Debug from here to identify desync");
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
					else
					{
						Debugger.Launch();
					}
				}
				LockstepDebug.lastSuccessfulStep = LockstepDebug.stepCount;
				return;
			}
			LockstepDebug.Enabled = false;
			Trace.WriteLine(string.Format("Lockstep mismatch. Step: {0}\nSent: {1}\nRecv: {2}", LockstepDebug.stepCount, state, text));
			if (LockstepDebug.lastSuccessfulStep < LockstepDebug.stepCount - 1L)
			{
				Trace.WriteLine(string.Format("Expensive steps were skipped. Rerun to narrow down the mismatch. Last successful step was {0} steps ago", LockstepDebug.stepCount - LockstepDebug.lastSuccessfulStep));
				return;
			}
			if (LockstepDebug.lastSuccessfulStep == LockstepDebug.stepCount)
			{
				Trace.WriteLine("Last successful step mismatch. The rerun was not deterministic.");
			}
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			Debugger.Launch();
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x004F897B File Offset: 0x004F6B7B
		private static void WriteStep(string state)
		{
			LockstepDebug._writer.Write(state);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x004F8988 File Offset: 0x004F6B88
		private static string ReadStep()
		{
			string text;
			for (;;)
			{
				text = LockstepDebug._reader.ReadString();
				if (!text.StartsWith(LockstepDebug._controlCode))
				{
					break;
				}
				LockstepDebug.HandleControlMessage(text.Substring(LockstepDebug._controlCode.Length));
			}
			return text;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x004F89C5 File Offset: 0x004F6BC5
		private static void WriteControlMessage(string s)
		{
			LockstepDebug._writer.Write(LockstepDebug._controlCode + s);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x004F89DC File Offset: 0x004F6BDC
		private static void HandleControlMessage(string v)
		{
			if (v.StartsWith("StepRate: "))
			{
				int num = int.Parse(v.Substring("StepRate: ".Length));
				Trace.WriteLine("LockstepDebug control message received. Reducing step rate to 1 in " + num);
				LockstepDebug.expensiveStepState.stepRateIncreaseCount = LockstepDebug.expensiveStepState.stepRateIncreaseCount + 1;
				LockstepDebug.expensiveStepState.stepRate = (long)num;
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x004F8A3C File Offset: 0x004F6C3C
		// Note: this type is marked as 'beforefieldinit'.
		static LockstepDebug()
		{
		}

		// Token: 0x0400151E RID: 5406
		[CompilerGenerated]
		private static bool <Enabled>k__BackingField;

		// Token: 0x0400151F RID: 5407
		private static long stepCount;

		// Token: 0x04001520 RID: 5408
		private static long lastSuccessfulStep;

		// Token: 0x04001521 RID: 5409
		private static bool stopAtLastStep;

		// Token: 0x04001522 RID: 5410
		private static LockstepDebug.ExpensiveStepRateState expensiveStepState;

		// Token: 0x04001523 RID: 5411
		private static bool _init;

		// Token: 0x04001524 RID: 5412
		private static bool isHost;

		// Token: 0x04001525 RID: 5413
		private static string Identifier = "Terraria.LockstepDebug";

		// Token: 0x04001526 RID: 5414
		private static int BufSize = 65535;

		// Token: 0x04001527 RID: 5415
		private static BinaryReader _reader;

		// Token: 0x04001528 RID: 5416
		private static BinaryWriter _writer;

		// Token: 0x04001529 RID: 5417
		private static readonly object _lock = new object();

		// Token: 0x0400152A RID: 5418
		private static string _controlCode = "ģ4䕧";

		// Token: 0x02000722 RID: 1826
		private struct ExpensiveStepRateState
		{
			// Token: 0x06004068 RID: 16488 RVA: 0x0069E064 File Offset: 0x0069C264
			public ExpensiveStepRateState(bool increaseThreshold)
			{
				this.stepRate = 1L;
				this.remainingSkips = 0L;
				this.stepRateIncreaseCount = 0;
				this.stepRateIncreaseThreshold = TimeSpan.FromSeconds((double)(increaseThreshold ? 30 : 20));
				this.stepTime = new Stopwatch();
				this.timedStepCount = 0L;
			}

			// Token: 0x06004069 RID: 16489 RVA: 0x0069E0B0 File Offset: 0x0069C2B0
			internal void CheckAndIncreaseStepRate()
			{
				if (this.stepTime.Elapsed < this.stepRateIncreaseThreshold || this.timedStepCount <= 1L)
				{
					return;
				}
				this.stepRateIncreaseCount++;
				this.stepRate *= this.timedStepCount;
				this.timedStepCount = 0L;
				this.stepTime.Restart();
				Trace.WriteLine("LockstepDebug is taking too long. Reducing step rate to 1 in " + this.stepRate);
				LockstepDebug.WriteControlMessage("StepRate: " + this.stepRate);
			}

			// Token: 0x04006959 RID: 26969
			public long stepRate;

			// Token: 0x0400695A RID: 26970
			public long remainingSkips;

			// Token: 0x0400695B RID: 26971
			public int stepRateIncreaseCount;

			// Token: 0x0400695C RID: 26972
			public TimeSpan stepRateIncreaseThreshold;

			// Token: 0x0400695D RID: 26973
			public Stopwatch stepTime;

			// Token: 0x0400695E RID: 26974
			public long timedStepCount;
		}
	}
}
