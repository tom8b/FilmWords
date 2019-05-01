using AutoMapper;
using FilmWords.Data.Dto;
using FilmWords.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmWords.Data.Profiles
{
    public class SentenceProfile : Profile
    {
        public SentenceProfile()
        {
            this.CreateMap<Sentence, SentenceDto>()
                .ReverseMap();
        }
    }
}
