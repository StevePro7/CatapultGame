using System;
using System.IO;
using Microsoft.Xna.Framework;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Master.Implementation
{
	public class RealFileProxy : IFileProxy
	{
		public Stream GetStream(String path)
		{
			return TitleContainer.OpenStream(path);
		}
	}
}