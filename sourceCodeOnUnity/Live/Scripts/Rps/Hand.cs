using System.Collections.Generic;

public enum Hand
{
	Rock, Paper, Scissor
}

public static class HandExtensions
{
	public static RpsResult GetResult(this Hand mine, Hand opponent)
	{
		Dictionary<Hand, Hand> toCanBeats = new()
		{
			{Hand.Rock,Hand.Scissor},
			{Hand.Paper,Hand.Rock},
			{Hand.Scissor,Hand.Paper}
		};

		if (mine == opponent) return RpsResult.Draw;
		if (toCanBeats[mine] == opponent) return RpsResult.Win;
		return RpsResult.Lose;
	}
}