using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using WindowsGame.Common.Data;

namespace WindowsGame.Common.Managers
{
	public interface IStorageManager
	{
		void Initialize();
		void LoadContent();
		void SaveContent();
	}

	public class StorageManager : IStorageManager
	{
		private IsolatedStorageFile storage;
		private StoragePersistData persist;
		private String fileName;

		public void Initialize()
		{
			fileName = "SimpsonsTrivia.xml";
		}

		public void LoadContent()
		{
			persist = null;
			try
			{
				using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
				{
					if (storage.FileExists(fileName))
					{
						using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, storage))
						{
							XmlSerializer serializer = new XmlSerializer(typeof(StoragePersistData));
							persist = (StoragePersistData)serializer.Deserialize(fileStream);
						}
					}
				}
			}
			catch
			{
			}

			if (null == persist)
			{
				return;
			}

			MyGame.Manager.SoundManager.SetPlaySound(persist.PlaySound);
		}

		public void SaveContent()
		{
			if (null == persist)
			{
				persist = new StoragePersistData
				{
					PlaySound = MyGame.Manager.ConfigManager.GlobalConfigData.PlaySound
				};
			}

			persist.PlaySound = MyGame.Manager.SoundManager.PlaySound;
			try
			{
				using (storage = GetUserStoreAsAppropriateForCurrentPlatform())
				{
					using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(fileName, FileMode.Create, storage))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(StoragePersistData));
						serializer.Serialize(fileStream, persist);
					}
				}
			}
			catch
			{
			}
		}


		// http://blogs.msdn.com/b/shawnhar/archive/2010/12/16/isolated-storage-windows-and-clickonce.aspx
		private static IsolatedStorageFile GetUserStoreAsAppropriateForCurrentPlatform()
		{
#if WINDOWS
			return IsolatedStorageFile.GetUserStoreForDomain();
#else
			return IsolatedStorageFile.GetUserStoreForApplication();
#endif
		}

	}
}