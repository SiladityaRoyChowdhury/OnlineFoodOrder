namespace Omf.Common.Events
{
    public class UpdateRestaurantEvent
    {
        public UpdateRestaurantEvent(string restaurantId)
        {
            
        }

        public string RestaurantId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Rating { get; set; }
        public string Location { get; set; }
        public string ListedCity { get; set; }
        public decimal ApproxCost { get; set; }
    }
}
