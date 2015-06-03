using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain;

namespace TryCatch.WebShopCase.DataAccess.NHibernate.Maps
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(o => o.Id).GeneratedBy.GuidComb();

            Map(o => o.CheckoutDate).Not.Nullable();
            Map(o => o.CreationDate).Not.Nullable().Not.Update();
            Map(o => o.SubTotal).Not.Nullable();
            Map(o => o.Total).Not.Nullable();
            Map(o => o.Vat).Not.Nullable();

            References(o => o.Customer).Class(typeof(Customer)).Column("CustomerId").Not.LazyLoad().Fetch.Join().Cascade.SaveUpdate().Not.Nullable();
            HasMany(o => o.OrderLines).KeyColumn("OrderId").Cascade.AllDeleteOrphan();
        }
    }
}
