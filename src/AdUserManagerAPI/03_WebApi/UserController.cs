using System.Collections.Generic;
using AdUserManagerAPI._00_CommonUtility.I_Contracts.ISevices;
using AdUserManagerAPI._00_CommonUtility.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AdUserManagerAPI._03_WebApi
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {


        //private readonly DataContext _context;
        //public UserController(DataContext context)
        //{
        //    _context = context;
        //}



        private readonly IUserServices _user;
        public UserController(IUserServices users)
        {
            this._user = users;
        }

        // GET: api/user
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _user.GetAll();
        }
        [HttpGet]
        public bool AccountnameExist(string accountname)
        {
            return _user.CheckIfAccountnameExist(accountname);
        }

        [HttpGet]
        public bool EmailExist(string eMail)
        {
            return _user.CheckIfEmailExist(eMail);
        }

        [HttpGet]
        public bool FullnameExist(string firstname, string lastname)
        {
            return _user.CheckIfFullNameExist(firstname, lastname);
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(string accountName)
        {
            return _user.FindByAccountName(accountName);
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Users.Add(user);
                //_context.SaveChanges();
                _user.Add(user);
            }
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                _user.Update(user);
            }
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(string accountName)
        {
            _user.Remove(accountName);
        }
    }
}
