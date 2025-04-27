using AutoMapper;
using Masrofy.Application.Abstraction.Models.Identity;
using Masrofy.Application.Abstraction.Models.Identity.Account;
using Masrofy.Application.Features.Identity.Account.Commands.Models;
using Masrofy.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masrofy.Application.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateUserDto, ApplicationUser>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
				.ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

			CreateMap<ApplicationUser, ReturnUserDto>();
			CreateMap<RegisterCommand, CreateUserDto>();
		}
	}
}
