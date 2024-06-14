﻿using System.Net;
using Microsoft.AspNetCore.Http;

namespace GroupExpenses.Shared.Exceptions
{
   public class ExceptionMiddleware
   {
      private const string JsonContentType = "application/json";
      private readonly RequestDelegate _next;

      public ExceptionMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task InvokeAsync(HttpContext httpContext)
      {
         try
         {
            await _next(httpContext);
         }
         catch (SystemException ex)
         {
            await HandleExceptionAsync(httpContext,ex);
         }
      }

      private async Task HandleExceptionAsync(HttpContext context,SystemException exception)
      {
         context.Response.ContentType = JsonContentType;

         var error = new ErrorDetails();

         switch (exception)
         {
            case BadRequestException badRequestException:
               error.StatusCode = (int)HttpStatusCode.BadRequest;
               error.Message = $"Invalid request: {badRequestException.Message}";
               context.Response.StatusCode = StatusCodes.Status400BadRequest;
               break;
            case GeneralException appException:
               error.StatusCode = (int)HttpStatusCode.InternalServerError;
               error.Message = appException.Message;
               context.Response.StatusCode = StatusCodes.Status500InternalServerError;
               break;
            case NotFoundException notFoundException:
               error.StatusCode = (int)HttpStatusCode.NotFound;
               error.Message = notFoundException.Message;
               context.Response.StatusCode = StatusCodes.Status404NotFound;
               break;
            default:
               error.StatusCode = (int)HttpStatusCode.InternalServerError;
               error.Message = $"Internal Server Error: {exception.Message}";
               context.Response.StatusCode = StatusCodes.Status500InternalServerError;
               break;
         }

         await context.Response.WriteAsync(error.ToString());
      }
   }
}
