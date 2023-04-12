using System;

namespace Dane
{
    public abstract class DaneAPI
    {
        public static DaneAPI CreateApi()
        {
            return new DaneAPIBase();
        }
    }

    internal class DaneAPIBase : DaneAPI
    {

    }
}
