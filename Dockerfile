# Get base SDK image from Microsoft registry and alias as build-env
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env 
# Set the working directory for the image for any subsequent ADD, COPY, CMD, ENTRYPOINT, or RUN instructions that follow it in the Dockerfile
WORKDIR /app

# # Copy the CSPROJ file and restore any dependecies (via NUGET) into the current image directory previously set as WORKDIR /app
# COPY *.csproj ./
# RUN dotnet restore

# # Copy the project files from current location into image directory and build our release
# COPY . ./
# RUN dotnet publish -c Release -o out
COPY SudokuSolver.Web.Api/*.csproj ./
RUN dotnet restore 
COPY . ./
RUN dotnet publish SudokuSolver.Web.Api/*.csproj -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "SudokuSolver.Web.Api.dll" ]
