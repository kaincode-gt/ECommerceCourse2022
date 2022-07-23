using Microsoft.JSInterop;

namespace ECommerceCourse2022.Helper;

public static class IJSRuntimeExtensions
{
    public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public static async ValueTask ToastrError(this IJSRuntime jsRuntime, string message)
    {
        await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
    }
}
