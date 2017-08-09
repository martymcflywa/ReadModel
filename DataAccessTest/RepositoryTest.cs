using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessTest
{
    public class RepositoryTest
    {
        [Fact]
        public void Connect()
        {
            Assert.True(Repository.Connect());
        }
    }
}
