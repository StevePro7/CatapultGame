using WindowsGame.Common.Interfaces;

namespace WindowsGame.Common.Managers
{
	public interface IDeviceManager
	{
		void Initialize();

		// Left arrow.
		void DrawBackButton();
		void DrawOverButton();
		void DrawQuitButton();

		// Right arrow.
		void DrawPlayButton();
		void DrawQuizButton();
		void DrawViewButton();
	}

	public class DeviceManager : IDeviceManager
	{
		private readonly IDeviceFactory deviceFactory;

		public DeviceManager(IDeviceFactory deviceFactory)
		{
			this.deviceFactory = deviceFactory;
		}

		public void Initialize()
		{
			deviceFactory.Initialize();
		}

		// Left arrow.
		public void DrawBackButton()
		{
			deviceFactory.DrawBackButton();
		}
		public void DrawOverButton()
		{
			deviceFactory.DrawOverButton();
		}
		public void DrawQuitButton()
		{
			deviceFactory.DrawQuitButton();
		}

		// Right
		public void DrawPlayButton()
		{
			deviceFactory.DrawPlayButton();
		}
		public void DrawQuizButton()
		{
			deviceFactory.DrawQuizButton();
		}
		public void DrawViewButton()
		{
			deviceFactory.DrawViewButton();
		}

	}

}