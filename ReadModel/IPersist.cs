using ReadModel.Models;

namespace ReadModel
{
    public interface IPersist
    {
        void Write(IModel model, string filename);
    }
}
