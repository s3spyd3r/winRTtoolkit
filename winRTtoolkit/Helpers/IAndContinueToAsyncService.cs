using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace winRTtoolkit.Helpers
{
    public interface IAndContinueToAsyncService
    {
        Task<IActivatedEventArgs> GetTaskToNextActivatedEvent();

        void ContinueHaltedOperation(IActivatedEventArgs args);
    }
}