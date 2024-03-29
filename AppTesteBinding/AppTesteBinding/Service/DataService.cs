﻿using System;
using System.Collections.Generic;

namespace AppTesteBinding.Service
{
    public class DataService
    {
        public string ApiBaseAddress { get; set; } = "https://ocaribedemaragogi.com.br/api";

        public Uri BuilderUri(string uri, string parameter, string arg)
        {
            return new Uri($"{ApiBaseAddress}/{uri}?{parameter}={arg}");
        }

        public Uri BuilderUri(string uri)
        {
            return new Uri(string.Format("{0}/{1}/{2}", ApiBaseAddress, uri, string.Empty));
        }

        public Uri BuilderUri(Dictionary<string, string> keyValues) 
        {
            var uri = "";
            foreach (var item in keyValues)
            {
                uri += $"/{item.Key}={item.Value}";
            }

            return new Uri(string.Format("{0}/{1}", ApiBaseAddress, uri));
        }

        public DataService()
        {
        }
    }
}