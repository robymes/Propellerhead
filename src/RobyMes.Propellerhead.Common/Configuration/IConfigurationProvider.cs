namespace RobyMes.Propellerhead.Common.Configuration
{
    public interface IConfigurationProvider
    {
        string DocumentStoreConnectionString
        {
            get;
        }

        string DocumentStoreSchemaName
        {
            get;
        }
    }
}
