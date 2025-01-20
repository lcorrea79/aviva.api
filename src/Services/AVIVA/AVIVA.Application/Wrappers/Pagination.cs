using System.Collections.Generic;

namespace AVIVA.Application.Wrappers
{
    /// <summary>
    /// Pagination
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T>
    {
        /// <summary>
        /// Properties of CurrentPage
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// TotalPages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// TotalItems
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// List of result
        /// </summary>
        public List<T> Result { get; set; }
    }
}