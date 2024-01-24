using System;

namespace Entities.Exceptions
{
    public class PriceOutOfRangeBadRequestException : BadImageFormatException
    {
        public PriceOutOfRangeBadRequestException() 
            : base("Max price should be less than 1000 and greater than 10")
        {

        }
    }
}
