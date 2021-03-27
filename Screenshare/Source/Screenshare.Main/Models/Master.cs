using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Screenshare.Main.Models
{
	public class Master : BaseScreenshareUser
	{
		public Master()
		{
			this.Slaves = new List<Slave>();
		}

		public ICollection<Slave> Slaves { get; set; }
	}
}