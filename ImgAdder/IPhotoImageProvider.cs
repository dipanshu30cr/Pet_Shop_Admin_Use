namespace Pet_Shop_Management.ImgAdder
{
    public interface IPhotoImageProvider
    {
        string UploadFileAsync(IFormFile file);
    }
}
