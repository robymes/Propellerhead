using System;

namespace RobyMes.Propellerhead.Tests.Web
{
    public class CustomerQueryingContext : IDisposable
    {
        

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool managed)
        {
            
        }
    }
}
