namespace Common.Util;

public class Utils
{
    public static void NotNull<T>(T obj, string message = "Data Is Null") where T : class?
    {
        if (obj is null)
            throw new ArgumentNullException($"{nameof(obj)} {message}");
    } 
    
    public static void NotNullEntity<T>(T obj, string message = "Data Is Null")
    {
        if (obj is null)
            throw new ArgumentNullException($"{nameof(obj)} {message}");
    } 
    public static void StateOperation(bool isSave, string message = "Operation Failed")
    {
        if (!isSave)
            throw new ArgumentNullException(message);
    }

}