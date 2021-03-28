using Application.Dto;
using Application.ExtensionMethods;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PictureService(IPictureRepository pictureRepository, IPostRepository postRepository, IMapper mapper)
        {
            _pictureRepository = pictureRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PictureDto>> GetPicturesByPostIdAsync(int postId)
        {
            var pictures = await _pictureRepository.GetByPostIdAsync(postId);
            return _mapper.Map<IEnumerable<PictureDto>>(pictures);
        }

        public async Task<PictureDto> GetPictureByIdAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            return _mapper.Map<PictureDto>(picture);
        }

        public async Task<PictureDto> AddPictureToPostAsync(int postId, IFormFile file)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            var existingPictures = await _pictureRepository.GetByPostIdAsync(postId);

            var picture = new Picture()
            {
                Posts = new List<Post> { post },
                Name = file.FileName,
                Image = file.GetBytes(),
                Main = existingPictures.Count() == 0 ? true : false
            };

            var result = await _pictureRepository.AddAsync(picture);
            return _mapper.Map<PictureDto>(result);
        }

        public async Task SetMainPictureAsync(int postId, int id)
        {
            await _pictureRepository.SetMainPictureAsync(postId, id);
        }

        public async Task DeletePictureAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            await _pictureRepository.DeleteAsync(picture);
        }
    }
}
