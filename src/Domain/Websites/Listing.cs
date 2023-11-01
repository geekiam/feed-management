using Domain;

namespace Geekiam.Websites;

public class Listing
{
  
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Protocol { get; set; }
        public string Description { get; set; }
        public List<Feed> Feeds { get; set; }
        public List<string> Categories { get; set; }
    
}