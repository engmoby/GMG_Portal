using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UploadFile.Custom;

namespace GMG_Portal.UI.Controllers
{
    public class FileUploadHotlesController : ApiController
    {
         
        [Route("api/uploadHotles")]
        public async Task<HttpResponseMessage> Post()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                //Save To this server location
                var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads/Hotels");
                //The reason we not use the default MultipartFormDataStreamProvider is because
                //the saved file name is look weird, not believe me? uncomment below and try out, 
                //the odd file name is designed for security reason -_-'.
                //var streamProvider = new MultipartFormDataStreamProvider(uploadPath);

                //Save file via CustomUploadMultipartFormProvider
                var multipartFormDataStreamProvider = new CustomUploadMultipartFormProvider(uploadPath);
                
                // Read the MIME multipart asynchronously 
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                // Show all the key-value pairs.
                foreach (var key in multipartFormDataStreamProvider.FormData.AllKeys)
                {
                    var enumerable = multipartFormDataStreamProvider.FormData.GetValues(key);
                    if (enumerable != null)
                        foreach (var val in enumerable)
                        {
                            Console.WriteLine(string.Format("{0}: {1}", key, val));
                        }
                }


                //In Case you want to get the files name
                string localFileName = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK);

               // return new HttpResponseMessage(HttpStatusCode.OK, localFileName) ;
               

            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotImplemented) {
                    Content = new StringContent(e.Message)
                };
            }




             
        }
    }
}
