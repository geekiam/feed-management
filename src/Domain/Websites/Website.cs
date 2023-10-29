using Models;


namespace Domain.Websites;

public class Website : AggregateRoot<Website,string>
{
    public Website(string name, string domain, string description)
    {
        Name = name;
        Domain = domain;
        Description = description;
    }
    public string Name { get; private set; } 
    public string Domain { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyCollection<Feed> Feeds { get; set; }
    
    
}