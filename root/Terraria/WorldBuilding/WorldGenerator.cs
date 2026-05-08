using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ReLogic.Threading;
using Terraria.GameContent.UI.States;
using Terraria.Testing;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000BE RID: 190
	public class WorldGenerator
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x004DF688 File Offset: 0x004DD888
		public static List<GenPassResult> PassResults
		{
			get
			{
				return WorldGen.Manifest.GenPassResults;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x004DF694 File Offset: 0x004DD894
		public WorldGenerator(int seed, WorldGenConfiguration configuration, GenerationProgress progress = null, WorldGenerator.Controller controller = null)
		{
			this._seed = seed;
			this._configuration = configuration;
			this._progress = ((progress == null) ? new GenerationProgress() : progress);
			this._controller = ((controller == null) ? new WorldGenerator.Controller(null) : controller);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x004DF6F0 File Offset: 0x004DD8F0
		public void Append(GenPass pass)
		{
			this._passes.Add(pass);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x004DF700 File Offset: 0x004DD900
		public bool GenerateWorld()
		{
			WorldGenerator._hashTime.Reset();
			this._controller.SetGenerator(this);
			WorldGenerator.CurrentController = this._controller;
			this._progress.TotalWeight = this._passes.Where((GenPass p) => p.Enabled).Sum((GenPass p) => p.Weight);
			WorldGenerator.CurrentGenerationProgress = this._progress;
			if (this._controller.PauseAfterPass != null)
			{
				WorldGenerator.SetDebugWorldGenUIVisibility(true);
			}
			bool flag = false;
			while (!this._controller.QueuedAbort)
			{
				if (!this._controller.Paused)
				{
					object controlLock = this._controlLock;
					lock (controlLock)
					{
						if (WorldGenerator.PassResults.Count != this._passes.Count)
						{
							this._currentPass = this._passes[WorldGenerator.PassResults.Count];
							GenPass currentPass = this._currentPass;
							lock (currentPass)
							{
								WorldGenerator.PassResults.Add(this.RunPass(this._currentPass));
								this._controller.OnPassCompleted();
							}
							this._currentPass = null;
							continue;
						}
					}
					IL_0163:
					string text = string.Join<GenPassResult>("\n", WorldGenerator.PassResults);
					string text2 = "\nFinished world - Seed: {0} Width: {1}, Height: {2}, Evil: {3}, Difficulty: {4}\nTotal Generation Time: {5}\n";
					object[] array = new object[6];
					array[0] = Main.ActiveWorldFileData.SeedText;
					array[1] = Main.maxTilesX;
					array[2] = Main.maxTilesY;
					array[3] = WorldGen.WorldGenParam_Evil;
					array[4] = Main.GameMode;
					array[5] = WorldGenerator.PassResults.Sum((GenPassResult r) => r.DurationMs);
					Trace.WriteLine(text + string.Format(text2, array));
					WorldGenerator.SetDebugWorldGenUIVisibility(false);
					WorldGenerator.CurrentGenerationProgress = null;
					WorldGenerator.CurrentController = null;
					return !flag;
				}
				this._controller.OnPaused();
			}
			flag = true;
			goto IL_0163;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x004DF940 File Offset: 0x004DDB40
		private static void SetDebugWorldGenUIVisibility(bool visible)
		{
			bool flag = UIWorldGenDebug.ActiveInstance != null;
			if (visible == flag)
			{
				return;
			}
			Main.RunOnMainThread(delegate
			{
				if (visible)
				{
					UIWorldGenDebug.Open();
					return;
				}
				UIWorldGenDebug.Close();
			}).Wait();
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x004DF984 File Offset: 0x004DDB84
		private GenPassResult RunPass(GenPass pass)
		{
			if (!pass.Enabled)
			{
				return new GenPassResult
				{
					Name = pass.Name,
					Skipped = true
				};
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			Main.rand = new UnifiedRandom(this._seed);
			this._progress.Start(pass.Weight);
			try
			{
				pass.Apply(this._progress, this._configuration.GetPassConfiguration(pass.Name));
			}
			catch (Exception ex)
			{
				this._controller.ReportException("Exception in Pass: " + pass.Name, ex);
			}
			this._progress.End();
			return new GenPassResult
			{
				Name = pass.Name,
				DurationMs = (int)stopwatch.ElapsedMilliseconds,
				RandNext = WorldGen.genRand.Next()
			};
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x004DFA60 File Offset: 0x004DDC60
		public static uint HashWorld()
		{
			WorldGenerator._hashTime.Start();
			uint[] line_hashes = new uint[Main.maxTilesX];
			FastParallel.For(0, Main.maxTilesX, delegate(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				int maxTilesY = Main.maxTilesY;
				for (int j = x0; j < x1; j++)
				{
					uint num3 = 0U;
					for (int k = 0; k < maxTilesY; k++)
					{
						num3 ^= (uint)TileSnapshot.TileStruct.From(tile[j, k]).GetHashCode();
						num3 = (num3 << 13) | (num3 >> 19);
						num3 = num3 * 5U + 3864292196U;
					}
					line_hashes[j] = num3;
				}
			}, null);
			uint num = 0U;
			foreach (uint num2 in line_hashes)
			{
				num ^= num2;
				num = (num << 13) | (num >> 19);
				num = num * 5U + 3864292196U;
			}
			WorldGenerator._hashTime.Stop();
			return num;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x004DFAE5 File Offset: 0x004DDCE5
		// Note: this type is marked as 'beforefieldinit'.
		static WorldGenerator()
		{
		}

		// Token: 0x0400128D RID: 4749
		internal readonly List<GenPass> _passes = new List<GenPass>();

		// Token: 0x0400128E RID: 4750
		private readonly int _seed;

		// Token: 0x0400128F RID: 4751
		private readonly WorldGenConfiguration _configuration;

		// Token: 0x04001290 RID: 4752
		private readonly GenerationProgress _progress;

		// Token: 0x04001291 RID: 4753
		private readonly WorldGenerator.Controller _controller;

		// Token: 0x04001292 RID: 4754
		private readonly object _controlLock = new object();

		// Token: 0x04001293 RID: 4755
		private GenPass _currentPass;

		// Token: 0x04001294 RID: 4756
		public static GenerationProgress CurrentGenerationProgress;

		// Token: 0x04001295 RID: 4757
		public static WorldGenerator.Controller CurrentController;

		// Token: 0x04001296 RID: 4758
		private static Stopwatch _hashTime = new Stopwatch();

		// Token: 0x020006E7 RID: 1767
		public enum SnapshotFrequency
		{
			// Token: 0x040067E4 RID: 26596
			None = -1,
			// Token: 0x040067E5 RID: 26597
			Manual,
			// Token: 0x040067E6 RID: 26598
			Automatic,
			// Token: 0x040067E7 RID: 26599
			Always
		}

		// Token: 0x020006E8 RID: 1768
		public class Controller
		{
			// Token: 0x170004F0 RID: 1264
			// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0069A3F1 File Offset: 0x006985F1
			public List<GenPass> Passes
			{
				get
				{
					return this._generator._passes;
				}
			}

			// Token: 0x170004F1 RID: 1265
			// (get) Token: 0x06003F62 RID: 16226 RVA: 0x0069A3FE File Offset: 0x006985FE
			public GenPass CurrentPass
			{
				get
				{
					return this._generator._currentPass;
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06003F63 RID: 16227 RVA: 0x0069A40B File Offset: 0x0069860B
			public GenPass LastCompletedPass
			{
				get
				{
					if (WorldGenerator.PassResults.Count != 0)
					{
						return this.Passes[WorldGenerator.PassResults.Count - 1];
					}
					return null;
				}
			}

			// Token: 0x170004F3 RID: 1267
			// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0069A432 File Offset: 0x00698632
			// (set) Token: 0x06003F65 RID: 16229 RVA: 0x0069A43A File Offset: 0x0069863A
			public GenPass PauseAfterPass
			{
				[CompilerGenerated]
				get
				{
					return this.<PauseAfterPass>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PauseAfterPass>k__BackingField = value;
				}
			}

			// Token: 0x170004F4 RID: 1268
			// (get) Token: 0x06003F66 RID: 16230 RVA: 0x0069A443 File Offset: 0x00698643
			// (set) Token: 0x06003F67 RID: 16231 RVA: 0x0069A44B File Offset: 0x0069864B
			public bool PauseOnHashMismatch
			{
				[CompilerGenerated]
				get
				{
					return this.<PauseOnHashMismatch>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PauseOnHashMismatch>k__BackingField = value;
				}
			}

			// Token: 0x170004F5 RID: 1269
			// (get) Token: 0x06003F68 RID: 16232 RVA: 0x0069A454 File Offset: 0x00698654
			// (set) Token: 0x06003F69 RID: 16233 RVA: 0x0069A45C File Offset: 0x0069865C
			public bool PausedDueToHashMismatch
			{
				[CompilerGenerated]
				get
				{
					return this.<PausedDueToHashMismatch>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<PausedDueToHashMismatch>k__BackingField = value;
				}
			}

			// Token: 0x170004F6 RID: 1270
			// (get) Token: 0x06003F6A RID: 16234 RVA: 0x0069A465 File Offset: 0x00698665
			// (set) Token: 0x06003F6B RID: 16235 RVA: 0x0069A46D File Offset: 0x0069866D
			public WorldGenerator.SnapshotFrequency SnapshotFrequency
			{
				[CompilerGenerated]
				get
				{
					return this.<SnapshotFrequency>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<SnapshotFrequency>k__BackingField = value;
				}
			}

			// Token: 0x170004F7 RID: 1271
			// (get) Token: 0x06003F6C RID: 16236 RVA: 0x0069A476 File Offset: 0x00698676
			// (set) Token: 0x06003F6D RID: 16237 RVA: 0x0069A47E File Offset: 0x0069867E
			public bool Paused
			{
				get
				{
					return this._paused;
				}
				set
				{
					this._paused = value;
					if (value)
					{
						this.PauseAfterPass = null;
						return;
					}
					this.PausedDueToHashMismatch = false;
				}
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x06003F6E RID: 16238 RVA: 0x0069A499 File Offset: 0x00698699
			// (set) Token: 0x06003F6F RID: 16239 RVA: 0x0069A4A1 File Offset: 0x006986A1
			public bool QueuedAbort
			{
				[CompilerGenerated]
				get
				{
					return this.<QueuedAbort>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<QueuedAbort>k__BackingField = value;
				}
			}

			// Token: 0x06003F70 RID: 16240 RVA: 0x0069A4AC File Offset: 0x006986AC
			public WorldGenSnapshot GetSnapshot(GenPass pass)
			{
				WorldGenSnapshot worldGenSnapshot;
				if (!this._snapshots.TryGetValue(pass, out worldGenSnapshot))
				{
					return null;
				}
				return worldGenSnapshot;
			}

			// Token: 0x06003F71 RID: 16241 RVA: 0x0069A4CC File Offset: 0x006986CC
			public Controller(WorldManifest prevManifest = null)
			{
				this._previousManifest = prevManifest;
				this.PauseOnHashMismatch = true;
				this.SnapshotFrequency = WorldGenerator.SnapshotFrequency.None;
			}

			// Token: 0x06003F72 RID: 16242 RVA: 0x0069A4EC File Offset: 0x006986EC
			internal void SetGenerator(WorldGenerator generator)
			{
				this._generator = generator;
				this._snapshots = WorldGenSnapshot.LoadSnapshots(this._previousManifest, this.Passes);
				if (this._previousManifest != null)
				{
					using (IEnumerator<GenPassResult> enumerator = this._previousManifest.GenPassResults.Where((GenPassResult r) => r.Skipped).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GenPassResult r = enumerator.Current;
							GenPass genPass = this.Passes.SingleOrDefault((GenPass p) => p.Name == r.Name);
							if (genPass != null)
							{
								genPass.Disable();
							}
						}
					}
				}
				if (this.OnPassesLoaded != null)
				{
					this.OnPassesLoaded(this);
				}
			}

			// Token: 0x06003F73 RID: 16243 RVA: 0x0069A5C4 File Offset: 0x006987C4
			internal void OnPaused()
			{
				WorldGenerator.SetDebugWorldGenUIVisibility(true);
				this.ForceUpdateProgress();
				Thread.Sleep(10);
			}

			// Token: 0x06003F74 RID: 16244 RVA: 0x0069A5DC File Offset: 0x006987DC
			internal void OnPassCompleted()
			{
				int num = WorldGenerator.PassResults.Count - 1;
				GenPassResult genPassResult = WorldGenerator.PassResults[num];
				WorldGenSnapshot snapshot = this.GetSnapshot(this.CurrentPass);
				GenPass genPass = this.Passes.Skip(WorldGenerator.PassResults.Count).FirstOrDefault<GenPass>();
				if (UIWorldGenDebug.ActiveInstance != null || genPass == null)
				{
					genPassResult.Hash = new uint?(WorldGenerator.HashWorld());
				}
				Trace.WriteLine(genPassResult);
				foreach (GenPass genPass2 in this.Passes.Skip(num))
				{
					WorldGenSnapshot snapshot2 = this.GetSnapshot(genPass2);
					if (snapshot2 != null && !snapshot2.GenPassResults[num].Matches(genPassResult))
					{
						this._snapshots.Remove(genPass2);
					}
				}
				bool flag = this.SnapshotFrequency == WorldGenerator.SnapshotFrequency.Always || (this.SnapshotFrequency == WorldGenerator.SnapshotFrequency.Automatic && (this.MsSinceLastSnapshot() > 500 || (genPass != null && genPass == this.PauseAfterPass)));
				if (genPassResult.Skipped)
				{
					flag = false;
				}
				if (this.QueuedAbort)
				{
					flag = false;
				}
				if (snapshot != null && snapshot.IsValidHistoryOf(WorldGen.Manifest))
				{
					flag = false;
					if (snapshot.Outdated)
					{
						snapshot.ResaveForCurrentVersion();
					}
				}
				if (flag)
				{
					this.TryCreateSnapshot();
				}
				this.CheckLatestPassResultAgainstManifest(num, genPassResult, snapshot);
				if (this.PauseAfterPass == this.CurrentPass)
				{
					this.Paused = true;
				}
				if (!Main.gameMenu)
				{
					Main.QueueMainThreadAction(new Action(Main.sectionManager.SetAllFramedSectionsAsNeedingRefresh));
				}
			}

			// Token: 0x06003F75 RID: 16245 RVA: 0x0069A778 File Offset: 0x00698978
			private void CheckLatestPassResultAgainstManifest(int currentPassIndex, GenPassResult result, WorldGenSnapshot prevSnapshot)
			{
				if (this._previousManifest == null)
				{
					return;
				}
				if (currentPassIndex >= this._previousManifest.GenPassResults.Count)
				{
					return;
				}
				if (this._previousManifest.GenPassResults[currentPassIndex].Matches(result))
				{
					return;
				}
				this._previousManifest = null;
				string text = string.Format("{0} output changed since last gen.", this.CurrentPass.Name);
				if (this.PauseOnHashMismatch && prevSnapshot != null)
				{
					try
					{
						prevSnapshot.Load();
						this.ReportException(text + " The previous output has been loaded as a snapshot (use /swap and /snapshotdiff to compare)", null);
						goto IL_0096;
					}
					catch (Exception ex)
					{
						this.ReportException(text + "An attempt was made to load a snapshot of the previous output, but an exception occurred", ex);
						goto IL_0096;
					}
				}
				this.ReportException(text, null);
				IL_0096:
				if (this.PauseOnHashMismatch)
				{
					this.Paused = true;
					this.PausedDueToHashMismatch = true;
				}
			}

			// Token: 0x06003F76 RID: 16246 RVA: 0x0069A844 File Offset: 0x00698A44
			public void DeleteSnapshot(GenPass pass)
			{
				Utils.TryOperateInLock(pass, delegate
				{
					WorldGenSnapshot worldGenSnapshot;
					if (this._snapshots.TryGetValue(pass, out worldGenSnapshot))
					{
						this._snapshots.Remove(pass);
						WorldGenSnapshot.Delete(worldGenSnapshot);
					}
				});
			}

			// Token: 0x06003F77 RID: 16247 RVA: 0x0069A87D File Offset: 0x00698A7D
			public void DeleteAllSnapshots()
			{
				this.TryOperateInControlLock(delegate
				{
					this._snapshots.Clear();
					WorldGenSnapshot.DeleteAllForCurrentWorld();
				});
			}

			// Token: 0x06003F78 RID: 16248 RVA: 0x0069A894 File Offset: 0x00698A94
			private int MsSinceLastSnapshot()
			{
				int num = this.Passes.GetRange(0, WorldGenerator.PassResults.Count).FindLastIndex(new Predicate<GenPass>(this._snapshots.ContainsKey));
				return WorldGenerator.PassResults.Skip(num + 1).Sum((GenPassResult r) => r.DurationMs);
			}

			// Token: 0x06003F79 RID: 16249 RVA: 0x0069A900 File Offset: 0x00698B00
			public void ForceUpdateProgress()
			{
				GenerationProgress progress = this._generator._progress;
				progress.Message = ((WorldGenerator.PassResults.Count == 0) ? "World Cleared" : ("Paused after " + this.Passes[WorldGenerator.PassResults.Count - 1].Name));
				progress.TotalWeight = this.Passes.Where((GenPass p) => p.Enabled).Sum((GenPass p) => p.Weight);
				progress.TotalWeightedProgress = (from p in this.Passes.Take(WorldGenerator.PassResults.Count)
					where p.Enabled
					select p).Sum((GenPass p) => p.Weight);
			}

			// Token: 0x06003F7A RID: 16250 RVA: 0x0069AA0D File Offset: 0x00698C0D
			public bool TryOperateInControlLock(Action action)
			{
				return Utils.TryOperateInLock(this._generator._controlLock, action);
			}

			// Token: 0x06003F7B RID: 16251 RVA: 0x0069AA20 File Offset: 0x00698C20
			public bool TryCreateSnapshot()
			{
				return this.TryOperateInControlLock(delegate
				{
					if (WorldGen.Manifest.FinalHash == null)
					{
						Main.NewText("Pass was not run with worldgen debugging enabled, please re-run", 240, 30, 30);
						return;
					}
					uint? finalHash = WorldGen.Manifest.FinalHash;
					uint num = WorldGenerator.HashWorld();
					if (!((finalHash.GetValueOrDefault() == num) & (finalHash != null)))
					{
						Main.NewText("World has been modified since last gen pass completed. Please rerun or use /snapshot instead", 240, 30, 30);
						return;
					}
					try
					{
						this._snapshots[this.LastCompletedPass] = WorldGenSnapshot.Create();
					}
					catch (Exception ex)
					{
						this.ReportException("Exception occured while creating snapshot", ex);
					}
				});
			}

			// Token: 0x06003F7C RID: 16252 RVA: 0x0069AA34 File Offset: 0x00698C34
			public bool TryReset()
			{
				return this.TryOperateInControlLock(delegate
				{
					this.UpdatePreviousManifest();
					WorldGen.RestoreTemporaryStateChanges();
					WorldGen.clearWorld();
					WorldGen.Reset();
					this.ForceUpdateProgress();
					this.Paused = true;
					Main.NewText("World Reset", byte.MaxValue, byte.MaxValue, 0);
				});
			}

			// Token: 0x06003F7D RID: 16253 RVA: 0x0069AA48 File Offset: 0x00698C48
			private void UpdatePreviousManifest()
			{
				if (this._previousManifest == null || WorldGenerator.PassResults.Count > this._previousManifest.GenPassResults.Count)
				{
					this._previousManifest = WorldGen.Manifest;
				}
			}

			// Token: 0x06003F7E RID: 16254 RVA: 0x0069AA7C File Offset: 0x00698C7C
			public bool TryResetToSnapshot(GenPass pass)
			{
				WorldGenSnapshot snap = this.GetSnapshot(pass);
				return snap != null && !snap.Outdated && this.TryOperateInControlLock(delegate
				{
					try
					{
						this.UpdatePreviousManifest();
						snap.Restore();
						this.ForceUpdateProgress();
					}
					catch (Exception ex)
					{
						this.ReportException("Exception occured while restoring snapshot", ex);
					}
				});
			}

			// Token: 0x06003F7F RID: 16255 RVA: 0x0069AACC File Offset: 0x00698CCC
			public bool TryRunToEndOfPass(GenPass pass, bool useSnapshots = true, bool mustRunPass = true)
			{
				if (!pass.Enabled)
				{
					return false;
				}
				int passIndex = this.Passes.IndexOf(pass);
				Func<GenPass, bool> <>9__1;
				if (this.TryOperateInControlLock(delegate
				{
					IEnumerable<GenPass> enumerable = this.Passes.Take(passIndex + (mustRunPass ? 0 : 1)).Reverse<GenPass>();
					Func<GenPass, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (GenPass p) => this.GetSnapshot(p) != null && !this.GetSnapshot(p).Outdated);
					}
					GenPass genPass = enumerable.FirstOrDefault(func);
					bool flag = passIndex < WorldGenerator.PassResults.Count;
					if (useSnapshots && genPass != null && (flag || this.Passes.IndexOf(genPass) >= WorldGenerator.PassResults.Count))
					{
						this.TryResetToSnapshot(genPass);
					}
					else if (flag)
					{
						this.TryReset();
					}
					if (WorldGenerator.PassResults.Count == passIndex + 1)
					{
						this.Paused = true;
						return;
					}
					this.PauseAfterPass = pass;
					this.Paused = false;
				}))
				{
					return true;
				}
				if (pass == this.CurrentPass || passIndex > WorldGenerator.PassResults.Count)
				{
					this.PauseAfterPass = pass;
					return true;
				}
				return false;
			}

			// Token: 0x06003F80 RID: 16256 RVA: 0x0069AB68 File Offset: 0x00698D68
			public bool TryResetToPreviousPass(GenPass pass)
			{
				int num = this.Passes.IndexOf(pass);
				GenPass genPass = this.Passes.Take(num).Reverse<GenPass>().FirstOrDefault((GenPass p) => p.Enabled);
				if (genPass == null)
				{
					return this.TryReset();
				}
				return this.TryRunToEndOfPass(genPass, true, false);
			}

			// Token: 0x06003F81 RID: 16257 RVA: 0x0069ABCB File Offset: 0x00698DCB
			internal void ReportException(string message, Exception ex = null)
			{
				Trace.WriteLine((ex != null) ? ex.ToString() : message);
				if (!DebugOptions.enableDebugCommands)
				{
					return;
				}
				this.Paused = true;
				WorldGenerator.SetDebugWorldGenUIVisibility(true);
				UIWorldGenDebug.ActiveInstance.UnhideChat();
				Main.NewText(message, byte.MaxValue, 0, 0);
			}

			// Token: 0x06003F82 RID: 16258 RVA: 0x0069AC0A File Offset: 0x00698E0A
			[CompilerGenerated]
			private void <DeleteAllSnapshots>b__41_0()
			{
				this._snapshots.Clear();
				WorldGenSnapshot.DeleteAllForCurrentWorld();
			}

			// Token: 0x06003F83 RID: 16259 RVA: 0x0069AC1C File Offset: 0x00698E1C
			[CompilerGenerated]
			private void <TryCreateSnapshot>b__45_0()
			{
				if (WorldGen.Manifest.FinalHash == null)
				{
					Main.NewText("Pass was not run with worldgen debugging enabled, please re-run", 240, 30, 30);
					return;
				}
				uint? finalHash = WorldGen.Manifest.FinalHash;
				uint num = WorldGenerator.HashWorld();
				if (!((finalHash.GetValueOrDefault() == num) & (finalHash != null)))
				{
					Main.NewText("World has been modified since last gen pass completed. Please rerun or use /snapshot instead", 240, 30, 30);
					return;
				}
				try
				{
					this._snapshots[this.LastCompletedPass] = WorldGenSnapshot.Create();
				}
				catch (Exception ex)
				{
					this.ReportException("Exception occured while creating snapshot", ex);
				}
			}

			// Token: 0x06003F84 RID: 16260 RVA: 0x0069ACC4 File Offset: 0x00698EC4
			[CompilerGenerated]
			private void <TryReset>b__46_0()
			{
				this.UpdatePreviousManifest();
				WorldGen.RestoreTemporaryStateChanges();
				WorldGen.clearWorld();
				WorldGen.Reset();
				this.ForceUpdateProgress();
				this.Paused = true;
				Main.NewText("World Reset", byte.MaxValue, byte.MaxValue, 0);
			}

			// Token: 0x040067E8 RID: 26600
			private WorldManifest _previousManifest;

			// Token: 0x040067E9 RID: 26601
			private Dictionary<GenPass, WorldGenSnapshot> _snapshots;

			// Token: 0x040067EA RID: 26602
			public Action<WorldGenerator.Controller> OnPassesLoaded;

			// Token: 0x040067EB RID: 26603
			private WorldGenerator _generator;

			// Token: 0x040067EC RID: 26604
			[CompilerGenerated]
			private GenPass <PauseAfterPass>k__BackingField;

			// Token: 0x040067ED RID: 26605
			[CompilerGenerated]
			private bool <PauseOnHashMismatch>k__BackingField;

			// Token: 0x040067EE RID: 26606
			[CompilerGenerated]
			private bool <PausedDueToHashMismatch>k__BackingField;

			// Token: 0x040067EF RID: 26607
			[CompilerGenerated]
			private WorldGenerator.SnapshotFrequency <SnapshotFrequency>k__BackingField;

			// Token: 0x040067F0 RID: 26608
			private bool _paused;

			// Token: 0x040067F1 RID: 26609
			[CompilerGenerated]
			private bool <QueuedAbort>k__BackingField;

			// Token: 0x02000A49 RID: 2633
			[CompilerGenerated]
			private sealed class <>c__DisplayClass36_0
			{
				// Token: 0x06004AB6 RID: 19126 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass36_0()
				{
				}

				// Token: 0x06004AB7 RID: 19127 RVA: 0x006D4A97 File Offset: 0x006D2C97
				internal bool <SetGenerator>b__1(GenPass p)
				{
					return p.Name == this.r.Name;
				}

				// Token: 0x04007718 RID: 30488
				public GenPassResult r;
			}

			// Token: 0x02000A4A RID: 2634
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004AB8 RID: 19128 RVA: 0x006D4AAF File Offset: 0x006D2CAF
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004AB9 RID: 19129 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004ABA RID: 19130 RVA: 0x006D4ABB File Offset: 0x006D2CBB
				internal bool <SetGenerator>b__36_0(GenPassResult r)
				{
					return r.Skipped;
				}

				// Token: 0x06004ABB RID: 19131 RVA: 0x0069AD19 File Offset: 0x00698F19
				internal int <MsSinceLastSnapshot>b__42_0(GenPassResult r)
				{
					return r.DurationMs;
				}

				// Token: 0x06004ABC RID: 19132 RVA: 0x0069AD09 File Offset: 0x00698F09
				internal bool <ForceUpdateProgress>b__43_0(GenPass p)
				{
					return p.Enabled;
				}

				// Token: 0x06004ABD RID: 19133 RVA: 0x0069AD11 File Offset: 0x00698F11
				internal double <ForceUpdateProgress>b__43_1(GenPass p)
				{
					return p.Weight;
				}

				// Token: 0x06004ABE RID: 19134 RVA: 0x0069AD09 File Offset: 0x00698F09
				internal bool <ForceUpdateProgress>b__43_2(GenPass p)
				{
					return p.Enabled;
				}

				// Token: 0x06004ABF RID: 19135 RVA: 0x0069AD11 File Offset: 0x00698F11
				internal double <ForceUpdateProgress>b__43_3(GenPass p)
				{
					return p.Weight;
				}

				// Token: 0x06004AC0 RID: 19136 RVA: 0x0069AD09 File Offset: 0x00698F09
				internal bool <TryResetToPreviousPass>b__50_0(GenPass p)
				{
					return p.Enabled;
				}

				// Token: 0x04007719 RID: 30489
				public static readonly WorldGenerator.Controller.<>c <>9 = new WorldGenerator.Controller.<>c();

				// Token: 0x0400771A RID: 30490
				public static Func<GenPassResult, bool> <>9__36_0;

				// Token: 0x0400771B RID: 30491
				public static Func<GenPassResult, int> <>9__42_0;

				// Token: 0x0400771C RID: 30492
				public static Func<GenPass, bool> <>9__43_0;

				// Token: 0x0400771D RID: 30493
				public static Func<GenPass, double> <>9__43_1;

				// Token: 0x0400771E RID: 30494
				public static Func<GenPass, bool> <>9__43_2;

				// Token: 0x0400771F RID: 30495
				public static Func<GenPass, double> <>9__43_3;

				// Token: 0x04007720 RID: 30496
				public static Func<GenPass, bool> <>9__50_0;
			}

			// Token: 0x02000A4B RID: 2635
			[CompilerGenerated]
			private sealed class <>c__DisplayClass40_0
			{
				// Token: 0x06004AC1 RID: 19137 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass40_0()
				{
				}

				// Token: 0x06004AC2 RID: 19138 RVA: 0x006D4AC4 File Offset: 0x006D2CC4
				internal void <DeleteSnapshot>b__0()
				{
					WorldGenSnapshot worldGenSnapshot;
					if (this.<>4__this._snapshots.TryGetValue(this.pass, out worldGenSnapshot))
					{
						this.<>4__this._snapshots.Remove(this.pass);
						WorldGenSnapshot.Delete(worldGenSnapshot);
					}
				}

				// Token: 0x04007721 RID: 30497
				public WorldGenerator.Controller <>4__this;

				// Token: 0x04007722 RID: 30498
				public GenPass pass;
			}

			// Token: 0x02000A4C RID: 2636
			[CompilerGenerated]
			private sealed class <>c__DisplayClass48_0
			{
				// Token: 0x06004AC3 RID: 19139 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass48_0()
				{
				}

				// Token: 0x06004AC4 RID: 19140 RVA: 0x006D4B08 File Offset: 0x006D2D08
				internal void <TryResetToSnapshot>b__0()
				{
					try
					{
						this.<>4__this.UpdatePreviousManifest();
						this.snap.Restore();
						this.<>4__this.ForceUpdateProgress();
					}
					catch (Exception ex)
					{
						this.<>4__this.ReportException("Exception occured while restoring snapshot", ex);
					}
				}

				// Token: 0x04007723 RID: 30499
				public WorldGenerator.Controller <>4__this;

				// Token: 0x04007724 RID: 30500
				public WorldGenSnapshot snap;
			}

			// Token: 0x02000A4D RID: 2637
			[CompilerGenerated]
			private sealed class <>c__DisplayClass49_0
			{
				// Token: 0x06004AC5 RID: 19141 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass49_0()
				{
				}

				// Token: 0x06004AC6 RID: 19142 RVA: 0x006D4B5C File Offset: 0x006D2D5C
				internal void <TryRunToEndOfPass>b__0()
				{
					IEnumerable<GenPass> enumerable = this.<>4__this.Passes.Take(this.passIndex + (this.mustRunPass ? 0 : 1)).Reverse<GenPass>();
					Func<GenPass, bool> func;
					if ((func = this.<>9__1) == null)
					{
						func = (this.<>9__1 = (GenPass p) => this.<>4__this.GetSnapshot(p) != null && !this.<>4__this.GetSnapshot(p).Outdated);
					}
					GenPass genPass = enumerable.FirstOrDefault(func);
					bool flag = this.passIndex < WorldGenerator.PassResults.Count;
					if (this.useSnapshots && genPass != null && (flag || this.<>4__this.Passes.IndexOf(genPass) >= WorldGenerator.PassResults.Count))
					{
						this.<>4__this.TryResetToSnapshot(genPass);
					}
					else if (flag)
					{
						this.<>4__this.TryReset();
					}
					if (WorldGenerator.PassResults.Count == this.passIndex + 1)
					{
						this.<>4__this.Paused = true;
						return;
					}
					this.<>4__this.PauseAfterPass = this.pass;
					this.<>4__this.Paused = false;
				}

				// Token: 0x06004AC7 RID: 19143 RVA: 0x006D4C50 File Offset: 0x006D2E50
				internal bool <TryRunToEndOfPass>b__1(GenPass p)
				{
					return this.<>4__this.GetSnapshot(p) != null && !this.<>4__this.GetSnapshot(p).Outdated;
				}

				// Token: 0x04007725 RID: 30501
				public WorldGenerator.Controller <>4__this;

				// Token: 0x04007726 RID: 30502
				public int passIndex;

				// Token: 0x04007727 RID: 30503
				public bool mustRunPass;

				// Token: 0x04007728 RID: 30504
				public bool useSnapshots;

				// Token: 0x04007729 RID: 30505
				public GenPass pass;

				// Token: 0x0400772A RID: 30506
				public Func<GenPass, bool> <>9__1;
			}
		}

		// Token: 0x020006E9 RID: 1769
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003F85 RID: 16261 RVA: 0x0069ACFD File Offset: 0x00698EFD
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003F86 RID: 16262 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003F87 RID: 16263 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <GenerateWorld>b__13_0(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x06003F88 RID: 16264 RVA: 0x0069AD11 File Offset: 0x00698F11
			internal double <GenerateWorld>b__13_1(GenPass p)
			{
				return p.Weight;
			}

			// Token: 0x06003F89 RID: 16265 RVA: 0x0069AD19 File Offset: 0x00698F19
			internal int <GenerateWorld>b__13_2(GenPassResult r)
			{
				return r.DurationMs;
			}

			// Token: 0x040067F2 RID: 26610
			public static readonly WorldGenerator.<>c <>9 = new WorldGenerator.<>c();

			// Token: 0x040067F3 RID: 26611
			public static Func<GenPass, bool> <>9__13_0;

			// Token: 0x040067F4 RID: 26612
			public static Func<GenPass, double> <>9__13_1;

			// Token: 0x040067F5 RID: 26613
			public static Func<GenPassResult, int> <>9__13_2;
		}

		// Token: 0x020006EA RID: 1770
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06003F8A RID: 16266 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06003F8B RID: 16267 RVA: 0x0069AD21 File Offset: 0x00698F21
			internal void <SetDebugWorldGenUIVisibility>b__0()
			{
				if (this.visible)
				{
					UIWorldGenDebug.Open();
					return;
				}
				UIWorldGenDebug.Close();
			}

			// Token: 0x040067F6 RID: 26614
			public bool visible;
		}

		// Token: 0x020006EB RID: 1771
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x06003F8C RID: 16268 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06003F8D RID: 16269 RVA: 0x0069AD38 File Offset: 0x00698F38
			internal void <HashWorld>b__0(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					uint num = 0U;
					for (int j = 0; j < maxTilesY; j++)
					{
						num ^= (uint)TileSnapshot.TileStruct.From(tile[i, j]).GetHashCode();
						num = (num << 13) | (num >> 19);
						num = num * 5U + 3864292196U;
					}
					this.line_hashes[i] = num;
				}
			}

			// Token: 0x040067F7 RID: 26615
			public uint[] line_hashes;
		}
	}
}
