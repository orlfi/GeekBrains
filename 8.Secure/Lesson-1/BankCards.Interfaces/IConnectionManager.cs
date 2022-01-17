using System.Data.Common;

namespace BankCards.Interfaces
{
    public interface IConnectionManager
    {
        void Dispose();
        DbConnection GetOpenedConnection();
    }
}