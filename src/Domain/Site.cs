using System.ComponentModel;

namespace Domain;

public class Site
{
    public string Identifier { get; set; }
    public string Name { get; set; } 
    public string Domain { get; set; }
    public string Description { get; set; }
    public string  Status { get; set; }
    public List<Feed> Feeds { get; set; }
    
}