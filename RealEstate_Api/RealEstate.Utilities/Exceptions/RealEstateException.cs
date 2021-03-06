﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Utilities.Exceptions
{
    public class RealEstateException : Exception
    {
        public RealEstateException()
        {
        }

        public RealEstateException(string message)
            : base(message)
        {
        }

        public RealEstateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
