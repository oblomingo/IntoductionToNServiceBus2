namespace Sales.Orders.Domain
{
    public class Enums
    {
        public enum OrderStatuses
        {
            OrderCreated = 0,
            PackagePrepared = 1,
            OutOfStock = 2,
            PackageSent = 3,
            Complete = 4
        }

        public enum ShippingType
        {
            FirstClass = 0,
            Priority = 1
        }
    }
}
