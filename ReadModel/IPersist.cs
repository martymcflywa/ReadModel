using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        void Write(IModel model, string filename);
        T Read<T>(string filename);
    }
}
