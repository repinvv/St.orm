//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace StormTestProject
{
    using System.Linq;
    using St.Orm.Interfaces;

    public partial class StormTestContext : IStormContext
    {
        private StormCommands stormCommands;

        IQueryable<T> IStormContext.Set<T>()
        {
            return Set<T>();
        }

        IDalRepository<T> IStormContext.GetDalRepository<T>()
        {
            return DalRepositoryStorage.GetDalRepository<T>();
        }

        public StormCommands Storm
        {
            get
            {
                return stormCommands = stormCommands ?? new StormCommands(this);
            }
        }
    }
}
