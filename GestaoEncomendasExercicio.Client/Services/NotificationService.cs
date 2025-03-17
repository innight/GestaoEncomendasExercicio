namespace GestaoEncomendasExercicio.Client.Services;

public class NotificationService
{
    public event Func<NotificationMessage, Task>? OnNotify;

    public async Task ShowSuccess(string message, int duration = 5000)
        => await Notify(message, true, duration);

    public async Task ShowError(string message, int duration = 5000)
        => await Notify(message, false, duration);

    private async Task Notify(string message, bool isSuccess, int duration)
    {
        if (OnNotify is not null)
        {
            await OnNotify.Invoke(new NotificationMessage
            {
                Message = message,
                IsSuccess = isSuccess,
                Duration = duration
            });
        }
    }
}

public class NotificationMessage
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public int Duration { get; set; }
}
