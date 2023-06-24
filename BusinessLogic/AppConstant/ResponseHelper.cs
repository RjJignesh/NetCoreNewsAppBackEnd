using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.AppConstant
{
    public class ResponseHelper
    {
        #region Public Methods

        /// <summary>
        /// Successes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The response model
        /// </returns>
        public static ResponseDto Success(object data)
        {
            ResponseDto response = new ResponseDto();
            response.Status = true;
            response.Data = data;
            response.Message = "Success";
            return response;
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// The response model
        /// </returns>
        public static ResponseDto Error(string message, object data = null)
        {
            ResponseDto response = new ResponseDto();
            response.Status = false;
            response.Data = data;
            response.Message = message;
            return response;
        }

        #endregion
    }
}
