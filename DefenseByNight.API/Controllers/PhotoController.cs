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
    [Route("api/user/{userId}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IPhotoRepository _photoReposity;
        private readonly IUserRepository _userRepository;
        private Cloudinary _cloudinary;

        public PhotoController(IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig, IPhotoRepository photoReposity, IUserRepository userRepository)
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
            _userRepository = userRepository;
        }

        [HttpGet("photoId", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int photoId)
        {
            var photo = await _photoReposity.GetPhotoAsync(photoId);
            var photoModel = _mapper.Map<PhotoViewModel>(photo);

            return Ok(photoModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(string userId, [FromForm] PhotoForCreationViewModel photoModel)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            
            var currentUser = await _userRepository.GetUserAsync(userId);

            if (currentUser.Photo != null && currentUser.Photo.Id != 1)
                await DeletePhotoAsync(userId, currentUser.Photo.Id);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhotoAsync(string userId, int photoId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            var user = await _userRepository.GetUserAsync(userId);

            if (user.Photo == null)
                return BadRequest("ERR_DELETE_PHOTO_DONT_EXIST");

            if (user.Photo.Id == 1)
                return BadRequest("ERR_DELETE_PHOTO_DEFAULT");

            if (user.Photo.PublicId != null)
            {
                var deleteParams = new DeletionParams(user.Photo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    await _photoReposity.DeletePhotoAsync(userId, photoId);
                    return Ok();
                }
            }
            return BadRequest("ERR_DELETE_PHOTO_GENRAL");
        }
    }
}