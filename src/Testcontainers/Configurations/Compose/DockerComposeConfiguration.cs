namespace DotNet.Testcontainers.Configurations
{
  using System.IO;

  internal sealed class DockerComposeConfiguration : DockerResourceConfiguration, IDockerComposeConfiguration
  {
    public DockerComposeConfiguration(IDockerResourceConfiguration dockerResourceConfiguration)
      : base(dockerResourceConfiguration)
    {
    }

    public DockerComposeConfiguration(string composeFilePath)
    {
      Guard.Argument(composeFilePath, nameof(composeFilePath))
        .NotNull()
        .NotEmpty();

      this.ComposeFile = new FileInfo(composeFilePath);
    }

    public DockerComposeConfiguration(FileInfo composeFile)
    {
      this.ComposeFile = composeFile;
    }

    public FileInfo ComposeFile { get; }
  }
}
