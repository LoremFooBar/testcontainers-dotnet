namespace DotNet.Testcontainers.Configurations
{
  using System.IO;

  public interface IDockerComposeConfiguration : IDockerResourceConfiguration
  {
    FileInfo ComposeFile { get; }
  }
}
