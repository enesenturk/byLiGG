using AutoMapper;
using byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Dtos;
using byLiGG.Domain.Entities;

namespace byLiGG.Application.UseCases.Modules.User.Commands.CreateUserRegistrationCommand.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Create_UserRegistration_CommandDto, t_user>()
				.ForMember(dest => dest.username, opt => opt.MapFrom(src => src.Username))
				.ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.display_name, opt => opt.MapFrom(src => src.DisplayName))
				.ForMember(dest => dest.language_preference, opt => opt.MapFrom(src => src.LanguagePreference))
				.ForMember(dest => dest.is_active, opt => opt.MapFrom(src => true))
				.ForMember(dest => dest.is_verified, opt => opt.MapFrom(src => false));

			CreateMap<t_user, Create_UserRegistration_ResponseDto>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.id))
				.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.username))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
				.ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.display_name));
		}
	}
}