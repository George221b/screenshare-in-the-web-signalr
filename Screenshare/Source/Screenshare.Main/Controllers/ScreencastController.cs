using Screenshare.Main.Models;
using Screenshare.Main.Models.InputModels;
using Screenshare.Main.Models.OutputModels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Screenshare.Main.Controllers
{
	[RoutePrefix("api/Screencast")]
	public class ScreencastController : ApiController
	{
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		[Route("RequestConnection")]
		[HttpPost]
		public IHttpActionResult RequestConnection(MasterDataInputModel inputModel)
		{
			Master client = new Master
			{
				Body = inputModel.Body,
				Head = inputModel.Head,
				Width = inputModel.Width,
				Height = inputModel.Height
			};

			GlobalCollections.users[client.GUID] = client;
			string slaveUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, string.Empty) + $"/home/slave?masterGuid={client.GUID}";

			return Ok(new ScreenshareDetailsOutputModel { GUID = client.GUID, URL = slaveUrl });
		}
	}
}