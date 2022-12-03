namespace Application.Common.Error
{
    public interface IErrorMessageLog
    {
        bool LogError(string layerName, string className, string methodName, string msg);
    }
}
