using WindowsGame.Common;

namespace WindowsGame
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			using (AnGame game = new AnGame())
			{
				game.Run();
			}
		}
	}
}
