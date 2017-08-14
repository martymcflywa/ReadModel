using System.Data.SqlClient;

namespace Repository.Data
{
    public interface IElementSelector
    {
        EventEntry Select(SqlDataReader reader);
    }
}
