using System;

namespace PythonCaller
{
    public class DataGenerator
    {
        public static Order[] GetOrders(int count)
        {
            var now = DateTime.Now;

            return Order.GetFaker(now.AddDays(-7), now).Generate(count).ToArray();
        }

         public static Order[] GetHistoricalOrders(int count)
        {
            var endDate = DateTime.Now.AddDays(-30);

            return Order.GetFaker(endDate.AddDays(-365), endDate).Generate(count).ToArray();
        }
    }
}