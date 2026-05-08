using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x02000074 RID: 116
	public static class TouchPanel
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00023F3F File Offset: 0x0002213F
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00023F46 File Offset: 0x00022146
		public static int DisplayWidth
		{
			[CompilerGenerated]
			get
			{
				return TouchPanel.<DisplayWidth>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TouchPanel.<DisplayWidth>k__BackingField = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00023F4E File Offset: 0x0002214E
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x00023F55 File Offset: 0x00022155
		public static int DisplayHeight
		{
			[CompilerGenerated]
			get
			{
				return TouchPanel.<DisplayHeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TouchPanel.<DisplayHeight>k__BackingField = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00023F5D File Offset: 0x0002215D
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x00023F64 File Offset: 0x00022164
		public static DisplayOrientation DisplayOrientation
		{
			[CompilerGenerated]
			get
			{
				return TouchPanel.<DisplayOrientation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TouchPanel.<DisplayOrientation>k__BackingField = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00023F6C File Offset: 0x0002216C
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x00023F73 File Offset: 0x00022173
		public static GestureType EnabledGestures
		{
			[CompilerGenerated]
			get
			{
				return TouchPanel.<EnabledGestures>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TouchPanel.<EnabledGestures>k__BackingField = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00023F7B File Offset: 0x0002217B
		public static bool IsGestureAvailable
		{
			get
			{
				return TouchPanel.gestures.Count > 0;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00023F8A File Offset: 0x0002218A
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00023F91 File Offset: 0x00022191
		public static IntPtr WindowHandle
		{
			[CompilerGenerated]
			get
			{
				return TouchPanel.<WindowHandle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				TouchPanel.<WindowHandle>k__BackingField = value;
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00023F99 File Offset: 0x00022199
		public static TouchPanelCapabilities GetCapabilities()
		{
			return FNAPlatform.GetTouchCapabilities();
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00023FA8 File Offset: 0x000221A8
		public static TouchCollection GetState()
		{
			TouchPanel.validTouches.Clear();
			for (int i = 0; i < 8; i++)
			{
				if (TouchPanel.touches[i].State != TouchLocationState.Invalid)
				{
					TouchPanel.validTouches.Add(TouchPanel.touches[i]);
				}
			}
			return new TouchCollection(TouchPanel.validTouches.ToArray());
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00024001 File Offset: 0x00022201
		public static GestureSample ReadGesture()
		{
			if (TouchPanel.gestures.Count == 0)
			{
				throw new InvalidOperationException();
			}
			return TouchPanel.gestures.Dequeue();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0002401F File Offset: 0x0002221F
		internal static void EnqueueGesture(GestureSample gesture)
		{
			TouchPanel.gestures.Enqueue(gesture);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0002402C File Offset: 0x0002222C
		internal static void INTERNAL_onTouchEvent(int fingerId, TouchLocationState state, float x, float y, float dx, float dy)
		{
			Vector2 vector = new Vector2((float)Math.Round((double)(x * (float)TouchPanel.DisplayWidth)), (float)Math.Round((double)(y * (float)TouchPanel.DisplayHeight)));
			switch (state)
			{
			case TouchLocationState.Released:
				GestureDetector.OnReleased(fingerId, vector);
				return;
			case TouchLocationState.Pressed:
				GestureDetector.OnPressed(fingerId, vector);
				return;
			case TouchLocationState.Moved:
			{
				Vector2 vector2 = new Vector2((float)Math.Round((double)(dx * (float)TouchPanel.DisplayWidth)), (float)Math.Round((double)(dy * (float)TouchPanel.DisplayHeight)));
				GestureDetector.OnMoved(fingerId, vector, vector2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000240B4 File Offset: 0x000222B4
		internal static void SetFinger(int index, int fingerId, Vector2 fingerPos)
		{
			if (fingerId == -1)
			{
				if (TouchPanel.prevTouches[index].State != TouchLocationState.Invalid && TouchPanel.prevTouches[index].State != TouchLocationState.Released)
				{
					TouchPanel.touches[index] = new TouchLocation(TouchPanel.prevTouches[index].Id, TouchLocationState.Released, TouchPanel.prevTouches[index].Position, TouchPanel.prevTouches[index].State, TouchPanel.prevTouches[index].Position);
					return;
				}
				TouchPanel.touches[index] = new TouchLocation(-1, TouchLocationState.Invalid, Vector2.Zero);
				return;
			}
			else
			{
				if (TouchPanel.prevTouches[index].State == TouchLocationState.Invalid)
				{
					TouchPanel.touches[index] = new TouchLocation(fingerId, TouchLocationState.Pressed, fingerPos);
					return;
				}
				TouchPanel.touches[index] = new TouchLocation(fingerId, TouchLocationState.Moved, fingerPos, TouchPanel.prevTouches[index].State, TouchPanel.prevTouches[index].Position);
				return;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000241B0 File Offset: 0x000223B0
		internal static void Update()
		{
			GestureDetector.OnUpdate();
			TouchPanel.touches.CopyTo(TouchPanel.prevTouches, 0);
			FNAPlatform.UpdateTouchPanelState();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000241D1 File Offset: 0x000223D1
		// Note: this type is marked as 'beforefieldinit'.
		static TouchPanel()
		{
		}

		// Token: 0x0400079D RID: 1949
		internal const int MAX_TOUCHES = 8;

		// Token: 0x0400079E RID: 1950
		internal const int NO_FINGER = -1;

		// Token: 0x0400079F RID: 1951
		[CompilerGenerated]
		private static int <DisplayWidth>k__BackingField;

		// Token: 0x040007A0 RID: 1952
		[CompilerGenerated]
		private static int <DisplayHeight>k__BackingField;

		// Token: 0x040007A1 RID: 1953
		[CompilerGenerated]
		private static DisplayOrientation <DisplayOrientation>k__BackingField;

		// Token: 0x040007A2 RID: 1954
		[CompilerGenerated]
		private static GestureType <EnabledGestures>k__BackingField;

		// Token: 0x040007A3 RID: 1955
		[CompilerGenerated]
		private static IntPtr <WindowHandle>k__BackingField;

		// Token: 0x040007A4 RID: 1956
		internal static bool TouchDeviceExists;

		// Token: 0x040007A5 RID: 1957
		private static Queue<GestureSample> gestures = new Queue<GestureSample>();

		// Token: 0x040007A6 RID: 1958
		private static TouchLocation[] touches = new TouchLocation[8];

		// Token: 0x040007A7 RID: 1959
		private static TouchLocation[] prevTouches = new TouchLocation[8];

		// Token: 0x040007A8 RID: 1960
		private static List<TouchLocation> validTouches = new List<TouchLocation>();
	}
}
