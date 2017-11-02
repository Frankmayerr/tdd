using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
	public static class TagCloudPainter
	{
		public static Bitmap TagCloudPainting(Point center, List<Rectangle> rectangles)
		{
			var shift = GetShiftFromRectangles(rectangles);
			rectangles = rectangles.Select(rect => new Rectangle(new Point(rect.X + shift.X, rect.Top + shift.Y), rect.Size)).ToList();
			var size = GetSizeFromRectangles(rectangles);
			var picture = new Bitmap(size.Width, size.Height);
			var graphics = Graphics.FromImage(picture);
			var brush = new SolidBrush(Color.FloralWhite);
			var frame = new Rectangle(0, 0, size.Width, size.Height);
			graphics.FillRectangle(brush, frame);
			var rand = new Random();
			foreach (var rect in rectangles)
			{
				graphics.FillRectangle(new SolidBrush(GetRandomColor(rand)), rect);
			}
			return picture;
		}


		private static Size GetSizeFromRectangles(IEnumerable<Rectangle> rectangles)
		{
			var points = rectangles.SelectMany(rect => rect.MakePoints());
			var size = new Size
			{
				Width = points.Select(p => p.X).Max(),
				Height = points.Select(p => p.Y).Max()
			};
			return size;
		}

		private static Point GetShiftFromRectangles(IEnumerable<Rectangle> rectangles)
		{
			var points = rectangles.SelectMany(rect => rect.MakePoints());
			var shift = new Point
			{
				X = points.Select(p => p.X).Min(),
				Y = points.Select(p => p.Y).Max()
			};
			return shift;
		}

		private static Color GetRandomColor(Random rand)
		{
			return Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
		}
	}
}