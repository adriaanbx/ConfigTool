using System;
using System.Collections.Generic;

namespace ConfigTool.Models
{
    public partial class ReadWriteType
    {

        public bool ReadOnly
        {
            get
            {
                return Id == 1;
            }
        }
        public bool WriteOnly
        {
            get
            {
                return Id == 2;
            }
        }

        public bool ReadWrite
        {
            get
            {
                return Id == 3;
            }
        }


        /// <summary>Created by: GHRS
        /// Date: 29/04/2020
        /// Description: Gets a value indicating whether this <see cref="T:ConfigTool.Models.ReadWriteType" /> is read (means Read on ReadWrite).</summary>
        /// <value>
        ///   <c>true</c> if read; otherwise, <c>false</c>.</value>
        public bool Read
        {
            get
            {
                return ReadOnly || ReadWrite;
            }
        }

        public bool Write {
            get {
                return WriteOnly || ReadWrite;
            }
        }
    }
}
