using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
				prevRects.Add(new Rectangle(Center - rectangleSize / 2, rectangleSize));
				return prevRects.Last();
			}
			var nextRect = prevRects
				.SelectMany(rect => rect.MakePoints())
				.SelectMany(point => point.MakeRectanglesAroundPoint(rectangleSize))
				.Distinct()
				.Where(rect => CanAdd(rect))
				.OrderBy(rect => rect.Center.Distance(Center))
				.FirstOrDefault();

			//var a = prevRects.SelectMany(rect => rect.MakePoints()).ToList();
			//var b = a.SelectMany(point => point.MakeRectanglesAroundPoint(rectangleSize)).Distinct().ToList();
			//var c = b.Where(rect => CanAdd(rect)).ToList();
			//var d = c.OrderBy(rect => rect.Center.Distance(Center)).ToList();
			//var nextRect = d.FirstOrDefault();
			if (nextRect != null)
				prevRects.Add(nextRect);
			return nextRect;

		}

		private bool CanAdd(Rectangle rect)
		{
			foreach(var r in prevRects)
				if (rect.Intersection(r))
					return false;
			return true;
		}
	}


}