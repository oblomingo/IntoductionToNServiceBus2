namespace Sales.Orders.Domain
{
    public class Order
    {
        private readonly int _orderId;
        private int _status;
        private int _userId;
        private int _productId;
        private int _shippingTypeId;

        public Order(int orderId)
        {
            _orderId = orderId;
            _status = (int) Enums.OrderStatuses.OrderCreated;
        }

        public Order ToUser(int userId)
        {
            _userId = userId;
            return this;
        }

        public Order WithProduct(int productId)
        {
            _productId = productId;
            return this;
        }

        public Order Using(int shippingTypeId)
        {
            _shippingTypeId = shippingTypeId;
            return this;
        }

        public int OrderId => _orderId;
        
        public int Status => _status;
        public int UserId => _userId;
        public int ProductId => _productId;
        public int ShippingTypeId => _shippingTypeId;

        public void ChangeStatus(int status)
        {
            _status = status;
        }

    }
}