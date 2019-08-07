namespace FilmRent.Service
{
    using System;
    using System.Windows;

    class MsgBox
    {
        public static bool AskForConfirmation(string message)
        {
            MessageBoxResult result = MessageBox.Show(
                message,
                "Предупреждение",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Information
                );
            return result.HasFlag(MessageBoxResult.OK);
        }

        public static void ShowNotification(string message)
        {
            MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
