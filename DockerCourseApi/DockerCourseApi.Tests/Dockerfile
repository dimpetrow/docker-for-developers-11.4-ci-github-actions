FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# # For Ductus.FluentDocker
# RUN apt-get -qq update && apt-get -qq install wget
# RUN wget --quiet https://download.docker.com/linux/static/stable/x86_64/docker-20.10.8.tgz && \
#     tar -xzf docker-20.10.8.tgz
# RUN cp docker/* /usr/bin/ && rm -rf docker

# Do I need this caching in integration tests?
# COPY ["DockerCourseApi/DockerCourseApi/DockerCourseApi.csproj", "DockerCourseApi/"]
# RUN dotnet restore "DockerCourseApi/DockerCourseApi.csproj"

# COPY "docker-compose.yaml" .
# COPY "docker-compose.integration-test.override.yaml" .
COPY . .

# ARG DOCKERCOURSEAPI_DATABASE_INTEGRATION_TEST_PORT=1434
# RUN dotnet test "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj" -e "DOCKERCOURSEAPI_DATABASE_INTEGRATION_TEST_PORT=${DOCKERCOURSEAPI_DATABASE_INTEGRATION_TEST_PORT}"

# CMD [ "dotnet", "test", "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj", "--logger:html", "--logger "GitHubActions;summary.includePassedTests=true;summary.includeSkippedTests=true", "-- RunConfiguration.CollectSourceInformation=true" ]
CMD [ "dotnet", "test", "DockerCourseApi.Tests/DockerCourseApi.Tests.csproj", "--logger:html" ]
