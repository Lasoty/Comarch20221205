using Restauracja.Common.Model;
using Restauracja.Web.Models;

namespace Restauracja.Web.Services.Interfaces;

public interface IBaseService
{
    Task<T> SendAsync<T>(ApiRequest apiRequest) where T : Result;
}
