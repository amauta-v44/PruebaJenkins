﻿using System;
using System.Net;

namespace Devsoft.Api.Middlewares
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }

        public HttpResponseException(HttpStatusCode status, string message)
        {
            Status = (int) status;
            Value = new {message};
        }
    }
}