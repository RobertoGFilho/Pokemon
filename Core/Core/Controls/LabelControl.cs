using Xamarin.Forms;

namespace Core.Controls
{
    public class LabelControl : Label
    {
        public LabelControl()
        {
            IsVisible = false;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Text))
                IsVisible = !string.IsNullOrWhiteSpace(Text);
        }
    }
}
