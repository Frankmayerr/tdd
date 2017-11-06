using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace TagsCloudVisualization
{
	public class CircularCloudLayouter
	{
		public readonly Point Center;
		public List<Rectangle> prevRects = new List<Rectangle>();

		public CircularCloudLayouter(Point center)
		{
			Center = center;
		}

		public Rectangle PutNextRectangle(Size rectangleSize)
		{
			if (prevRects.Count == 0)
			{
				prevRects.Add(new Rectangle(new Point(Center.X - rectangleSize.Width / 2, Center.Y - rectangleSize.Height / 2 ), rectangleSize));
				return prevRects[0];
			}
			var nextRect = prevRects
				.SelectMany(rect => rect.GetRectangleVertexes())
				.SelectMany(point => point.GetRectanglesAroundPoint(rectangleSize))
				.Distinct()
				.Where(CanAdd)
				.OrderBy(rect => rect.Center().GetDistance(Center))
				.FirstOrDefault();

			prevRects.Add(nextRect);
			return nextRect;

		}

		private bool CanAdd(Rectangle rect)
		{
			return prevRects.All(r => !rect.IntersectsWith(r));
		}
	}


}