using EventReader.Event;
using EventReader.Read;
using ReadModel;
using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
