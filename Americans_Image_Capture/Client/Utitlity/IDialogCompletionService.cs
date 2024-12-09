namespace Americans_Image_Capture.Client.Utitlity
{
    public interface IDialogCompletionService
    {
        bool IsCompleted { get; set; }
        event EventHandler CompletionChanged;

        void SetCompletion(bool isCompleted);
    }
    public class CompletionService : IDialogCompletionService
    {
        private bool _isCompleted;

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                CompletionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CompletionChanged;

        public void SetCompletion(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }
    }
}
