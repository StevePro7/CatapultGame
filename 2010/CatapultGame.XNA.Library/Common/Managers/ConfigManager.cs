using System;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public interface IConfigManager
	{
		void Initialize();
		void Initialize(String root);
		void LoadContent();
		void LoadGlobalConfigData();
		GlobalConfigData GlobalConfigData { get; }
	}

	public class ConfigManager : BaseManager, IConfigManager
	{
		public void Initialize()
		{
			BaseData.Initialize();
		}
		public void Initialize(String root)
		{
			BaseData.Initialize(root);
		}

		public void LoadContent()
		{
			LoadGlobalConfigData();
			BaseData.LoadContent();
		}

		public void LoadGlobalConfigData()
		{
			String file = GetGlobalConfigFile(Constants.GLOBAL_CONFIG_FILENAME);
			GlobalConfigData = MyGame.Manager.FileManager.LoadXml<GlobalConfigData>(file);
		}

		public GlobalConfigData GlobalConfigData { get; private set; }

		private static String GetGlobalConfigFile(String configFile)
		{
			return GetConfigFile(GetGlobalBaseContentRoot(), configFile);
		}
		private static String GetConfigFile(String root, String file)
		{
			return String.Format("{0}/{1}/{2}/{3}", root, Constants.DATA_DIRECTORY, Constants.CONFIG_DIRECTORY, file);
		}
	}
}