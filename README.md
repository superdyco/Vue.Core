# enverionment
1. asp.net core 3.0
2. vue cli 3.11
3. swagger
4. PostgreSQL
5. nodejs 12.10.0
6. redis cache
7. nLOG
8. docker & docker-compose
9. Nginx

# Vue + asp.net core
此專案主要是要實現幾個功能需求
1. windows/mac 皆能開發使用
2. 完整的前後端分離,ClientApp也可以做到獨立執行
3. 程式執行後,實現webpack的HMR功能(Hot Reload)
4. 使用Jwt做驗證機制(httponly),有refresh token機制,會自動交換token
   + short token : 2 hours 
   + long token : 7 days
5. Facebook 登錄實作
6. 運作在docker環境(Redis,Postgres,Nginx,Web獨立分開)
7. 發怖時會自動做測試後發怖至Docker (publish2docker.sh)
8. 簡易的LineBot 拿取最新消息功能

#### Future
1. K8s
2. SingleR

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
docker-compose -f app.yml up --build
```
5. now,you can open your broswer and entry url 
    - http://localhost:8888
    - https://localhost 


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

# publish to GCP
1. Install GCP CLI (https://cloud.google.com/sdk/docs/)
2. gcloud auth configure-docker
3. docker tag zlinebot:production asia.gcr.io/{projectid}/zlinebot:production
4. 將docker register publish to GCE
5. 建立Cloud Sql For Postgres Sql
- 連到gcp的postgres table //記得裝psql clinet tool
- gcloud sql connect [instance name] --user=postgres --quiet
- //可以用pgadmin連(記得公開public ip 記得填) 

# other issue

### about publish
- 當發怖時發生MSB4018 TransoformWebConfig 轉換錯誤時
    1. 打開.csproj
    2. 移除下列這行
    ```config
    <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
    ```