using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.Cinematics
{
	// Token: 0x020005B2 RID: 1458
	public class Film
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060039B2 RID: 14770 RVA: 0x00653B0A File Offset: 0x00651D0A
		public int Frame
		{
			get
			{
				return this._frame;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x00653B12 File Offset: 0x00651D12
		public int FrameCount
		{
			get
			{
				return this._frameCount;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x00653B1A File Offset: 0x00651D1A
		public int AppendPoint
		{
			get
			{
				return this._nextSequenceAppendTime;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x00653B22 File Offset: 0x00651D22
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00653B2A File Offset: 0x00651D2A
		public void AddSequence(int start, int duration, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, start, duration));
			this._nextSequenceAppendTime = Math.Max(this._nextSequenceAppendTime, start + duration);
			this._frameCount = Math.Max(this._frameCount, start + duration);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x00653B67 File Offset: 0x00651D67
		public void AppendSequence(int duration, FrameEvent frameEvent)
		{
			this.AddSequence(this._nextSequenceAppendTime, duration, frameEvent);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x00653B78 File Offset: 0x00651D78
		public void AddSequences(int start, int duration, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddSequence(start, duration, frameEvent);
			}
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x00653BA4 File Offset: 0x00651DA4
		public void AppendSequences(int duration, params FrameEvent[] frameEvents)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this._sequences.Add(new Film.Sequence(frameEvent, nextSequenceAppendTime, duration));
				this._nextSequenceAppendTime = Math.Max(this._nextSequenceAppendTime, nextSequenceAppendTime + duration);
				this._frameCount = Math.Max(this._frameCount, nextSequenceAppendTime + duration);
			}
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x00653C07 File Offset: 0x00651E07
		public void AppendEmptySequence(int duration)
		{
			this.AddSequence(this._nextSequenceAppendTime, duration, new FrameEvent(Film.EmptyFrameEvent));
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x00653C22 File Offset: 0x00651E22
		public void AppendKeyFrame(FrameEvent frameEvent)
		{
			this.AddKeyFrame(this._nextSequenceAppendTime, frameEvent);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x00653C34 File Offset: 0x00651E34
		public void AppendKeyFrames(params FrameEvent[] frameEvents)
		{
			int nextSequenceAppendTime = this._nextSequenceAppendTime;
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this._sequences.Add(new Film.Sequence(frameEvent, nextSequenceAppendTime, 1));
			}
			this._frameCount = Math.Max(this._frameCount, nextSequenceAppendTime + 1);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x00653C83 File Offset: 0x00651E83
		public void AddKeyFrame(int frame, FrameEvent frameEvent)
		{
			this._sequences.Add(new Film.Sequence(frameEvent, frame, 1));
			this._frameCount = Math.Max(this._frameCount, frame + 1);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x00653CAC File Offset: 0x00651EAC
		public void AddKeyFrames(int frame, params FrameEvent[] frameEvents)
		{
			foreach (FrameEvent frameEvent in frameEvents)
			{
				this.AddKeyFrame(frame, frameEvent);
			}
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00653CD8 File Offset: 0x00651ED8
		public bool OnUpdate(GameTime gameTime)
		{
			if (this._sequences.Count == 0)
			{
				return false;
			}
			foreach (Film.Sequence sequence in this._sequences)
			{
				int num = this._frame - sequence.Start;
				if (num >= 0 && num < sequence.Duration)
				{
					sequence.Event(new FrameEventData(this._frame, sequence.Start, sequence.Duration));
				}
			}
			int num2 = this._frame + 1;
			this._frame = num2;
			return num2 != this._frameCount;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x00653D8C File Offset: 0x00651F8C
		public virtual void OnBegin()
		{
			this._isActive = true;
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x00653D95 File Offset: 0x00651F95
		public virtual void OnEnd()
		{
			this._isActive = false;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x00009E46 File Offset: 0x00008046
		private static void EmptyFrameEvent(FrameEventData evt)
		{
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x00653D9E File Offset: 0x00651F9E
		public Film()
		{
		}

		// Token: 0x04005DAF RID: 23983
		private int _frame;

		// Token: 0x04005DB0 RID: 23984
		private int _frameCount;

		// Token: 0x04005DB1 RID: 23985
		private int _nextSequenceAppendTime;

		// Token: 0x04005DB2 RID: 23986
		private bool _isActive;

		// Token: 0x04005DB3 RID: 23987
		private List<Film.Sequence> _sequences = new List<Film.Sequence>();

		// Token: 0x020009C4 RID: 2500
		private class Sequence
		{
			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06004A42 RID: 19010 RVA: 0x006D4019 File Offset: 0x006D2219
			public FrameEvent Event
			{
				get
				{
					return this._frameEvent;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06004A43 RID: 19011 RVA: 0x006D4021 File Offset: 0x006D2221
			public int Duration
			{
				get
				{
					return this._duration;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x06004A44 RID: 19012 RVA: 0x006D4029 File Offset: 0x006D2229
			public int Start
			{
				get
				{
					return this._start;
				}
			}

			// Token: 0x06004A45 RID: 19013 RVA: 0x006D4031 File Offset: 0x006D2231
			public Sequence(FrameEvent frameEvent, int start, int duration)
			{
				this._frameEvent = frameEvent;
				this._start = start;
				this._duration = duration;
			}

			// Token: 0x040076E2 RID: 30434
			private FrameEvent _frameEvent;

			// Token: 0x040076E3 RID: 30435
			private int _duration;

			// Token: 0x040076E4 RID: 30436
			private int _start;
		}
	}
}
