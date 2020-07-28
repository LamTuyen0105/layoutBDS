using RealEstate.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using Microsoft.AspNetCore.Http;
using RealEstate.ViewModels.Service.PropertyImage;
using RealEstate.Application.Common;
using RealEstate.Data.Entity;
using RealEstate.Utilities.Exceptions;
using System.Net.Http.Headers;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Http;
using RealEstate.ViewModels.System.Users;
using Newtonsoft.Json;

namespace RealEstate.Application.Service.NewsManagers
{
    public class PropertyOwnerService : IPropertyOwnerService
    {
        private readonly RealEstateDbContext _context;
        private readonly IStorageService _storageService;

        public PropertyOwnerService (RealEstateDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int propertyId, PropertyImageCreateRequest request)
        {
            var propertyImage = new PropertyImage()
            {
                Caption = "Image",
                DateCreated = DateTime.Now,
                IsDefault = false,
                PropertyId = propertyId,
                SortOrder = 2
            };

            if (request.ImageFile != null)
            {
                propertyImage.LinkName = await this.SaveFile(request.ImageFile);
                propertyImage.FileSize = request.ImageFile.Length;
            }
            _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync();
            return propertyImage.Id;
        }

        public async Task<int> Create(PropertyCreateRequest request)
        {
            var property = new Property()
            {
                Title = request.Title,
                ApartmentNumber = request.ApartmentNumber,
                StreetNames = request.StreetNames,
                WardId = request.WardId,
                Area = request.Area,
                AreaFrom = request.AreaFrom,
                AreaTo = request.AreaTo,
                Length = request.Length,
                Width = request.Width,
                Facade = request.Facade,
                Price = request.Price,
                PriceFrom = request.PriceFrom,
                PriceTo = request.PriceTo,
                Description = request.Description,
                EvaluationStatusId = request.EvaluationStatusId,
                LegalPapersId = request.LegalPapersId,
                TypeOfPropertyId = request.TypeOfPropertyId,
                TypeOfTransactionId = request.TypeOfTransactionId,
                StartDate = DateTime.Now,
                EndDate = request.EndDate,
                NumberOfStoreys = request.NumberOfStoreys,
                NumberOfBedrooms = request.NumberOfBedrooms,
                NumberOfWCs = request.NumberOfWCs,
                HouseDirectionId = request.HouseDirectionId,
                Lat = request.Lat,
                Lng = request.Lng,
                ContactName = request.ContactName,
                EmailContact = request.EmailContact,
                ContactPhone = request.ContactPhone,
                UserID = request.UserID,
            };
            if (request.ThumbnailImage != null && request.ThumbnailImage.Count > 0)
            {
                property.PropertyImages = new List<PropertyImage>();
                foreach (var item in request.ThumbnailImage)
                {
                    if (property.PropertyImages.Count == 0)
                    {
                        property.PropertyImages.Add(new PropertyImage()
                        {
                            Caption = "Thumbnail image",
                            DateCreated = DateTime.Now,
                            FileSize = item.Length,
                            LinkName = await this.SaveFile(item),
                            IsDefault = true,
                            SortOrder = 1
                        });
                    }
                    else
                    {
                        property.PropertyImages.Add(new PropertyImage()
                        {
                            Caption = "Thumbnail image",
                            DateCreated = DateTime.Now,
                            FileSize = item.Length,
                            LinkName = await this.SaveFile(item),
                            IsDefault = false,
                            SortOrder = 2
                        });
                    }
                }
            }
            var IsValid_capcha = await validateCaptchaAsync(request.checkReCaptCha ?? "");
            if (!IsValid_capcha)
            {
                return 0;
            }
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property.Id;
        }

        public async Task<int> Delete(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy tin đăng: {propertyId}");
            var images = _context.PropertyImages.Where(i => i.PropertyId == propertyId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.LinkName);
            }

            _context.Properties.Remove(property);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<PropertyViewModel>> GetAllPaging(GetPostPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == true && p.IsDelete == false
                        select new { p, pc, ds, tt, tp, e, d, j, l };
            if (request.TypeOfTransactionIds.Count > 0)
                query = query.Where(x => request.TypeOfTransactionIds.Contains(x.tt.Id));
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    ProvinceName = x.pc.Name,
                    DistrictName = x.ds.Name,
                    LinkName = x.j.LinkName,
                    LegalPaperName = x.l.TypeOfLegalPapers,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    TypeOfPropertyName = x.tp.TypeOfPropertyName,
                    EvaluationStatusName = x.e.EvaluationStatusName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<PropertyViewModel>> GetById(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            var propertyImage = await _context.PropertyImages.FirstOrDefaultAsync(x => x.PropertyId == propertyId);

            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join ej in _context.EvaluationStatuses on p.EvaluationStatusId equals ej.Id into evaluationjoined
                        from e in evaluationjoined.DefaultIfEmpty()
                        join dj in _context.Directions on p.HouseDirectionId equals dj.Id into directionjoined
                        from d in directionjoined.DefaultIfEmpty()
                        join lj in _context.LegalPapers on p.LegalPapersId equals lj.Id into legaljoined
                        from l in legaljoined.DefaultIfEmpty()
                        join dsj in _context.Districts on w.DistrictId equals dsj.Id into districtjoined
                        from ds in districtjoined.DefaultIfEmpty()
                        join pcj in _context.Provinces on ds.ProvinceId equals pcj.Id into provincejoined
                        from pc in provincejoined.DefaultIfEmpty()
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Id == propertyId
                        select new { p, pc, ds, tt, tp, e, d, j, l };
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone
            }).ToListAsync();
            return data;
        }

        public async Task<PropertyImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.PropertyImages.FindAsync(imageId);
            if (image == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");

            var viewModel = new PropertyImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                LinkName = image.LinkName,
                IsDefault = image.IsDefault,
                PropertyId = image.PropertyId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<PropertyImageViewModel>> GetListImages(int propertyId)
        {
            return await _context.PropertyImages.Where(x => x.PropertyId == propertyId)
                .Select(i => new PropertyImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    LinkName = i.LinkName,
                    IsDefault = i.IsDefault,
                    PropertyId = i.PropertyId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var propertyImage = await _context.PropertyImages.FindAsync(imageId);
            if (propertyImage == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");
            _context.PropertyImages.Remove(propertyImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(int propertyId, PropertyUpdateRequest request)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã tin: {propertyId}");
            property.Title = request.Title;
            property.ApartmentNumber = request.ApartmentNumber;
            property.StreetNames = request.StreetNames;
            property.WardId = request.WardId;
            property.Area = request.Area;
            property.AreaFrom = request.AreaFrom;
            property.AreaTo = request.AreaTo;
            property.Length = request.Length;
            property.Width = request.Width;
            property.Facade = request.Facade;
            property.Price = request.Price;
            property.PriceFrom = request.PriceFrom;
            property.PriceTo = request.PriceTo;
            property.Description = request.Description;
            property.EvaluationStatusId = request.EvaluationStatusId;
            property.LegalPapersId = request.LegalPapersId;
            property.TypeOfPropertyId = request.TypeOfPropertyId;
            property.TypeOfTransactionId = request.TypeOfTransactionId;
            property.EndDate = request.EndDate;
            property.NumberOfStoreys = request.NumberOfStoreys;
            property.NumberOfBedrooms = request.NumberOfBedrooms;
            property.NumberOfWCs = request.NumberOfWCs;
            property.HouseDirectionId = request.HouseDirectionId;
            property.Lat = request.Lat;
            property.Lng = request.Lng;
            property.ContactName = request.ContactName;
            property.EmailContact = request.EmailContact;
            property.ContactPhone = request.ContactPhone;
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.PropertyImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.PropertyId == propertyId);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.LinkName = await this.SaveFile(request.ThumbnailImage);
                    _context.PropertyImages.Update(thumbnailImage);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, PropertyImageUpdateRequest request)
        {
            var propertyImage = await _context.PropertyImages.FindAsync(imageId);
            if (propertyImage == null)
                throw new RealEstateException($"Không thể tìm thấy hình: {imageId}");

            if (request.ImageFile != null)
            {
                propertyImage.LinkName = await this.SaveFile(request.ImageFile);
                propertyImage.FileSize = request.ImageFile.Length;
            }
            _context.PropertyImages.Update(propertyImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<PropertyViewModel>> GetAll()
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == true && p.IsDelete == false
                        select new { p, pc, ds, tt, tp, e, d, j, l };
            var data = await query.Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    ProvinceName = x.pc.Name,
                    DistrictName = x.ds.Name,
                    LegalPaperName = x.l.TypeOfLegalPapers,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    TypeOfPropertyName = x.tp.TypeOfPropertyName,
                    EvaluationStatusName = x.e.EvaluationStatusName,
                    LinkName = x.j.LinkName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<PropertyViewModel>> GetAllByTypeOfTransaction(GetViewPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == true && p.IsDelete == false
                        select new { p, pc, ds, tt, tp, e, d, j, l };
            if (request.typeOfTransactionId.HasValue && request.typeOfTransactionId >0)
                query = query.Where(x => x.p.TypeOfTransactionId == request.typeOfTransactionId);

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    ProvinceName = x.pc.Name,
                    DistrictName = x.ds.Name,
                    LinkName = x.j.LinkName,
                    LegalPaperName = x.l.TypeOfLegalPapers,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    TypeOfPropertyName = x.tp.TypeOfPropertyName,
                    EvaluationStatusName = x.e.EvaluationStatusName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone,
                    Status = x.p.Status
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<PagedResult<PropertyViewModel>> Find(GetFindPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == true && p.IsDelete == false
                        select new { p, w, tt, d, tp, e, ds, pc, j, l };
            if (request.typeOfTransactionId > 0)
                query = query.Where(x => x.p.TypeOfTransactionId == request.typeOfTransactionId);
            if (request.typeOfPropertyId.HasValue && request.typeOfPropertyId > 0)
                query = query.Where(x => x.p.TypeOfPropertyId == request.typeOfPropertyId);
            if (request.price.HasValue && request.price > 0)
                query = query.Where(x => x.p.Price <= request.price);
            if (request.area.HasValue && request.area > 0)
                query = query.Where(x => x.p.Area <= request.area);
            if (request.houseDirectionId.HasValue && request.houseDirectionId > 0)
                query = query.Where(x => x.p.HouseDirectionId == request.houseDirectionId);
            if (request.numberOfBedrooms.HasValue && request.numberOfBedrooms > 0)
                query = query.Where(x => x.p.NumberOfBedrooms <= request.numberOfBedrooms);
            if (request.evaluationStatusId.HasValue && request.evaluationStatusId > 0)
                query = query.Where(x => x.p.EvaluationStatusId == request.evaluationStatusId);
            if (request.provinceId.HasValue && request.provinceId > 0)
                query = query.Where(x => x.pc.Id == request.provinceId);
            if (request.districtId.HasValue && request.districtId > 0)
                query = query.Where(x => x.ds.Id == request.districtId);
            if (request.wardId.HasValue && request.wardId > 0)
                query = query.Where(x => x.p.WardId == request.wardId);

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    ProvinceName = x.pc.Name,
                    DistrictName = x.ds.Name,
                    LinkName = x.j.LinkName,
                    LegalPaperName = x.l.TypeOfLegalPapers,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    TypeOfPropertyName = x.tp.TypeOfPropertyName,
                    EvaluationStatusName = x.e.EvaluationStatusName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<PagedResult<PropertyViewModel>> GetPaging(GetPropertyPagingRequest request)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Status == true && p.IsDelete == false
                        select new { p, ds, pc, tt, tp, e, d, j, l };

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new PropertyViewModel()
                {
                    Id = x.p.Id,
                    Title = x.p.Title,
                    ProvinceName = x.pc.Name,
                    DistrictName = x.ds.Name,
                    LinkName = x.j.LinkName,
                    LegalPaperName = x.l.TypeOfLegalPapers,
                    Area = x.p.Area,
                    AreaFrom = x.p.AreaFrom,
                    AreaTo = x.p.AreaTo,
                    Length = x.p.Length,
                    Width = x.p.Width,
                    Facade = x.p.Facade,
                    Price = x.p.Price,
                    PriceFrom = x.p.PriceFrom,
                    PriceTo = x.p.PriceTo,
                    Description = x.p.Description,
                    NumberOfStoreys = x.p.NumberOfStoreys,
                    NumberOfBedrooms = x.p.NumberOfBedrooms,
                    NumberOfWCs = x.p.NumberOfWCs,
                    DirectionName = x.d.DirectionName,
                    TypeOfPropertyName = x.tp.TypeOfPropertyName,
                    EvaluationStatusName = x.e.EvaluationStatusName,
                    Lat = x.p.Lat,
                    Lng = x.p.Lng,
                    ContactName = x.p.ContactName,
                    EmailContact = x.p.EmailContact,
                    ContactPhone = x.p.ContactPhone
                }).ToListAsync();
            var pagedResult = new PagedResult<PropertyViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<PropertyViewModel>> GetHot()
        {
            var query = (from p in _context.Properties
                         join w in _context.Wards on p.WardId equals w.Id
                         join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                         join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                         join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                         join d in _context.Directions on p.HouseDirectionId equals d.Id
                         join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                         join ds in _context.Districts on w.DistrictId equals ds.Id
                         join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                         join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                         from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                         where p.Status == true && p.IsDelete == false
                         select new { p, ds, pc, tt, tp, e, d, j, l }).Take(8);
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone
            }).ToListAsync();
            return data;
        }

        public async Task<List<PropertyViewModel>> GetHotSell()
        {
            var query = (from p in _context.Properties
                         join w in _context.Wards on p.WardId equals w.Id
                         join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                         join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                         join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                         join d in _context.Directions on p.HouseDirectionId equals d.Id
                         join ds in _context.Districts on w.DistrictId equals ds.Id
                         join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                         join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                         join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                         from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                         where p.Status == true && tt.Id == 1 && p.IsDelete == false
                         select new { p, ds, pc, tt, tp, e, d, j, l }).Take(8);
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone
            }).ToListAsync();
            return data;
        }

        public async Task<List<PropertyViewModel>> GetHotRent()
        {
            var query = (from p in _context.Properties
                         join w in _context.Wards on p.WardId equals w.Id
                         join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                         join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                         join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                         join d in _context.Directions on p.HouseDirectionId equals d.Id
                         join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                         join ds in _context.Districts on w.DistrictId equals ds.Id
                         join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                         join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                         from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                         where p.Status == true && tt.Id == 2 && p.IsDelete == false
                         select new { p, ds, pc, tt, tp, e, d, j, l }).Take(8);
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone
            }).ToListAsync();
            return data;
        }

        public async Task<List<PropertyImageViewModel>> GetImageDefault(int propertyId)
        {
            return await _context.PropertyImages.Where(x => x.PropertyId == propertyId && x.IsDefault == true)
                .Select(i => new PropertyImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    LinkName = i.LinkName,
                    IsDefault = i.IsDefault,
                    PropertyId = i.PropertyId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<List<PropertyImageViewModel>> GetImageNonDefault(int propertyId)
        {
            return await _context.PropertyImages.Where(x => x.PropertyId == propertyId && x.IsDefault == false)
                .Select(i => new PropertyImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    LinkName = i.LinkName,
                    IsDefault = i.IsDefault,
                    PropertyId = i.PropertyId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<List<PropertyViewModel>> GetByUserId(string userId)
        {
            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join e in _context.EvaluationStatuses on p.EvaluationStatusId equals e.Id
                        join d in _context.Directions on p.HouseDirectionId equals d.Id
                        join l in _context.LegalPapers on p.LegalPapersId equals l.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.UserID == Guid.Parse(userId) && p.IsDelete == false
                        select new { p, pc, ds, tt, tp, e, d, j, l };
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone,
                Status = x.p.Status
            }).ToListAsync();
            return data;
        }

        public async Task<List<PropertyForPutViewModel>> GetPutById(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            var propertyImage = await _context.PropertyImages.FirstOrDefaultAsync(x => x.PropertyId == propertyId);

            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join ds in _context.Districts on w.DistrictId equals ds.Id
                        join pc in _context.Provinces on ds.ProvinceId equals pc.Id
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Id == propertyId
                        select new { p, pc, ds, j};           
            var data = await query.Select(x => new PropertyForPutViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceId = x.pc.Id,
                DistrictId = x.ds.Id,
                WardId = x.p.WardId,
                LegalPapersId = x.p.LegalPapersId,
                ApartmentNumber = x.p.ApartmentNumber,
                StreetNames = x.p.StreetNames,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                HouseDirectionId = x.p.HouseDirectionId,
                TypeOfPropertyId = x.p.TypeOfPropertyId,
                TypeOfTransactionId = x.p.TypeOfTransactionId,
                EvaluationStatusId = x.p.EvaluationStatusId,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone,
                StartDate = x.p.StartDate,
                EndDate = x.p.EndDate,
                UserID = x.p.UserID,
                Status = x.p.Status
            }).ToListAsync();
            return data;
        }

        public async Task<List<PropertyViewModel>> GetElasticSearchById(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            var propertyImage = await _context.PropertyImages.FirstOrDefaultAsync(x => x.PropertyId == propertyId);

            var query = from p in _context.Properties
                        join w in _context.Wards on p.WardId equals w.Id
                        join tt in _context.TypeOfTransactions on p.TypeOfTransactionId equals tt.Id
                        join tp in _context.TypeOfProperties on p.TypeOfPropertyId equals tp.Id
                        join ej in _context.EvaluationStatuses on p.EvaluationStatusId equals ej.Id into evaluationjoined
                        from e in evaluationjoined.DefaultIfEmpty()
                        join dj in _context.Directions on p.HouseDirectionId equals dj.Id into directionjoined
                        from d in directionjoined.DefaultIfEmpty()
                        join lj in _context.LegalPapers on p.LegalPapersId equals lj.Id into legaljoined
                        from l in legaljoined.DefaultIfEmpty()
                        join dsj in _context.Districts on w.DistrictId equals dsj.Id into districtjoined
                        from ds in districtjoined.DefaultIfEmpty()
                        join pcj in _context.Provinces on ds.ProvinceId equals pcj.Id into provincejoined
                        from pc in provincejoined.DefaultIfEmpty()
                        join i in _context.PropertyImages on p.Id equals i.PropertyId into joined
                        from j in (from i in joined where i.IsDefault == true select i).DefaultIfEmpty()
                        where p.Id == propertyId
                        select new { p, tt, tp, e, j, l, d, pc, ds };
            var data = await query.Select(x => new PropertyViewModel()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                ProvinceName = x.pc.Name,
                DistrictName = x.ds.Name,
                LegalPaperName = x.l.TypeOfLegalPapers,
                Area = x.p.Area,
                AreaFrom = x.p.AreaFrom,
                AreaTo = x.p.AreaTo,
                Length = x.p.Length,
                Width = x.p.Width,
                Facade = x.p.Facade,
                Price = x.p.Price,
                PriceFrom = x.p.PriceFrom,
                PriceTo = x.p.PriceTo,
                Description = x.p.Description,
                NumberOfStoreys = x.p.NumberOfStoreys,
                NumberOfBedrooms = x.p.NumberOfBedrooms,
                NumberOfWCs = x.p.NumberOfWCs,
                DirectionName = x.d.DirectionName,
                TypeOfPropertyName = x.tp.TypeOfPropertyName,
                EvaluationStatusName = x.e.EvaluationStatusName,
                LinkName = x.j.LinkName,
                Lat = x.p.Lat,
                Lng = x.p.Lng,
                ContactName = x.p.ContactName,
                EmailContact = x.p.EmailContact,
                ContactPhone = x.p.ContactPhone,
                Status = x.p.Status
            }).ToListAsync();
            return data;
        }

        public async Task<bool> UpdateIsDelete(int propertyId, bool delete)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã: {propertyId}");
            property.IsDelete = delete;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> validateCaptchaAsync(string captchares)
        {
            object jres = new object();
            if (String.IsNullOrEmpty(captchares))
            {
                return false;
            }
            string secret_key = "6LcoSAEVAAAAAKkUicWjcqefDkcoKuEOz-FMFGv9";
            if (string.IsNullOrEmpty(secret_key))
            {
                return false;
            }
            var content = new FormUrlEncodedContent(new[]
              {
                new KeyValuePair<string, string>("secret",  secret_key),
                new KeyValuePair<string, string>("response", captchares)
              });
            HttpClient client = new HttpClient();
            var res = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            captchaResult captchaRes = JsonConvert.DeserializeObject<captchaResult>(res.Content.ReadAsStringAsync().Result);

            if (!captchaRes.success)
            {
                return false;
            }
            return true;
        }
    }
}
