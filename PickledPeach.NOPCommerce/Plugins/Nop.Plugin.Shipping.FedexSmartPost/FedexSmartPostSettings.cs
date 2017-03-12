
using Nop.Core.Configuration;

namespace Nop.Plugin.Shipping.FedexSmartPost
{
    public class FedexSmartPostSettings : ISettings
    {
        public string Url { get; set; }

        public string Key { get; set; }

        public string Password { get; set; }

        public string AccountNumber { get; set; }

        public string MeterNumber { get; set; }

        public DropoffType DropoffType { get; set; }

        public bool UseResidentialRates { get; set; }

        public bool ApplyDiscounts { get; set; }

        public decimal AdditionalHandlingCharge { get; set; }

        public string CarrierServicesOffered { get; set; }

        public bool PassDimensions { get; set; }

        public int PackingPackageVolume { get; set; }

        public PackingType PackingType { get; set; }

        //DAE - TODO HUB ID for Smartpost
        public string HubID { get; set; }

        //DAE - TODO Packaging Type ? (maybe default to Your_Packaging
    }
}