#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ArticleAPI/ArticleAPI.csproj", "ArticleAPI/"]
COPY ["ArticleReview.Common.Business/ArticleReview.Common.Business.csproj", "ArticleReview.Common.Business/"]
COPY ["ArticleReview.Common.Dto/ArticleReview.Common.Dto.csproj", "ArticleReview.Common.Dto/"]
COPY ["ArticleReview.Common.Data/ArticleReview.Common.Data.csproj", "ArticleReview.Common.Data/"]
RUN dotnet restore "ArticleAPI/ArticleAPI.csproj"
COPY . .
WORKDIR "/src/ArticleAPI"
RUN dotnet build "ArticleAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleAPI.dll"]