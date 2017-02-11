using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Helper
{
    public class ReplacedToken
    {
        private readonly string randomKey;
        private readonly string value;
        public string Key => this.randomKey;
        public string Value => this.value;
        public ReplacedToken(string value)
        {
            this.randomKey = $"#[{Guid.NewGuid().ToString()}]";
            this.value = value;
        }
        
        public static bool Recovery(ReplacedToken token , ref string source)
        {
            return replace(token.Key, token.Value, ref source);
        }

        private static bool replace(string oldString , string newString,ref string source)
        {
            var length = oldString.Length;
            var index = source.IndexOf(oldString);
            if (index != -1)
            {
                source = source.Remove(index, length).Insert(index, newString);
                return true;
            }
            return false;
        }

        public static ReplacedToken Generate(string target , ref string source)
        {
            var token = (ReplacedToken)target;
            if (replace(token.Value, token.Key, ref source))
                return token;
            return default(ReplacedToken);
        }

        public static explicit operator ReplacedToken (string value)=>new ReplacedToken(value);
        public static explicit operator string(ReplacedToken token) => token.Key;
    }
}
