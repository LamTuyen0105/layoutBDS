using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.NewsManagers;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyNewsManagersController : ControllerBase
    {
        private readonly IPropertyNewsManagerService _propertyNewsManagerService;
        public PropertyNewsManagersController (IPropertyNewsManagerService propertyNewsManagerService)
        {
            _propertyNewsManagerService = propertyNewsManagerService;
        }

        [HttpPatch("{propertyId}/{newStatus}")]
        public async Task<IActionResult> UpdateStatus(int propertyId, bool newStatus)
        {
            var isSuccessful = await _propertyNewsManagerService.UpdateStatus(propertyId, newStatus);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForManager()
        {
            var properties = await _propertyNewsManagerService.GetAllForManager();
            return Ok(properties);
        }
    }
}
