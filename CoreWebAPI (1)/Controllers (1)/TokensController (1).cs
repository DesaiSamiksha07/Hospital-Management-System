using DataAccess;
using DataEntity;
using DataModel.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("Tokens")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TokensController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetToken/{name?}")]
        public ResponseEntity<TokenEntity> Get(string name = "")
        {
           return _unitOfWork.token.GetToken(name);
        }
    }
}
