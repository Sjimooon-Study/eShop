using System;
using System.Collections.Generic;
using System.Text;
using static DataLayer.Models.ModelItem;

namespace ServiceLayer
{
    public abstract class AddModelItemDto : AddProductDto
    {
        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }
    }
}
