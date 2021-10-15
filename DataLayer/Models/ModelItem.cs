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
            [Display(Name = "HO (1:87)")]
            HO = 0,
            [Display(Name = "TT (1:120)")]
            TT,
            [Display(Name = "N (1∶160)")]
            N
        }

        public enum EEpoch
        {
            [Display(Name = "I")]
            I = 0,
            [Display(Name = "II")]
            II,
            [Display(Name = "III")]
            III,
            [Display(Name = "IV")]
            IV,
            [Display(Name = "V")]
            V,
            [Display(Name = "VI")]
            VI
        }

        public EScale Scale { get; set; }
        public EEpoch Epoch { get; set; }
    }
}
