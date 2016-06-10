using AdUserManagerAPI._00_CommonUtility.I_Contracts.IRepo;

namespace AdUserManagerAPI._00_CommonUtility.I_Contracts
{
    public interface IUoW
    {
        IUserRepo Users { get; }
        void SaveChanges();
    }
}
