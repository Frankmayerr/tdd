using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class PointTests
	{
		[Test]
		public void MakePoint_CorrecItnitialization()
		{
			var point = new Point(1, 2);
			point.X.Should().Be(1);
			point.Y.Should().Be(2);
		}

		[TestCase(1, 1, 1, 1, 0)]
		[TestCase(1, 0, 2, 0, 1)]
		[TestCase(0, 3, 4, 0, 25)]
		[TestCase(0, 6, 8, 0, 100)]
		[TestCase(3, 5, -1, -2, 65)]
		[TestCase(6, 15, -10, 31, 512)]
		public void PointDistance(int x1, int y1, int x2, int y2, double expectedAns)
		{
			var a = new Point(x1, y1);
			var b = new Point(x2, y2);
			var distance = a.Distance(b);
			Assert.AreEqual(distance, Math.Sqrt(expectedAns), 1e-4);
		}

		[Test]
		public void PointMinusPlusPoint()
		{
			var a = new Point(0, 0);
			var b = new Point(3, 1);

			(a - b).Should().Be(new Point(-3, -1));
		}

		[Test]
		public void PointPlusMinusSize()
		{
			var a = new Point(0, 0);
			var s = new Size(2, 5);

			(a - s).Should().Be(new Point(-2, -5));
			(a + s).Should().Be(new Point(2, 5));
		}

		[Test]
		public void RectanglesAroundPoint_CorrectResult()
		{
			var p = new Point(1, 1);
			var s = new Size(2, 4);
			var excpectedResult = new List<Rectangle>()
			{
				new Rectangle(p, s),
				new Rectangle(new Point(-1, -3), s),
				new Rectangle(new Point(1, -3), s),
				new Rectangle(new Point(-1, 1), s)
			};
			var result = p.MakeRectanglesAroundPoint(s);
			result.ShouldAllBeEquivalentTo(excpectedResult);
		}
	}

	[TestFixture]
	public class SizeTests
	{
		[Test]
		public void MakeSize_CorrecItnitialization()
		{
			var size = new Size(1, 2);
			size.Width.Should().Be(1);
			size.Height.Should().Be(2);
		}

		[Test]
		public void NegativeOrZeroSize_MakesArgumentException()
		{
			Assert.Throws<ArgumentException>(() => new Size(-1, 1));
			Assert.Throws<ArgumentException>(() => new Size(1, 0));
			Assert.Throws<ArgumentException>(() => new Size(0, 0));
		}

		[TestCase(3, 2, 4, 1, 1, TestName = "Int part < 1 => return 1")]
		[TestCase(2, 3, 1, 2, 3, TestName = "Division on 1")]
		[TestCase(6, 2, 2, 3, 1, TestName = "Int division")]
		[TestCase(7, 11, 3, 2, 3, TestName = "Division with rest")]
		public void SizeDivision(int w, int h, int divider, int newW, int newH)
		{
			var size = new Size(w, h);
			var expectedSize = new Size(newW, newH);

			var dividedSize = size / divider;

			dividedSize.Should().Be(expectedSize);
		}
	}

	[TestFixture]
	public class RectangleTests
	{
		[Test]
		public void NewRectangle_CorrectFields()
		{
			var rect = new Rectangle(new Point(2, 3), new Size(5, 1));
			rect.LeftDown.Should().Be(new Point(2, 3));
			rect.Size.Should().Be(new Size(5, 1));
		}

		[TestCase(1, -5, 1, 5, -3, -1, 5, 1, true, TestName = "common corner and intersected")]
		[TestCase(0, 0, 3, 1, 1, -1, 1, 2, true, TestName = "one more")]
		[TestCase(0, 0, 10, 10, 5, 5, 12, 12, true, TestName = "are intersected")]
		[TestCase(0, 0, 1, 2, 2, 0, 1, 1, false, TestName = "on same y but not intersected")]
		[TestCase(0, 0, 1, 1, 0, 2, 1, 2, false, TestName = "on same x but not intersected")]
		[TestCase(0, 0, 1, 2, 0, 0, 1, 2, true, TestName = "equal rectangles are intersected")]
		[TestCase(0, 0, 3, 1, 1, 0, 1, 2, true, TestName = "y3, y4 in y1, y2 (and not x) => intersected")]
		[TestCase(0, 0, 1, 3, 0, 1, 2, 1, true, TestName = "x3, x4 in x1, x2 (and not y) => intersected")]
		[TestCase(0, 0, 1, 1, 0, 1, 1, 1, false, TestName = "intersected by segment but we assume that not")]
		[TestCase(0, 0, 1, 1, 1, 1, 2, 10, false, TestName = "intersected by point segment but we assume that not")]
		public void Rectangles_Intersection(int x1, int y1, int w1, int h1, int x2, int y2, int w2, int h2, bool intersected)
		{
			var a = new Rectangle(new Point(x1, y1), new Size(w1, h1));
			var b = new Rectangle(new Point(x2, y2), new Size(w2, h2));
			a.Intersection(b).Should().Be(intersected);
		}

		[Test]
		public void RectanglePoints_CorrectResult()
		{
			var rect = new Rectangle(new Point(0, 0), new Size(2, 5));
			rect.MakePoints()
				.ShouldAllBeEquivalentTo(new[] {new Point(0, 0), new Point(2, 0), new Point(0, 5), new Point(2, 5)});
		}
	}
}