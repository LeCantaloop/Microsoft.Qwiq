using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Qwiq.Core.Tests
{
    [TestClass]
    public class WiqlFlatQueryTests : IntegrationContextSpecification
    {
        private const int Id = 10726528;

        private static readonly string Wiql = $"SELECT {string.Join(", ", CoreFields)} FROM WorkItems WHERE [System.Id] = {Id}";

        public override void When()
        {
            RestResult.WorkItem = RestResult.WorkItemStore.Query(Wiql).Single();
            SoapResult.WorkItem = SoapResult.WorkItemStore.Query(Wiql).Single();
        }
    }
}