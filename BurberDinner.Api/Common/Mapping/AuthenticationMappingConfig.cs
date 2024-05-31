
using BurberDinner.Application.Authentication.Commands.Register;
using BurberDinner.Application.Authentication.Common;
using BurberDinner.Application.Authentication.Queries.Login;
using BurberDinner.Contracts.Authentication;
using Mapster;

namespace BurberDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            
            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
        }
    }
}