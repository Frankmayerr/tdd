using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
	[TestFixture]
	public class RectangleExtensionsTests
	{
		[Test]
		public void RectanglePoints_CorrectResult()
		{
			var rect = new Rectangle(new Point(0, 0), new Size(2, 5));
			rect.MakePoints()
				.ShouldAllBeEquivalentTo(new[] {new Point(0, 0), new Point(2, 0), new Point(0, 5), new Point(2, 5)});
		}
	}
}