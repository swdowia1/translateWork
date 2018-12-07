﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translator
{
    public class Item
    {
        public string Root { get; set; }
        public string Name { get; set; }
        public string Property { get; set; }
        public string Translation { get; set; }
        public Item()
        {
            Root = "";
            Name = "";
            Property = "";
            Translation = "";

        }
        public Item(string _Root,string _Name,string _Property,string _Translation )
        {
            Root = _Root;
            Name = _Name;
            Property = _Property;
            Translation = _Translation;
        }
    }
}
