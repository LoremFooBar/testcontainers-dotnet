namespace DotNet.Testcontainers.Builders
{
  using System.IO;
  using DotNet.Testcontainers.Clients;
  using DotNet.Testcontainers.Configurations;

  public class DockerComposeBuilder : AbstractBuilder<IDockerComposeBuilder, IDockerComposeConfiguration>, IDockerComposeBuilder
  {
    public DockerComposeBuilder(IDockerComposeConfiguration dockerResourceConfiguration)
      : base(dockerResourceConfiguration)
    {
    }

    public IDockerComposeBuilder WithDockerComposeFile(string dockerComposeFilePath)
    {
      return this.MergeNewConfiguration(new DockerComposeConfiguration(dockerComposeFilePath));
    }

    public IDockerComposeBuilder WithDockerComposeFile(FileInfo dockerComposeFile)
    {
      return this.MergeNewConfiguration(new DockerComposeConfiguration(dockerComposeFile));
    }

    public IDockerComposeCliClient Build()
    {
      return new DockerComposeCliClient(this.DockerResourceConfiguration.ComposeFile);
    }

    protected override IDockerComposeBuilder MergeNewConfiguration(IDockerResourceConfiguration dockerResourceConfiguration)
    {
      return this.MergeNewConfiguration(new DockerComposeConfiguration(dockerResourceConfiguration));
    }

    protected virtual IDockerComposeBuilder MergeNewConfiguration(IDockerComposeConfiguration dockerComposeConfiguration)
    {
      var composeFile = BuildConfiguration.Combine(dockerComposeConfiguration.ComposeFile, this.DockerResourceConfiguration.ComposeFile);
      return new DockerComposeBuilder(new DockerComposeConfiguration(composeFile));
    }
  }
}
