using MatrixChallenge.Application.UseCases.GetWordFinder;
using Microsoft.AspNetCore.Mvc;

namespace MatrixChallenge.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatrixController
    {
        /// <summary>
        /// Look for the words in the matrix
        /// </summary>
        /// <param name="request"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("words")]
        public IActionResult FindWords([FromQuery] GetWordFinderQuery request, GetWordFinderHandler service)
        {
           var response = service.Handle(request);
           if(response.IsSuccess)
                return new ObjectResult(response.Value);
            return new ObjectResult(response.Error);
        }
    }
}
