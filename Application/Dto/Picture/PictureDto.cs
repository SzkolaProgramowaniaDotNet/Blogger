using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PictureDto : IMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public bool Main { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Picture, PictureDto>();
        }
    }
}
