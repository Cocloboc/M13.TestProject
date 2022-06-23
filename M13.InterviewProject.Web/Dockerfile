FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["M13.InterviewProject.Web/M13.InterviewProject.Web.csproj", "M13.InterviewProject.Web/"]
RUN dotnet restore "M13.InterviewProject.Web/M13.InterviewProject.Web.csproj"
COPY . .
WORKDIR "/src/M13.InterviewProject.Web"
RUN dotnet build "M13.InterviewProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "M13.InterviewProject.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "M13.InterviewProject.Web.dll"]
