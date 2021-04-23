using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using System.ComponentModel.DataAnnotations;
namespace Business.BookManagement.Dto
{
    public class BookDto : EntityDto<Guid?>
    {
        
        /// <summary>
        /// 书名
        /// </summary>
        [Required]
        public string Name { get; set; }

        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }

        
    }
}