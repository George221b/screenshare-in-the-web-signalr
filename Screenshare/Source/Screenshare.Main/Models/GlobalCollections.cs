using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screenshare.Main.Models
{
	public class GlobalCollections
	{
		public static IDictionary<string, Master> users = new ConcurrentDictionary<string, Master>();
	}
}