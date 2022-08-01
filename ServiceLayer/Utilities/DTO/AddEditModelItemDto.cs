using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static DataLayer.Models.ModelItem;

namespace ServiceLayer
{
    public abstract class AddEditModelItemDto : AddEditProductDto
    {
        [Required]
        public EScale Scale { get; set; }
        [Required]
        public EEpoch Epoch { get; set; }
    }
}
