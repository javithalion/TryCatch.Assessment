using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain;

namespace TryCatch.WebShopCase.DataAccess.NHibernate.Maps
{
    public class OrderLineMap : ClassMap<OrderLine>
    {
        public OrderLineMap()
        {
            Id(ol => ol.Id).GeneratedBy.GuidComb();

            Map(ol => ol.Amount).Not.Nullable();
            Map(ol => ol.CreationDate).Not.Update().Not.Nullable();
            Map(ol => ol.SubTotal).Not.Nullable();
            Map(ol => ol.Total).Not.Nullable();
            Map(ol => ol.Vat).Not.Nullable();
            Map(ol => ol.VatPercentageFromProduct).Not.Nullable().Not.Update();
            Map(ol => ol.Product.Id).Column("ProductId").Not.Nullable();            
        }
    }
}
