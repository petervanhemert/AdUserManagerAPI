using System;
using System.Collections.Generic;
using AdUserManagerAPI._00_CommonUtility.I_Contracts;
using AdUserManagerAPI._00_CommonUtility.I_Contracts.IRepo;
using AdUserManagerAPI._00_CommonUtility.I_Contracts.ISevices;
using AdUserManagerAPI._00_CommonUtility.Models;

namespace AdUserManagerAPI._02_BusinessLogic.ServicesManager
{
    public class UserServices : IUserServices
    {
        private readonly IUoW _uow;
        private readonly IUserRepo _userRepo;
        public UserServices(IUoW uow, IUserRepo userRepo)
        {
            _uow = uow;
            _userRepo = userRepo;
        }      


        public User FindByAccountName(string accountName)
        {
            return _userRepo.FindByAccountName(accountName);
            //return _uow.Users.Find(id);
        }
        public User FindByEmail(string eMail)
        {
            return _userRepo.FindByEmail(eMail);
            //return _uow.Users.Find(id);
        }

        public bool CheckIfAccountnameExist(string accountname)
        {
            return !string.IsNullOrEmpty(_userRepo.FindByAccountName(accountname).AccountName) ;
        }
        public bool CheckIfEmailExist(string eMail)
        {
            return !string.IsNullOrEmpty(_userRepo.FindByEmail(eMail).Email);
        }
        public bool CheckIfFullNameExist(string firstname, string lastname)
        {
            return !string.IsNullOrEmpty(_userRepo.FindByFullName(firstname, lastname).AccountName);
        }

        
        public string GenerateNewAccountname(string firstName , string lastName, int number = 0 )
        {
            var newAccountname ="";
            // if last name has less char then 4 add x to get 4 char     
            if (lastName.Length < 4)
            {
                var loopLenght = 4 - lastName.Length;
                for (int i = 0; i < loopLenght; i++)
                {
                    lastName = lastName + "x";
                }               
            }

            if (number == 0)
            {
                newAccountname = firstName.Substring(0, 2) + lastName.Substring(0, 4);
            }
            else
            {
                newAccountname = firstName.Substring(0, 2) + lastName.Substring(0, 4) + number.ToString();
            }                      
            if (CheckIfAccountnameExist(newAccountname))
            {
                number = number + 1;
                
                GenerateNewAccountname(firstName, lastName, number);
            }; //check if generated accountName is unique
            return newAccountname;
        }

        public IEnumerable<User> GetAll()
        {
           return _userRepo.GetAll();
        }

        public void Add(User user)
        {
            // check if users eMail or account name exist            
            if (CheckIfEmailExist(user.Email) || CheckIfAccountnameExist(user.AccountName))
            {
                //return message user with this eMail or account name already exist
            }
            else 
            {
                if (CheckIfFullNameExist(user.FirstName, user.LastName) && !user.DoubleUserIsOk)
                {
                        User UserExist = _userRepo.FindByFullName(user.FirstName, user.LastName);
                        //return message user exist with UserExist.username last name account name and email. 
                }
                else //register user to ad
                {
                    if (string.IsNullOrEmpty(user.AccountName) && user.DoubleUserIsOk)//external user
                    {
                        //generate a accountName for external user
                       user.AccountName = GenerateNewAccountname(user.FirstName, user.LastName);
                    }
                    _uow.Users.Add(user);
                    _uow.SaveChanges();
                }              
            }
        }

        public User Remove(string accountName)
        {
            var user = _uow.Users.Remove(accountName);
            _uow.SaveChanges();
            return user;
        }

        public void Update(User user)
        {
            _uow.Users.Update(user);
            _uow.SaveChanges();
        }
    }
}
