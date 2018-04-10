using System;

namespace RobyMes.Propellerhead.Common.Data.Projections
{
    public abstract class BaseProjection : IProjection
    {
        public Guid Id
        {
            get;
            set;
        }
    }
}
