using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class CircularCloudLayouterTests
	{
		private CircularCloudLayouter layouter;

		[SetUp]
		public void SetUp()
		{
			layouter = new CircularCloudLayouter(new Point(0, 0));
		}

		[Test, Explicit]
		public void DifferentRectangle_ShowTagCloud()
		{
			layouter.PutNextRectangle(new Size(500, 100));
			layouter.PutNextRectangle(new Size(100, 500));
			layouter.PutNextRectangle(new Size(300, 300));
			layouter.PutNextRectangle(new Size(200, 600));
			layouter.PutNextRectangle(new Size(500, 100));
			layouter.PutNextRectangle(new Size(50, 300));
			for (int i = 0; i < 5; i++)
				layouter.PutNextRectangle(new Size(150, 150));
			for (int i = 0; i < layouter.prevRects.Count; ++i)
			for (int j = i + 1; j < layouter.prevRects.Count; ++j)
				layouter.prevRects[i].IntersectsWith(layouter.prevRects[j]).Should().BeFalse();

			var picture = TagCloudPainter.TagCloudPainting(layouter.Center, layouter.prevRects);
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var file = "DifferentRectangles.png";
			var path = Path.Combine(dir, file);
			picture.Save(path);
		}

		[Test, Explicit]
		public void TallRectangles_ShowTagCloud()
		{
			for (int i = 0; i < 8; i++)
				layouter.PutNextRectangle(new Size(50, 400));
			for (int i = 0; i < 8; i++)
				layouter.PutNextRectangle(new Size(400, 50));
			var picture = TagCloudPainter.TagCloudPainting(layouter.Center, layouter.prevRects);
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var file = "TallRectangles.png";
			var path = Path.Combine(dir, file);
			picture.Save(path);
		}

		[Test, Explicit]
		public void WordRectangles_ShowTagCloud()
		{
			var rand = new Random();
			for (int i = 0; i < 30; i++)
			{
				var size = new Size(rand.Next(250, 400), rand.Next(100, 200));
				layouter.PutNextRectangle(size);
			}
			var picture = TagCloudPainter.TagCloudPainting(layouter.Center, layouter.prevRects);
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var file = "WordRectangles.png";
			var path = Path.Combine(dir, file);
			picture.Save(path);
		}

		[Test, Explicit]
		public void ThirteenSameSquares_ShowTagCloud()
		{
			for (int i = 0; i < 13; i++)
				layouter.PutNextRectangle(new Size(50, 50));
			var picture = TagCloudPainter.TagCloudPainting(layouter.Center, layouter.prevRects);
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			var file = "ThirteenSameSquares.png";
			var path = Path.Combine(dir, file);
			picture.Save(path);
		}


		[Test]
		public void OneRectangle_CorrectSize()
		{
			var size = new Size(300, 200);
			var rect = layouter.PutNextRectangle(size);
			rect.Size.Should().Be(size);
		}

		[Test]
		public void Rectangles_NotIntersected()
		{
			layouter.PutNextRectangle(new Size(200, 200));
			layouter.PutNextRectangle(new Size(300, 110));
			layouter.PutNextRectangle(new Size(70, 600));
			layouter.PutNextRectangle(new Size(300, 300));
			layouter.PutNextRectangle(new Size(200, 600));
			layouter.PutNextRectangle(new Size(500, 100));
			for (int i = 0; i < layouter.prevRects.Count; ++i)
			for (int j = i + 1; j < layouter.prevRects.Count; ++j)
				layouter.prevRects[i].IntersectsWith(layouter.prevRects[j]).Should().BeFalse();
		}

		[Test]
		public void ManySquares_BestLocation()
		{
			var side = 40;
			for (int i = 0; i < 31; i++)
				layouter.PutNextRectangle(new Size(side, side));
			var points = layouter.prevRects.SelectMany(rect => rect.GetRectangleTops());
			var maxX = points.Select(p => Math.Abs(p.X)).Max();
			var maxY = points.Select(p => Math.Abs(p.Y)).Max();
			var maxRadius = 5 * side;
			maxX.Should().BeLessOrEqualTo(maxRadius);
			maxY.Should().BeLessOrEqualTo(maxRadius);
		}
	}
}