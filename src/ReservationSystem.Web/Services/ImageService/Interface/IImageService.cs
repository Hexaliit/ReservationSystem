namespace ReservationSystem.Web.Services.ImageService.Interface
{
    public interface IImageService
    {
        string Save(IFormFile file);
        void Delete(string path);
    }
}
