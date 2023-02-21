using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalogue.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }


        [HttpPost("AddAccount")]
        public IActionResult AddAccount(CreateAccountDto createAccountDto)
        {
            var result = _accountServices.AddAccount(createAccountDto);
            return Ok(result);
        }


        [HttpPost("ValidateAccount")]
        public IActionResult ValidateAccount(ValidateAccountDto validateAccountDto)
        {
            var result = _accountServices.ValidateAccount(validateAccountDto);
            return Ok(result);
        }


        [HttpGet("GetAccounts")]
        public IActionResult GetAccounts()
        {
            var result = _accountServices.GetAccounts();
            return Ok(result);
        }





    }
}
