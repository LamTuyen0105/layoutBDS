﻿using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public interface IPropertyOwnerService
    {
        Task<int> Create(PropertyCreateRequest request);

        Task<int> Update(int propertyId, PropertyUpdateRequest request);

        Task<int> Delete(int propertyId);

        Task<List<PropertyViewModel>> GetById(int propertyId);

        Task<List<PropertyViewModel>> GetElasticSearchById(int propertyId);

        Task<List<PropertyForPutViewModel>> GetPutById(int propertyId);

        Task <PagedResult<PropertyViewModel>> Find(GetFindPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetAllPaging(GetPostPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetPaging(GetPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetAllByTypeOfTransaction(GetViewPropertyPagingRequest request);

        Task<List<PropertyViewModel>> GetAll();

        Task<List<PropertyViewModel>> GetByUserId(string userId);

        Task<List<PropertyViewModel>> GetHot();

        Task<List<PropertyViewModel>> GetHotSell();

        Task<List<PropertyViewModel>> GetHotRent();

        Task<int> AddImage(int propertyId, PropertyImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, PropertyImageUpdateRequest request);

        Task<PropertyImageViewModel> GetImageById(int imageId);

        Task<List<PropertyImageViewModel>> GetListImages(int propertyId);

        Task<List<PropertyImageViewModel>> GetImageDefault(int propertyId);

        Task<List<PropertyImageViewModel>> GetImageNonDefault(int propertyId);

        Task<bool> UpdateIsDelete(int propertyId, bool delete);

    }
}
