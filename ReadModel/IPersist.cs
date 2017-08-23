using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        long WritePageSize { get; }
        void Write(IModel model);
        T Read<T>(string filename);
        bool IsFileExists(string filename);
    }
}
