using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uDrive.Core.Models;

namespace uDrive.Core.Helpers
{
    public static class TestData
    {
        public static IEnumerable<TestModel> GetAll()
        {
            return TestModels;
        }

        private static readonly TestModel[] TestModels = new TestModel[]
        {
            new TestModel() { Id = 1, Name = "Test One" },
            new TestModel() { Id = 2, Name = "Test Two" }
        };

        public static TestModel Get(int id)
        {
            return TestModels.FirstOrDefault(x => x.Id == id);
        }
    }
}
