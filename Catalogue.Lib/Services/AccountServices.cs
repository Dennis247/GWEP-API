using AutoMapper;
using Catalogue.api.Utils.Response;
using Catalogue.Lib.Data;
using Catalogue.Lib.Models.Dto;
using Catalogue.Lib.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue.Lib.Services
{
    public interface IAccountServices
    {
     
        public Response<AccountDto> AddAccount(CreateAccountDto createAccountDto);
        public Response<AccountDto> ValidateAccount(ValidateAccountDto validateAccount);

        public Response<IEnumerable<AccountDto>> GetAccounts();


    }


    public class AccountServices : IAccountServices
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public AccountServices(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public Response<AccountDto> AddAccount(CreateAccountDto createAccountDto)
        {

            //check if account already exist 

            var existingAccount = _applicationDbContext.Accounts.FirstOrDefault(x => x.PhoneNumber.ToLower().Trim()
            == createAccountDto.PhoneNumber.ToLower().Trim());

            if(existingAccount != null)
            {
                throw new ApplicationException($"User with {createAccountDto.PhoneNumber} already exist");
            }

            Account account = new Account
            {
                Created = DateTime.UtcNow,
                FirstName = createAccountDto.FirstName,
                LastName = createAccountDto.LastName,
                PhoneNumber = createAccountDto.PhoneNumber,

            };

            _applicationDbContext.Add(account);
            _applicationDbContext.SaveChanges();

            var accountToReturn = _mapper.Map<AccountDto>(account);

            return new Response<AccountDto>
            {
                Data = accountToReturn,
                Message = "Registration Sucessful",
                Succeeded = true
            };
        }

        public Response<IEnumerable<AccountDto>> GetAccounts()
        {
           var accounts = _applicationDbContext.Accounts.AsEnumerable();
            var accountToReturn =_mapper.Map<IEnumerable<AccountDto>>(accounts);

            return new Response<IEnumerable<AccountDto>>
            {
                Data = accountToReturn,
                Message = "Sucessful",
                Succeeded = true
            };
        }

        public Response<AccountDto> ValidateAccount(ValidateAccountDto validateAccount)
        {
            var existingUser = _applicationDbContext.Accounts.FirstOrDefault(x => x.PhoneNumber.Trim()
             == validateAccount.PhoneNumber.Trim());

            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            var userToReturn = _mapper.Map<AccountDto>(existingUser);

            return new Response<AccountDto> { 
                Data = userToReturn,
                Message = "Login Sucessful",
                Succeeded= true
            };
        }
    }
}
