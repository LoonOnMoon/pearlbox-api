using AutoMapper;
using pearlbox_api.web.Models.FormModels.Authentication;
using pearlbox_api.business.DataTransferObjects;

namespace pearlbox_api.web.Infrastructure.Mapper
{
    
    public class WebMapperProfile : Profile
	{
		public WebMapperProfile()
		{
			CreateMap<SignInWithPasswordFormModel, SignInWithPasswordUserDetails>().ReverseMap();
			CreateMap<SignUpFormModel, SignUpUserDetails>().ReverseMap();
		}
	}
    
}