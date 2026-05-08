using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x0200006E RID: 110
	internal static class GestureDetector
	{
		// Token: 0x060010AA RID: 4266 RVA: 0x000233A8 File Offset: 0x000215A8
		internal static void OnPressed(int fingerId, Vector2 touchPosition)
		{
			GestureDetector.fingerIds.Add(fingerId);
			if (GestureDetector.state == GestureDetector.GestureState.PINCHING)
			{
				return;
			}
			if (GestureDetector.activeFingerId == -1)
			{
				GestureDetector.activeFingerId = fingerId;
				GestureDetector.activeFingerPosition = touchPosition;
				if (GestureDetector.state == GestureDetector.GestureState.JUST_TAPPED && GestureDetector.IsGestureEnabled(GestureType.DoubleTap) && DateTime.UtcNow - GestureDetector.eventTimestamp <= TimeSpan.FromMilliseconds(300.0) && (touchPosition - GestureDetector.pressPosition).Length() <= 35f)
				{
					TouchPanel.EnqueueGesture(new GestureSample(GestureType.DoubleTap, GestureDetector.GetGestureTimestamp(), touchPosition, Vector2.Zero, Vector2.Zero, Vector2.Zero, fingerId, -1));
					GestureDetector.justDoubleTapped = true;
				}
				GestureDetector.state = GestureDetector.GestureState.HOLDING;
				GestureDetector.pressPosition = touchPosition;
				GestureDetector.eventTimestamp = DateTime.UtcNow;
				return;
			}
			if (GestureDetector.IsGestureEnabled(GestureType.Pinch))
			{
				GestureDetector.secondFingerId = fingerId;
				GestureDetector.secondFingerPosition = touchPosition;
				GestureDetector.state = GestureDetector.GestureState.PINCHING;
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00023488 File Offset: 0x00021688
		internal static void OnReleased(int fingerId, Vector2 touchPosition)
		{
			GestureDetector.fingerIds.Remove(fingerId);
			if (GestureDetector.state == GestureDetector.GestureState.PINCHING)
			{
				GestureDetector.OnReleased_Pinch(fingerId, touchPosition);
				return;
			}
			if (fingerId == GestureDetector.activeFingerId)
			{
				GestureDetector.activeFingerId = -1;
			}
			if (FNAPlatform.GetNumTouchFingers() > 0)
			{
				return;
			}
			if (GestureDetector.state == GestureDetector.GestureState.HOLDING)
			{
				bool flag = GestureDetector.IsGestureEnabled(GestureType.Tap);
				bool flag2 = GestureDetector.IsGestureEnabled(GestureType.DoubleTap);
				if ((flag || flag2) && DateTime.UtcNow - GestureDetector.eventTimestamp < TimeSpan.FromSeconds(1.0) && !GestureDetector.justDoubleTapped)
				{
					if (flag)
					{
						TouchPanel.EnqueueGesture(new GestureSample(GestureType.Tap, GestureDetector.GetGestureTimestamp(), touchPosition, Vector2.Zero, Vector2.Zero, Vector2.Zero, fingerId, -1));
					}
					GestureDetector.state = GestureDetector.GestureState.JUST_TAPPED;
				}
			}
			GestureDetector.justDoubleTapped = false;
			if (GestureDetector.IsGestureEnabled(GestureType.Flick))
			{
				if ((touchPosition - GestureDetector.pressPosition).Length() > 35f && GestureDetector.velocity.Length() >= 100f)
				{
					TouchPanel.EnqueueGesture(new GestureSample(GestureType.Flick, GestureDetector.GetGestureTimestamp(), Vector2.Zero, Vector2.Zero, GestureDetector.velocity, Vector2.Zero, fingerId, -1));
				}
				GestureDetector.velocity = Vector2.Zero;
				GestureDetector.lastUpdatePosition = Vector2.Zero;
				GestureDetector.updateTimestamp = DateTime.MinValue;
			}
			if (GestureDetector.IsGestureEnabled(GestureType.DragComplete) && (GestureDetector.state == GestureDetector.GestureState.DRAGGING_H || GestureDetector.state == GestureDetector.GestureState.DRAGGING_V || GestureDetector.state == GestureDetector.GestureState.DRAGGING_FREE))
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.DragComplete, GestureDetector.GetGestureTimestamp(), Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, fingerId, -1));
			}
			if (GestureDetector.callBelatedPinchComplete && GestureDetector.IsGestureEnabled(GestureType.PinchComplete))
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.PinchComplete, GestureDetector.GetGestureTimestamp(), Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, -1, -1));
			}
			GestureDetector.callBelatedPinchComplete = false;
			if (GestureDetector.state != GestureDetector.GestureState.JUST_TAPPED)
			{
				GestureDetector.state = GestureDetector.GestureState.NONE;
			}
			GestureDetector.eventTimestamp = DateTime.UtcNow;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00023674 File Offset: 0x00021874
		internal static void OnMoved(int fingerId, Vector2 touchPosition, Vector2 delta)
		{
			if (GestureDetector.state == GestureDetector.GestureState.PINCHING)
			{
				GestureDetector.OnMoved_Pinch(fingerId, touchPosition, delta);
				return;
			}
			if (GestureDetector.activeFingerId == -1)
			{
				GestureDetector.activeFingerId = fingerId;
			}
			if (fingerId != GestureDetector.activeFingerId)
			{
				return;
			}
			GestureDetector.activeFingerPosition = touchPosition;
			bool flag = GestureDetector.IsGestureEnabled(GestureType.HorizontalDrag);
			bool flag2 = GestureDetector.IsGestureEnabled(GestureType.VerticalDrag);
			bool flag3 = GestureDetector.IsGestureEnabled(GestureType.FreeDrag);
			if ((GestureDetector.state == GestureDetector.GestureState.HOLDING || GestureDetector.state == GestureDetector.GestureState.HELD) && (touchPosition - GestureDetector.pressPosition).Length() > 35f)
			{
				if (flag && Math.Abs(delta.X) > Math.Abs(delta.Y))
				{
					GestureDetector.state = GestureDetector.GestureState.DRAGGING_H;
				}
				else if (flag2 && Math.Abs(delta.Y) > Math.Abs(delta.X))
				{
					GestureDetector.state = GestureDetector.GestureState.DRAGGING_V;
				}
				else if (flag3)
				{
					GestureDetector.state = GestureDetector.GestureState.DRAGGING_FREE;
				}
				else
				{
					GestureDetector.state = GestureDetector.GestureState.NONE;
				}
			}
			if (GestureDetector.state == GestureDetector.GestureState.DRAGGING_H && flag)
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.HorizontalDrag, GestureDetector.GetGestureTimestamp(), touchPosition, Vector2.Zero, new Vector2(delta.X, 0f), Vector2.Zero, fingerId, -1));
			}
			else if (GestureDetector.state == GestureDetector.GestureState.DRAGGING_V && flag2)
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.VerticalDrag, GestureDetector.GetGestureTimestamp(), touchPosition, Vector2.Zero, new Vector2(0f, delta.Y), Vector2.Zero, fingerId, -1));
			}
			else if (GestureDetector.state == GestureDetector.GestureState.DRAGGING_FREE && flag3)
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.FreeDrag, GestureDetector.GetGestureTimestamp(), touchPosition, Vector2.Zero, delta, Vector2.Zero, fingerId, -1));
			}
			if ((GestureDetector.state == GestureDetector.GestureState.DRAGGING_H && !flag) || (GestureDetector.state == GestureDetector.GestureState.DRAGGING_V && !flag2) || (GestureDetector.state == GestureDetector.GestureState.DRAGGING_FREE && !flag3))
			{
				GestureDetector.state = GestureDetector.GestureState.HELD;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00023810 File Offset: 0x00021A10
		internal static void OnUpdate()
		{
			if (GestureDetector.state == GestureDetector.GestureState.PINCHING)
			{
				if (!GestureDetector.IsGestureEnabled(GestureType.Pinch))
				{
					GestureDetector.state = GestureDetector.GestureState.HELD;
					GestureDetector.secondFingerId = -1;
					GestureDetector.callBelatedPinchComplete = true;
				}
				return;
			}
			if (GestureDetector.activeFingerId == -1)
			{
				return;
			}
			if (GestureDetector.IsGestureEnabled(GestureType.Flick))
			{
				if (GestureDetector.updateTimestamp != DateTime.MinValue)
				{
					float num = (float)(DateTime.UtcNow - GestureDetector.updateTimestamp).TotalSeconds;
					Vector2 vector = (GestureDetector.activeFingerPosition - GestureDetector.lastUpdatePosition) / (0.001f + num);
					GestureDetector.velocity += (vector - GestureDetector.velocity) * 0.45f;
				}
				GestureDetector.lastUpdatePosition = GestureDetector.activeFingerPosition;
				GestureDetector.updateTimestamp = DateTime.UtcNow;
			}
			if (GestureDetector.IsGestureEnabled(GestureType.Hold) && GestureDetector.state == GestureDetector.GestureState.HOLDING && DateTime.UtcNow - GestureDetector.eventTimestamp >= TimeSpan.FromSeconds(1.0))
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.Hold, GestureDetector.GetGestureTimestamp(), GestureDetector.activeFingerPosition, Vector2.Zero, Vector2.Zero, Vector2.Zero, GestureDetector.activeFingerId, -1));
				GestureDetector.state = GestureDetector.GestureState.HELD;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00023938 File Offset: 0x00021B38
		private static TimeSpan GetGestureTimestamp()
		{
			return TimeSpan.FromTicks((long)Environment.TickCount);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00023945 File Offset: 0x00021B45
		private static bool IsGestureEnabled(GestureType gestureType)
		{
			return (TouchPanel.EnabledGestures & gestureType) > GestureType.None;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00023954 File Offset: 0x00021B54
		private static void OnReleased_Pinch(int fingerId, Vector2 touchPosition)
		{
			if (fingerId != GestureDetector.activeFingerId && fingerId != GestureDetector.secondFingerId)
			{
				return;
			}
			if (GestureDetector.IsGestureEnabled(GestureType.PinchComplete))
			{
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.PinchComplete, GestureDetector.GetGestureTimestamp(), Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, GestureDetector.activeFingerId, GestureDetector.secondFingerId));
			}
			if (fingerId == GestureDetector.activeFingerId)
			{
				GestureDetector.activeFingerId = GestureDetector.secondFingerId;
				GestureDetector.activeFingerPosition = GestureDetector.secondFingerPosition;
			}
			GestureDetector.secondFingerId = -1;
			bool flag = false;
			foreach (int num in GestureDetector.fingerIds)
			{
				if (num != GestureDetector.activeFingerId)
				{
					GestureDetector.secondFingerId = num;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				GestureDetector.state = GestureDetector.GestureState.HELD;
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00023A30 File Offset: 0x00021C30
		private static void OnMoved_Pinch(int fingerId, Vector2 touchPosition, Vector2 delta)
		{
			if (fingerId != GestureDetector.activeFingerId && fingerId != GestureDetector.secondFingerId)
			{
				return;
			}
			if (fingerId == GestureDetector.activeFingerId)
			{
				GestureDetector.activeFingerPosition = touchPosition;
				TouchPanel.EnqueueGesture(new GestureSample(GestureType.Pinch, GestureDetector.GetGestureTimestamp(), GestureDetector.activeFingerPosition, GestureDetector.secondFingerPosition, delta, Vector2.Zero, GestureDetector.activeFingerId, GestureDetector.secondFingerId));
				return;
			}
			GestureDetector.secondFingerPosition = touchPosition;
			TouchPanel.EnqueueGesture(new GestureSample(GestureType.Pinch, GestureDetector.GetGestureTimestamp(), GestureDetector.activeFingerPosition, GestureDetector.secondFingerPosition, Vector2.Zero, delta, GestureDetector.activeFingerId, GestureDetector.secondFingerId));
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00023AB9 File Offset: 0x00021CB9
		// Note: this type is marked as 'beforefieldinit'.
		static GestureDetector()
		{
		}

		// Token: 0x0400076F RID: 1903
		private static int activeFingerId = -1;

		// Token: 0x04000770 RID: 1904
		private static Vector2 activeFingerPosition;

		// Token: 0x04000771 RID: 1905
		private static bool callBelatedPinchComplete = false;

		// Token: 0x04000772 RID: 1906
		private static DateTime eventTimestamp;

		// Token: 0x04000773 RID: 1907
		private static List<int> fingerIds = new List<int>();

		// Token: 0x04000774 RID: 1908
		private static bool justDoubleTapped = false;

		// Token: 0x04000775 RID: 1909
		private static Vector2 lastUpdatePosition;

		// Token: 0x04000776 RID: 1910
		private static Vector2 pressPosition;

		// Token: 0x04000777 RID: 1911
		private static int secondFingerId = -1;

		// Token: 0x04000778 RID: 1912
		private static Vector2 secondFingerPosition;

		// Token: 0x04000779 RID: 1913
		private static GestureDetector.GestureState state = GestureDetector.GestureState.NONE;

		// Token: 0x0400077A RID: 1914
		private static DateTime updateTimestamp;

		// Token: 0x0400077B RID: 1915
		private static Vector2 velocity;

		// Token: 0x0400077C RID: 1916
		private const int MOVE_THRESHOLD = 35;

		// Token: 0x0400077D RID: 1917
		private const int MIN_FLICK_VELOCITY = 100;

		// Token: 0x020003A1 RID: 929
		private enum GestureState
		{
			// Token: 0x04001C42 RID: 7234
			NONE,
			// Token: 0x04001C43 RID: 7235
			HOLDING,
			// Token: 0x04001C44 RID: 7236
			HELD,
			// Token: 0x04001C45 RID: 7237
			JUST_TAPPED,
			// Token: 0x04001C46 RID: 7238
			DRAGGING_FREE,
			// Token: 0x04001C47 RID: 7239
			DRAGGING_H,
			// Token: 0x04001C48 RID: 7240
			DRAGGING_V,
			// Token: 0x04001C49 RID: 7241
			PINCHING
		}
	}
}
