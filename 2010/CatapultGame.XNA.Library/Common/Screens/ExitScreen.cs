using Microsoft.Xna.Framework;
using WindowsGame.Common.Interfaces;
using WindowsGame.Common.Static;
using WindowsGame.Master;

namespace WindowsGame.Common.Screens
{
	public class ExitScreen : BaseScreen, IScreen
	{
		public ScreenType Update(GameTime gameTime)
		{
			MyGame.Manager.StorageManager.SaveContent();

#if ANDROID
			// Android
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			System.Environment.Exit(0);
			return ScreenType.Exit;
#endif
#if IOS
			// iOS
			throw new System.DivideByZeroException();
#endif
#if !IOS && !ANDROID
			// Default.
			Engine.Game.Exit();
			return ScreenType.Exit;
#endif
		}

	}
}