namespace RobyMes.Propellerhead.Common.Configuration
{
    public interface IConfigurationProvider
    {
        string DocumentStoreConnectionString
        {
            get;
            set;
        }

        string DocumentStoreSchemaName
        {
            get;
            set;
        }
    }
}
