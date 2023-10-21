using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Application.DTOs
{
    /// <summary>
    /// مدل برای دراپدون
    /// </summary>
    public class SelectListDTO
    {
        #region Constructors
        public SelectListDTO() { }

        public SelectListDTO(long id, string title)
        {
            Id = id;
            Title = title;
        }
        #endregion



        #region Properties
        /// <summary>
        /// شناسه
        /// </summary>
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        #endregion



    }
}
