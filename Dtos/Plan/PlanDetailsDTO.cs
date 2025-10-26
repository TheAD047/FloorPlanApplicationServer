namespace FloorPlanApplication.Dtos.Plan
{
    public class PlanDetailsDTO
    {
        public double Price { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public double Area { get; set; }

        public IEnumerable<string> PhotoURLs { get; set; }
    }
}
