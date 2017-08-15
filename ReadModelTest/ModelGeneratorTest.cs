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
            var connectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
            var selector = new EventElementSelector();
            var source = new SqlSource(connectionString, selector);

            ModelGenerator generator = new ModelGenerator(new EventStream(source));
            var model = generator.GetCustomerModel();
        }
    }
}
