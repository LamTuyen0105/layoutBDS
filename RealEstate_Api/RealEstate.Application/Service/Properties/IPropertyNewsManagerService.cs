using RealEstate.ViewModels.Service.Property;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public interface IPropertyNewsManagerService
    {
        Task<bool> UpdateStatus(int propertyId, bool newStatus);

        Task<List<PropertyUserManagerViewModel>> GetAllForManager();

    }
}