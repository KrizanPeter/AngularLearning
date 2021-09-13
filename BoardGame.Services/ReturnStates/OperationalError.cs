using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BoardGame.Services.ReturnStates
{
    public class OperationalError
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public OperationalError(string code, string description)
        {
            Code = code;
            Description = description;
        }
        public OperationalError(HttpStatusCode code, string description)
        {
            Code = code.GetTypeCode().ToString();
            Description = description;
        }
    }
}
