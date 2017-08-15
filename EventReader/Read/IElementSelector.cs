using System.Data.SqlClient;

namespace EventReader.Read
{
    public interface IElementSelector
    {
        EventEntry Select(SqlDataReader reader);
    }
}
