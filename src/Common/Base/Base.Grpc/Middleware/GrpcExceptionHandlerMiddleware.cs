using Application.DTOs;
using Application.Exceptions;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using Utilities;

namespace Base.Grpc.Middleware
{
    #region interceptor
    public class GrpcGlobalExceptionHandlerInterceptor : Interceptor
    {
        private readonly ILogger<GrpcGlobalExceptionHandlerInterceptor> _logger;

        public GrpcGlobalExceptionHandlerInterceptor(ILogger<GrpcGlobalExceptionHandlerInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await base.UnaryServerHandler(request, context, continuation);
            }
            catch (Exception ex)
            {
                var msg = "خطایی رخ داده است!";
                if (ex != null && !string.IsNullOrEmpty(ex.Message) && !ex.Message.IsEnglishText())
                    msg = ex.Message;

                StatusCode grpcStatus = StatusCode.Internal;
                var metadata = new Metadata();
                var errors = new List<string>() { msg };
                var logItems = new List<string>();
                switch (ex)
                {
                    case ValidationException e:
                        errors = e.Errors;
                        grpcStatus = StatusCode.InvalidArgument;
                        break;
                    case BadRequestException e:
                        grpcStatus = StatusCode.InvalidArgument;
                        break;
                    case UnAuthorizedException e:
                        grpcStatus = StatusCode.Unauthenticated;
                        break;
                    case XssException e:
                        grpcStatus = StatusCode.InvalidArgument;
                        break;
                    case NotFoundException e:
                        grpcStatus = StatusCode.NotFound;
                        break;
                    case ForbiddenException e:
                        grpcStatus = StatusCode.PermissionDenied;
                        break;
                    case CustomSqlException e:
                        grpcStatus = StatusCode.Internal;
                        break;
                    case ExternalServiceException e:
                        grpcStatus = StatusCode.Unknown;
                        logItems.Add($"Service name : {e.ServiceName}");
                        logItems.Add($"Fucntion: {e.Function}");
                        break;
                    case UserNotVerifiedException e:
                        errors = new List<string>() { "کاربر گرامی، ابتدا تلفن همراه خود را تایید کنید!" };
                        grpcStatus = StatusCode.PermissionDenied;
                        break;
                    case InternalServerException e:
                        grpcStatus = StatusCode.Internal;
                        break;


                    case FileException e:
                        grpcStatus = StatusCode.Internal;
                        break;
                    case FileNotFoundException e:
                        grpcStatus = StatusCode.NotFound;
                        break;
                    case DirectoryNotFoundException e:
                        grpcStatus = StatusCode.NotFound;
                        break;

                    default:
                        grpcStatus = StatusCode.Internal;
                        errors = new List<string>() { "خطای سرور رخ داده است. مجددا اقدام کنید." };
                        break;
                }

                #region لاگ ارور
                string exType = ex.GetType().ToString();
                logItems.Add($"ExceptionType: {exType}");
                _logger.LogError(ex, $"{ex.Message} - {string.Join(" | ", logItems)}");
                #endregion


                #region تولید خروجی مناسب
                var responseModel = new BaseResult(false, errors);
                return MapResponse<TRequest, TResponse>(responseModel);
                #endregion

                #region تولید اکسپشن اختصاصی grpc
                //metadata.Add(new Metadata.Entry("Errors", string.Join(" | ", errors)));
                //throw new RpcException(new Status(grpcStatus, string.Join(" | ", errors), ex), metadata);
                #endregion
            }
        }


        #region تبدیل خطای اکسپشن به مدل خروجی grpc
        private TResponse MapResponse<TRequest, TResponse>(BaseResult responseViewModel)
        {
            // ایجاد یک اینستنس از نوع ریسپانس
            var responseObject = Activator.CreateInstance<TResponse>();

            #region مقدار دهی به فیلد IsSuccess
            var isSuccessProperty = responseObject?.GetType().GetProperty(nameof(responseViewModel.IsSuccess));
            if (isSuccessProperty != null)
                isSuccessProperty.SetValue(responseObject, responseViewModel.IsSuccess);
            #endregion


            #region مقدار دهی به فیلد ارور ها
            var errorProperty = responseObject?.GetType().GetProperty(nameof(responseViewModel.Errors));
            if (errorProperty != null)
            {
                foreach (var item in responseViewModel.Errors)
                {
                    var AddMethod = errorProperty.PropertyType.GetMethod("Add", new Type[] { typeof(string) });
                    if (AddMethod != null)
                    {
                        var propValue = errorProperty.GetValue(responseObject, null);
                        AddMethod.Invoke(propValue, new object[] { item });
                    }
                }
            }
            #endregion

            return responseObject;
        } 
        #endregion
    }
    #endregion
}
