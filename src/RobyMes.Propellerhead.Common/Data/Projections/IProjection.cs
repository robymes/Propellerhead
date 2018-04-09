using System;

namespace RobyMes.Propellerhead.Common.Data.Projections
{
    public interface IProjection
    {
        Guid Id
        {
            get;
            set;
        }
    }
}
