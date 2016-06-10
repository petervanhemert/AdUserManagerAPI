using System.Collections.Generic;
using AdUserManagerAPI._00_CommonUtility.Models;

namespace AdUserManagerAPI._00_CommonUtility.I_Contracts.IRepo
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll();
        User FindByAccountName(string accountName);
        User FindByEmail(string email);
        User FindByFullName(string firstname, string lastname);
        void Add(User user);
        User Remove(string accountName);
        void Update(User user);
    }
}
