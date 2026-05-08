using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;

namespace Terraria.Audio
{
	// Token: 0x020005CE RID: 1486
	public class DisabledAudioSystem : IAudioSystem, IDisposable
	{
		// Token: 0x06003A51 RID: 14929 RVA: 0x00009E46 File Offset: 0x00008046
		public void LoadFromSources()
		{
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x00009E46 File Offset: 0x00008046
		public void UseSources(List<IContentSource> sources)
		{
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x00009E46 File Offset: 0x00008046
		public void Update()
		{
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateMisc()
		{
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0000357B File Offset: 0x0000177B
		public DisabledAudioSystem()
		{
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x006554FD File Offset: 0x006536FD
		public IEnumerator PrepareWaveBank()
		{
			yield break;
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x00009E46 File Offset: 0x00008046
		public void LoadCue(int cueIndex, string cueName)
		{
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x00009E46 File Offset: 0x00008046
		public void PauseAll()
		{
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x00009E46 File Offset: 0x00008046
		public void ResumeAll()
		{
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateAmbientCueState(int i, bool gameIsActive, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateAmbientCueTowardStopping(int i, float stoppingSpeed, ref float trackVolume, float systemVolume)
		{
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public bool IsTrackPlaying(int trackIndex)
		{
			return false;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateCommonTrack(bool active, int i, float totalVolume, ref float tempFade)
		{
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateCommonTrackTowardStopping(int i, float totalVolume, ref float tempFade, bool isMainTrackAudible)
		{
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x00009E46 File Offset: 0x00008046
		public void UpdateAudioEngine()
		{
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x00009E46 File Offset: 0x00008046
		public void SetPlayCallback(int trackIndex, AudioTrackPlayCallback callback)
		{
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x00009E46 File Offset: 0x00008046
		public void Dispose()
		{
		}

		// Token: 0x020009CB RID: 2507
		[CompilerGenerated]
		private sealed class <PrepareWaveBank>d__5 : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06004A5A RID: 19034 RVA: 0x006D40D8 File Offset: 0x006D22D8
			[DebuggerHidden]
			public <PrepareWaveBank>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06004A5B RID: 19035 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004A5C RID: 19036 RVA: 0x006D40E8 File Offset: 0x006D22E8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06004A5D RID: 19037 RVA: 0x006D4109 File Offset: 0x006D2309
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004A5E RID: 19038 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06004A5F RID: 19039 RVA: 0x006D4109 File Offset: 0x006D2309
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040076F0 RID: 30448
			private int <>1__state;

			// Token: 0x040076F1 RID: 30449
			private object <>2__current;
		}
	}
}
