using System;
using AdUserManagerAPI._00_CommonUtility.I_Contracts;
using AdUserManagerAPI._00_CommonUtility.I_Contracts.IRepo;

namespace AdUserManagerAPI._01_DataAccess.Repo
{
    public class UoW : IDisposable, IUoW
    {
        private readonly DataContext _context;
        public UoW(DataContext context)
        {
            _context = context;
            Users = new UserRepo(_context);

        }

        public IUserRepo Users { get; private set; }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
