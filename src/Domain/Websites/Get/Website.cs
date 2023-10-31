using Domain;

namespace Geekiam.Websites.Get;

public record Website(string Name, string Domain, string Description) : AggregateRoot<Website,string>
{
    public string Name { get; private set; } = Name;
    public string Domain { get; private set; } = Domain;
    public string Description { get; private set; } = Description;
    public List<Feed> Feeds { get; init; }
    
    public List<string> Categories { get; set; }
    
    
}