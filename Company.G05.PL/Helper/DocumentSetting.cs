namespace Company.G05.PL.Helper
{
    public class DocumentSetting
    {
        //1 UpLoadFile 

        public static string UploadFile (IFormFile file , string folderName)
        {
            //1.Get Location Folder Path 
            //string folderPath = $"D:\\MyValues\\Programs\\MVC\\Company.G05  Solution\\Company.G05.PL\\wwwroot\\Files\\{folderName} ";
            //string folderPath = Directory.GetCurrentDirectory()+$"\\wwwroot\\Files\\ " + $"{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory() ,$@"wwwroot\Files\{folderName}");
            
            //2.Get File Name ( Make it Unique )

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3.Get File Path 

            string filePath = Path.Combine(folderPath,fileName);

            //4. save file as stream : Data Per Time

           using var filestream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(filestream);

            return fileName; 
        }

        public static void Delete(string fileName , string folderName )
        {
            string filePath= Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\Files\{folderName}\{fileName}");
            if(File.Exists(filePath) ) 
                File.Delete(filePath);
        }

    }
}
