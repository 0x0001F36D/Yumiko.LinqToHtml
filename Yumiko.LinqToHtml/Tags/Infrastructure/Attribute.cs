﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Tags.Infrastructure
{
    public struct Attribute
    {
        public Attribute(string key , string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            this.Key = key;
            this.Value = value;
        }
        public Attribute(string key) : this(key, null) { }
        public string Value { get; private set; }
        public string Key { get; private set; }
        public override string ToString() => $"[{this.Key} : {this.Value ?? string.Empty}]";
    }
}
