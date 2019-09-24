# enverionment
1. asp.net core 3.0
2. vue cli 3.11
3. swagger
4. PostgreSQL
5. nodejs 12.10.0
6. redis cache
7. nLOG


# Vue + asp.net core
此專案主要是要實現幾個功能需求
1. 完整的前後端分離,ClientApp 也可以做到獨立執行
2. 程式執行後,前端可以做到webpack的HMR功能(Hot Reload)
3. 使用Jwt做驗證機制,有refresh token機制,會自動交換token
4. 可以運作在docker環境
5. 

# .net core
1. 安裝 postgres 
```config
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```
2. migrations database
  - add migrations
```
./shell/migrations.sh init
```
   - update database    
```
./shell/update-database.sh
```

# startup docker
1. 確認已裝好了docker與docker-compose
   - how to install docker-compse in powershell
     https://docs.docker.com/compose/install/
     
3. 執行 ./shell/startup-docker.sh   
4. 停止 ./shell/shutdown-docker.sh
5. 查看狀態 docker-compose -f startup.yml ps

### Access to webpage
```config
Username: admin
Password: p12345
```
### Access to postgres:
``` 
localhost:5432

Username: postgres (as a default)

Password: p12345 (as a default)
```
### Access to PgAdmin:
```
URL: http://localhost:5050
Username:postgres@admin.com (as a default)
Password: p12345 (as a default)

pgadmin 建立server設定參數
host: host.docker.internal
database: postgres
user: postgres
password: p12345
```

### Access Redis
1. 進入redis shell
```
docker exec -it redis_container redis-cli
```
2. command
```
get key //拿取值 by string
hgetall key //拿取值by hash
```

### .csproj
```config
<Project Sdk="Microsoft.NET.Sdk.Web">
    <PropertyGroup>
        <TargetFramework>netcoreapp2.2</TargetFramework>    
        <!--Spa路徑-->   
		<SpaRoot>ClientApp\</SpaRoot>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.AspNetCore.App" />
        <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.2.0" PrivateAssets="All" />
        <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.2.0" />
    </ItemGroup>

  <ItemGroup>
    <!-- Files not to publish (note that the 'dist' subfolders are re-added below) -->
    <Content Remove="ClientApp\**" />
  </ItemGroup>

  <ItemGroup>    
    <Folder Include="wwwroot\" />
  </ItemGroup>

  <!--環境是否有安裝node.js-->   
	<Target Name="EnsureNode">
	  <Exec Command="node --version" ContinueOnError="true">
		<Output TaskParameter="ExitCode" PropertyName="ErrorCode" />
	  </Exec>
	  <Error Condition="'$(ErrorCode)' != '0'" Text="Node.js is required to build and run this project. To continue, please install Node.js from https://nodejs.org/, and then restart your command prompt or IDE." />
	</Target>
 
 <!--發怖前要先做npm run build--> 
  <Target Name="RunWebpack" AfterTargets="ComputeFilesToPublish">
    <!-- As part of publishing, ensure the JS resources are freshly built in production mode -->
    <ItemGroup>
        <FilesToDelete Include="wwwroot\**\*" />
    </ItemGroup>
    <Delete Files="@(FilesToDelete)" />
	<CallTarget Targets="EnsureNode" />	
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm install --ignore-scripts" />
	<Message Importance="high" Text="npm run build ..." />
    <Exec WorkingDirectory="$(SpaRoot)" Command="npm run build:prod" />

    <!-- Include the newly-built files in the publish output -->
    <ItemGroup>
      <DistFiles Include="wwwroot\**" />
      <ResolvedFileToPublish Include="@(DistFiles->'%(FullPath)')" Exclude="@(ResolvedFileToPublish)">
        <RelativePath>%(DistFiles.Identity)</RelativePath>
        <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
      </ResolvedFileToPublish>
    </ItemGroup>
  </Target>  
</Project>

```

# vue 

### Runing
```
cd /CleintApp
npm run dev //development
npm run prod //production
npm run build  //publish
```
### Customize configuration
```
global [.env] file
Production [.env.production] file
Development [.env.development] file
```

# other issue

### about publish
- 當發怖時發生MSB4018 TransoformWebConfig 轉換錯誤時
    1. 打開.csproj
    2. 移除下列這行
    ```config
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    ```