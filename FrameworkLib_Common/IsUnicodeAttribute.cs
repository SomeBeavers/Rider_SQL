using System;
using System.Linq;

namespace FrameworkLib_Common
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class IsUnicode : Attribute
    {
        public IsUnicode(bool isUnicode)
        {
            Unicode = isUnicode;
        }

        public bool Unicode { get; set; }
    }
}