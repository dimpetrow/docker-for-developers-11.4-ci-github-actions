FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /src
COPY . .
ENTRYPOINT ["dotnet", "test", "/DockerCourseApi.Tests", "--logger:html"]
