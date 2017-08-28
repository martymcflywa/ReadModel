using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        void Write(IModel model);
        T Read<T>(string filename);
    }
}
