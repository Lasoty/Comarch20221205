using Restauracja.Common.Model;
using Restauracja.Web.Models;

namespace Restauracja.Web.Services.Interfaces;

public interface IBaseService : IDisposable
{
    Result ResponseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}
