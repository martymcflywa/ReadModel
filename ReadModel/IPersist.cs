using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        // TODO: Remove this from interface, maybe add to constructor of implementation
        long WritePageSize { get; }
        void Write(IModel model);
        T Read<T>(string filename);
        // TODO: Remove
        bool IsFileExists(string filename);
    }
}
