namespace DotNet.Testcontainers.Clients
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

  public interface IDockerComposeCliClient : IAsyncDisposable
  {
    Task UpAsync(CancellationToken ct = default);

    Task DownAsync(CancellationToken ct = default);
  }
}
