FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app 
COPY ./Recommendation.API.Tests/Recommendation.API.Tests.csproj ./Recommendation.API.Tests/
COPY ./Recommendation.API/Recommendation.API.csproj  ./Recommendation.API/
COPY ./ETLProcess.APP/ETLProcess.APP.csproj  ./ETLProcess.APP/
COPY ./StreamReaderApp.Consumer/StreamReaderApp.Consumer.csproj  ./StreamReaderApp.Consumer/
COPY ./ViewProducerApp.Publisher/ViewProducerApp.Publisher.csproj  ./ViewProducerApp.Publisher/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet test ./Recommendation.API.Tests/Recommendation.API.Tests.csproj
RUN dotnet publish ./Recommendation.API -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5001"
ENTRYPOINT ["dotnet", "Recommendation.API.dll"]