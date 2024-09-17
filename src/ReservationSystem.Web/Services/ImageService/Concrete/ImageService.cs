using ReservationSystem.Web.Services.ImageService.Interface;

namespace ReservationSystem.Web.Services.ImageService.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment webHost;

        public ImageService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        public void Delete(string path)
        {
            var file = webHost.WebRootPath + path;
            System.IO.File.Delete(file);
        }

        public string Save(IFormFile file)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            fileName += Path.GetExtension(file.FileName);
            var filePath = webHost.WebRootPath + "/images/";
            using (var stream = System.IO.File.Create(filePath + fileName))
            {
                file.CopyTo(stream);
            }
            return  "/images/" + fileName;
        }
    }
}
