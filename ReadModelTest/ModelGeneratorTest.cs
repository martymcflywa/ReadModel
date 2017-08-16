using EventReader;
using ReadModel;
using System;
using Xunit;

namespace ReadModelTest
{
    public class ModelGeneratorTest
    {
        [Fact]
        public void PopulateModel()
        {
            ModelGenerator generator = new ModelGenerator(new EventStream());
            var model = generator.Build();
            var winners = generator.GetHighestPayingCustomerFor(model, new DateTime(2016, 1, 1));
            // need some validation here that model contains expected data
        }

        public void TargetBuildSignature()
        {
            //var model = new ModelGenerator().Build(new EventStream().Get());
        }
    }
}
