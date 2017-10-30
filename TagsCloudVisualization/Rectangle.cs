using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework.Constraints;

namespace TagsCloudVisualization
{
	public class Rectangle
	{
		public readonly Point LeftDown;
		public readonly Size Size;
		public readonly Point RightUp;
		public readonly Point Center;


		public Rectangle(Point leftDown, Size size)
		{
			LeftDown = leftDown;
			Size = size;
			RightUp = new Point(leftDown.X + size.Width, leftDown.Y + size.Height);
			Center = LeftDown + size / 2;
		}

		public bool Intersection(Rectangle rect)
		{
			return Math.Max(LeftDown.X, rect.LeftDown.X) < Math.Min(RightUp.X, rect.RightUp.X) &&
			       Math.Max(LeftDown.Y, rect.LeftDown.Y) < Math.Min(RightUp.Y, rect.RightUp.Y);
		}

		public List<Point> MakePoints()
		{
			var pts = new List<Point>();
			pts.Add(LeftDown);
			pts.Add(RightUp);
			pts.Add(new Point(LeftDown.X, RightUp.Y));
			pts.Add(new Point(RightUp.X, LeftDown.Y));
			return pts;
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Rectangle)) return false;
			var other = (Rectangle) obj;
			var a = LeftDown.Equals(other.LeftDown) && Size.Equals(other.Size);
			return a;
		}

		public override int GetHashCode()
		{
			return (LeftDown.GetHashCode() * 397) ^ Size.GetHashCode();
		}
	}
}