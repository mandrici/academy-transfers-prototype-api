﻿using Newtonsoft.Json;
using System;

namespace API.Models.Downstream.D365
{
    public class GetTrustsD365Model : BaseD365Model
    {
        [JsonProperty("accountid")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string TrustName { get; set; }

        [JsonProperty("sip_companieshousenumber")]
        public string CompaniesHouseNumber { get; set; }

        [JsonProperty("sip_trustreferencenumber")]
        public string TrustReferenceNumber { get; set; }

        [JsonProperty("address1_composite")]
        public string Address { get; set; }

        [JsonProperty("_sip_establishmenttypeid_value@OData.Community.Display.V1.FormattedValue")]
        public string EstablishmentType { get; set; }

        [JsonProperty("_sip_establishmenttypeid_value")]
        public Guid EstablishmentTypeId { get; set; }

        [JsonProperty("_sip_establismenttypegroupid_value@OData.Community.Display.V1.FormattedValue")]
        public string EstablishmentTypeGroup { get; set; }

        [JsonProperty("sip_ukprn")]
        public string Ukprn { get; set; }

        [JsonProperty("sip_upin")]
        public string Upin { get; set; }
    }

    public enum TrustStatusReason
    {
        Open = 907660000,
        OpenButProposedToClose = 907660002
    }
}
