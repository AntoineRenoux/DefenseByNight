using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
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
        private Cloudinary _cloud;

        public PhotosController(IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;

            Account account = new Account (
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloud = new Cloudinary(account);
        }

        // [HttpPost]
        // public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForCreationViewModel photoModel)
        // {
        //     return Ok();
        // }
    }
}