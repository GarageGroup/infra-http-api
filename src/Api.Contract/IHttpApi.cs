using System;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Infra;

public interface IHttpApi
{
    ValueTask<Result<HttpSendOut, HttpSendFailure>> SendAsync(HttpSendIn input, CancellationToken cancellationToken);
}