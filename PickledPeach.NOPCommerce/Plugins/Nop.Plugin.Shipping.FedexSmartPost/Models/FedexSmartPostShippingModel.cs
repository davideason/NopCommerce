using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;

namespace Nop.Plugin.Shipping.FedexSmartPost.Models
{
    public class FedexSmartPostShippingModel
    {
        public FedexSmartPostShippingModel()
        {
            CarrierServicesOffered = new List<string>();
            AvailableCarrierServices = new List<string>();
        }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.Url")]
        public string Url { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.Key")]
        public string Key { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.Password")]
        public string Password { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.AccountNumber")]
        public string AccountNumber { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.MeterNumber")]
        public string MeterNumber { get; set; }


        //DAE Added Hub ID
        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.HubID")]
        public string HubID { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.UseResidentialRates")]
        public bool UseResidentialRates { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.ApplyDiscounts")]
        public bool ApplyDiscounts { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.AdditionalHandlingCharge")]
        public decimal AdditionalHandlingCharge { get; set; }

        public IList<string> CarrierServicesOffered { get; set; }
        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.CarrierServices")]
        public IList<string> AvailableCarrierServices { get; set; }
        public string[] CheckedCarrierServices { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.PassDimensions")]
        public bool PassDimensions { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.PackingPackageVolume")]
        public int PackingPackageVolume { get; set; }

        public int PackingType { get; set; }
        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.PackingType")]
        public SelectList PackingTypeValues { get; set; }

        public int DropoffType { get; set; }
        [NopResourceDisplayName("Plugins.Shipping.FedexSmartPost.Fields.DropoffType")]
        public SelectList AvailableDropOffTypes { get; set; }

    }
}