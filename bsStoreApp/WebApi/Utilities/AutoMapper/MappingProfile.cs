using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDtoForInsertion, Book>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
