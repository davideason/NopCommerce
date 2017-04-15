using System;
using System.Text;
using System.Web.Mvc;
using Nop.Plugin.Shipping.FedexSmartPost.Domain;
using Nop.Plugin.Shipping.FedexSmartPost.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Shipping.FedexSmartPost.Controllers
{
    [AdminAuthorize]
    public class ShippingFedexSmartPostController : BasePluginController
    {
        private readonly FedexSmartPostSettings _fedexSettings;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public ShippingFedexSmartPostController(FedexSmartPostSettings fedexSettings,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            this._fedexSettings = fedexSettings;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }

        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new FedexSmartPostShippingModel();
            model.Url = _fedexSettings.Url;
            model.Key = _fedexSettings.Key;
            model.Password = _fedexSettings.Password;
            model.AccountNumber = _fedexSettings.AccountNumber;
            model.MeterNumber = _fedexSettings.MeterNumber;
            model.DropoffType = Convert.ToInt32(_fedexSettings.DropoffType);
            model.AvailableDropOffTypes = _fedexSettings.DropoffType.ToSelectList();
            model.UseResidentialRates = _fedexSettings.UseResidentialRates;
            model.ApplyDiscounts = _fedexSettings.ApplyDiscounts;
            model.AdditionalHandlingCharge = _fedexSettings.AdditionalHandlingCharge;
            model.PackingPackageVolume = _fedexSettings.PackingPackageVolume;
            model.PackingType = Convert.ToInt32(_fedexSettings.PackingType);
            model.PackingTypeValues = _fedexSettings.PackingType.ToSelectList();
            model.PassDimensions = _fedexSettings.PassDimensions;


            var services = new FedexSmartPostServices();
            // Load service names
            string carrierServicesOfferedDomestic = _fedexSettings.CarrierServicesOffered;
            foreach (string service in services.Services)
                model.AvailableCarrierServices.Add(service);

            if (!String.IsNullOrEmpty(carrierServicesOfferedDomestic))
                foreach (string service in services.Services)
                {
                    string serviceId = FedexSmartPostServices.GetServiceId(service);
                    if (!String.IsNullOrEmpty(serviceId) && !String.IsNullOrEmpty(carrierServicesOfferedDomestic))
                    {
                        if (carrierServicesOfferedDomestic.Contains(serviceId))
                            model.CarrierServicesOffered.Add(service);
                    }
                }

            return View("~/Plugins/Shipping.FedexSmartPost/Views/ShippingFedex/Configure.cshtml", model);
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult Configure(FedexSmartPostShippingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }

            //save settings
            _fedexSettings.Url = model.Url;
            _fedexSettings.Key = model.Key;
            _fedexSettings.Password = model.Password;
            _fedexSettings.AccountNumber = model.AccountNumber;
            _fedexSettings.MeterNumber = model.MeterNumber;
            _fedexSettings.DropoffType = (DropoffType)model.DropoffType;
            _fedexSettings.UseResidentialRates = model.UseResidentialRates;
            _fedexSettings.ApplyDiscounts = model.ApplyDiscounts;
            _fedexSettings.AdditionalHandlingCharge = model.AdditionalHandlingCharge;
            _fedexSettings.PackingPackageVolume = model.PackingPackageVolume;
            _fedexSettings.PackingType = (PackingType)model.PackingType;
            _fedexSettings.PassDimensions = model.PassDimensions;



            // Save selected services
            var carrierServicesOfferedDomestic = new StringBuilder();
            int carrierServicesDomesticSelectedCount = 0;
            if (model.CheckedCarrierServices != null)
            {
                foreach (var cs in model.CheckedCarrierServices)
                {
                    carrierServicesDomesticSelectedCount++;
                    string serviceId = FedexSmartPostServices.GetServiceId(cs);
                    if (!String.IsNullOrEmpty(serviceId))
                        carrierServicesOfferedDomestic.AppendFormat("{0}:", serviceId);
                }
            }
            // Add default options if no services were selected
            if (carrierServicesDomesticSelectedCount == 0)
                _fedexSettings.CarrierServicesOffered = "FEDEX_2_DAY:PRIORITY_OVERNIGHT:FEDEX_GROUND:GROUND_HOME_DELIVERY:INTERNATIONAL_ECONOMY:SMART_POST";
            else
                _fedexSettings.CarrierServicesOffered = carrierServicesOfferedDomestic.ToString();


            _settingService.SaveSetting(_fedexSettings);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
    }
}
