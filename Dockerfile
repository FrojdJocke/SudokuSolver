FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env 
WORKDIR /app

COPY SudokuSolver.Web.Api/*.csproj ./
RUN dotnet restore 
COPY . ./
RUN dotnet publish SudokuSolver.Web.Api/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "SudokuSolver.Web.Api.dll" ]
