using Microsoft.Extensions.DiagnosticAdapter;
using System.Diagnostics;

namespace Shadow.Tool.Diagnostics
{
    public static class DiagnositcAdatperExtensions
    {
        public static void AddToolkitDiagnositcs(this DiagnosticListener diagnosticListener)
        {
            diagnosticListener.SubscribeWithAdapter(new ExceptionHandlerDiagnostic());
        }
    }
}
