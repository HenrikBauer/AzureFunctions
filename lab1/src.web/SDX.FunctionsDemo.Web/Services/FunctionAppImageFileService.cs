﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SDX.FunctionsDemo.ImageProcessing;

namespace SDX.FunctionsDemo.Web.Services
{
    public class FunctionAppImageFileService : IImageFileService
    {
        static HttpClient _client = new HttpClient();

        private readonly IConfiguration _configuration;

        public FunctionAppImageFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        async Task<string> IImageFileService.UploadImageAsync(string fileName, string contentType, byte[] data)
        {
            try
            {
                var content = new ByteArrayContent(data);
                content.Headers.Add("x-sdx-fileName", fileName);
                content.Headers.Add("x-sdx-contentType", contentType);

                var baseUrl = _configuration["Settings:FunctionApp.Url"];
                var requestUri = baseUrl + "/api/UploadImage";
                var response = await _client.PostAsync(requestUri, content);
                if (!response.IsSuccessStatusCode)
                    return null;

                var id = await response.Content.ReadAsStringAsync();
                return id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        async Task<byte[]> IImageFileService.GetImageAsync(string id, string imageType)
        {
            try
            {
                var baseUrl = _configuration["Settings:FunctionApp.Url"];
                var requestUri = baseUrl + $"/api/GetImage?id={id}&imageType={imageType}";
                var response = await _client.GetAsync(requestUri);
                if (!response.IsSuccessStatusCode)
                    return null;

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
