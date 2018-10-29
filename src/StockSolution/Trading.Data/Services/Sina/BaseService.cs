using Qin.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.Services.Sina
{
    public abstract class BaseService
    {
        protected HtmlService _service;

        public BaseService(HtmlService service)
        {
            this._service = service;
        }
    }
}
