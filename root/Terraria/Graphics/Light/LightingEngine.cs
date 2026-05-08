using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.Graphics.Capture;
using Terraria.Map;

namespace Terraria.Graphics.Light
{
	// Token: 0x020001FD RID: 509
	public class LightingEngine : ILightingEngine
	{
		// Token: 0x060020F8 RID: 8440 RVA: 0x0052BC02 File Offset: 0x00529E02
		public void AddLight(int x, int y, Vector3 color)
		{
			this._perFrameLights.Add(new LightingEngine.PerFrameLight(new Point(x, y), color));
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0052BC1C File Offset: 0x00529E1C
		public void Clear()
		{
			this._activeLightMap.Clear();
			this._workingLightMap.Clear();
			this._perFrameLights.Clear();
			this._oldPerFrameLights.Clear();
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0052BC4C File Offset: 0x00529E4C
		public Vector3 GetColor(int x, int y)
		{
			if (!this._activeProcessedArea.Contains(x, y))
			{
				return Vector3.Zero;
			}
			x -= this._activeProcessedArea.X;
			y -= this._activeProcessedArea.Y;
			return this._activeLightMap[x, y];
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0052BC9C File Offset: 0x00529E9C
		public void ProcessArea(Rectangle area)
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			switch (this._state)
			{
			case LightingEngine.EngineState.MinimapUpdate:
				if (Main.mapDelay > 0)
				{
					Main.mapDelay--;
				}
				else
				{
					this.ExportToMiniMap();
				}
				Main.renderCount = 3;
				break;
			case LightingEngine.EngineState.ExportMetrics:
				Main.UpdateSceneMetrics();
				Main.renderCount = 0;
				break;
			case LightingEngine.EngineState.Scan:
				this.ProcessScan(area);
				Main.renderCount = 1;
				break;
			case LightingEngine.EngineState.Blur:
				this.ProcessBlur();
				this.Present();
				Main.renderCount = 2;
				break;
			}
			TimeLogger.LightingByPass[(int)this._state].AddTime(startTimestamp);
			this.IncrementState();
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0052BD38 File Offset: 0x00529F38
		private void IncrementState()
		{
			this._state = (this._state + 1) % LightingEngine.EngineState.Max;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0052BD4C File Offset: 0x00529F4C
		private void ProcessScan(Rectangle area)
		{
			area.Inflate(28, 28);
			this._workingProcessedArea = area;
			this._workingLightMap.SetSize(area.Width, area.Height);
			this._workingLightMap.NonVisiblePadding = 18;
			this._tileScanner.Update();
			this._tileScanner.ExportTo(area, this._workingLightMap, new TileLightScannerOptions
			{
				DrawInvisibleWalls = Main.ShouldShowInvisibleBlocksAndWalls()
			});
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0052BDC1 File Offset: 0x00529FC1
		private void ProcessBlur()
		{
			this.UpdateLightDecay();
			this.ApplyPerFrameLights();
			this._workingLightMap.Blur();
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0052BDDA File Offset: 0x00529FDA
		private void Present()
		{
			Utils.Swap<LightMap>(ref this._activeLightMap, ref this._workingLightMap);
			Utils.Swap<Rectangle>(ref this._activeProcessedArea, ref this._workingProcessedArea);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0052BE00 File Offset: 0x0052A000
		private void UpdateLightDecay()
		{
			LightMap workingLightMap = this._workingLightMap;
			workingLightMap.LightDecayThroughAir = 0.91f;
			workingLightMap.LightDecayThroughSolid = 0.56f;
			workingLightMap.LightDecayThroughHoney = new Vector3(0.75f, 0.7f, 0.6f) * 0.91f;
			switch (Main.waterStyle)
			{
			case 0:
			case 1:
			case 7:
			case 8:
				workingLightMap.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;
				break;
			case 2:
				workingLightMap.LightDecayThroughWater = new Vector3(0.94f, 0.85f, 1.01f) * 0.91f;
				break;
			case 3:
				workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.95f, 1.015f) * 0.91f;
				break;
			case 4:
				workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 0.86f, 1.01f) * 0.91f;
				break;
			case 5:
				workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.99f, 1.01f) * 0.91f;
				break;
			case 6:
				workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 0.93f, 0.98f) * 0.91f;
				break;
			case 9:
				workingLightMap.LightDecayThroughWater = new Vector3(1f, 0.88f, 0.84f) * 0.91f;
				break;
			case 10:
				workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 1f, 1f) * 0.91f;
				break;
			case 12:
				workingLightMap.LightDecayThroughWater = new Vector3(0.95f, 0.98f, 0.85f) * 0.91f;
				break;
			case 13:
				workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 1f, 1.02f) * 0.91f;
				break;
			}
			Player perspectivePlayer = Main.SceneMetrics.PerspectivePlayer;
			if (perspectivePlayer.nightVision)
			{
				workingLightMap.LightDecayThroughAir *= 1.03f;
				workingLightMap.LightDecayThroughSolid *= 1.03f;
			}
			if (perspectivePlayer.blind)
			{
				workingLightMap.LightDecayThroughAir *= 0.95f;
				workingLightMap.LightDecayThroughSolid *= 0.95f;
			}
			if (perspectivePlayer.blackout)
			{
				workingLightMap.LightDecayThroughAir *= 0.85f;
				workingLightMap.LightDecayThroughSolid *= 0.85f;
			}
			if (perspectivePlayer.headcovered)
			{
				workingLightMap.LightDecayThroughAir *= 0.85f;
				workingLightMap.LightDecayThroughSolid *= 0.85f;
			}
			workingLightMap.LightDecayThroughAir *= Main.SceneState.airLightDecay;
			workingLightMap.LightDecayThroughSolid *= Main.SceneState.solidLightDecay;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0052C10C File Offset: 0x0052A30C
		private void ApplyPerFrameLights()
		{
			List<LightingEngine.PerFrameLight> list = this._perFrameLights;
			if (Main.gamePaused)
			{
				list = this._oldPerFrameLights;
			}
			for (int i = 0; i < list.Count; i++)
			{
				Point position = list[i].Position;
				if (this._workingProcessedArea.Contains(position))
				{
					Vector3 color = list[i].Color;
					Vector3 vector = this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y];
					Vector3.Max(ref vector, ref color, out color);
					this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y] = color;
				}
			}
			if (!CaptureManager.Instance.IsCapturing && !Main.gamePaused)
			{
				Utils.Swap<List<LightingEngine.PerFrameLight>>(ref this._perFrameLights, ref this._oldPerFrameLights);
				this._perFrameLights.Clear();
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0052C208 File Offset: 0x0052A408
		public void Rebuild()
		{
			this._activeProcessedArea = Rectangle.Empty;
			this._workingProcessedArea = Rectangle.Empty;
			this._state = LightingEngine.EngineState.MinimapUpdate;
			this._activeLightMap = new LightMap();
			this._workingLightMap = new LightMap();
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0052C240 File Offset: 0x0052A440
		private void ExportToMiniMap()
		{
			if (!Main.mapEnabled)
			{
				return;
			}
			if (this._activeProcessedArea.Width <= 0 || this._activeProcessedArea.Height <= 0)
			{
				return;
			}
			Rectangle area = new Rectangle(this._activeProcessedArea.X + 28, this._activeProcessedArea.Y + 28, this._activeProcessedArea.Width - 56, this._activeProcessedArea.Height - 56);
			Rectangle rectangle = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
			rectangle.Inflate(-40, -40);
			area = Rectangle.Intersect(area, rectangle);
			area = Rectangle.Intersect(area, MapHelper.sceneArea);
			FastParallel.For(area.Left, area.Right, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					for (int j = area.Top; j < area.Bottom; j++)
					{
						Vector3 vector = this._activeLightMap[i - this._activeProcessedArea.X, j - this._activeProcessedArea.Y];
						float num = Math.Max(Math.Max(vector.X, vector.Y), vector.Z);
						byte b = (byte)Math.Min(255, (int)(num * 255f));
						Main.Map.UpdateLighting(i, j, b);
					}
				}
			}, null);
			Main.updateMap = new Rectangle?(area);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0052C340 File Offset: 0x0052A540
		public LightingEngine()
		{
		}

		// Token: 0x04004B74 RID: 19316
		public const int AREA_PADDING = 28;

		// Token: 0x04004B75 RID: 19317
		private const int NON_VISIBLE_PADDING = 18;

		// Token: 0x04004B76 RID: 19318
		private List<LightingEngine.PerFrameLight> _perFrameLights = new List<LightingEngine.PerFrameLight>();

		// Token: 0x04004B77 RID: 19319
		private List<LightingEngine.PerFrameLight> _oldPerFrameLights = new List<LightingEngine.PerFrameLight>();

		// Token: 0x04004B78 RID: 19320
		private TileLightScanner _tileScanner = new TileLightScanner();

		// Token: 0x04004B79 RID: 19321
		private LightMap _activeLightMap = new LightMap();

		// Token: 0x04004B7A RID: 19322
		private Rectangle _activeProcessedArea;

		// Token: 0x04004B7B RID: 19323
		private LightMap _workingLightMap = new LightMap();

		// Token: 0x04004B7C RID: 19324
		private Rectangle _workingProcessedArea;

		// Token: 0x04004B7D RID: 19325
		private LightingEngine.EngineState _state;

		// Token: 0x020007A6 RID: 1958
		private enum EngineState
		{
			// Token: 0x0400709B RID: 28827
			MinimapUpdate,
			// Token: 0x0400709C RID: 28828
			ExportMetrics,
			// Token: 0x0400709D RID: 28829
			Scan,
			// Token: 0x0400709E RID: 28830
			Blur,
			// Token: 0x0400709F RID: 28831
			Max
		}

		// Token: 0x020007A7 RID: 1959
		private struct PerFrameLight
		{
			// Token: 0x060041B3 RID: 16819 RVA: 0x006BC952 File Offset: 0x006BAB52
			public PerFrameLight(Point position, Vector3 color)
			{
				this.Position = position;
				this.Color = color;
			}

			// Token: 0x040070A0 RID: 28832
			public readonly Point Position;

			// Token: 0x040070A1 RID: 28833
			public readonly Vector3 Color;
		}

		// Token: 0x020007A8 RID: 1960
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060041B4 RID: 16820 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060041B5 RID: 16821 RVA: 0x006BC964 File Offset: 0x006BAB64
			internal void <ExportToMiniMap>b__0(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					for (int j = this.area.Top; j < this.area.Bottom; j++)
					{
						Vector3 vector = this.<>4__this._activeLightMap[i - this.<>4__this._activeProcessedArea.X, j - this.<>4__this._activeProcessedArea.Y];
						float num = Math.Max(Math.Max(vector.X, vector.Y), vector.Z);
						byte b = (byte)Math.Min(255, (int)(num * 255f));
						Main.Map.UpdateLighting(i, j, b);
					}
				}
			}

			// Token: 0x040070A2 RID: 28834
			public Rectangle area;

			// Token: 0x040070A3 RID: 28835
			public LightingEngine <>4__this;
		}
	}
}
