using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace TagsCloudVisualization
{
	public static class PointExtensions
	{
		public static List<Rectangle> GetRectangles_PointIsEachTop(this Point point, Size sz)
		{
			var rects = new List<Rectangle>();
			rects.Add(new Rectangle(point, sz));
			rects.Add(new Rectangle(new Point(point.X - sz.Width, point.Y - sz.Height), sz));
			rects.Add(new Rectangle(new Point(point.X, point.Y - sz.Height), sz));
			rects.Add(new Rectangle(new Point(point.X - sz.Width, point.Y), sz));
			return rects;
		}

		public static double GetDistance(this Point p, Point point)
		{
			var xx = point.X - p.X;
			var yy = point.Y - p.Y;
			return Math.Sqrt(xx * xx + yy * yy);
		}
	}
}