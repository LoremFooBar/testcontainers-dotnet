namespace DotNet.Testcontainers.Clients
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using CliWrap;

  public sealed class DockerComposeCliClient : IDockerComposeCliClient
  {
    private readonly FileInfo composeFile;

    private readonly Command command;

    public DockerComposeCliClient(FileInfo composeFile)
    {
      Guard.Argument(composeFile, nameof(composeFile)).NotNull();
      if (!composeFile.Exists)
      {
        throw new FileNotFoundException("Docker compose file does not exist.", composeFile.FullName);
      }

      this.composeFile = composeFile;
      var useDirectoryName = composeFile.Directory?.Name is { Length: > 1 };
      var projectName = $"Testcontainers_{(useDirectoryName ? composeFile.Directory.Name : Guid.NewGuid().ToString("D"))}";
      this.command = Cli.Wrap("docker")
        .WithArguments(new[] { "compose", "-f", composeFile.FullName, "-p", projectName })
        .WithValidation(CommandResultValidation.ZeroExitCode)
        .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.Write))
        .WithStandardErrorPipe(PipeTarget.ToDelegate(Console.Write));
    }

    public Task UpAsync(CancellationToken ct = default)
    {
      return this.command
        .WithArguments("up")
        .ExecuteAsync(ct).Task;
    }

    public Task DownAsync(CancellationToken ct = default)
    {
      return this.command
        .WithArguments("down")
        .ExecuteAsync(ct).Task;
    }

    public async ValueTask DisposeAsync()
    {
      var downTask = this.DownAsync(new CancellationTokenSource(TimeSpan.FromSeconds(15)).Token);
      await downTask;

      if (downTask.IsCanceled)
      {
        await this.KillAsync();
      }
    }

    private Task KillAsync()
    {
      return this.command.WithArguments("kill").ExecuteAsync();
    }
  }
}
