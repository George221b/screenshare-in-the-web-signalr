using System;

namespace Screenshare.Main.Models
{
	public abstract class BaseScreenshareUser
	{
		public BaseScreenshareUser()
		{
            this.GUID = Guid.NewGuid().ToString();
        }

        public string ConnectionID { get; set; }
        public string GUID { get; set; }
        public string Title { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CursorTop { get; set; }
        public int CursorLeft { get; set; }
        public int ScrollTop { get; set; }
        public int ScrollLeft { get; set; }
    }
}