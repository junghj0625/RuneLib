namespace Rune
{
    public class OperationStatus
    {
        public void SetSuccess()
        {
            Success = true;
        }

        public void SetError(string code)
        {
            Success = false;

            Code = code;
        }

        public void SetMessage(string message)
        {
            Message = message;

            OnMessageChanged.Invoke(message);
        }

        public void SetProgress(float progress)
        {
            Progress = progress;

            OnProgressChanged.Invoke(progress);
        }



        public bool Failed
        {
            get => !Success;
        }



        public bool Success { get; private set; } = true;

        public string Code { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;

        public float Progress { get; private set; } = 0.0f;

        public LooseEvent<string> OnMessageChanged { get; } = new();

        public LooseEvent<float> OnProgressChanged { get; } = new();
    }
}