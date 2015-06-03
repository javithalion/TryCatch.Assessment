using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain;

namespace TryCatch.WebShopCase.DataAccess.NHibernate.Maps
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(c => c.Id).GeneratedBy.GuidComb();

            Map(c => c.Address).Not.Nullable();
            Map(c => c.City).Not.Nullable();
            Map(c => c.CreationDate).Not.Nullable().Not.Update();
            Map(c => c.Email).Not.Nullable();
            Map(c => c.FirstName).Not.Nullable();
            Map(c => c.HouseNumber).Not.Nullable();
            Map(c => c.LastName).Not.Nullable();
            Map(c => c.Title).Not.Nullable();            
        }
    }
}
