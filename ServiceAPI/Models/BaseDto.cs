using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceAPI.Models
{

   public class BaseDto<T>
    {
        ///// <summary>
        ///// Gets or sets a value indicating whether this <see cref="BaseDto{T}"/> is success.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if success; otherwise, <c>false</c>.
        ///// </value>
        public bool Success { get; set; }

        ///// <summary>
        ///// Gets or sets the message.
        ///// </summary>
        ///// <value>
        ///// The message.
        ///// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }
    }
}