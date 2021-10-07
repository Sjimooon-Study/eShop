using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public abstract class ModelItem : Product
    {
        public enum EScale
        {
            HO = 1,
            HOe,
            TT
        }

        public enum EEpoch
        {
            I = 1,
            II,
            III,
            IV,
            V,
            VI
        }

        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }
    }
}
