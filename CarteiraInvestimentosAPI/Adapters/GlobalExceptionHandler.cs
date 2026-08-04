using CarteiraInvestimentosAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CarteiraInvestimentosAPI.Adapters;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Intercepta erros de negócio (ex: quantidade <= 0)
        if (exception is DomainException domainException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new { mensagem = domainException.Message }, cancellationToken);
            return true;
        }

        // Intercepta recursos não encontrados (ex: cliente não existe)
        if (exception is NotFoundException notFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(new { mensagem = notFoundException.Message }, cancellationToken);
            return true;
        }

        // Se for um erro desconhecido (banco caiu, null reference), deixa o .NET devolver 500
        return false; 
    }
}