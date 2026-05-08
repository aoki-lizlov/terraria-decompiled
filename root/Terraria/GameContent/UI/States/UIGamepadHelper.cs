using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AB RID: 939
	public struct UIGamepadHelper
	{
		// Token: 0x06002B5B RID: 11099 RVA: 0x0058CDF4 File Offset: 0x0058AFF4
		public UILinkPoint[,] CreateUILinkPointGrid(ref int currentID, List<SnapPoint> pointsForGrid, int pointsPerLine, UILinkPoint topLinkPoint, UILinkPoint leftLinkPoint, UILinkPoint rightLinkPoint, UILinkPoint bottomLinkPoint)
		{
			int num = (int)Math.Ceiling((double)((float)pointsForGrid.Count / (float)pointsPerLine));
			UILinkPoint[,] array = new UILinkPoint[pointsPerLine, num];
			for (int i = 0; i < pointsForGrid.Count; i++)
			{
				int num2 = i % pointsPerLine;
				int num3 = i / pointsPerLine;
				UILinkPoint[,] array2 = array;
				int num4 = num2;
				int num5 = num3;
				int num6 = currentID;
				currentID = num6 + 1;
				array2[num4, num5] = this.MakeLinkPointFromSnapPoint(num6, pointsForGrid[i]);
			}
			for (int j = 0; j < array.GetLength(0); j++)
			{
				for (int k = 0; k < array.GetLength(1); k++)
				{
					UILinkPoint uilinkPoint = array[j, k];
					if (uilinkPoint != null)
					{
						if (j < array.GetLength(0) - 1)
						{
							UILinkPoint uilinkPoint2 = array[j + 1, k];
							if (uilinkPoint2 != null)
							{
								this.PairLeftRight(uilinkPoint, uilinkPoint2);
							}
						}
						if (k < array.GetLength(1) - 1)
						{
							UILinkPoint uilinkPoint3 = array[j, k + 1];
							if (uilinkPoint3 != null)
							{
								this.PairUpDown(uilinkPoint, uilinkPoint3);
							}
						}
						if (leftLinkPoint != null && j == 0)
						{
							uilinkPoint.Left = leftLinkPoint.ID;
						}
						if (topLinkPoint != null && k == 0)
						{
							uilinkPoint.Up = topLinkPoint.ID;
						}
						if (rightLinkPoint != null && j == pointsPerLine - 1)
						{
							uilinkPoint.Right = rightLinkPoint.ID;
						}
						if (bottomLinkPoint != null && k == num - 1)
						{
							uilinkPoint.Down = bottomLinkPoint.ID;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0058CF54 File Offset: 0x0058B154
		public void LinkVerticalStrips(UILinkPoint[] stripOnLeft, UILinkPoint[] stripOnRight, int leftStripStartOffset)
		{
			if (stripOnLeft == null || stripOnRight == null)
			{
				return;
			}
			int num = Math.Max(stripOnLeft.Length, stripOnRight.Length);
			int num2 = Math.Min(stripOnLeft.Length, stripOnRight.Length);
			for (int i = 0; i < leftStripStartOffset; i++)
			{
				this.PairLeftRight(stripOnLeft[i], stripOnRight[0]);
			}
			for (int j = 0; j < num2; j++)
			{
				this.PairLeftRight(stripOnLeft[j + leftStripStartOffset], stripOnRight[j]);
			}
			for (int k = num2; k < num; k++)
			{
				if (stripOnLeft.Length > k)
				{
					stripOnLeft[k].Right = stripOnRight[stripOnRight.Length - 1].ID;
				}
				if (stripOnRight.Length > k)
				{
					stripOnRight[k].Left = stripOnLeft[stripOnLeft.Length - 1].ID;
				}
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0058CFFC File Offset: 0x0058B1FC
		public void LinkVerticalStripRightSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			int num = Math.Max(strip.Length, 1);
			int num2 = Math.Min(strip.Length, 1);
			for (int i = 0; i < num2; i++)
			{
				this.PairLeftRight(strip[i], theSingle);
			}
			for (int j = num2; j < num; j++)
			{
				if (strip.Length > j)
				{
					strip[j].Right = theSingle.ID;
				}
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0058D05C File Offset: 0x0058B25C
		public void RemovePointsOutOfView(List<SnapPoint> pts, UIElement containerPanel, SpriteBatch spriteBatch)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 vector = clippingRectangle.TopLeft() * num;
			Vector2 vector2 = clippingRectangle.BottomRight() * num;
			for (int i = 0; i < pts.Count; i++)
			{
				if (!pts[i].Position.Between(vector, vector2))
				{
					pts.Remove(pts[i]);
					i--;
				}
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0058D0D0 File Offset: 0x0058B2D0
		public void LinkHorizontalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			for (int i = strip.Length - 1; i >= 0; i--)
			{
				this.PairUpDown(strip[i], theSingle);
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0058D100 File Offset: 0x0058B300
		public void LinkHorizontalStripUpSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			for (int i = strip.Length - 1; i >= 0; i--)
			{
				this.PairUpDown(theSingle, strip[i]);
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0058D12E File Offset: 0x0058B32E
		public void LinkVerticalStripBottomSideToSingle(UILinkPoint[] strip, UILinkPoint theSingle)
		{
			if (strip == null || theSingle == null)
			{
				return;
			}
			this.PairUpDown(strip[strip.Length - 1], theSingle);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0058D148 File Offset: 0x0058B348
		public UILinkPoint[] CreateUILinkStripVertical(ref int currentID, List<SnapPoint> currentStrip)
		{
			UILinkPoint[] array = new UILinkPoint[currentStrip.Count];
			for (int i = 0; i < currentStrip.Count; i++)
			{
				UILinkPoint[] array2 = array;
				int num = i;
				int num2 = currentID;
				currentID = num2 + 1;
				array2[num] = this.MakeLinkPointFromSnapPoint(num2, currentStrip[i]);
			}
			for (int j = 0; j < currentStrip.Count - 1; j++)
			{
				this.PairUpDown(array[j], array[j + 1]);
			}
			return array;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0058D1B0 File Offset: 0x0058B3B0
		public UILinkPoint[] CreateUILinkStripHorizontal(ref int currentID, List<SnapPoint> currentStrip)
		{
			UILinkPoint[] array = new UILinkPoint[currentStrip.Count];
			for (int i = 0; i < currentStrip.Count; i++)
			{
				UILinkPoint[] array2 = array;
				int num = i;
				int num2 = currentID;
				currentID = num2 + 1;
				array2[num] = this.MakeLinkPointFromSnapPoint(num2, currentStrip[i]);
			}
			for (int j = 0; j < currentStrip.Count - 1; j++)
			{
				this.PairLeftRight(array[j], array[j + 1]);
			}
			return array;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0058D218 File Offset: 0x0058B418
		public void TryMovingBackIntoCreativeGridIfOutOfIt(int start, int currentID)
		{
			List<UILinkPoint> list = new List<UILinkPoint>();
			for (int i = start; i < currentID; i++)
			{
				list.Add(UILinkPointNavigator.Points[i]);
			}
			if (PlayerInput.UsingGamepadUI && UILinkPointNavigator.CurrentPoint >= currentID)
			{
				this.MoveToVisuallyClosestPoint(list);
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0058D260 File Offset: 0x0058B460
		public void MoveToVisuallyClosestPoint(List<UILinkPoint> lostrefpoints)
		{
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			foreach (UILinkPoint uilinkPoint2 in lostrefpoints)
			{
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0058D2E4 File Offset: 0x0058B4E4
		public List<SnapPoint> GetOrderedPointsByCategoryName(List<SnapPoint> pts, string name)
		{
			return (from x in pts
				where x.Name == name
				orderby x.Id
				select x).ToList<SnapPoint>();
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0058D339 File Offset: 0x0058B539
		public void PairLeftRight(UILinkPoint leftSide, UILinkPoint rightSide)
		{
			if (leftSide != null)
			{
				leftSide.Right = ((rightSide == null) ? (-1) : rightSide.ID);
			}
			if (rightSide != null)
			{
				rightSide.Left = ((leftSide == null) ? (-1) : leftSide.ID);
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0058D365 File Offset: 0x0058B565
		public void PairUpDown(UILinkPoint upSide, UILinkPoint downSide)
		{
			if (upSide != null)
			{
				upSide.Down = ((downSide == null) ? (-1) : downSide.ID);
			}
			if (downSide != null)
			{
				downSide.Up = ((upSide == null) ? (-1) : upSide.ID);
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0058BE69 File Offset: 0x0058A069
		public UILinkPoint MakeLinkPointFromSnapPoint(int id, SnapPoint snap)
		{
			UILinkPointNavigator.SetPosition(id, snap.Position);
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[id];
			uilinkPoint.Unlink();
			return uilinkPoint;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0058D394 File Offset: 0x0058B594
		public UILinkPoint GetLinkPoint(int id, UIElement element)
		{
			SnapPoint snapPoint;
			if (element.GetSnapPoint(out snapPoint))
			{
				return this.MakeLinkPointFromSnapPoint(id, snapPoint);
			}
			return null;
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0058D3B8 File Offset: 0x0058B5B8
		public UILinkPoint TryMakeLinkPoint(ref int id, SnapPoint snap)
		{
			if (snap == null)
			{
				return null;
			}
			int num = id;
			id = num + 1;
			return this.MakeLinkPointFromSnapPoint(num, snap);
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0058D3DC File Offset: 0x0058B5DC
		public UILinkPoint[] GetVerticalStripFromCategoryName(ref int currentID, List<SnapPoint> pts, string categoryName)
		{
			List<SnapPoint> orderedPointsByCategoryName = this.GetOrderedPointsByCategoryName(pts, categoryName);
			UILinkPoint[] array = null;
			if (orderedPointsByCategoryName.Count > 0)
			{
				array = this.CreateUILinkStripVertical(ref currentID, orderedPointsByCategoryName);
			}
			return array;
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0058D408 File Offset: 0x0058B608
		public void MoveToVisuallyClosestPoint(int idRangeStartInclusive, int idRangeEndExclusive)
		{
			if (UILinkPointNavigator.CurrentPoint >= idRangeStartInclusive && UILinkPointNavigator.CurrentPoint < idRangeEndExclusive)
			{
				return;
			}
			Dictionary<int, UILinkPoint> points = UILinkPointNavigator.Points;
			Vector2 mouseScreen = Main.MouseScreen;
			UILinkPoint uilinkPoint = null;
			for (int i = idRangeStartInclusive; i < idRangeEndExclusive; i++)
			{
				UILinkPoint uilinkPoint2;
				if (!points.TryGetValue(i, out uilinkPoint2))
				{
					return;
				}
				if (uilinkPoint == null || Vector2.Distance(mouseScreen, uilinkPoint.Position) > Vector2.Distance(mouseScreen, uilinkPoint2.Position))
				{
					uilinkPoint = uilinkPoint2;
				}
			}
			if (uilinkPoint != null)
			{
				UILinkPointNavigator.ChangePoint(uilinkPoint.ID);
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0058D47C File Offset: 0x0058B67C
		public void CullPointsOutOfElementArea(SpriteBatch spriteBatch, List<SnapPoint> pointsAtMiddle, UIElement container)
		{
			float num = 1f / Main.UIScale;
			Rectangle clippingRectangle = container.GetClippingRectangle(spriteBatch);
			Vector2 vector = clippingRectangle.TopLeft() * num;
			Vector2 vector2 = clippingRectangle.BottomRight() * num;
			for (int i = 0; i < pointsAtMiddle.Count; i++)
			{
				if (!pointsAtMiddle[i].Position.Between(vector, vector2))
				{
					pointsAtMiddle.Remove(pointsAtMiddle[i]);
					i--;
				}
			}
		}

		// Token: 0x02000904 RID: 2308
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x0600474D RID: 18253 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x0600474E RID: 18254 RVA: 0x006CB498 File Offset: 0x006C9698
			internal bool <GetOrderedPointsByCategoryName>b__0(SnapPoint x)
			{
				return x.Name == this.name;
			}

			// Token: 0x04007431 RID: 29745
			public string name;
		}

		// Token: 0x02000905 RID: 2309
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600474F RID: 18255 RVA: 0x006CB4AB File Offset: 0x006C96AB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004750 RID: 18256 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004751 RID: 18257 RVA: 0x006CB45E File Offset: 0x006C965E
			internal int <GetOrderedPointsByCategoryName>b__11_1(SnapPoint x)
			{
				return x.Id;
			}

			// Token: 0x04007432 RID: 29746
			public static readonly UIGamepadHelper.<>c <>9 = new UIGamepadHelper.<>c();

			// Token: 0x04007433 RID: 29747
			public static Func<SnapPoint, int> <>9__11_1;
		}
	}
}
