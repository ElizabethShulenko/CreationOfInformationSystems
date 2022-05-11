using System;

namespace LW4.Models
{
    public class Category
    {
        public string Name { get; set; }

        public string Uri { get; set; }

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
