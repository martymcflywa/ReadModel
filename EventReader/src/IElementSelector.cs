using System.Data.SqlClient;

namespace EventReader
{
    public interface IElementSelector
    {
        EventEntry Select(SqlDataReader reader);
    }
}
