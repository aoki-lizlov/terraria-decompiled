using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ReLogic.Utilities
{
	// Token: 0x02000004 RID: 4
	public class MultiTimer
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		public MultiTimer(int ticksBetweenPrint = 100)
		{
			this._ticksBetweenPrint = ticksBetweenPrint;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021ED File Offset: 0x000003ED
		public void Start()
		{
			this._timer.Reset();
			this._timer.Start();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002208 File Offset: 0x00000408
		public void Record(string key)
		{
			this._timer.Stop();
			double totalMilliseconds = this._timer.Elapsed.TotalMilliseconds;
			MultiTimer.TimerData timerData;
			if (!this._timerDataMap.TryGetValue(key, ref timerData))
			{
				this._timerDataMap.Add(key, new MultiTimer.TimerData(totalMilliseconds));
			}
			else
			{
				this._timerDataMap[key] = timerData.AddTick(totalMilliseconds);
			}
			this._timer.Reset();
			this._timer.Start();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002284 File Offset: 0x00000484
		public bool StopAndPrint()
		{
			this._timer.Stop();
			this._ticksElapsedForPrint++;
			if (this._ticksElapsedForPrint == this._ticksBetweenPrint)
			{
				this._ticksElapsedForPrint = 0;
				Console.WriteLine("Average elapsed time: ");
				double num = 0.0;
				int num2 = 0;
				foreach (KeyValuePair<string, MultiTimer.TimerData> keyValuePair in this._timerDataMap)
				{
					num2 = Math.Max(keyValuePair.Key.Length, num2);
				}
				foreach (KeyValuePair<string, MultiTimer.TimerData> keyValuePair2 in this._timerDataMap)
				{
					MultiTimer.TimerData value = keyValuePair2.Value;
					string text = new string(' ', num2 - keyValuePair2.Key.Length);
					Console.WriteLine(string.Concat(new object[]
					{
						keyValuePair2.Key,
						text,
						" : (Average: ",
						value.Average.ToString("F4"),
						" Min: ",
						value.Min.ToString("F4"),
						" Max: ",
						value.Max.ToString("F4"),
						" from ",
						(int)value.Ticks,
						" records)"
					}));
					num += value.Total;
				}
				this._timerDataMap.Clear();
				Console.WriteLine("Total : " + (float)num / (float)this._ticksBetweenPrint + "ms");
				return true;
			}
			return false;
		}

		// Token: 0x04000002 RID: 2
		private readonly int _ticksBetweenPrint;

		// Token: 0x04000003 RID: 3
		private int _ticksElapsedForPrint;

		// Token: 0x04000004 RID: 4
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x04000005 RID: 5
		private readonly Dictionary<string, MultiTimer.TimerData> _timerDataMap = new Dictionary<string, MultiTimer.TimerData>();

		// Token: 0x020000AB RID: 171
		private struct TimerData
		{
			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000DD83 File Offset: 0x0000BF83
			public double Average
			{
				get
				{
					return this.Total / this.Ticks;
				}
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0000DD92 File Offset: 0x0000BF92
			private TimerData(double min, double max, double ticks, double total)
			{
				this.Min = min;
				this.Max = max;
				this.Ticks = ticks;
				this.Total = total;
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
			public TimerData(double startTime)
			{
				this.Min = startTime;
				this.Max = startTime;
				this.Ticks = 1.0;
				this.Total = startTime;
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x0000DDD7 File Offset: 0x0000BFD7
			public MultiTimer.TimerData AddTick(double time)
			{
				return new MultiTimer.TimerData(Math.Min(this.Min, time), Math.Max(this.Max, time), this.Ticks + 1.0, this.Total + time);
			}

			// Token: 0x0400053D RID: 1341
			public readonly double Min;

			// Token: 0x0400053E RID: 1342
			public readonly double Max;

			// Token: 0x0400053F RID: 1343
			public readonly double Ticks;

			// Token: 0x04000540 RID: 1344
			public readonly double Total;
		}
	}
}
