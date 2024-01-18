using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record CreateTokenCommand(TokenRequest Model) : IRequest<ApiResponse<TokenResponse>>;
