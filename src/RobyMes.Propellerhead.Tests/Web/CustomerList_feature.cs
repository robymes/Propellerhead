using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using System.Threading.Tasks;

[assembly: LightBddScope]

namespace RobyMes.Propellerhead.Tests.Web
{
    [FeatureDescription(@"Pippo")]
    public partial class CustomerList_feature : FeatureFixture
    {
        [Scenario]        
        public async Task customer_list_inmemory()
        {
            await this.Runner.RunScenarioAsync(
                given => Customers_are_available_inmemory_repository(),
                then => Customers_are_retrieved()
            );
        }
    }
}
