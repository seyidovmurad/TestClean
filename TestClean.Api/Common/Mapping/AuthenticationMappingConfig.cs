using Mapster;
using TestClean.Application.Services.Authentication;
using TestClean.Contracts.Authentication;

namespace TestClean.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    // Add your mapping configuration code here
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}