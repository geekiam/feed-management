namespace Services;

public interface IFactory<in TObj> 
   where TObj : class
 
{
    Task<string> Create(TObj obj);
}