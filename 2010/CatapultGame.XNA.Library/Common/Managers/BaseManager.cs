using System;
using WindowsGame.Common.Data;
using WindowsGame.Common.Static;

namespace WindowsGame.Common.Managers
{
	public abstract class BaseManager
	{
		protected static String GetGlobalBaseContentRoot()
		{
			return String.Format("{0}{1}", BaseData.BaseRoot, Constants.CONTENT_DIRECTORY);
		}
	}
}