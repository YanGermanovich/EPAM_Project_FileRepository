using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomExpressionVisitor
{
    public class CustomAttributeMapper : Attribute
    {
        public string MapTo { get; private set; }
        public CustomAttributeMapper(string mapTo)
        {
            MapTo = mapTo;  
        }

    }
}
