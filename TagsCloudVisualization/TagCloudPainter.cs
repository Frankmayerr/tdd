using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	public static class TagCloudPainter
	{
		public static Bitmap Painter(Point center, List<Rectangle> rectangles)
		{
			var size = GetSizeFromRectangles(rectangles, center);
			var width = size.Item1;
			var height = size.Item2;
			var picture = new Bitmap(width, height);
			var graphics = Graphics.FromImage(picture);
			var brush = new SolidBrush(Color.FloralWhite);
			var frame = new System.Drawing.Rectangle(0, 0, width, height);
			graphics.FillRectangle(brush, frame);
			var rand = new Random();
			var shift = new Point(width / 2, height / 2) - center;
			foreach (var rect in rectangles)
			{
				var graphRect = new System.Drawing.Rectangle(rect.LeftDown.X + shift.X, rect.LeftDown.Y + shift.Y, rect.Size.Width, rect.Size.Height);
				graphics.FillRectangle(new SolidBrush(GetRandomColor(rand)), graphRect);
			}
			return picture;
		}


		private static Tuple<int, int> GetSizeFromRectangles(List<Rectangle> rectangles, Point center)
		{
			var points = rectangles.SelectMany(rect => rect.MakePoints());
			var xx = points.Select(p => p.X);
			int width = xx.Max() - Math.Min(0,xx.Min());
			var yy = points.Select(p => p.Y);
			int height = yy.Max() - Math.Min(0, yy.Min());
			return Tuple.Create(width, height);
		}

		private static Color GetRandomColor(Random rand)
		{
			return Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
		}
	}
}