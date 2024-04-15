
namespace RickAndMorty.Shared.Helpers;
public static class ApmHelper
{
    public static void CaptureExceptionInApm(Exception ex)
    {
        var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
        transaction?.CaptureException(ex);
    }

    public static void CaptureErrorInApm(string error, string culprit)
    {
        var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
        transaction?.CaptureError(error, culprit, null);
    }
}
