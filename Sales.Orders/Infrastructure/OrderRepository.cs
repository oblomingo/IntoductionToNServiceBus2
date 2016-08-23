using Sales.Orders.Domain;

namespace Sales.Orders.Infrastructure
{
    /// <summary>
    /// Fake order repository 
    /// </summary>
    public class OrderRepository
    {
        public int SaveOrder(int productId, int userId, int shippingTypeId)
        {
            return 1;
        }

        public void ChangeOrderStatus(int orderId, int orderStatus)
        {
            //Some logic to change order status
        }

        public Order GetOrderBy(int orderId)
        {
            //Mock order objext
            return new Order(orderId)
                .ToUser(12)
                .WithProduct(1)
                .Using((int)Enums.ShippingType.Priority);
        }
    }
}
