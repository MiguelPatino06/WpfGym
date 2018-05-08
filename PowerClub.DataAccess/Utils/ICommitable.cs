using System;
using System.Threading.Tasks;

namespace PowerClub.DataAccess.Utils
{
    public interface ICommitable : IDisposable
    {
        bool IsDisposed { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}
