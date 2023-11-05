using DownloadVideo.Domain.Commons;
using DownloadVideo.Domain.Configurations;
using DownloadVideo.Service.Exceptions;
using DownloadVideo.Service.Helpers;
using Newtonsoft.Json;

namespace DownloadVideo.Service.Extensions;

public static class CollectionExtension
{
    public static IQueryable<T> ToPagedList<T>(this IQueryable<T> sources,
            PaginationParams @params = null) where T : Auditable
    {
        var metaData = new PaginationMetaData(sources.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);

        }
        return @params.PageIndex > 0 && @params.PageSize > 0 ?
                sources.OrderBy(e => e.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
                    throw new DownloadVideoException(400, "Please, enter valid numbers");

    }
}
