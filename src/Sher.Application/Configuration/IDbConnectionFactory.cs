using System.Data;

namespace Sher.Application.Configuration
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}