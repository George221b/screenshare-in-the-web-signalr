using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Screenshare.Main.Models
{
	public class GlobalCollections
	{
		public static IDictionary<string, Master> users = new ConcurrentDictionary<string, Master>();
	}
}