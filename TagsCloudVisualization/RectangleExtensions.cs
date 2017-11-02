using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TagsCloudVisualization
{
	public static class RectangleExtensions
	{
		public static List<Point> MakePoints(this Rectangle rect)
		{
			var pts = new List<Point>();
			pts.Add(new Point(rect.Left, rect.Bottom));
			pts.Add(new Point(rect.Right, rect.Top));
			pts.Add(new Point(rect.Left, rect.Top));
			pts.Add(new Point(rect.Right, rect.Bottom));
			return pts;
		}

		public static Point Center(this Rectangle rect) =>
			new Point(rect.Left + rect.Width / 2, rect.Bottom + rect.Height / 2);
	}
}