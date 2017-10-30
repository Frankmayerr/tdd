using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
	public class Point
	{
		public readonly int X, Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public double Distance(Point point)
		{
			var xx = point.X - X;
			var yy = point.Y - Y;
			return Math.Sqrt(xx * xx + yy * yy);
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Point)) return false;
			var other = (Point)obj;
			return X == other.X && Y == other.Y;
		}

		public override int GetHashCode()
		{
			return (X * 397) ^ Y;
		}

		public static Point operator -(Point a, Point b)
		{
			return new Point(a.X - b.X, a.Y - b.Y);
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y);
		}


		public static Point operator +(Point a, Size b)
		{
			return new Point(a.X + b.Width, a.Y + b.Height);
		}

		public static Point operator -(Point a, Size b)
		{
			return new Point(a.X - b.Width, a.Y - b.Height);
		}

		public List<Rectangle> MakeRectanglesAroundPoint(Size sz)
		{
			var rects = new List<Rectangle>();
			rects.Add(new Rectangle(this, sz));
			rects.Add(new Rectangle(this - sz, sz));
			rects.Add(new Rectangle(new Point(X, Y - sz.Height), sz));
			rects.Add(new Rectangle(new Point(X - sz.Width, Y), sz));
			return rects;
		}
	}
}
