using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Content.Sources;
using SDL3;

namespace Terraria.Audio
{
	// Token: 0x020005D4 RID: 1492
	public class LegacyAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06003A91 RID: 14993 RVA: 0x0065560C File Offset: 0x0065380C
		public void LoadFromSources()
		{
			List<IContentSource> fileSources = this.FileSources;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				string text;
				if (this.TrackNamesByIndex.TryGetValue(i, out text))
				{
					string text2 = "Music" + Path.DirectorySeparatorChar.ToString() + text;
					IAudioTrack audioTrack = this.DefaultTrackByIndex[i];
					IAudioTrack audioTrack2 = audioTrack;
					IAudioTrack audioTrack3 = this.FindReplacementTrack(fileSources, text2);
					if (audioTrack3 != null)
					{
						audioTrack2 = audioTrack3;
					}
					if (this.AudioTracks[i] != audioTrack2)
					{
						this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
					}
					if (this.AudioTracks[i] != audioTrack)
					{
						this.AudioTracks[i].Dispose();
					}
					this.AudioTracks[i] = audioTrack2;
				}
			}
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x006556C4 File Offset: 0x006538C4
		public void UseSources(List<IContentSource> sourcesFromLowestToHighest)
		{
			this.FileSources = sourcesFromLowestToHighest;
			this.LoadFromSources();
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x006556D4 File Offset: 0x006538D4
		public void Update()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null)
				{
					this.AudioTracks[i].Update();
				}
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x0065571C File Offset: 0x0065391C
		private IAudioTrack FindReplacementTrack(List<IContentSource> sources, string assetPath)
		{
			IAudioTrack audioTrack = null;
			for (int i = 0; i < sources.Count; i++)
			{
				IContentSource contentSource = sources[i];
				if (contentSource.HasAsset(assetPath))
				{
					string extension = contentSource.GetExtension(assetPath);
					try
					{
						IAudioTrack audioTrack2 = null;
						if (!(extension == ".ogg"))
						{
							if (!(extension == ".wav"))
							{
								if (extension == ".mp3")
								{
									audioTrack2 = new MP3AudioTrack(contentSource.OpenStream(assetPath));
								}
							}
							else
							{
								audioTrack2 = new WAVAudioTrack(contentSource.OpenStream(assetPath));
							}
						}
						else
						{
							audioTrack2 = new OGGAudioTrack(contentSource.OpenStream(assetPath));
						}
						if (audioTrack2 != null)
						{
							if (audioTrack != null)
							{
								audioTrack.Dispose();
							}
							audioTrack = audioTrack2;
						}
					}
					catch
					{
						string text = "A resource pack failed to load " + assetPath + "!";
						Main.IssueReporter.AddReport(text);
						Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
					}
				}
			}
			return audioTrack;
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x00655808 File Offset: 0x00653A08
		public LegacyAudioSystem()
		{
			this.Engine = new AudioEngine("Content\\TerrariaMusic.xgs");
			Console.WriteLine("SDL Audio Driver: " + SDL.SDL_GetCurrentAudioDriver());
			this.SoundBank = new SoundBank(this.Engine, "Content\\Sound Bank.xsb");
			this.Engine.Update();
			this.WaveBank = new WaveBank(this.Engine, "Content\\Wave Bank.xwb", 0, 512);
			this.Engine.Update();
			this.AudioTracks = new IAudioTrack[Main.maxMusic];
			this.TrackNamesByIndex = new Dictionary<int, string>();
			this.DefaultTrackByIndex = new Dictionary<int, IAudioTrack>();
			this.TrackLoopCounts = new int[Main.maxMusic];
			this.PlayCallbacks = new AudioTrackPlayCallback[Main.maxMusic];
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x006558CD File Offset: 0x00653ACD
		public IEnumerator PrepareWaveBank()
		{
			while (!this.WaveBank.IsPrepared)
			{
				this.Engine.Update();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x006558DC File Offset: 0x00653ADC
		public void LoadCue(int cueIndex, string cueName)
		{
			CueAudioTrack cueAudioTrack = new CueAudioTrack(this.SoundBank, cueName);
			this.TrackNamesByIndex[cueIndex] = cueName;
			this.DefaultTrackByIndex[cueIndex] = cueAudioTrack;
			this.AudioTracks[cueIndex] = cueAudioTrack;
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateMisc()
		{
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x0065591C File Offset: 0x00653B1C
		public void PauseAll()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			float[] musicFade = Main.musicFade;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null && !this.AudioTracks[i].IsPaused && this.AudioTracks[i].IsPlaying && musicFade[i] > 0f)
				{
					try
					{
						this.AudioTracks[i].Pause();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x006559A4 File Offset: 0x00653BA4
		public void ResumeAll()
		{
			if (!this.WaveBank.IsPrepared)
			{
				return;
			}
			float[] musicFade = Main.musicFade;
			for (int i = 0; i < this.AudioTracks.Length; i++)
			{
				if (this.AudioTracks[i] != null && this.AudioTracks[i].IsPaused && musicFade[i] > 0f)
				{
					try
					{
						this.AudioTracks[i].Resume();
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x00655A20 File Offset: 0x00653C20
		public void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume)
		{
			if (!this.WaveBank.IsPrepared || this.AudioTracks[i] == null)
			{
				return;
			}
			if (systemVolume == 0f)
			{
				if (this.AudioTracks[i].IsPlaying)
				{
					this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
					return;
				}
			}
			else
			{
				if (!this.AudioTracks[i].IsPlaying)
				{
					this.AudioTracks[i].Reuse();
					this.AudioTracks[i].Play();
					this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
					return;
				}
				if (this.AudioTracks[i].IsPaused && gameIsActive)
				{
					this.AudioTracks[i].Resume();
					return;
				}
				trackVolume += 0.005f;
				if (trackVolume > 1f)
				{
					trackVolume = 1f;
				}
				this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x00655B00 File Offset: 0x00653D00
		public void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume)
		{
			if (!this.WaveBank.IsPrepared || this.AudioTracks[i] == null)
			{
				return;
			}
			if (!this.AudioTracks[i].IsPlaying)
			{
				trackVolume = 0f;
				return;
			}
			if (trackVolume > 0f)
			{
				trackVolume -= stoppingSpeed;
				if (trackVolume < 0f)
				{
					trackVolume = 0f;
				}
			}
			if (trackVolume <= 0f)
			{
				this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", trackVolume * systemVolume);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x00655B8A File Offset: 0x00653D8A
		public bool IsTrackPlaying(int trackIndex)
		{
			return this.WaveBank.IsPrepared && this.AudioTracks[trackIndex] != null && this.AudioTracks[trackIndex].IsPlaying;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x00655BB4 File Offset: 0x00653DB4
		public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
		{
			if (!this.WaveBank.IsPrepared || this.AudioTracks[i] == null)
			{
				return;
			}
			tempFade += 0.005f;
			if (tempFade > 1f)
			{
				tempFade = 1f;
			}
			if (!this.AudioTracks[i].IsPlaying && active)
			{
				this.AudioTracks[i].Reuse();
				this.AudioTracks[i].SetVariable("Volume", totalVolume);
				this.AudioTracks[i].Play();
				if (this.PlayCallbacks[i] != null)
				{
					this.PlayCallbacks[i](i, this.TrackLoopCounts[i]);
				}
				this.TrackLoopCounts[i]++;
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", totalVolume);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x00655C80 File Offset: 0x00653E80
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
			if (!this.WaveBank.IsPrepared || this.AudioTracks[i] == null)
			{
				return;
			}
			if (!this.AudioTracks[i].IsPlaying && this.AudioTracks[i].IsStopped)
			{
				tempFade = 0f;
				return;
			}
			if (isMainTrackAudible)
			{
				tempFade -= 0.005f;
			}
			else if (Main.curMusic == 0)
			{
				tempFade = 0f;
			}
			if (tempFade <= 0f)
			{
				tempFade = 0f;
				this.AudioTracks[i].SetVariable("Volume", 0f);
				this.AudioTracks[i].Stop(AudioStopOptions.Immediate);
				this.TrackLoopCounts[i] = 0;
				return;
			}
			this.AudioTracks[i].SetVariable("Volume", totalVolume);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x00655D3B File Offset: 0x00653F3B
		public void UpdateAudioEngine()
		{
			this.Engine.Update();
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x00655D48 File Offset: 0x00653F48
		public void SetPlayCallback(int trackIndex, AudioTrackPlayCallback callback)
		{
			this.PlayCallbacks[trackIndex] = callback;
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x00655D53 File Offset: 0x00653F53
		public void Dispose()
		{
			this.SoundBank.Dispose();
			this.WaveBank.Dispose();
			this.Engine.Dispose();
		}

		// Token: 0x04005DE7 RID: 24039
		public IAudioTrack[] AudioTracks;

		// Token: 0x04005DE8 RID: 24040
		public int MusicReplayDelay;

		// Token: 0x04005DE9 RID: 24041
		public AudioEngine Engine;

		// Token: 0x04005DEA RID: 24042
		public SoundBank SoundBank;

		// Token: 0x04005DEB RID: 24043
		public WaveBank WaveBank;

		// Token: 0x04005DEC RID: 24044
		public Dictionary<int, string> TrackNamesByIndex;

		// Token: 0x04005DED RID: 24045
		public Dictionary<int, IAudioTrack> DefaultTrackByIndex;

		// Token: 0x04005DEE RID: 24046
		public List<IContentSource> FileSources;

		// Token: 0x04005DEF RID: 24047
		public int[] TrackLoopCounts;

		// Token: 0x04005DF0 RID: 24048
		public AudioTrackPlayCallback[] PlayCallbacks;

		// Token: 0x020009CC RID: 2508
		[CompilerGenerated]
		private sealed class <PrepareWaveBank>d__15 : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06004A60 RID: 19040 RVA: 0x006D4111 File Offset: 0x006D2311
			[DebuggerHidden]
			public <PrepareWaveBank>d__15(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06004A61 RID: 19041 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004A62 RID: 19042 RVA: 0x006D4120 File Offset: 0x006D2320
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				LegacyAudioSystem legacyAudioSystem = this;
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
				}
				if (legacyAudioSystem.WaveBank.IsPrepared)
				{
					return false;
				}
				legacyAudioSystem.Engine.Update();
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06004A63 RID: 19043 RVA: 0x006D417D File Offset: 0x006D237D
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004A64 RID: 19044 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06004A65 RID: 19045 RVA: 0x006D417D File Offset: 0x006D237D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040076F2 RID: 30450
			private int <>1__state;

			// Token: 0x040076F3 RID: 30451
			private object <>2__current;

			// Token: 0x040076F4 RID: 30452
			public LegacyAudioSystem <>4__this;
		}
	}
}
