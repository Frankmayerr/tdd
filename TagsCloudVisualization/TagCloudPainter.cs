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
			var size = GetSizeFromRectangles(rectangles);
			var picture = new Bitmap(size.Width, size.Height);
			var graphics = Graphics.FromImage(picture);
			var brush = new SolidBrush(Color.FloralWhite);
			var frame = new Rectangle(0, 0, size.Width, size.Height);
			graphics.FillRectangle(brush, frame);
			var rand = new Random();
			var shift = new Point(rectangles.SelectMany(rect => rect.MakePoints()).Select(p=>p.X).Min(), 
				rectangles.SelectMany(rect => rect.MakePoints()).Select(p => p.Y).Min());
			foreach (var rect in rectangles)
			{
				var centerRect = new Rectangle(new Point(rect.X - shift.X, rect.Top - shift.Y), rect.Size);
				graphics.FillRectangle(new SolidBrush(GetRandomColor(rand)), centerRect);
			}
			return picture;
		}


		private static Size GetSizeFromRectangles(IEnumerable<Rectangle> rectangles)
		{
			var points = rectangles.SelectMany(rect => rect.MakePoints());
			var size = new Size();
			var xx = points.Select(p => p.X);
			size.Width = xx.Max() - Math.Min(0, xx.Min());
			var yy = points.Select(p => p.Y);
			size.Height = yy.Max() - Math.Min(0, yy.Min());
			return size;
		}

		private static Color GetRandomColor(Random rand)
		{
			return Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
		}
	}
}