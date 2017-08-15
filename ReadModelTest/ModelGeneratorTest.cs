using EventReader;
using ReadModel;
using Xunit;

namespace ReadModelTest
{
    public class ModelGeneratorTest
    {
        [Fact]
        public void PopulateModel()
        {
            ModelGenerator generator = new ModelGenerator(new EventStream());
            var model = generator.GetCustomerModel();
            // need some validation here that model contains expected data
        }
    }
}
