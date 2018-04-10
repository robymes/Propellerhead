using RobyMes.Propellerhead.Common.Configuration;
using RobyMes.Propellerhead.Common.Data;
using RobyMes.Propellerhead.Data.Marten;
using StructureMap;

namespace RobyMes.Propellerhead.Web
{
    public class ContainerRegistry : Registry
    {
        public ContainerRegistry()
        {
            this
                .For<IConfigurationProvider>()
                .Use<EnvironmentVariablesConfigurationProvider>()
                .Singleton();
            this
                .For<IRepository>()
                .Use<MartenRepository>()
                .Transient();
        }
    }
}
