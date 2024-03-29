﻿using System;
using System.Collections.Generic;
using System.Text;
using static DataLayer.Models.ModelItem;

namespace ServiceLayer
{
    public abstract class AddModelItemDto : AddEditProductDto
    {
        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }
    }
}
