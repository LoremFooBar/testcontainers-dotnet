namespace DotNet.Testcontainers.Builders
{
  using System.IO;
  using DotNet.Testcontainers.Clients;

  public interface IDockerComposeBuilder : IAbstractBuilder<IDockerComposeBuilder>
  {
    IDockerComposeBuilder WithDockerComposeFile(string dockerComposeFilePath);

    IDockerComposeBuilder WithDockerComposeFile(FileInfo dockerComposeFile);

    IDockerComposeCliClient Build();
  }
}
