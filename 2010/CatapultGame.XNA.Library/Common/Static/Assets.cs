using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame.Common.Static
{
	public static class Assets
	{
		// Fonts.
		public static SpriteFont EmulogicFont;

		// Textures.
		public static Texture2D armTexture;
		public static Texture2D body_backTexture;
		public static Texture2D body_frontTexture;
		public static Texture2D backgroundTexture;
		public static Texture2D logTexture;
		public static Texture2D pumpkinTexture;
		public static Texture2D pumpkinsmashTexture;
		public static Texture2D skyTexture;
		public static Texture2D SplashTexture;
		//public static Texture2D SpritesheetTexture;

		// Sound.
		public static IDictionary<SoundEffectType, SoundEffectInstance> SoundEffectDictionary;
		public static Song TitleMusicSong;
		//public static Song GameOverSong;
	}
}