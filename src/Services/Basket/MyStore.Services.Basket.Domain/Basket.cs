namespace MyStore.Services.Basket.Domain
{
    public class Basket
    {
        private readonly List<BasketItem> _items;

        public Guid CustomerId { get; set; }

        public IEnumerable<BasketItem> Items => _items.AsReadOnly();

        protected Basket()
        {
            _items = new List<BasketItem>();
        }

        public Basket(Guid customerId, IEnumerable<BasketItem> items) : this()
        {
            CustomerId = customerId;

            if (items != null)
            {
                foreach(var item in items)
                {
                    Add(item);
                }
            }
        }

        public void Add(BasketItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (item.Quantity <= 0)
            {
                throw new ArgumentException("The value must be greater than zero.", $"{nameof(item)}.{nameof(item.Quantity)}");
            }

            if (_items.Any(p => p.ItemId == item.ItemId))
            {
                _items.Single(p => p.ItemId == item.ItemId).Quantity += item.Quantity;
            }
            else
            {
                _items.Add(item);
            }
        }

        public void Remove(BasketItem item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (item.Quantity <= 0)
            {
                throw new ArgumentException("The value must be greater than zero.", $"{nameof(item)}.{nameof(item.Quantity)}");
            }

            if (_items.Any(p => p.ItemId == item.ItemId))
            {
                var basketItem = _items.Single(p => p.ItemId == item.ItemId);
                basketItem.Quantity -= item.Quantity;

                if (basketItem.Quantity <= 0)
                {
                    _items.Remove(basketItem);
                }
            }
            else
            {
                _items.Add(item);
            }
        }
    }
}
