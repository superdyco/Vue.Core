# enverionment
1. asp.net core 3.0
2. vue cli 3.11
3. swagger
4. PostgreSQL
5. nodejs 12.10.0
6. redis cache
7. nLOG
8. docker & docker-compose

# Vue + asp.net core
此專案主要是要實現幾個功能需求
1. windows/mac 皆能開發使用
2. 完整的前後端分離,ClientApp也可以做到獨立執行
3. 程式執行後,實現webpack的HMR功能(Hot Reload)
4. 使用Jwt做驗證機制,有refresh token機制,會自動交換token
   + short token : 2 hours 
   + long token : 7 days
5. Facebook 登錄實作
6. 運作在docker環境(Redis,Postgres,Web獨立分開)
7. 發怖時會自動做測試後發怖至Docker (publish2docker.sh)

#### Future
1. Line Bot 整合應用
2. K8s
3. SingleR

# Step By Step to Build
1. make sure installed below env.
    - .net core 3 runtime
    - vue cli 3.11
    - nodejs 12.10.0 
    - docker & docker-compose
2. run .\startup-docker.sh
3. run .\shell\update-database.sh
4. run publish2docker.sh    
    - if just only run app,you can type in docker folder
```
docker-compose -f app.yml up
```
5. open browser and access http://localhost:7080 or https://localhost:7081(need install ssl [ref](https://docs.microsoft.com/zh-tw/aspnet/core/security/docker-https?view=aspnetcore-3.0))
      


# .net core
1. 安裝 postgres /dotnet ef tools
```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
//EF CORE3.0
dotnet tool install --global dotnet-ef
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

3.publish
```
./shell/publish.sh
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

# vue 

### Running
```
cd /CleintApp
npm run dev //development
npm run prod //production
npm run build:dev
npm run build:prod
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