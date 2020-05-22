using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Helpers;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IPhotoRepository _photoReposity;
        private Cloudinary _cloudinary;

        public PhotosController(IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig, IPhotoRepository photoReposity)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _photoReposity = photoReposity;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("photoId", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int photoId)
        {
            var photo = await _photoReposity.GetPhotoAsync(photoId);
            var photoModel = _mapper.Map<PhotoViewModel>(photo);

            return Ok(photoModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(string userId, [FromForm]PhotoForCreationViewModel photoModel)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var photoDto = _mapper.Map<PhotoDto>(photoModel);

            var file = photoModel.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoDto.Url = uploadResult.Uri.ToString();
            photoDto.PublicId = uploadResult.PublicId;

            var result = await _photoReposity.AddPhotoAsync(userId, photoDto);

            if (result == null)
            {
                return BadRequest("ERR_ADDING_PHOTO");
            }

            var photoReturn = _mapper.Map<PhotoViewModel>(result);

            return CreatedAtRoute("GetPhoto", new { photoId = photoReturn.Id, userId = userId }, photoReturn);
        }
    }
}