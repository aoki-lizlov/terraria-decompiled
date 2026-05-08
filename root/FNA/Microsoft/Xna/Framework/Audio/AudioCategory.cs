using System;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x0200014C RID: 332
	public struct AudioCategory : IEquatable<AudioCategory>
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0003BE7F File Offset: 0x0003A07F
		public string Name
		{
			get
			{
				return this.INTERNAL_name;
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0003BE87 File Offset: 0x0003A087
		internal AudioCategory(AudioEngine engine, ushort category, string name)
		{
			this.parent = engine;
			this.index = category;
			this.INTERNAL_name = name;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0003BEA0 File Offset: 0x0003A0A0
		public void Pause()
		{
			object gcSync = this.parent.gcSync;
			lock (gcSync)
			{
				if (!this.parent.IsDisposed)
				{
					FAudio.FACTAudioEngine_Pause(this.parent.handle, this.index, 1);
				}
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0003BF08 File Offset: 0x0003A108
		public void Resume()
		{
			object gcSync = this.parent.gcSync;
			lock (gcSync)
			{
				if (!this.parent.IsDisposed)
				{
					FAudio.FACTAudioEngine_Pause(this.parent.handle, this.index, 0);
				}
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0003BF70 File Offset: 0x0003A170
		public void SetVolume(float volume)
		{
			object gcSync = this.parent.gcSync;
			lock (gcSync)
			{
				if (!this.parent.IsDisposed)
				{
					FAudio.FACTAudioEngine_SetVolume(this.parent.handle, this.index, volume);
				}
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0003BFD8 File Offset: 0x0003A1D8
		public void Stop(AudioStopOptions options)
		{
			object gcSync = this.parent.gcSync;
			lock (gcSync)
			{
				if (!this.parent.IsDisposed)
				{
					FAudio.FACTAudioEngine_Stop(this.parent.handle, this.index, (options == AudioStopOptions.Immediate) ? 1U : 0U);
				}
			}
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0003C044 File Offset: 0x0003A244
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0003C051 File Offset: 0x0003A251
		public bool Equals(AudioCategory other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0003C06E File Offset: 0x0003A26E
		public override bool Equals(object obj)
		{
			return obj is AudioCategory && this.Equals((AudioCategory)obj);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0003C086 File Offset: 0x0003A286
		public static bool operator ==(AudioCategory value1, AudioCategory value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0003C090 File Offset: 0x0003A290
		public static bool operator !=(AudioCategory value1, AudioCategory value2)
		{
			return !value1.Equals(value2);
		}

		// Token: 0x04000AE5 RID: 2789
		private string INTERNAL_name;

		// Token: 0x04000AE6 RID: 2790
		private AudioEngine parent;

		// Token: 0x04000AE7 RID: 2791
		private ushort index;
	}
}
