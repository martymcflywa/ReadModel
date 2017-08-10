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
        public void ExecuteReader()
        {
            var json = Repository.ExecuteReader<string>(@"select top 10 Content from MessageHub.MessageContent as string");
            foreach(var item in json)
            {
                Console.Write(item);
            }
        }
    }
}
