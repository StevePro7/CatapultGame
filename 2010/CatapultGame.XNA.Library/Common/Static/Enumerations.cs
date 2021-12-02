namespace WindowsGame.Common.Static
{
	public enum ScreenType
	{
		Splash,
		Init,
		Title,
		Diff,
		Long,
		Ready,
		Play,
		Quiz,
		Score,
		Over,
		Exit,
		Test,
	}

	public enum CatapultState
	{
		Rolling,
		Firing,
		Crash,
		ProjectileFlying,
		ProjectileHit,
		Reset
	}

	public enum DifficultyType
	{
		Easy,
		Norm,
		Hard,
		Argh,
	}

	public enum SoundEffectType
	{
		countdown_expire,
		engine_1,
		engine_2,
		explosion1,
		hyperspace_activate,
		menu_select,
		pdp3_fire,
		weapon_pickup_alt,
	}

	public enum SpriteType
	{
		Select,
		Right,
		Wrong,
		LeftArrow,
		RightArrow,
		VolumeOn,
		VolumeOff,
		White
	}

	public enum OptionType
	{
		A,
		B,
		C,
		D,
		None
	}

	public enum ActorType
	{
		Bart1,
		Bart2,
		Comic,
		Drhibbert,
		Drnick,
		Flanders,
		Grampa1,
		Homer1,
		Homer2,
		Homer3,
		Lisa1,
		Lisa2,
		Maggie,
		Marge0,
		Skinner,
		Troy
	}

}