using Application.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Cosmos
{
    public class CreateCosmosPostDto : IMap
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCosmosPostDto, CosmosPost>();
        }
    }
}
