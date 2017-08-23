using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        long WritePageSize { get; }
        long NextPage { get; set; }
        void Write(IModel model);
        T Read<T>(string filename);
        bool IsFileExists(string filename);
    }
}
