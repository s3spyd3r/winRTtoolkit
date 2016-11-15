using System.Threading.Tasks;
using winRTtoolkit.Model;

namespace winRTtoolkit.Helpers
{
    public interface IPicturePickerService
    {
        Task<PictureData> GetPicture();
    }
}