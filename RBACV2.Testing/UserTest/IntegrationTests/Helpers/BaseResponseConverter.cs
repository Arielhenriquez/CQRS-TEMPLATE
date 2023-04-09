using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.BaseResponse;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RBACV2.Test.UserTest.IntegrationTests.Helpers
{
    public class BaseResponseConverter<T> : JsonConverter<BaseResponse<T>> where T : class
    {
        public override BaseResponse<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonSerializer.Deserialize<JsonElement>(ref reader);

            var response = new BaseResponse<T>();

            if (jsonObject.TryGetProperty("statusCode", out var statusCode))
                response.StatusCode = (HttpStatusCode)statusCode.GetInt32();

            if (jsonObject.TryGetProperty("message", out var message))
                response.Message = message.GetString();

            if (jsonObject.TryGetProperty("data", out var data))
            {
                var userResponse = DeserializeUserResponseDto(data);
                response.Data = userResponse as T;
            }

            return response;
        }

        public override void Write(Utf8JsonWriter writer, BaseResponse<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private static UserResponseDto DeserializeUserResponseDto(JsonElement data)
        {
            var dto = new UserResponseDto();
            var items = data.GetProperty("items");

            dto.FirstName = items[0].GetProperty("firstName").GetString();
            dto.Id = Guid.Parse(items[0].GetProperty("id").GetString()!);
            dto.FullName = items[0].GetProperty("fullName").GetString();
            dto.UserName = items[0].GetProperty("userName").GetString();
            dto.FullEmail = items[0].GetProperty("fullEmail").GetString();
            dto.IsEnabled = items[0].GetProperty("isEnabled").GetBoolean();
            dto.UserOid = items[0].GetProperty("userOid").GetString();

            return dto;
        }
    }
}
