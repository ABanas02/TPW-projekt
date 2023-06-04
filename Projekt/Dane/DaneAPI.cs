using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace Dane
{
    public abstract class DaneAPI
    {
        public Boundary Boundary { get; protected set; }

        public static DaneAPI CreateApi(int width, int height)
        {
            return new DaneAPIBase(width, height);
        }
    }

    internal class DaneAPIBase : DaneAPI
    {
        public DaneAPIBase(int width, int height)
        {
            Boundary = new Boundary(width, height);
        }
    }
}