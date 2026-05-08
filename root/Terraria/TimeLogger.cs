using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using ReLogic.OS;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameInput;
using Terraria.Testing;

namespace Terraria
{
	// Token: 0x02000052 RID: 82
	public static class TimeLogger
	{
		// Token: 0x06000F1E RID: 3870 RVA: 0x00409AA0 File Offset: 0x00407CA0
		private static TimeLogger.TimeLogData NewEntry(string name, TimeSpan? budget = null)
		{
			TimeLogger.TimeLogData timeLogData = new TimeLogger.TimeLogData
			{
				name = name,
				format = new Func<int, string>(TimeLogger.FormatTicks),
				budget = (int)Utils.TimeSpanToSWTicks(budget ?? TimeSpan.FromMilliseconds(2.0))
			};
			TimeLogger.entries.Add(timeLogData);
			return timeLogData;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00409B08 File Offset: 0x00407D08
		private static TimeLogger.TimeLogData NewCounterEntry(string name, int budget)
		{
			TimeLogger.TimeLogData timeLogData = new TimeLogger.TimeLogData();
			timeLogData.name = name;
			timeLogData.format = (int i) => TimeLogger._intFormat.Format((double)i);
			timeLogData.budget = budget;
			TimeLogger.TimeLogData timeLogData2 = timeLogData;
			TimeLogger.entries.Add(timeLogData2);
			return timeLogData2;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00409B5C File Offset: 0x00407D5C
		static TimeLogger()
		{
			for (int i = 0; i < TimeLogger.TotalDrawByRenderCount.Length; i++)
			{
				TimeLogger.TotalDrawByRenderCount[i] = TimeLogger.NewEntry("Total Draw (Render #" + i + ")", new TimeSpan?(TimeSpan.FromMilliseconds(15.0)));
			}
			TimeLogger.LightingByPass = new TimeLogger.TimeLogData[4];
			for (int j = 0; j < TimeLogger.LightingByPass.Length; j++)
			{
				TimeLogger.LightingByPass[j] = TimeLogger.NewEntry("Lighting Pass #" + j, new TimeSpan?(TimeSpan.FromMilliseconds(1.0)));
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0040A436 File Offset: 0x00408636
		public static void Reset()
		{
			TimeLogger.OnNextFrame(delegate
			{
				foreach (TimeLogger.TimeLogData timeLogData in TimeLogger.entries)
				{
					timeLogData.Reset();
				}
			});
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0040A45C File Offset: 0x0040865C
		public static void ToggleLogging()
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.endLoggingThisFrame = true;
				TimeLogger.startLoggingNextFrame = false;
				return;
			}
			TimeLogger.startLoggingNextFrame = true;
			TimeLogger.endLoggingThisFrame = false;
			Main.NewText("Detailed logging started", 250, 250, 0);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0040A493 File Offset: 0x00408693
		public static void OnNextFrame(Action action)
		{
			TimeLogger._onNextFrame.Enqueue(action);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0040A4A0 File Offset: 0x004086A0
		public static void StartNextFrame()
		{
			while (TimeLogger._onNextFrame.Count > 0)
			{
				TimeLogger._onNextFrame.Dequeue()();
			}
			TimeLogger.ABTest();
			foreach (TimeLogger.TimeLogData timeLogData in TimeLogger.entries)
			{
				timeLogData.StartNextFrame();
			}
			if (TimeLogger.startLoggingNextFrame)
			{
				TimeLogger.startLoggingNextFrame = false;
				DateTime now = DateTime.Now;
				string text = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "TerrariaDrawLog.7z";
				try
				{
					TimeLogger.logWriter = new StreamWriter(new GZipStream(new FileStream(text, FileMode.Create), CompressionMode.Compress));
					TimeLogger.logBuilder = new StringBuilder(5000);
					TimeLogger.framesToLog = 600;
					TimeLogger.currentFrame = 1;
					TimeLogger.currentlyLogging = true;
				}
				catch
				{
					Main.NewText("Detailed logging could not be started.", 250, 250, 0);
				}
			}
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("Start of Frame #{0}", TimeLogger.currentFrame));
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0040A5CC File Offset: 0x004087CC
		public static void EndDrawFrame()
		{
			if (TimeLogger.currentFrame <= TimeLogger.framesToLog)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("End of Frame #{0}", TimeLogger.currentFrame));
				TimeLogger.logBuilder.AppendLine();
				if (TimeLogger.endLoggingThisFrame)
				{
					TimeLogger.endLoggingThisFrame = false;
					TimeLogger.logBuilder.AppendLine("Logging ended early");
					TimeLogger.currentFrame = TimeLogger.framesToLog;
				}
				if (TimeLogger.logBuilder.Length > 4000)
				{
					TimeLogger.logWriter.Write(TimeLogger.logBuilder.ToString());
					TimeLogger.logBuilder.Clear();
				}
				TimeLogger.currentFrame++;
				if (TimeLogger.currentFrame > TimeLogger.framesToLog)
				{
					Main.NewText("Detailed logging ended.", 250, 250, 0);
					TimeLogger.logWriter.Write(TimeLogger.logBuilder.ToString());
					TimeLogger.logBuilder.Clear();
					TimeLogger.logBuilder = null;
					TimeLogger.logWriter.Flush();
					TimeLogger.logWriter.Close();
					TimeLogger.logWriter = null;
					TimeLogger.framesToLog = -1;
					TimeLogger.currentFrame = 0;
					TimeLogger.currentlyLogging = false;
				}
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0040A6E8 File Offset: 0x004088E8
		private static void LogAdd(string logText, int ticks, bool newMax)
		{
			if (TimeLogger.currentFrame != 0)
			{
				TimeLogger.logBuilder.AppendLine(string.Format("    {0} : {1:F4}ms {2}", logText, Utils.SWTicksToTimeSpan((long)ticks).TotalMilliseconds, newMax ? " - New Maximum" : string.Empty));
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0040A738 File Offset: 0x00408938
		public static TimeLogger.StartTimestamp Start()
		{
			return new TimeLogger.StartTimestamp
			{
				ticks = Stopwatch.GetTimestamp()
			};
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0040A75A File Offset: 0x0040895A
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0040A761 File Offset: 0x00408961
		public static bool ABTestFlag
		{
			get
			{
				return TileDrawingBase.DrawOwnBlacks;
			}
			set
			{
				TileDrawingBase.DrawOwnBlacks = value;
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0040A76C File Offset: 0x0040896C
		public static void ABTest()
		{
			if (Main.renderCount == 0 && TimeLogger.ABTestMode == 2)
			{
				TimeLogger.ABTestFlag = !TimeLogger.ABTestFlag;
			}
			if (TimeLogger.ABTestFlag)
			{
				TimeLogger.DataSeriesHeaders[0] = "Baseline";
				TimeLogger.DataSeriesHeaders[1] = TimeLogger.ABTestName;
			}
			TimeLogger.activeDataSeries = (TimeLogger.ABTestFlag ? 1 : 0);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0040A7C4 File Offset: 0x004089C4
		public static void DrawException(Exception e)
		{
			if (TimeLogger.currentlyLogging)
			{
				TimeLogger.logBuilder.AppendLine(e.ToString());
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0040A7DE File Offset: 0x004089DE
		public static void Draw()
		{
			TimeLogger.DrawTimes();
			TimeLogger.DrawExtras();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0040A7EC File Offset: 0x004089EC
		private static void DrawTimes()
		{
			TimeLogger.DrawnEntryNumber = 0;
			foreach (TimeLogger.TimeLogData timeLogData in TimeLogger.entries)
			{
				timeLogData.pendingDisplay = timeLogData.ShouldDrawByDefault;
			}
			int num = 100;
			TimeLogger.TableWidth = ((TimeLogger.DataSeriesHeaders[1] != null) ? 900 : 440);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, num, TimeLogger.TableWidth, 800), new Color(60, 60, 60, 80));
			TimeLogger.DrawString("Render Time (ms) F7 to hide", new Vector2(80f, (float)(num - 16)), Color.White);
			TimeLogger.DrawString("Median", new Vector2(273f, (float)num), Color.White);
			TimeLogger.DrawString("P90", new Vector2(325f, (float)num), Color.White);
			TimeLogger.DrawString("Max", new Vector2(365f, (float)num), Color.White);
			TimeLogger.DrawString("Freq", new Vector2(400f, (float)num), Color.White);
			if (TimeLogger.DataSeriesHeaders[1] != null)
			{
				TimeLogger.DrawString(TimeLogger.DataSeriesHeaders[0], new Vector2(280f, (float)(num - 16)), (TimeLogger.activeDataSeries == 0) ? Color.LightGreen : Color.White);
				TimeLogger.DrawString(TimeLogger.DataSeriesHeaders[1], new Vector2((float)(280 + TimeLogger.ColumnSpacing), (float)(num - 16)), (TimeLogger.activeDataSeries == 1) ? Color.LightGreen : Color.White);
				TimeLogger.DrawString("Delta", new Vector2((float)(320 + 2 * TimeLogger.ColumnSpacing), (float)(num - 16)), Color.White);
				TimeLogger.DrawString("Median", new Vector2((float)(273 + TimeLogger.ColumnSpacing), (float)num), Color.White);
				TimeLogger.DrawString("P90", new Vector2((float)(325 + TimeLogger.ColumnSpacing), (float)num), Color.White);
				TimeLogger.DrawString("Max", new Vector2((float)(365 + TimeLogger.ColumnSpacing), (float)num), Color.White);
				TimeLogger.DrawString("Freq", new Vector2((float)(400 + TimeLogger.ColumnSpacing), (float)num), Color.White);
				TimeLogger.DrawString("Median", new Vector2((float)(250 + 2 * TimeLogger.ColumnSpacing), (float)num), Color.White);
				TimeLogger.DrawString("P90", new Vector2((float)(360 + 2 * TimeLogger.ColumnSpacing), (float)num), Color.White);
			}
			num += 16;
			TimeLogger.DrawEntry(TimeLogger.TotalDrawAndUpdate, ref num, -1);
			TimeLogger.DrawEntry(TimeLogger.TotalDraw, ref num, -1);
			if (!Main.drawToScreen)
			{
				TimeLogger.DrawEntry(TimeLogger.TotalDrawByRenderCount[0], ref num, 0);
				TimeLogger.DrawEntry(TimeLogger.TotalDrawByRenderCount[1], ref num, 0);
				TimeLogger.DrawEntry(TimeLogger.RenderLiquid, ref num, 1);
				TimeLogger.DrawEntry(TimeLogger.LiquidDrawCalls, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.TotalDrawByRenderCount[2], ref num, 0);
				if (TimeLogger.RenderUndergroundBackground.pendingDisplay)
				{
					TimeLogger.DrawEntry(TimeLogger.RenderUndergroundBackground, ref num, 1);
					TimeLogger.DrawEntry(TimeLogger.DrawUndergroundBackground, ref num, 2);
				}
				TimeLogger.DrawEntry(TimeLogger.RenderBackgroundLiquid, ref num, 1);
				TimeLogger.DrawEntry(TimeLogger.LiquidBackgroundDrawCalls, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.TotalDrawByRenderCount[3], ref num, 0);
				TimeLogger.DrawEntry(TimeLogger.RenderBlacksAndWalls, ref num, 1);
				TimeLogger.DrawEntry(TimeLogger.DrawBlackTiles, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.DrawWallTiles, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.FlushWallTiles, ref num, 3);
				TimeLogger.DrawEntry(TimeLogger.WallDrawCalls, ref num, 4);
				TimeLogger.DrawEntry(TimeLogger.RenderSolidTiles, ref num, 1);
				TimeLogger.DrawEntry(TimeLogger.FlushSolidTiles, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.SolidDrawCalls, ref num, 3);
				TimeLogger.DrawEntry(TimeLogger.RenderNonSolidTiles, ref num, 1);
				TimeLogger.DrawEntry(TimeLogger.FlushNonSolidTiles, ref num, 2);
				TimeLogger.DrawEntry(TimeLogger.NonSolidDrawCalls, ref num, 3);
				TimeLogger.DrawWaterTiles.pendingDisplay = false;
				TimeLogger.DrawBackgroundWaterTiles.pendingDisplay = false;
				TimeLogger.DrawSolidTiles.pendingDisplay = false;
				TimeLogger.DrawNonSolidTiles.pendingDisplay = false;
			}
			foreach (TimeLogger.TimeLogData timeLogData2 in TimeLogger.TotalDrawByRenderCount)
			{
				if (timeLogData2.pendingDisplay)
				{
					TimeLogger.DrawEntry(timeLogData2, ref num, 0);
				}
			}
			if (TimeLogger.TotalDrawRenderNow.pendingDisplay)
			{
				TimeLogger.DrawEntry(TimeLogger.TotalDrawRenderNow, ref num, 0);
			}
			num += 12;
			TimeLogger.DrawEntry(TimeLogger.Lighting, ref num, 0);
			if (TimeLogger.LightingInit.pendingDisplay)
			{
				TimeLogger.DrawEntry(TimeLogger.LightingInit, ref num, 1);
			}
			foreach (TimeLogger.TimeLogData timeLogData3 in TimeLogger.LightingByPass)
			{
				if (timeLogData3.pendingDisplay)
				{
					TimeLogger.DrawEntry(timeLogData3, ref num, 1);
				}
			}
			num += 12;
			foreach (TimeLogger.TimeLogData timeLogData4 in TimeLogger.entries)
			{
				if (timeLogData4.pendingDisplay)
				{
					TimeLogger.DrawEntry(timeLogData4, ref num, 0);
				}
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0040ACF0 File Offset: 0x00408EF0
		private static void DrawExtras()
		{
			if (Platform.IsWindows)
			{
				WindowsPerformanceDiagnostics.Data data = WindowsPerformanceDiagnostics.GetData();
				int num = Main.screenWidth - 180;
				int num2 = Main.screenHeight - 100;
				TimeLogger.DrawString((data.PinnedToProcessor ? TimeLogger._PinnedCPUFormat : TimeLogger._AssignedCPUFormat).Format((double)data.CurrentProcessor), new Vector2((float)num, (float)(num2 += 12)), Color.White);
				TimeLogger.DrawString(TimeLogger._procThrottleFormat.Format(data.ProcessorThrottlePercent), new Vector2((float)num, (float)(num2 += 12)), Color.White);
				TimeLogger.DrawString(TimeLogger._expectedCPUFormat.Format(data.ExpectedCPUPercent), new Vector2((float)num, (float)(num2 += 12)), Color.White);
				if (data.PinnedToProcessor)
				{
					TimeLogger.DrawString(TimeLogger._terrariaCPUFormat.Format(data.MainThreadCPUPercent), new Vector2((float)num, (float)(num2 += 12)), Color.White);
				}
				TimeLogger.FormatPool pendingCPUFormat = TimeLogger._pendingCPUFormat;
				long? contentionQueueLength = data.ContentionQueueLength;
				TimeLogger.DrawString(pendingCPUFormat.Format((contentionQueueLength != null) ? new double?((double)contentionQueueLength.GetValueOrDefault()) : null), new Vector2((float)num, (float)(num2 += 12)), Color.White);
				num2 += 2;
				if (data.RecommendUnpinning)
				{
					TimeLogger.DrawString("Core pinning may be", new Vector2((float)num, (float)(num2 += 11)), Color.Orange);
					TimeLogger.DrawString("reducing performance", new Vector2((float)num, (float)(num2 + 12)), Color.Orange);
				}
			}
			if (PlayerInput.UsingGamepad)
			{
				for (int i = 0; i < 2; i++)
				{
					string text = "";
					int num3 = Main.screenWidth - 220;
					int num4 = Main.screenHeight - 140 + i * 12;
					if (i == 0)
					{
						text = string.Format("Thumbstick (L): {0,7:P2} ,{1,7:P2}", PlayerInput.GamepadThumbstickLeft.X, PlayerInput.GamepadThumbstickLeft.Y);
					}
					if (i == 1)
					{
						text = string.Format("Thumbstick (R): {0,7:P2} ,{1,7:P2}", PlayerInput.GamepadThumbstickRight.X, PlayerInput.GamepadThumbstickRight.Y);
					}
					TimeLogger.DrawString(text, new Vector2((float)num3, (float)num4), Color.White);
				}
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0040AF28 File Offset: 0x00409128
		private static void DrawEntry(TimeLogger.TimeLogData e, ref int y, int indent = 0)
		{
			e.pendingDisplay = false;
			if (TimeLogger.DrawnEntryNumber++ % 2 == 1)
			{
				Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, y, TimeLogger.TableWidth, 12), new Color(0, 0, 0, 80));
			}
			int num = 0;
			TimeLogger.DrawString(e.name, new Vector2((float)(num + 20 + indent * 12), (float)y), TimeLogger.PerformanceColor((long)e.data[0].median, (long)e.budget));
			TimeLogger.DrawEntryData(num, y, e.data[0], e.format, e.budget);
			num += TimeLogger.ColumnSpacing;
			TimeLogger.DrawEntryData(num, y, e.data[1], e.format, e.budget);
			num += TimeLogger.ColumnSpacing;
			TimeLogger.DrawDelta(num, y, e.data[0], e.data[1], e.format, e.budget);
			y += 12;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0040B028 File Offset: 0x00409228
		private static void DrawEntryData(int x, int y, TimeLogger.DataSeries data, Func<int, string> format, int budget)
		{
			if (!data.HasData)
			{
				return;
			}
			TimeLogger.DrawValue(data.previous, new Vector2((float)(x + 240), (float)y), format, budget);
			TimeLogger.DrawValue(data.median, new Vector2((float)(x + 280), (float)y), format, budget);
			TimeLogger.DrawValue(data.p90, new Vector2((float)(x + 320), (float)y), format, budget);
			TimeLogger.DrawValue(data.max, new Vector2((float)(x + 360), (float)y), format, budget);
			if (data.frequency < 1f)
			{
				TimeLogger.DrawString(TimeLogger._percentFormat.Format((double)(data.frequency * 100f)), new Vector2((float)(x + 400), (float)y), Color.White);
			}
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0040B0F0 File Offset: 0x004092F0
		private static void DrawDelta(int x, int y, TimeLogger.DataSeries data0, TimeLogger.DataSeries data1, Func<int, string> format, int budget)
		{
			if (!data0.HasData || !data1.HasData)
			{
				return;
			}
			TimeLogger.DrawDelta(data0.median, data1.median, new Vector2((float)(x + 240), (float)y), format, budget);
			TimeLogger.DrawDelta(data0.p90, data1.p90, new Vector2((float)(x + 340), (float)y), format, budget);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0040B158 File Offset: 0x00409358
		private static string FormatTicks(int ticks)
		{
			TimeSpan timeSpan = Utils.SWTicksToTimeSpan((long)ticks);
			string text = TimeLogger._msFormat.Format((double)((float)timeSpan.TotalMilliseconds));
			if (text.Length > 5)
			{
				text = text.Substring(0, 5);
			}
			return text;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0040B194 File Offset: 0x00409394
		private static void DrawValue(int value, Vector2 pos, Func<int, string> format, int budget)
		{
			TimeLogger.DrawString(format(value), pos, TimeLogger.PerformanceColor((long)value, (long)budget));
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0040B1AC File Offset: 0x004093AC
		private static void DrawDelta(int value0, int value1, Vector2 pos, Func<int, string> format, int budget)
		{
			int num = value1 - value0;
			int num2 = (int)Math.Round((double)value1 * 100.0 / (double)value0 - 100.0);
			if (value0 == 0 || Math.Abs(num2) <= 1 || Math.Abs(num * 100) <= budget)
			{
				return;
			}
			Color color = ((num2 < 0) ? Color.Lerp(Color.White, new Color(0, 200, 0), (float)(-(float)num2) / 100f) : Color.Lerp(Color.White, new Color(200, 0, 0), (float)num2 / 100f));
			TimeLogger.DrawString(format(num), pos, color);
			pos.X += 40f;
			TimeLogger.DrawString(TimeLogger._percentFormat.Format((double)num2), pos, color);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0040B26C File Offset: 0x0040946C
		private static void DrawString(string text, Vector2 pos, Color color)
		{
			Utils.DrawBorderString(Main.spriteBatch, text, pos, color, 0.75f, 0f, 0f, -1);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0040B28C File Offset: 0x0040948C
		private static Color PerformanceColor(long value, long budget)
		{
			Color white = Color.White;
			Color color = new Color(255, 255, 170);
			Color color2 = new Color(255, 160, 50);
			Color color3 = new Color(255, 50, 50);
			Color color4 = new Color(200, 0, 0);
			float num = (float)((double)value / (double)budget);
			if ((double)num <= 0.33)
			{
				return Color.Lerp(white, color, num / 0.33f);
			}
			if ((double)num <= 0.67)
			{
				return Color.Lerp(color, color2, (num - 0.33f) / 0.34f);
			}
			if (num <= 1f)
			{
				return Color.Lerp(color2, color3, (num - 0.67f) / 0.32999998f);
			}
			return Color.Lerp(color3, color4, Utils.Clamp<float>(num - 1f, 0f, 1f));
		}

		// Token: 0x04000E7A RID: 3706
		public static readonly int FrameCount = DetailedFPS.FrameCount;

		// Token: 0x04000E7B RID: 3707
		private static StreamWriter logWriter;

		// Token: 0x04000E7C RID: 3708
		private static StringBuilder logBuilder;

		// Token: 0x04000E7D RID: 3709
		private static int framesToLog = -1;

		// Token: 0x04000E7E RID: 3710
		private static int currentFrame;

		// Token: 0x04000E7F RID: 3711
		private static bool startLoggingNextFrame;

		// Token: 0x04000E80 RID: 3712
		private static bool endLoggingThisFrame;

		// Token: 0x04000E81 RID: 3713
		private static bool currentlyLogging;

		// Token: 0x04000E82 RID: 3714
		private static string[] DataSeriesHeaders = new string[2];

		// Token: 0x04000E83 RID: 3715
		private static int activeDataSeries = 0;

		// Token: 0x04000E84 RID: 3716
		private static List<TimeLogger.TimeLogData> entries = new List<TimeLogger.TimeLogData>();

		// Token: 0x04000E85 RID: 3717
		public static TimeLogger.TimeLogData TotalDrawAndUpdate = TimeLogger.NewEntry("Total Draw+Update", new TimeSpan?(TimeSpan.FromSeconds(Main.TARGET_FRAME_TIME)));

		// Token: 0x04000E86 RID: 3718
		public static TimeLogger.TimeLogData DrawSolidTiles = TimeLogger.NewEntry("Draw Solid Tiles", null);

		// Token: 0x04000E87 RID: 3719
		public static TimeLogger.TimeLogData FlushSolidTiles = TimeLogger.NewEntry("Flush Solid Tiles", null);

		// Token: 0x04000E88 RID: 3720
		public static TimeLogger.TimeLogData SolidDrawCalls = TimeLogger.NewCounterEntry("Solid Draw Calls", 200);

		// Token: 0x04000E89 RID: 3721
		public static TimeLogger.TimeLogData DrawNonSolidTiles = TimeLogger.NewEntry("Draw Non-Solid Tiles", null);

		// Token: 0x04000E8A RID: 3722
		public static TimeLogger.TimeLogData FlushNonSolidTiles = TimeLogger.NewEntry("Flush Non-Solid Tiles", null);

		// Token: 0x04000E8B RID: 3723
		public static TimeLogger.TimeLogData NonSolidDrawCalls = TimeLogger.NewCounterEntry("Non-Solid Draw Calls", 200);

		// Token: 0x04000E8C RID: 3724
		public static TimeLogger.TimeLogData DrawBlackTiles = TimeLogger.NewEntry("Draw Black Tiles", null);

		// Token: 0x04000E8D RID: 3725
		public static TimeLogger.TimeLogData DrawWallTiles = TimeLogger.NewEntry("Draw Wall Tiles", null);

		// Token: 0x04000E8E RID: 3726
		public static TimeLogger.TimeLogData FlushWallTiles = TimeLogger.NewEntry("Flush Wall Tiles", null);

		// Token: 0x04000E8F RID: 3727
		public static TimeLogger.TimeLogData WallDrawCalls = TimeLogger.NewCounterEntry("Wall Draw Calls", 200);

		// Token: 0x04000E90 RID: 3728
		public static TimeLogger.TimeLogData DrawWaterTiles = TimeLogger.NewEntry("Draw Liquid Tiles", null);

		// Token: 0x04000E91 RID: 3729
		public static TimeLogger.TimeLogData LiquidDrawCalls = TimeLogger.NewCounterEntry("Liquid Draw Calls", 200);

		// Token: 0x04000E92 RID: 3730
		public static TimeLogger.TimeLogData DrawBackgroundWaterTiles = TimeLogger.NewEntry("Draw Bg Liquid Tiles", null);

		// Token: 0x04000E93 RID: 3731
		public static TimeLogger.TimeLogData LiquidBackgroundDrawCalls = TimeLogger.NewCounterEntry("Bg Liquid Draw Calls", 200);

		// Token: 0x04000E94 RID: 3732
		public static TimeLogger.TimeLogData DrawUndergroundBackground = TimeLogger.NewEntry("Draw Underground Bg", null);

		// Token: 0x04000E95 RID: 3733
		public static TimeLogger.TimeLogData DrawOldUndergroundBackground = TimeLogger.NewEntry("Draw Old Underground Bg", null);

		// Token: 0x04000E96 RID: 3734
		public static TimeLogger.TimeLogData DrawWireTiles = TimeLogger.NewEntry("Draw Wire Tiles", null);

		// Token: 0x04000E97 RID: 3735
		public static TimeLogger.TimeLogData ClothingRacks = TimeLogger.NewEntry("Hat Racks & Display Dolls", null);

		// Token: 0x04000E98 RID: 3736
		public static TimeLogger.TimeLogData TileExtras = TimeLogger.NewEntry("Tile Extras", null);

		// Token: 0x04000E99 RID: 3737
		public static TimeLogger.TimeLogData Nature = TimeLogger.NewEntry("Nature Renderer", null);

		// Token: 0x04000E9A RID: 3738
		public static TimeLogger.TimeLogData RenderSolidTiles = TimeLogger.NewEntry("Render Solid Tiles", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000E9B RID: 3739
		public static TimeLogger.TimeLogData RenderNonSolidTiles = TimeLogger.NewEntry("Render Non-Solid Tiles", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000E9C RID: 3740
		public static TimeLogger.TimeLogData RenderBlacksAndWalls = TimeLogger.NewEntry("Render Black Tiles & Walls", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000E9D RID: 3741
		public static TimeLogger.TimeLogData RenderUndergroundBackground = TimeLogger.NewEntry("Render Underground Bg", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000E9E RID: 3742
		public static TimeLogger.TimeLogData RenderBackgroundLiquid = TimeLogger.NewEntry("Render Bg Water Tiles", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000E9F RID: 3743
		public static TimeLogger.TimeLogData RenderLiquid = TimeLogger.NewEntry("Render Water Tiles", new TimeSpan?(TimeSpan.FromMilliseconds(3.0)));

		// Token: 0x04000EA0 RID: 3744
		public static TimeLogger.TimeLogData[] TotalDrawByRenderCount = new TimeLogger.TimeLogData[9];

		// Token: 0x04000EA1 RID: 3745
		public static TimeLogger.TimeLogData TotalDrawRenderNow = TimeLogger.NewEntry("Total Draw, Render Now", new TimeSpan?(TimeSpan.FromMilliseconds(16.0)));

		// Token: 0x04000EA2 RID: 3746
		public static TimeLogger.TimeLogData TotalDraw = TimeLogger.NewEntry("Total Draw", new TimeSpan?(TimeSpan.FromMilliseconds(16.0)));

		// Token: 0x04000EA3 RID: 3747
		public static TimeLogger.TimeLogData Lighting = TimeLogger.NewEntry("Lighting", null);

		// Token: 0x04000EA4 RID: 3748
		public static TimeLogger.TimeLogData LightingInit = TimeLogger.NewEntry("Lighting Init", null);

		// Token: 0x04000EA5 RID: 3749
		public static TimeLogger.TimeLogData[] LightingByPass;

		// Token: 0x04000EA6 RID: 3750
		public static TimeLogger.TimeLogData FindPaintedTiles = TimeLogger.NewEntry("Find Painted Tiles", null);

		// Token: 0x04000EA7 RID: 3751
		public static TimeLogger.TimeLogData PrepareRequests = TimeLogger.NewEntry("Prep Render Target Content", null);

		// Token: 0x04000EA8 RID: 3752
		public static TimeLogger.TimeLogData FindingWaterfalls = TimeLogger.NewEntry("Find Waterfalls", null);

		// Token: 0x04000EA9 RID: 3753
		public static TimeLogger.TimeLogData MapChanges = TimeLogger.NewEntry("Queued Map Updates", null);

		// Token: 0x04000EAA RID: 3754
		public static TimeLogger.TimeLogData MapSectionUpdate = TimeLogger.NewEntry("Map Section Update", null);

		// Token: 0x04000EAB RID: 3755
		public static TimeLogger.TimeLogData MapUpdate = TimeLogger.NewEntry("Map Update", null);

		// Token: 0x04000EAC RID: 3756
		public static TimeLogger.TimeLogData SectionFraming = TimeLogger.NewEntry("Section Framing", null);

		// Token: 0x04000EAD RID: 3757
		public static TimeLogger.TimeLogData SectionRefresh = TimeLogger.NewEntry("Section Refresh", null);

		// Token: 0x04000EAE RID: 3758
		public static TimeLogger.TimeLogData SkyBackground = TimeLogger.NewEntry("Sky Background", null);

		// Token: 0x04000EAF RID: 3759
		public static TimeLogger.TimeLogData SunMoonStars = TimeLogger.NewEntry("Sun, Moon & Stars", null);

		// Token: 0x04000EB0 RID: 3760
		public static TimeLogger.TimeLogData SurfaceBackground = TimeLogger.NewEntry("Surface Background", null);

		// Token: 0x04000EB1 RID: 3761
		public static TimeLogger.TimeLogData Map = TimeLogger.NewEntry("Map", null);

		// Token: 0x04000EB2 RID: 3762
		public static TimeLogger.TimeLogData PlayerChat = TimeLogger.NewEntry("Player Chat", null);

		// Token: 0x04000EB3 RID: 3763
		public static TimeLogger.TimeLogData Waterfalls = TimeLogger.NewEntry("Waterfalls", null);

		// Token: 0x04000EB4 RID: 3764
		public static TimeLogger.TimeLogData NPCs = TimeLogger.NewEntry("NPC", null);

		// Token: 0x04000EB5 RID: 3765
		public static TimeLogger.TimeLogData Projectiles = TimeLogger.NewEntry("Projectiles", null);

		// Token: 0x04000EB6 RID: 3766
		public static TimeLogger.TimeLogData Players = TimeLogger.NewEntry("Players", null);

		// Token: 0x04000EB7 RID: 3767
		public static TimeLogger.TimeLogData Items = TimeLogger.NewEntry("Items", null);

		// Token: 0x04000EB8 RID: 3768
		public static TimeLogger.TimeLogData Rain = TimeLogger.NewEntry("Rain", null);

		// Token: 0x04000EB9 RID: 3769
		public static TimeLogger.TimeLogData Gore = TimeLogger.NewEntry("Gore", null);

		// Token: 0x04000EBA RID: 3770
		public static TimeLogger.TimeLogData Dust = TimeLogger.NewEntry("Dust", null);

		// Token: 0x04000EBB RID: 3771
		public static TimeLogger.TimeLogData Particles = TimeLogger.NewEntry("Particles", null);

		// Token: 0x04000EBC RID: 3772
		public static TimeLogger.TimeLogData LeashedEntities = TimeLogger.NewEntry("Leashed Entities", null);

		// Token: 0x04000EBD RID: 3773
		public static TimeLogger.TimeLogData Interface = TimeLogger.NewEntry("Interface", null);

		// Token: 0x04000EBE RID: 3774
		public static TimeLogger.TimeLogData DrawFPSGraph = TimeLogger.NewEntry("Draw FPS Graph", null);

		// Token: 0x04000EBF RID: 3775
		public static TimeLogger.TimeLogData DrawTimeLogger = TimeLogger.NewEntry("Draw Render Timings", null);

		// Token: 0x04000EC0 RID: 3776
		public static TimeLogger.TimeLogData Overlays = TimeLogger.NewEntry("Overlays", null);

		// Token: 0x04000EC1 RID: 3777
		public static TimeLogger.TimeLogData Filters = TimeLogger.NewEntry("Screen Filters/Blit", null);

		// Token: 0x04000EC2 RID: 3778
		public static TimeLogger.TimeLogData SunVisibility = TimeLogger.NewEntry("Sun Visibility", null);

		// Token: 0x04000EC3 RID: 3779
		public static TimeLogger.TimeLogData MenuDrawTime = TimeLogger.NewEntry("Menu", null);

		// Token: 0x04000EC4 RID: 3780
		public static TimeLogger.TimeLogData SplashDrawTime = TimeLogger.NewEntry("Splash", null);

		// Token: 0x04000EC5 RID: 3781
		public static TimeLogger.TimeLogData DrawFullscreenMap = TimeLogger.NewEntry("Full Screen Map", null);

		// Token: 0x04000EC6 RID: 3782
		public static TimeLogger.TimeLogData GCPause = TimeLogger.NewEntry("GC Pause", new TimeSpan?(TimeSpan.FromMilliseconds(1.0)));

		// Token: 0x04000EC7 RID: 3783
		private static Queue<Action> _onNextFrame = new Queue<Action>();

		// Token: 0x04000EC8 RID: 3784
		public static int ABTestMode = (TimeLogger.ABTestFlag ? 1 : 0);

		// Token: 0x04000EC9 RID: 3785
		public static readonly string ABTestName = "New Draw Blacks";

		// Token: 0x04000ECA RID: 3786
		private static int TableWidth;

		// Token: 0x04000ECB RID: 3787
		private static int ColumnSpacing = 220;

		// Token: 0x04000ECC RID: 3788
		private static int DrawnEntryNumber = 0;

		// Token: 0x04000ECD RID: 3789
		private static Queue<TimeLogger.TimeLogData> _entriesToDraw = new Queue<TimeLogger.TimeLogData>();

		// Token: 0x04000ECE RID: 3790
		private static TimeLogger.FormatPool _PinnedCPUFormat = new TimeLogger.FormatPool("Pinned to CPU #{0}", 64.0, 0.0, 1.0);

		// Token: 0x04000ECF RID: 3791
		private static TimeLogger.FormatPool _AssignedCPUFormat = new TimeLogger.FormatPool("Assigned CPU #{0}", 64.0, 0.0, 1.0);

		// Token: 0x04000ED0 RID: 3792
		private static TimeLogger.FormatPool _procThrottleFormat = new TimeLogger.FormatPool("CPU Throttle/Boost {0:0}%", 200.0, 0.0, 1.0);

		// Token: 0x04000ED1 RID: 3793
		private static TimeLogger.FormatPool _expectedCPUFormat = new TimeLogger.FormatPool("Expected CPU Usage {0:0}%", 200.0, 0.0, 1.0);

		// Token: 0x04000ED2 RID: 3794
		private static TimeLogger.FormatPool _terrariaCPUFormat = new TimeLogger.FormatPool("Terraria CPU Usage {0:0}%", 200.0, 0.0, 1.0);

		// Token: 0x04000ED3 RID: 3795
		private static TimeLogger.FormatPool _pendingCPUFormat = new TimeLogger.FormatPool("#Threads pending CPU {0}", 100.0, 0.0, 1.0);

		// Token: 0x04000ED4 RID: 3796
		private static TimeLogger.FormatPool _percentFormat = new TimeLogger.FormatPool("{0,3:00}%", 100.0, 0.0, 1.0);

		// Token: 0x04000ED5 RID: 3797
		private static TimeLogger.FormatPool _msFormat = new TimeLogger.FormatPool("{0,5:F2}", 20.0, -10.0, 0.009999999776482582);

		// Token: 0x04000ED6 RID: 3798
		private static TimeLogger.FormatPool _intFormat = new TimeLogger.FormatPool("{0,5}", 5000.0, 0.0, 1.0);

		// Token: 0x0200063D RID: 1597
		public class DataSeries
		{
			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x06003C9D RID: 15517 RVA: 0x0066E401 File Offset: 0x0066C601
			public bool HasData
			{
				get
				{
					return this.usedCount > 0;
				}
			}

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x06003C9E RID: 15518 RVA: 0x0066E40C File Offset: 0x0066C60C
			public float frequency
			{
				get
				{
					if (this.count <= 0)
					{
						return 0f;
					}
					return (float)this.usedCount / (float)this.count;
				}
			}

			// Token: 0x06003C9F RID: 15519 RVA: 0x0066E42C File Offset: 0x0066C62C
			public void Add(int value, out bool newMax)
			{
				this.values[this.next] += value;
				this.used[this.next] = true;
				newMax = this.values[this.next] > this.max;
			}

			// Token: 0x06003CA0 RID: 15520 RVA: 0x0066E46C File Offset: 0x0066C66C
			public void StartNextFrame()
			{
				if (this.used[this.next])
				{
					this.previous = this.values[this.next];
				}
				if (this.count < this.values.Length)
				{
					this.count++;
				}
				this.next = (this.next + 1) % this.values.Length;
				this.usedCount = 0;
				for (int i = 0; i < this.count; i++)
				{
					if (this.used[i])
					{
						int[] sort = TimeLogger.DataSeries._sort;
						int num = this.usedCount;
						this.usedCount = num + 1;
						sort[num] = this.values[i];
					}
				}
				if (this.usedCount > 0)
				{
					Array.Sort<int>(TimeLogger.DataSeries._sort, 0, this.usedCount);
					this.median = TimeLogger.DataSeries.Quantile(TimeLogger.DataSeries._sort, this.usedCount, 0.5f);
					this.p90 = TimeLogger.DataSeries.Quantile(TimeLogger.DataSeries._sort, this.usedCount, 0.9f);
					this.max = TimeLogger.DataSeries.Quantile(TimeLogger.DataSeries._sort, this.usedCount, 1f);
				}
				else
				{
					this.previous = (this.median = (this.p90 = (this.max = 0)));
				}
				this.values[this.next] = 0;
				this.used[this.next] = false;
			}

			// Token: 0x06003CA1 RID: 15521 RVA: 0x0066E5BC File Offset: 0x0066C7BC
			public void Reset()
			{
				this.next = (this.count = (this.usedCount = 0));
				this.previous = (this.median = (this.p90 = (this.max = 0)));
			}

			// Token: 0x06003CA2 RID: 15522 RVA: 0x0066E604 File Offset: 0x0066C804
			private static int Quantile(int[] sorted, int len, float quantile)
			{
				quantile *= (float)(len - 1);
				return (int)MathHelper.Lerp((float)sorted[(int)Math.Floor((double)quantile)], (float)sorted[(int)Math.Ceiling((double)quantile)], quantile % 1f);
			}

			// Token: 0x06003CA3 RID: 15523 RVA: 0x0066E631 File Offset: 0x0066C831
			public DataSeries()
			{
			}

			// Token: 0x06003CA4 RID: 15524 RVA: 0x0066E659 File Offset: 0x0066C859
			// Note: this type is marked as 'beforefieldinit'.
			static DataSeries()
			{
			}

			// Token: 0x0400654B RID: 25931
			private int[] values = new int[TimeLogger.FrameCount];

			// Token: 0x0400654C RID: 25932
			private bool[] used = new bool[TimeLogger.FrameCount];

			// Token: 0x0400654D RID: 25933
			private int next;

			// Token: 0x0400654E RID: 25934
			private int count;

			// Token: 0x0400654F RID: 25935
			private int usedCount;

			// Token: 0x04006550 RID: 25936
			public int previous;

			// Token: 0x04006551 RID: 25937
			public int median;

			// Token: 0x04006552 RID: 25938
			public int p90;

			// Token: 0x04006553 RID: 25939
			public int max;

			// Token: 0x04006554 RID: 25940
			private static int[] _sort = new int[TimeLogger.FrameCount];
		}

		// Token: 0x0200063E RID: 1598
		public class TimeLogData
		{
			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x0066E66C File Offset: 0x0066C86C
			public bool ShouldDrawByDefault
			{
				get
				{
					foreach (TimeLogger.DataSeries dataSeries in this.data)
					{
						if (dataSeries.HasData && (float)dataSeries.p90 >= (float)this.budget * 0.05f)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x06003CA6 RID: 15526 RVA: 0x0066E6B3 File Offset: 0x0066C8B3
			public void AddTime(TimeLogger.StartTimestamp fromTimestamp)
			{
				this.Add(fromTimestamp.ElapsedTicks);
			}

			// Token: 0x06003CA7 RID: 15527 RVA: 0x0066E6C4 File Offset: 0x0066C8C4
			public void Add(int value)
			{
				bool flag;
				this.data[TimeLogger.activeDataSeries].Add(value, out flag);
				TimeLogger.LogAdd(this.name, value, flag);
			}

			// Token: 0x06003CA8 RID: 15528 RVA: 0x0066E6F4 File Offset: 0x0066C8F4
			public void Reset()
			{
				TimeLogger.DataSeries[] array = this.data;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Reset();
				}
			}

			// Token: 0x06003CA9 RID: 15529 RVA: 0x0066E71E File Offset: 0x0066C91E
			public void StartNextFrame()
			{
				this.data[TimeLogger.activeDataSeries].StartNextFrame();
			}

			// Token: 0x06003CAA RID: 15530 RVA: 0x0066E731 File Offset: 0x0066C931
			public TimeLogData()
			{
			}

			// Token: 0x04006555 RID: 25941
			public string name;

			// Token: 0x04006556 RID: 25942
			public Func<int, string> format;

			// Token: 0x04006557 RID: 25943
			public int budget;

			// Token: 0x04006558 RID: 25944
			public bool pendingDisplay;

			// Token: 0x04006559 RID: 25945
			public TimeLogger.DataSeries[] data = new TimeLogger.DataSeries[]
			{
				new TimeLogger.DataSeries(),
				new TimeLogger.DataSeries()
			};
		}

		// Token: 0x0200063F RID: 1599
		public struct StartTimestamp
		{
			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x06003CAB RID: 15531 RVA: 0x0066E755 File Offset: 0x0066C955
			public int ElapsedTicks
			{
				get
				{
					return (int)(Stopwatch.GetTimestamp() - this.ticks);
				}
			}

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06003CAC RID: 15532 RVA: 0x0066E764 File Offset: 0x0066C964
			public TimeSpan Elapsed
			{
				get
				{
					return Utils.SWTicksToTimeSpan((long)this.ElapsedTicks);
				}
			}

			// Token: 0x0400655A RID: 25946
			public long ticks;
		}

		// Token: 0x02000640 RID: 1600
		private class FormatPool
		{
			// Token: 0x06003CAD RID: 15533 RVA: 0x0066E774 File Offset: 0x0066C974
			public FormatPool(string format, double maxValue, double minValue = 0.0, double rounding = 1.0)
			{
				this._format = format;
				this._minValue = minValue;
				this._rounding = rounding;
				this._strings = new string[(int)((maxValue - minValue) / rounding) + 1];
				this._nullString = string.Format(this._format, null);
			}

			// Token: 0x06003CAE RID: 15534 RVA: 0x0066E7C4 File Offset: 0x0066C9C4
			public string Format(double value)
			{
				int num = (int)Math.Round((value - this._minValue) / this._rounding);
				if (num < 0 || num >= this._strings.Length)
				{
					return string.Format(this._format, value);
				}
				string text = this._strings[num];
				if (text == null)
				{
					text = (this._strings[num] = string.Format(this._format, value));
				}
				return text;
			}

			// Token: 0x06003CAF RID: 15535 RVA: 0x0066E830 File Offset: 0x0066CA30
			public string Format(double? value)
			{
				if (value != null)
				{
					return this.Format(value.Value);
				}
				return this._nullString;
			}

			// Token: 0x0400655B RID: 25947
			private readonly string _format;

			// Token: 0x0400655C RID: 25948
			private readonly double _minValue;

			// Token: 0x0400655D RID: 25949
			private readonly double _rounding;

			// Token: 0x0400655E RID: 25950
			private readonly string[] _strings;

			// Token: 0x0400655F RID: 25951
			private string _nullString;
		}

		// Token: 0x02000641 RID: 1601
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003CB0 RID: 15536 RVA: 0x0066E84F File Offset: 0x0066CA4F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003CB1 RID: 15537 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003CB2 RID: 15538 RVA: 0x0066E85B File Offset: 0x0066CA5B
			internal string <NewCounterEntry>b__81_0(int i)
			{
				return TimeLogger._intFormat.Format((double)i);
			}

			// Token: 0x06003CB3 RID: 15539 RVA: 0x0066E86C File Offset: 0x0066CA6C
			internal void <Reset>b__83_0()
			{
				foreach (TimeLogger.TimeLogData timeLogData in TimeLogger.entries)
				{
					timeLogData.Reset();
				}
			}

			// Token: 0x04006560 RID: 25952
			public static readonly TimeLogger.<>c <>9 = new TimeLogger.<>c();

			// Token: 0x04006561 RID: 25953
			public static Func<int, string> <>9__81_0;

			// Token: 0x04006562 RID: 25954
			public static Action <>9__83_0;
		}
	}
}
