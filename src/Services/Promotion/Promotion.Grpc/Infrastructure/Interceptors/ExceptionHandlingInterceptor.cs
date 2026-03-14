namespace Promotion.Grpc.Infrastructure.Interceptors
{
    public class ExceptionHandlingInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (NotFoundException ex)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, ex.Message));
            }
            catch (ValidationException ex)
            {
                var errors = string.Join("; ", ex.Errors.Select(e => e.ErrorMessage));

                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, errors));
            }
            catch (FormatException ex)
            {
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (Exception)
            {
                throw new RpcException(
                    new Status(StatusCode.Internal, "Internal server error"));
            }
        }
    }
}