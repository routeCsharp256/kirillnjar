FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["src/OzonEdu.MerchApi/OzonEdu.MerchApi.csproj", "src/OzonEdu.MerchApi/"]
RUN dotnet restore "src/OzonEdu.MerchApi/OzonEdu.MerchApi.csproj"

COPY . .

WORKDIR "/src/src/OzonEdu.MerchApi"

RUN dotnet build "OzonEdu.MerchApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OzonEdu.MerchApi.csproj" -c Release -o /app/publish
COPY "entrypoint.sh" "/app/publish/."

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

RUN chmod +x entrypoint.sh
CMD /bin/bash entrypoint.sh