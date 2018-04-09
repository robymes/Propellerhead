using System;

namespace RobyMes.Propellerhead.Common.Data.Events
{
    public interface IEvent
    {
        Guid Id
        {
            get;
        }
    }
}
