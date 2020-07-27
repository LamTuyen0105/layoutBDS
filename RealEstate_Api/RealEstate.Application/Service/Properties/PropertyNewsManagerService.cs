using Microsoft.EntityFrameworkCore;
using RealEstate.Data.EF;
using RealEstate.Utilities.Exceptions;
using RealEstate.ViewModels.Service.Property;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public class PropertyNewsManagerService : IPropertyNewsManagerService
    {
        private readonly RealEstateDbContext _context;

        public PropertyNewsManagerService (RealEstateDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateStatus(int propertyId, bool newStatus)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã: {propertyId}");
            property.Status = newStatus;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<PropertyUserManagerViewModel>> GetAllForManager()
        {
            var query = (from p in _context.Properties
                        join u in _context.Users on p.UserID equals u.Id into joined
                        from i in joined.DefaultIfEmpty()
                        where p.IsDelete == false
                        select new { p, i }).Take(500);
            var data = await query.Select(x => new PropertyUserManagerViewModel()
            {
                ID = x.p.Id,
                FullName = x.i.FullName,
                Status = x.p.Status,
                Tilte = x.p.Title
            }).ToListAsync();
            return data;
        }
    }
}