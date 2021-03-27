using Screenshare.Main.Models;
using Screenshare.Main.Models.InputModels;
using Screenshare.Main.Models.OutputModels;
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

            return Ok(new ScreenshareDetailsOutputModel { GUID = client.GUID });
        }
    }
}