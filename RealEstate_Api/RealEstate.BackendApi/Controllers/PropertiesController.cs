using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.NewsManagers;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyOwnerService _ownerPropertyService;
        public PropertiesController(IPropertyOwnerService ownerPropertyService)
        {
            _ownerPropertyService = ownerPropertyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var properties = await _ownerPropertyService.GetAll();
            return Ok(properties);
        }

        [HttpGet("HotProperties")]
        public async Task<IActionResult> GetHot()
        {
            var properties = await _ownerPropertyService.GetHot();
            return Ok(properties);
        }

        [HttpGet("HotSellProperties")]
        public async Task<IActionResult> GetHotSell()
        {
            var properties = await _ownerPropertyService.GetHotSell();
            return Ok(properties);
        }

        [HttpGet("HotRentProperties")]
        public async Task<IActionResult> GetHotRent()
        {
            var properties = await _ownerPropertyService.GetHotRent();
            return Ok(properties);
        }

        [HttpGet("ViewPagingByTransaction")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetViewPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.GetAllByTypeOfTransaction(request);
            return Ok(properties);
        }

        [HttpGet("ViewPaging")]
        public async Task<IActionResult> GetPaging([FromQuery]GetPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.GetPaging(request);
            return Ok(properties);
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find(GetFindPropertyPagingRequest request)
        {
            var properties = await _ownerPropertyService.Find(request);
            return Ok(properties);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetById(int propertyId)
        {
            var property = await _ownerPropertyService.GetById(propertyId);
            if (property == null)
                return BadRequest("Không tìm thấy được tin đăng");
            return Ok(property);
        }

        [HttpGet("ElasticSearch/{propertyId}")]
        public async Task<IActionResult> GetElasticSearchById(int propertyId)
        {
            var property = await _ownerPropertyService.GetElasticSearchById(propertyId);
            if (property == null)
                return BadRequest("Không tìm thấy được tin đăng");
            return Ok(property);
        }

        [HttpGet("GetForPut/{propertyId}")]
        public async Task<IActionResult> GetPutById(int propertyId)
        {
            var property = await _ownerPropertyService.GetPutById(propertyId);
            if (property == null)
                return BadRequest("Không tìm thấy được tin đăng");
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PropertyCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var propertyId = await _ownerPropertyService.Create(request);
            if (propertyId == 0)
                return BadRequest();
            var property = await _ownerPropertyService.GetById(propertyId);
            return CreatedAtAction(nameof(GetById), new { id = propertyId }, property);
        }

        [HttpPost("list")]
        public async Task<IActionResult> Createlist(PropertyCreateRequest[] request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(PropertyCreateRequest element in request){
                var propertyId = await _ownerPropertyService.Create(element);
                if (propertyId == 0)
                    return BadRequest();
                var property = await _ownerPropertyService.GetById(propertyId);
                
            }
            return Ok();

        }

        [HttpPut("{propertyId}")]
        public async Task<IActionResult> Update(int propertyId, [FromForm]PropertyUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _ownerPropertyService.Update(propertyId, request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{propertyId}")]
        public async Task<IActionResult> Delete(int propertyId)
        {
            var affectedResult = await _ownerPropertyService.Delete(propertyId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPost("{propertyId}/images")]
        public async Task<IActionResult> CreateImage(int propertyId, PropertyImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _ownerPropertyService.AddImage(propertyId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _ownerPropertyService.GetImageById(imageId);
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int propertyId, int imageId)
        {
            var image = await _ownerPropertyService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpGet("{propertyId}/images")]
        public async Task<IActionResult> GetListImage(int propertyId)
        {
            var image = await _ownerPropertyService.GetListImages(propertyId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpGet("{propertyId}/imagedefault")]
        public async Task<IActionResult> GetImageDefault(int propertyId)
        {
            var image = await _ownerPropertyService.GetImageDefault(propertyId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpGet("{propertyId}/imagenondefault")]
        public async Task<IActionResult> GetImageNonDefault(int propertyId)
        {
            var image = await _ownerPropertyService.GetImageNonDefault(propertyId);
            if (image == null)
                return BadRequest("Không tìm thấy hình ảnh");
            return Ok(image);
        }

        [HttpPut("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, PropertyImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ownerPropertyService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{propertyId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _ownerPropertyService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("PropertyByUser")]
        [Authorize]
        public async Task<IActionResult> GetByUserId()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var property = await _ownerPropertyService.GetByUserId(userId);
            if (property == null)
                return BadRequest("Không tìm thấy tin đăng");
            return Ok(property);
        }

        [HttpPatch("{propertyId}/{delete}")]
        public async Task<IActionResult> UpdateIsDelete(int propertyId, bool delete)
        {
            var isSuccessful = await _ownerPropertyService.UpdateIsDelete(propertyId, delete);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }
    }
}