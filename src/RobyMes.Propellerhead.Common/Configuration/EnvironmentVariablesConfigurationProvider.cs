using System;

namespace RobyMes.Propellerhead.Common.Configuration
{
    public class EnvironmentVariablesConfigurationProvider : IConfigurationProvider
    {
        private const string DOC_STORE_CNN_STRING_KEY = "DOC_STORE_CNN_STRING";
        private const string DOC_STORE_SCHEMA_NAME_KEY = "DOC_STORE_SCHEMA_NAME";

        public string DocumentStoreConnectionString
        {
            get => Environment.GetEnvironmentVariable(DOC_STORE_CNN_STRING_KEY);
        }

        public string DocumentStoreSchemaName
        {
            get => Environment.GetEnvironmentVariable(DOC_STORE_SCHEMA_NAME_KEY);
        }
    }
}
