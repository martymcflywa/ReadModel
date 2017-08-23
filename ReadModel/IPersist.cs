using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        long WritePageSize { get; }
        long NextPage { get; set; }
        void Write(IModel model, string filename);
        T Read<T>(string filename);
    }
}
