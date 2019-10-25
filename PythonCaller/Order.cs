using System;
using System.Collections.Generic;
using System.Text;
using Bogus;

namespace PythonCaller
{
    public class Order
    {
        public string OrderId { get; set; }

        public string OrderName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Quantity { get; set; }

        public static Faker<Order> GetFaker(DateTime startDate, DateTime endDate)
        {
            return new Faker<Order>()
                .StrictMode(true)
                .RuleFor(x => x.OrderId, f => f.Random.Guid().ToString())
                .RuleFor(x => x.OrderName, f => f.Commerce.Product())
                .RuleFor(x => x.OrderDate, f => f.Date.Between(startDate, endDate))
                .RuleFor(x => x.Quantity, f => f.Random.Number(1, 77));
        }
    }

}
