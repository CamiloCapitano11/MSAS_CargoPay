using System.Collections.Generic;

namespace MSAS_CargoPay.API.Errors
{
    public class APIValidationErrorResponce : APIResponce
    {
        public APIValidationErrorResponce() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
