using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
	public class Size
	{
		public readonly int Width;
		public readonly int Height;

		public int Area => Width * Height;

		public Size(int width, int height)
		{
			if (width <= 0 || height <= 0)
				throw new ArgumentException();
			Width = width;
			Height = height;
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(Size)) return false;
			var other = (Size)obj;
			return Width == other.Width && Height == other.Height;
		}

		public override int GetHashCode()
		{
			return (Width * 397) ^ Height;
		}

		public static Size operator /(Size a, int d)
		{
			return new Size(Math.Max(a.Width / d, 1), Math.Max(a.Height / d, 1));
		}
	}
}
