﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string LinkName { get; set; }
        public double? Area { get; set; }
        public double? AreaFrom { get; set; }
        public double? AreaTo { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Facade { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string Description { get; set; }
        public string NumberOfStoreys { get; set; }
        public int? NumberOfBedrooms { get; set; }
        public int? NumberOfWCs { get; set; }
        public string DirectionName { get; set; }
        public string TypeOfPropertyName { get; set; }
        public string EvaluationStatusName { get; set; }
        public string LegalPaperName { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string ContactName { get; set; }
        public string EmailContact { get; set; }
        public string ContactPhone { get; set; }
        public bool Status { get; set; }
    }
}
