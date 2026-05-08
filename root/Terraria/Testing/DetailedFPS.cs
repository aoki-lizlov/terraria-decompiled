using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.Utilities;

namespace Terraria.Testing
{
	// Token: 0x02000110 RID: 272
	public static class DetailedFPS
	{
		// Token: 0x06001AB8 RID: 6840 RVA: 0x004F7CA0 File Offset: 0x004F5EA0
		static DetailedFPS()
		{
			for (int i = 0; i < DetailedFPS.Frames.Length; i++)
			{
				DetailedFPS.Frames[i].Init();
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x004F7D1C File Offset: 0x004F5F1C
		public static void StartNextFrame()
		{
			TimeLogger.StartNextFrame();
			DetailedFPS.Frames[DetailedFPS.newest].Finish();
			DetailedFPS.newest++;
			if (DetailedFPS.newest == DetailedFPS.Frames.Length)
			{
				DetailedFPS.newest = 0;
			}
			if (DetailedFPS.newest == DetailedFPS.oldest)
			{
				DetailedFPS.oldest++;
			}
			if (DetailedFPS.oldest == DetailedFPS.Frames.Length)
			{
				DetailedFPS.oldest = 0;
			}
			DetailedFPS.Frames[DetailedFPS.newest].Start();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x004F7DA2 File Offset: 0x004F5FA2
		public static void Begin(DetailedFPS.OperationCategory category)
		{
			DetailedFPS.Frames[DetailedFPS.newest].Begin(category);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x004F7DB9 File Offset: 0x004F5FB9
		public static void End()
		{
			TimeLogger.EndDrawFrame();
			DetailedFPS.Begin(DetailedFPS.OperationCategory.Idle);
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x004F7DC8 File Offset: 0x004F5FC8
		public static TimeSpan CurrentFrameTime
		{
			get
			{
				DetailedFPS.Frame frame = DetailedFPS.Frames[DetailedFPS.newest];
				return Utils.SWTicksToTimeSpan(frame.events.Last<DetailedFPS.Frame.Event>().timestamp - frame.events[0].timestamp);
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x004F7E0C File Offset: 0x004F600C
		public static float GetCPUUtilization(int numFrames)
		{
			long[] array = new long[6];
			int num = 0;
			foreach (DetailedFPS.Frame frame in DetailedFPS.EnumerateFrames())
			{
				if (num++ == numFrames)
				{
					break;
				}
				if (frame.events.Count >= 2)
				{
					DetailedFPS.Frame.Event @event = frame.events[0];
					foreach (DetailedFPS.Frame.Event event2 in frame.events)
					{
						array[(int)@event.category] += event2.timestamp - @event.timestamp;
						@event = event2;
					}
				}
			}
			long num2 = array.Sum();
			return (float)((double)(array[2] + array[1]) / (double)num2);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x004F7F04 File Offset: 0x004F6104
		public static bool VsyncAppearsActive()
		{
			long num = 0L;
			int num2 = 60;
			int num3 = 0;
			foreach (DetailedFPS.Frame frame in DetailedFPS.EnumerateFrames())
			{
				if (num3++ == num2)
				{
					break;
				}
				if (frame.events.Count >= 2)
				{
					DetailedFPS.Frame.Event @event = frame.events[0];
					foreach (DetailedFPS.Frame.Event event2 in frame.events)
					{
						if (@event.category == DetailedFPS.OperationCategory.Present)
						{
							num += event2.timestamp - @event.timestamp;
						}
						@event = event2;
					}
				}
			}
			return Utils.SWTicksToTimeSpan(num / (long)num2).TotalSeconds >= Main.TARGET_FRAME_TIME * 0.1;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x004F8008 File Offset: 0x004F6208
		private static TimeSpan GetGCPauseTime()
		{
			TimeSpan timeSpan = NewRuntimeMethods.GC_GetTotalPauseDuration();
			TimeSpan timeSpan2 = timeSpan - DetailedFPS.LastGCPauseTime;
			DetailedFPS.LastGCPauseTime = timeSpan;
			return timeSpan2;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x004F802C File Offset: 0x004F622C
		private static int GetCollectionCount(int gen)
		{
			int num = GC.CollectionCount(gen);
			int num2 = num - DetailedFPS.LastCollectionCount[gen];
			DetailedFPS.LastCollectionCount[gen] = num;
			return num2;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x004F8054 File Offset: 0x004F6254
		private static int GetAllocatedBytes()
		{
			long num = NewRuntimeMethods.GC_GetTotalAllocatedBytes();
			long num2 = num - DetailedFPS.LastAllocatedBytes;
			DetailedFPS.LastAllocatedBytes = num;
			return (int)num2;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x004F8075 File Offset: 0x004F6275
		private static IEnumerable<DetailedFPS.Frame> EnumerateFrames()
		{
			int i = DetailedFPS.newest;
			while (i != DetailedFPS.oldest)
			{
				int num = i - 1;
				i = num;
				if (num < 0)
				{
					i = DetailedFPS.Frames.Length - 1;
				}
				yield return DetailedFPS.Frames[i];
			}
			yield break;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x004F8080 File Offset: 0x004F6280
		public static void Draw()
		{
			Rectangle rectangle = new Rectangle((Main.screenWidth - DetailedFPS.Frames.Length * 2) / 2, Main.screenHeight - 100, DetailedFPS.Frames.Length * 2, 100);
			DetailedFPS.DrawFPSBox(rectangle);
			int num = 0;
			long num2 = 0L;
			foreach (DetailedFPS.Frame frame in DetailedFPS.EnumerateFrames())
			{
				num++;
				DetailedFPS.DrawFrame(rectangle.Right - num * 2, frame);
				num2 += frame.Allocated;
			}
			if (num2 > 0L)
			{
				long num3 = num2 / (long)num;
				DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, string.Format("Avg Alloc: {0,5} bytes/frame", num3), new Vector2((float)((Main.screenWidth - DetailedFPS.Frames.Length * 2) / 2 - 240), (float)(Main.screenHeight - 24)), Color.White);
			}
			if (Main.keyState.PressingAlt())
			{
				DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, string.Format("Time Acc: {0,5:0.0} ms", Main.UpdateTimeAccumulator * 1000.0), new Vector2((float)(Main.screenWidth - 200), (float)(Main.screenHeight - 24)), Color.White);
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x004F81D4 File Offset: 0x004F63D4
		private static void DrawFPSBox(Rectangle r)
		{
			Color white = Color.White;
			DetailedFPS.DrawRect(new Rectangle(r.Left, r.Y, 2, r.Height), white);
			DetailedFPS.DrawRect(new Rectangle(r.Right, r.Y, 2, r.Height), white);
			DetailedFPS.DrawRect(new Rectangle(r.Left, r.Y, r.Width, 1), white);
			int num = 24;
			DetailedFPS.OperationCategory operationCategory = DetailedFPS.OperationCategory.Idle;
			while (operationCategory <= DetailedFPS.OperationCategory.GC)
			{
				if (operationCategory != DetailedFPS.OperationCategory.GC || !(DetailedFPS.LastGCPauseTime == TimeSpan.Zero))
				{
					DetailedFPS.DrawRect(new Rectangle(r.Right + 8, r.Bottom - num + 8, 8, 8), DetailedFPS.GetColor(operationCategory));
					DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, operationCategory.ToString(), new Vector2((float)(r.Right + 20), (float)(r.Bottom - num)), Color.White);
				}
				operationCategory++;
				num += 24;
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x004F82DC File Offset: 0x004F64DC
		private static void DrawFrame(int x, DetailedFPS.Frame frame)
		{
			if (frame.events.Count < 2)
			{
				return;
			}
			int num = 0;
			DetailedFPS.Frame.Event @event = frame.events[0];
			long timestamp = @event.timestamp;
			for (int i = 1; i < frame.events.Count; i++)
			{
				DetailedFPS.Frame.Event event2 = frame.events[i];
				int num2 = (int)(Utils.SWTicksToTimeSpan(@event.timestamp - timestamp).TotalMilliseconds * 6.0);
				int num3 = (int)(Utils.SWTicksToTimeSpan(event2.timestamp - timestamp).TotalMilliseconds * 6.0);
				DetailedFPS.DrawRect(new Rectangle(x, Main.screenHeight - num3, 2, num3 - num2), DetailedFPS.GetColor(@event.category));
				@event = event2;
				num = num3;
			}
			num = Math.Max(num, 100);
			for (int j = 0; j <= GC.MaxGeneration; j++)
			{
				for (int k = 0; k < frame.CollectionCount[j]; k++)
				{
					DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, DetailedFPS._gcGenText[j], new Vector2((float)(x - 10), (float)(Main.screenHeight - num - 15)), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f, null, null);
					num += 10;
				}
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x004F8428 File Offset: 0x004F6628
		private static Color GetColor(DetailedFPS.OperationCategory category)
		{
			switch (category)
			{
			case DetailedFPS.OperationCategory.Idle:
				return Color.Gray;
			case DetailedFPS.OperationCategory.Update:
				return Color.Orange;
			case DetailedFPS.OperationCategory.Draw:
				return Color.Green;
			case DetailedFPS.OperationCategory.Present:
				return Color.Magenta;
			case DetailedFPS.OperationCategory.GC:
				return Color.Blue;
			default:
				return Color.Black;
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x004F8474 File Offset: 0x004F6674
		private static void DrawRect(Rectangle r, Color c)
		{
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, r, c);
		}

		// Token: 0x04001511 RID: 5393
		public static readonly int FrameCount = 300;

		// Token: 0x04001512 RID: 5394
		private static DetailedFPS.Frame[] Frames = new DetailedFPS.Frame[DetailedFPS.FrameCount];

		// Token: 0x04001513 RID: 5395
		private static int oldest;

		// Token: 0x04001514 RID: 5396
		private static int newest;

		// Token: 0x04001515 RID: 5397
		private static TimeSpan LastGCPauseTime;

		// Token: 0x04001516 RID: 5398
		private static int[] LastCollectionCount = new int[GC.MaxGeneration + 1];

		// Token: 0x04001517 RID: 5399
		private static long LastAllocatedBytes;

		// Token: 0x04001518 RID: 5400
		private const int PixelsPerMs = 6;

		// Token: 0x04001519 RID: 5401
		private const int FrameWidth = 2;

		// Token: 0x0400151A RID: 5402
		private const int BoxHeight = 100;

		// Token: 0x0400151B RID: 5403
		private static string[] _gcGenText = new string[] { "G0", "G1", "G2" };

		// Token: 0x0200071F RID: 1823
		public enum OperationCategory
		{
			// Token: 0x0400694B RID: 26955
			Idle,
			// Token: 0x0400694C RID: 26956
			Update,
			// Token: 0x0400694D RID: 26957
			Draw,
			// Token: 0x0400694E RID: 26958
			Present,
			// Token: 0x0400694F RID: 26959
			GC,
			// Token: 0x04006950 RID: 26960
			End,
			// Token: 0x04006951 RID: 26961
			Count
		}

		// Token: 0x02000720 RID: 1824
		private struct Frame
		{
			// Token: 0x0600405C RID: 16476 RVA: 0x0069DDF0 File Offset: 0x0069BFF0
			public void Init()
			{
				this.events = new List<DetailedFPS.Frame.Event>(16);
				this.CollectionCount = new int[GC.MaxGeneration + 1];
			}

			// Token: 0x0600405D RID: 16477 RVA: 0x0069DE11 File Offset: 0x0069C011
			public void Start()
			{
				this.events.Clear();
				this.Begin(DetailedFPS.OperationCategory.Idle);
			}

			// Token: 0x0600405E RID: 16478 RVA: 0x0069DE28 File Offset: 0x0069C028
			public void Begin(DetailedFPS.OperationCategory category)
			{
				if (this.events.Count >= 1000)
				{
					return;
				}
				if (this.events.Count > 0 && this.events.Last<DetailedFPS.Frame.Event>().category == category)
				{
					return;
				}
				long timestamp = Stopwatch.GetTimestamp();
				if (this.events.Count > 0)
				{
					DetailedFPS.Frame.Event @event = this.events.Last<DetailedFPS.Frame.Event>();
					if (@event.category == DetailedFPS.OperationCategory.Draw || @event.category == DetailedFPS.OperationCategory.Update)
					{
						TimeLogger.TotalDrawAndUpdate.Add((int)(timestamp - @event.timestamp));
					}
				}
				this.events.Add(new DetailedFPS.Frame.Event(category, timestamp));
				TimeSpan gcpauseTime = DetailedFPS.GetGCPauseTime();
				if (gcpauseTime > TimeSpan.Zero)
				{
					long num = Utils.TimeSpanToSWTicks(gcpauseTime);
					this.events.Insert(this.events.Count - 1, new DetailedFPS.Frame.Event(DetailedFPS.OperationCategory.GC, timestamp - num));
					TimeLogger.GCPause.Add((int)num);
				}
			}

			// Token: 0x0600405F RID: 16479 RVA: 0x0069DF08 File Offset: 0x0069C108
			public void Finish()
			{
				this.Begin(DetailedFPS.OperationCategory.End);
				for (int i = 0; i <= GC.MaxGeneration; i++)
				{
					this.CollectionCount[i] = DetailedFPS.GetCollectionCount(i);
				}
				if (Main.CollectGen0EveryFrame)
				{
					this.CollectionCount[0]--;
				}
				this.Allocated = (long)DetailedFPS.GetAllocatedBytes();
			}

			// Token: 0x04006952 RID: 26962
			public List<DetailedFPS.Frame.Event> events;

			// Token: 0x04006953 RID: 26963
			public int[] CollectionCount;

			// Token: 0x04006954 RID: 26964
			public long Allocated;

			// Token: 0x02000A88 RID: 2696
			public struct Event
			{
				// Token: 0x06004BA7 RID: 19367 RVA: 0x006D8C64 File Offset: 0x006D6E64
				public Event(DetailedFPS.OperationCategory category, long timestamp)
				{
					this.category = category;
					this.timestamp = timestamp;
				}

				// Token: 0x04007768 RID: 30568
				public DetailedFPS.OperationCategory category;

				// Token: 0x04007769 RID: 30569
				public long timestamp;
			}
		}

		// Token: 0x02000721 RID: 1825
		[CompilerGenerated]
		private sealed class <EnumerateFrames>d__20 : IEnumerable<DetailedFPS.Frame>, IEnumerable, IEnumerator<DetailedFPS.Frame>, IDisposable, IEnumerator
		{
			// Token: 0x06004060 RID: 16480 RVA: 0x0069DF5E File Offset: 0x0069C15E
			[DebuggerHidden]
			public <EnumerateFrames>d__20(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06004061 RID: 16481 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004062 RID: 16482 RVA: 0x0069DF80 File Offset: 0x0069C180
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					i = DetailedFPS.newest;
				}
				if (i == DetailedFPS.oldest)
				{
					return false;
				}
				int num2 = i - 1;
				i = num2;
				if (num2 < 0)
				{
					i = DetailedFPS.Frames.Length - 1;
				}
				this.<>2__current = DetailedFPS.Frames[i];
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06004063 RID: 16483 RVA: 0x0069E008 File Offset: 0x0069C208
			DetailedFPS.Frame IEnumerator<DetailedFPS.Frame>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004064 RID: 16484 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000515 RID: 1301
			// (get) Token: 0x06004065 RID: 16485 RVA: 0x0069E010 File Offset: 0x0069C210
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004066 RID: 16486 RVA: 0x0069E020 File Offset: 0x0069C220
			[DebuggerHidden]
			IEnumerator<DetailedFPS.Frame> IEnumerable<DetailedFPS.Frame>.GetEnumerator()
			{
				DetailedFPS.<EnumerateFrames>d__20 <EnumerateFrames>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<EnumerateFrames>d__ = this;
				}
				else
				{
					<EnumerateFrames>d__ = new DetailedFPS.<EnumerateFrames>d__20(0);
				}
				return <EnumerateFrames>d__;
			}

			// Token: 0x06004067 RID: 16487 RVA: 0x0069E05C File Offset: 0x0069C25C
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Terraria.Testing.DetailedFPS.Frame>.GetEnumerator();
			}

			// Token: 0x04006955 RID: 26965
			private int <>1__state;

			// Token: 0x04006956 RID: 26966
			private DetailedFPS.Frame <>2__current;

			// Token: 0x04006957 RID: 26967
			private int <>l__initialThreadId;

			// Token: 0x04006958 RID: 26968
			private int <k>5__2;
		}
	}
}
