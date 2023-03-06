using System;

public enum Price
{
	Level1,
	Level2,
	Level3,
	Level4,
	Level5,
	Level6,
	Level7,
	Level8,
	Level9,
	Level10,
	Level11
}

public static class PriceExtensions
{
	public static int GetValue(this Price price)
	{
		return price switch
		{
			Price.Level1 => 25,
			Price.Level2 => 50,
			Price.Level3 => 100,
			Price.Level4 => 180,
			Price.Level5 => 300,
			Price.Level6 => 600,
			Price.Level7 => 900,
			Price.Level8 => 1800,
			Price.Level9 => 2500,
			Price.Level10 => 3000,
			Price.Level11 => 4000,
			_ => throw new Exception()
		};
	}

	public static int GetPoint(this Price price)
	{
		return price switch
		{
			Price.Level1 => 50,
			Price.Level2 => 125,
			Price.Level3 => 250,
			Price.Level4 => 540,
			Price.Level5 => 900,
			Price.Level6 => 1980,
			Price.Level7 => 2970,
			Price.Level8 => 5940,
			Price.Level9 => 9000,
			Price.Level10 => 10800,
			Price.Level11 => 14400,
			_ => throw new Exception()
		};
	}
}
