using System;

namespace DrawGantt.Drawing.Local
{
    public class LocalLanguageFactory
    {
        public static LocalLanguageBase CreateInstance(string localCode)
        {
            switch (localCode.ToLower())
            {
                case "cn":
                    return new LocalLanguageCN();
                case "en":
                    return new LocalLanguageEN();
                default:
                    throw new NotSupportedException("not support local code");
            }
        }
    }
}
