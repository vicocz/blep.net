using Blep.Contract.Model;
using Blep.Framework.Presentation;

namespace Blep.Framework.Registry
{
    public class WellKnownPresentationFormats
    {
        public static readonly IPresentationFormat Unknown = new UnknownPresentationFormat();
        public static readonly IPresentationFormat Utf8 = new Utf8PresentationFormat();
        public static readonly IPresentationFormat Int32 = new Int32PresentationFormat();
        public static readonly IPresentationFormat Int16 = new Int16PresentationFormat();

        public static readonly IPresentationFormat UInt16 = new UInt16PresentationFormat();
    }
}
