using System.Collections.Generic;

namespace AuthorApp
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string ProductionYears { get; set; }
        public string BodyType { get; set; }
        public string Engine { get; set; }
        public string Transmission { get; set; }
        public string Weight { get; set; }
        public string Image1Url { get; set; }
        public string Image2Url { get; set; }
        public string Description { get; set; }
        public List<string> AdditionalImages { get; set; }

        public Car()
        {
            AdditionalImages = new List<string>();
        }
    }
}