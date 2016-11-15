using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace winRTtoolkit.Helpers
{
    public class MessageBoxHelper
    {
        private bool _messageShowing = false;

        private readonly CoreDispatcher _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

        public virtual async Task ShowAsync(string text)
        {
            if (_messageShowing) return;

            var message = new MessageDialog(text);

            await _dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            {
                _messageShowing = true;
                await message.ShowAsync().AsTask().ContinueWith((showAsyncResult) =>
                {
                    _messageShowing = false;
                });
            });
        }

        public void Show(string text)
        {
            ShowAsync(text);
        }

        public virtual async Task ShowAsync(string text, string caption)
        {
            if (_messageShowing) return;

            var message = new MessageDialog(text, caption);

            await _dispatcher.RunAsync(CoreDispatcherPriority.High, async () =>
            {
                _messageShowing = true;
                await message.ShowAsync().AsTask().ContinueWith((showAsyncResult) =>
                {
                    _messageShowing = false;
                });
            });

        }

        public void Show(string text, string caption)
        {
            ShowAsync(text, caption);
        }

        public virtual async Task<int> ShowAsync(string text, string caption, IEnumerable<string> buttons)
        {
            var message = new MessageDialog(text, caption);

            foreach (var button in buttons)
            {
                message.Commands.Add(new UICommand(button));
            }

            var command = await message.ShowAsync();

            if (command != null)
            {
                return message.Commands.IndexOf(command);
            }

            return -1;
        }
    }
}
