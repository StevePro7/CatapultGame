namespace WindowsGame.Common.Interfaces
{
	public interface IDeviceFactory
	{
		void Initialize();

		// Left arrow.
		void DrawBackButton();
		void DrawOverButton();
		void DrawQuitButton();
		
		// Right
		void DrawPlayButton();
		void DrawQuizButton();
		void DrawViewButton();
	}
}