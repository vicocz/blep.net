using Blep.Framework.Model;
using System;

namespace Blep.Contract.Model
{
    public class KnownServiceInfo : IServiceInfo
    {
        public IServiceTemplate Template { get; }

        public string Name => Template.Name;

        public Guid Uuid => Template.Uuid;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="template">Requiered service template</param>
        public KnownServiceInfo(IServiceTemplate template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }
    }
}
