using System;

namespace LW4.Models
{
    public class Product
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public bool HasCode
        {
            get
            {
                return !String.IsNullOrEmpty(Code);
            }
        }

        public bool HasName
        {
            get
            {
                return !String.IsNullOrEmpty(Name);
            }
        }

        public bool HasUri
        {
            get
            {
                return !String.IsNullOrEmpty(Uri);
            }
        }
    }
}
