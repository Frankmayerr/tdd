using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingGame
{
	public class Frame
	{
		public int firstRoll = -1;
		public int secondRoll = -1;

		public Frame(int a = -1)
		{
			firstRoll = a;
		}

		public bool Add(int roll)
		{
			if (firstRoll == -1)
				firstRoll = roll;
			else if (secondRoll == -1)
				if (firstRoll == 10)
					return false;
				else
					secondRoll = roll;
			else
				return false;
			return true;
		}

		public bool isSpair()
		{
			return firstRoll + secondRoll == 10;
		}

		public bool isStrike()
		{
			return firstRoll == 10;
		}
	}

	public class Game
	{
		public int Score { get; set; }
		public int RollsCount { get; set; }
		public List<Frame> Frames = new List<Frame>();

		public void Roll(int pins)
		{
			if (RollsCount >= 20)
				throw new ArgumentException();
			if (pins > 10 || pins < 0)
				throw new ArgumentException();

			if (Frames.Count == 0 || !Frames.Last().Add(pins))
				Frames.Add(new Frame(pins));

			if (Frames.Count > 10) throw new ArgumentException();
		}

		public int GetScore()
		{
			int curScore = 0;
			int size = Frames.Count;
			for (int i = size - 1; i >= 0; i--)
			{
				if (Frames[i].firstRoll > 0)
					curScore += Frames[i].firstRoll;
				if (Frames[i].secondRoll > 0)
					curScore += Frames[i].secondRoll;

				if (Frames[i].isSpair() && i + 1 < size && Frames[i + 1].firstRoll > 0)
					curScore += Frames[i + 1].firstRoll;
				if (Frames[i].isStrike() && i + 1 < size)
				{
					if (Frames[i + 1].firstRoll > 0)
						curScore += Frames[i + 1].firstRoll;
					if (Frames[i + 1].secondRoll > 0)
						curScore += Frames[i + 1].secondRoll;
					else if (i + 2 < size && Frames[i + 2].firstRoll > 0)
						curScore += Frames[i + 2].firstRoll;
				}
			}
			return curScore;
		}
	}


	[TestFixture]
	public class Game_should : ReportingTest<Game_should>
	{
		// ReSharper disable once UnusedMember.Global
		public static string Names = "1 Гладких Волков";

		[Test]
		public void ThrowException_WhetNegativPin()
		{
			Assert.Throws<ArgumentException>(() => new Game().Roll(-1));
		}

		[TestCase(new[] {10, 10, 10}, ExpectedResult = 60, TestName = "three strikes")]
		[TestCase(new[] {3, 2, 3}, ExpectedResult = 8, TestName = "three small rolls")]
		[TestCase(new[] {10, 2, 3, 5, 5, 6}, ExpectedResult = 42, TestName = "one strike one spair and other rolls")]
		[TestCase(new[] {10}, ExpectedResult = 10, TestName = "one strike")]
		public int CorrectScore_After(int[] rolls)
		{
			var g = new Game();
			foreach (var e in rolls)
				g.Roll(e);
			return g.GetScore();
		}


		[Test]
		public void HaveException_When11Pins()
		{
			Game g = new Game();
			Assert.Throws<ArgumentException>(() => g.Roll(11));
		}

		[Test]
		public void After20Rolls_Without_Strikes()
		{
			Game g = new Game();
			for (int i = 0; i < 20; i++)
				g.Roll(2);
			Assert.Throws<ArgumentException>(() => g.Roll(1));
		}

		[Test]
		public void HaveZeroScore_BeforeAnyRolls()
		{
			new Game()
				.GetScore()
				.Should().Be(0);
		}
	}
}