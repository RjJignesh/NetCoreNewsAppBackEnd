namespace BusinessLogic.AppConstant
{
    /// <summary>
    /// This class use for define some common values uses in applicatio
    /// </summary>
    public class ApplicationHelper
    {
        /// <summary>
        /// Return file path with current year/month/date directories
        /// </summary>
        public static string FilePath
        {
            get
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "NewsImages", Path.Combine(DateTime.UtcNow.Year.ToString(), DateTime.UtcNow.ToString("MMMM"), DateTime.UtcNow.ToString("ddMMyyyy")));
            }
        }

        /// <summary>
        /// ImageNameLenth  use for validate image name.
        /// </summary>
        public static int ImageNameLenth { get { return 150; } }

        /// <summary>
        /// Image name lenth validation msg
        /// </summary>
        public static string ImageNameLenthMsg { get { return "Imagefile name is not valid! please verify that image name is less than 150 char."; } }



        /// <summary>
        /// Common exeption message if any exception occured.
        /// </summary>
        public static string ExceptionMsg { get { return "Something went wrong."; } }


        public static string ImageValidationMsg { get { return "Imagefile Extension is not valid! please upload only jpg/jpeg/png file."; } }


        /// <summary>
        /// Common save message if any exception occured.
        /// </summary>
        public static string NewsSaveSuccess
        {
            get
            {
                return "News save successfully";
            }
        }
    }
}
