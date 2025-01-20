# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app/Services/EHR/EHR/EHR.WebApi

# Copy the project file(s) to the container
COPY ./src /app

# Restore the NuGet packages
RUN dotnet restore

# Build the application
RUN dotnet build -c Release --no-restore -o out EHR.WebApi.csproj

# Use the official .NET Core runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app
RUN mkdir -p /app/Uploads/
RUN mkdir -p /app/wwwroot/
COPY src/Services/EHR/EHR/EHR.WebApi/Uploads/ /app/Uploads/
ENV ASPNETCORE_URLS=http://+:80

# Copy the published output from the build stage to the runtime stage
COPY --from=build /app/Services/EHR/EHR/EHR.WebApi/out ./

# Expose the port(s) that the application listens on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "EHR.WebApi.dll"]
