namespace Api.Activities.Website.Queries.Get;

public class Response
{
    public string Name { get; set; }
    public string Domain { get; set; }
    public string Description { get; set; }
    
   public IReadOnlyCollection<Feed> Feeds { get; set; }
   public IReadOnlyList<string> Categories { get; set; }
}

public class Feed
{
    public string Media { get; set; }
    public string Path { get; set; }
}
