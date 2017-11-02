using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class PointExtensionsTests
	{
		[Test]
		public void RectanglesAroundPoint_CorrectResult()
		{
			var p = new Point(1, 1);
			var s = new Size(2, 4);
			var excpectedResult = new List<Rectangle>()
			{
				new Rectangle(p, s),
				new Rectangle(new Point(-1, 5), s),
				new Rectangle(new Point(1, 5), s),
				new Rectangle(new Point(-1, 1), s)
			};
			var result = p.MakeRectanglesAroundPoint(s);
			result.ShouldAllBeEquivalentTo(excpectedResult);
		}
	}
}