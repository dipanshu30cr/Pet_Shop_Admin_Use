namespace Pet_Shop_Management.ImgAdder
{
    public class FileUpload:IPhotoImageProvider
    {
        readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _ihostingEnvironment;
        public FileUpload(Microsoft.AspNetCore.Hosting.IHostingEnvironment ihostingEnvironment)
        {
            _ihostingEnvironment = ihostingEnvironment;
        }

        public string UploadFileAsync(IFormFile file)
        {
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                string name = Path.GetFileNameWithoutExtension(file.FileName);
                string myfile = name + "_" + Guid.NewGuid().ToString() + ext;
                var filePath = Path.Combine(_ihostingEnvironment.ContentRootPath, @"wwwroot\Images", myfile);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
                return myfile;
            }
            else
            {
                return "none";
            }


        }
    }
}
